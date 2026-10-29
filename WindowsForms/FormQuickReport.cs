using System.Windows.Forms.DataVisualization.Charting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    /// <summary>
    /// Dashboard báo cáo doanh thu: doanh thu theo giờ, cơ cấu theo ngành hàng,
    /// top sản phẩm bán chạy, doanh thu kỳ báo cáo và giá trị hóa đơn trung bình.
    /// Dữ liệu lấy từ API Reports/analytics; không tự tạo số liệu minh họa.
    /// </summary>
    public partial class FormQuickReport : Form
    {
        private readonly Color ink = Color.FromArgb(30, 41, 59);
        private readonly Color muted = Color.FromArgb(100, 116, 139);
        private readonly Color blue = Color.FromArgb(37, 99, 235);
        private readonly Color teal = Color.FromArgb(13, 148, 136);
        private readonly Color orange = Color.FromArgb(217, 119, 6);
        private readonly Color red = Color.FromArgb(220, 38, 38);
        private readonly Color[] chartColors = new Color[]
        {
            Color.FromArgb(42, 116, 177),
            Color.FromArgb(220, 68, 58),
            Color.FromArgb(47, 155, 82),
            Color.FromArgb(235, 150, 35),
            Color.FromArgb(129, 86, 165),
            Color.FromArgb(25, 155, 165),
            Color.FromArgb(145, 157, 170),
            Color.FromArgb(205, 96, 145)
        };

        private List<HourlyRevenueDto> hourlyRevenue = new List<HourlyRevenueDto>();
        private List<DailyRevenueDto> dailyRevenue = new List<DailyRevenueDto>();
        private List<CategoryRevenueDto> revenueByCategory = new List<CategoryRevenueDto>();
        private ReportAnalyticsDto lastReport;
        private DateTime lastReportFrom;
        private DateTime lastReportTo;
        private Button btnExportCsv = new Button();
        private bool changingPeriod;
        private bool showDailyRevenueChart = true;

        public FormQuickReport()
        {
            InitializeComponent();
            ConfigureReportUi();

            hourlyChartPanel.Paint += HourlyChartPanel_Paint;
            categoryChartPanel.Paint += CategoryChartPanel_Paint;
            hourlyChartPanel.Resize += delegate { hourlyChartPanel.Invalidate(); };
            categoryChartPanel.Resize += delegate { categoryChartPanel.Invalidate(); };

            cboPeriod.SelectedIndexChanged += CboPeriod_SelectedIndexChanged;
            dtpFrom.ValueChanged += DateRange_ValueChanged;
            dtpTo.ValueChanged += DateRange_ValueChanged;
            btnRunReport.Click += btnRunReport_Click;
            Load += FormQuickReport_Load;
        }

        private void ConfigureReportUi()
        {
            BackColor = Color.FromArgb(245, 247, 251);
            ForeColor = ink;
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1000, 680);

            StyleButton(btnRunReport, blue);
            lblHourlyTitle.Text = "DOANH THU THEO NGÀY / TUẦN / THÁNG";

            // Nut xuat CSV duoc tao bang code, khong can sua file Designer.
            btnExportCsv.Name = "btnExportCsv";
            btnExportCsv.Text = "Xuất CSV";
            btnExportCsv.Size = new Size(120, 36);
            btnExportCsv.Location = new Point(
                Math.Max(0, headerPanel.ClientSize.Width - 132), 8);
            btnExportCsv.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportCsv.Enabled = false;
            StyleButton(btnExportCsv, teal);
            btnExportCsv.Click += btnExportCsv_Click;
            headerPanel.Controls.Add(btnExportCsv);

            cboPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
            if (cboPeriod.Items.Count == 0)
                cboPeriod.Items.AddRange(new object[] { "Hôm nay", "Tuần này", "Tháng này", "Tùy chỉnh" });

            ConfigureTopProductsGrid();
            SetSummaryPlaceholders();
        }

        private void StyleButton(Button button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        private void ConfigureTopProductsGrid()
        {
            dgvTopProducts.AutoGenerateColumns = true;
            dgvTopProducts.ReadOnly = true;
            dgvTopProducts.AllowUserToAddRows = false;
            dgvTopProducts.AllowUserToDeleteRows = false;
            dgvTopProducts.AllowUserToResizeRows = false;
            dgvTopProducts.MultiSelect = false;
            dgvTopProducts.RowHeadersVisible = false;
            dgvTopProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopProducts.BackgroundColor = Color.White;
            dgvTopProducts.BorderStyle = BorderStyle.None;
            dgvTopProducts.EnableHeadersVisualStyles = false;
            dgvTopProducts.ColumnHeadersHeight = 32;
            dgvTopProducts.RowTemplate.Height = 26;
            dgvTopProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(238, 242, 247);
            dgvTopProducts.ColumnHeadersDefaultCellStyle.ForeColor = ink;
            dgvTopProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            dgvTopProducts.DefaultCellStyle.Font = new Font("Segoe UI", 8F);
            dgvTopProducts.DefaultCellStyle.ForeColor = ink;
            dgvTopProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvTopProducts.DefaultCellStyle.SelectionForeColor = ink;
            dgvTopProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);
            dgvTopProducts.GridColor = Color.FromArgb(226, 232, 240);
        }

        private void SetSummaryPlaceholders()
        {
            lblRevenueTitle.Text = "DOANH THU THÁNG NÀY";
            lblRevenueValue.Text = "Đang tải...";
            lblAverageOrderValue.Text = "Đang tải...";
            lblOrderCountValue.Text = "Đang tải...";
        }

        private async void FormQuickReport_Load(object sender, EventArgs e)
        {
            changingPeriod = true;
            DateTime today = DateTime.Today;
            dtpFrom.Value = new DateTime(today.Year, today.Month, 1);
            dtpTo.Value = today;
            cboPeriod.SelectedItem = "Tháng này";
            changingPeriod = false;

            await LoadReportDataAsync();
        }

        private void CboPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (changingPeriod || cboPeriod.SelectedItem == null)
                return;

            string period = cboPeriod.SelectedItem.ToString();
            DateTime today = DateTime.Today;

            changingPeriod = true;
            if (period == "Hôm nay")
            {
                dtpFrom.Value = today;
                dtpTo.Value = today;
            }
            else if (period == "Tuần này")
            {
                int diff = ((int)today.DayOfWeek + 6) % 7; // tuần bắt đầu thứ Hai
                dtpFrom.Value = today.AddDays(-diff);
                dtpTo.Value = today;
            }
            else if (period == "Tháng này")
            {
                dtpFrom.Value = new DateTime(today.Year, today.Month, 1);
                dtpTo.Value = today;
            }

            changingPeriod = false;
        }

        private void DateRange_ValueChanged(object sender, EventArgs e)
        {
            if (changingPeriod || cboPeriod.SelectedItem == null)
                return;

            if (sender == dtpFrom || sender == dtpTo)
            {
                changingPeriod = true;
                cboPeriod.SelectedItem = "Tùy chỉnh";
                changingPeriod = false;
            }
        }

        private async void btnRunReport_Click(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
        }

        private async Task LoadReportDataAsync()
        {
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date;

            if (from > to)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.",
                    "Khoảng thời gian không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnRunReport.Enabled = false;
            System.Windows.Forms.Cursor previousCursor = this.Cursor;
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            lblReportStatus.Text = "Đang tải dữ liệu báo cáo...";
            SetSummaryPlaceholders();

            try
            {
                ApiClientService.SetAuthenticationToken();

                string url = "reports/analytics?from=" + from.ToString("yyyy-MM-dd")
                    + "&to=" + to.ToString("yyyy-MM-dd");

                ReportAnalyticsDto result = await ApiClientService.Client
                    .GetFromJsonAsync<ReportAnalyticsDto>(url);

                if (result == null)
                    throw new InvalidOperationException("API không trả dữ liệu báo cáo.");

                lastReport = result;
                lastReportFrom = from;
                lastReportTo = to;
                btnExportCsv.Enabled = true;

                hourlyRevenue = result.RevenueByHour ?? new List<HourlyRevenueDto>();
                dailyRevenue = result.RevenueByDay ?? new List<DailyRevenueDto>();
                revenueByCategory = result.RevenueByCategory ?? new List<CategoryRevenueDto>();

                // Chọn một ngày thì xem doanh thu theo giờ; nhiều ngày thì xem theo ngày.
                showDailyRevenueChart = from.Date != to.Date;
                lblHourlyTitle.Text = showDailyRevenueChart
                    ? "DOANH THU THEO NGÀY / TUẦN / THÁNG"
                    : "DOANH THU THEO GIỜ";

                // Ưu tiên dữ liệu do ADO.NET gọi Stored Procedure trả về.
                // Nếu SP chưa tồn tại/không khả dụng, dùng dữ liệu theo ngày của analytics làm dự phòng.
                bool usedStoredProcedure = false;
                try
                {
                    string dailyUrl = "reports/revenue-by-day?from="
                        + from.ToString("yyyy-MM-dd")
                        + "&to=" + to.ToString("yyyy-MM-dd");

                    List<DailyRevenueDto> storedProcedureRows =
                        await ApiClientService.Client
                            .GetFromJsonAsync<List<DailyRevenueDto>>(dailyUrl);

                    if (storedProcedureRows != null && storedProcedureRows.Count > 0)
                    {
                        dailyRevenue = storedProcedureRows;
                        usedStoredProcedure = true;
                    }
                }
                catch (Exception)
                {
                    // Không làm trắng biểu đồ nếu endpoint Stored Procedure chưa sẵn sàng.
                    // Dữ liệu dự phòng đã lấy từ reports/analytics ở phía trên.
                }

                bool currentMonth = from == new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
                    && to == DateTime.Today;
                lblRevenueTitle.Text = currentMonth ? "DOANH THU THÁNG NÀY" : "DOANH THU KỲ ĐÃ CHỌN";
                lblRevenueValue.Text = result.TotalRevenue.ToString("N0") + " đ";
                lblAverageOrderValue.Text = result.AverageOrderValue.ToString("N0") + " đ";
                lblOrderCountValue.Text = result.TotalOrders.ToString("N0") + " hóa đơn";

                BindTopProducts(result.TopProducts ?? new List<TopProductDto>());

                hourlyChartPanel.Invalidate();
                categoryChartPanel.Invalidate();
                lblReportStatus.Text = "Đã đồng bộ dữ liệu từ "
                    + from.ToString("dd/MM/yyyy") + " đến " + to.ToString("dd/MM/yyyy")
                    + (usedStoredProcedure ? " · ADO.NET/Stored Procedure" : " · dữ liệu dự phòng EF Core")
                    + " · " + DateTime.Now.ToString("HH:mm:ss");
            }
            catch (HttpRequestException ex)
            {
                lblReportStatus.Text = "Không kết nối được API báo cáo.";
                MessageBox.Show("Không kết nối được API báo cáo.\n" + ex.Message,
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                lblReportStatus.Text = "Không thể tải dữ liệu báo cáo.";
                MessageBox.Show("Không thể tải dữ liệu báo cáo.\n" + ex.Message,
                    "Lỗi báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = previousCursor;
                btnRunReport.Enabled = true;
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (lastReport == null)
            {
                MessageBox.Show(
                    "Chưa có dữ liệu báo cáo để xuất. Vui lòng tải báo cáo trước.",
                    "Chưa có dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Xuất báo cáo doanh thu CSV";
                dialog.Filter = "Tệp CSV UTF-8 (*.csv)|*.csv";
                dialog.DefaultExt = "csv";
                dialog.AddExtension = true;
                dialog.OverwritePrompt = true;
                dialog.FileName = "BaoCaoSakura_"
                    + lastReportFrom.ToString("yyyyMMdd") + "_"
                    + lastReportTo.ToString("yyyyMMdd") + ".csv";

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    List<string> lines = new List<string>();

                    lines.Add(CsvRow("BÁO CÁO DOANH THU SAKURA"));
                    lines.Add(CsvRow(
                        "Từ ngày",
                        lastReportFrom.ToString("dd/MM/yyyy"),
                        "Đến ngày",
                        lastReportTo.ToString("dd/MM/yyyy")));
                    lines.Add(CsvRow("Thời điểm xuất", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                    lines.Add(string.Empty);

                    lines.Add(CsvRow("TỔNG QUAN"));
                    lines.Add(CsvRow("Chỉ số", "Giá trị"));
                    lines.Add(CsvRow("Tổng doanh thu (VND)", lastReport.TotalRevenue.ToString("0.##", CultureInfo.InvariantCulture)));
                    lines.Add(CsvRow("Tổng số hóa đơn", lastReport.TotalOrders.ToString(CultureInfo.InvariantCulture)));
                    lines.Add(CsvRow("Giá trị hóa đơn trung bình (VND)", lastReport.AverageOrderValue.ToString("0.##", CultureInfo.InvariantCulture)));
                    lines.Add(string.Empty);

                    lines.Add(CsvRow("DOANH THU THEO NGÀY"));
                    lines.Add(CsvRow("Ngày", "Doanh thu (VND)"));
                    foreach (DailyRevenueDto row in dailyRevenue.OrderBy(x => x.Date))
                    {
                        lines.Add(CsvRow(
                            row.Date.ToString("dd/MM/yyyy"),
                            row.Revenue.ToString("0.##", CultureInfo.InvariantCulture)));
                    }
                    lines.Add(string.Empty);

                    lines.Add(CsvRow("DOANH THU THEO GIỜ"));
                    lines.Add(CsvRow("Giờ", "Doanh thu (VND)"));
                    foreach (HourlyRevenueDto row in (lastReport.RevenueByHour ?? new List<HourlyRevenueDto>()).OrderBy(x => x.Hour))
                    {
                        lines.Add(CsvRow(
                            row.Hour.ToString("00", CultureInfo.InvariantCulture) + ":00",
                            row.Revenue.ToString("0.##", CultureInfo.InvariantCulture)));
                    }
                    lines.Add(string.Empty);

                    lines.Add(CsvRow("DOANH THU THEO NGÀNH HÀNG"));
                    lines.Add(CsvRow("Tên ngành hàng", "Số lượng bán", "Doanh thu (VND)"));
                    foreach (CategoryRevenueDto row in (lastReport.RevenueByCategory ?? new List<CategoryRevenueDto>()).OrderByDescending(x => x.Revenue))
                    {
                        lines.Add(CsvRow(
                            row.CategoryName ?? "Chưa phân loại",
                            row.Quantity.ToString(CultureInfo.InvariantCulture),
                            row.Revenue.ToString("0.##", CultureInfo.InvariantCulture)));
                    }
                    lines.Add(string.Empty);

                    lines.Add(CsvRow("TOP SẢN PHẨM BÁN CHẠY"));
                    lines.Add(CsvRow("Hạng", "Mã sản phẩm", "Barcode", "Tên sản phẩm", "Đơn vị", "Số lượng", "Doanh thu (VND)"));
                    foreach (TopProductDto row in (lastReport.TopProducts ?? new List<TopProductDto>()).OrderBy(x => x.Rank))
                    {
                        lines.Add(CsvRow(
                            row.Rank.ToString(CultureInfo.InvariantCulture),
                            row.ProductId.ToString(CultureInfo.InvariantCulture),
                            row.Barcode ?? string.Empty,
                            row.ProductName ?? string.Empty,
                            row.Unit ?? "SP",
                            row.Quantity.ToString(CultureInfo.InvariantCulture),
                            row.Revenue.ToString("0.##", CultureInfo.InvariantCulture)));
                    }

                    // BOM UTF-8 giup Excel hien thi dung tieng Viet.
                    File.WriteAllText(
                        dialog.FileName,
                        string.Join(Environment.NewLine, lines),
                        new UTF8Encoding(true));

                    lblReportStatus.Text = "Đã xuất báo cáo CSV: " + Path.GetFileName(dialog.FileName);
                    MessageBox.Show(
                        "Xuất báo cáo CSV thành công.",
                        "Hoàn tất",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Không thể xuất báo cáo CSV.\n" + ex.Message,
                        "Lỗi xuất báo cáo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private static string CsvRow(params string[] values)
        {
            return string.Join(",", values.Select(CsvEscape));
        }

        private static string CsvEscape(string value)
        {
            if (value == null)
                value = string.Empty;

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        private void BindTopProducts(List<TopProductDto> items)
        {
            dgvTopProducts.DataSource = null;
            dgvTopProducts.DataSource = items;

            SetGridColumn("Rank", "Hạng", 45F, true);
            SetGridColumn("ProductId", "Mã SP", 55F, true);
            SetGridColumn("ProductName", "Tên sản phẩm", 150F, true);
            SetGridColumn("Unit", "ĐVT", 45F, true);
            SetGridColumn("Quantity", "Số lượng", 65F, true);
            SetGridColumn("Revenue", "Thành tiền", 95F, true);

            if (dgvTopProducts.Columns.Contains("Barcode"))
                dgvTopProducts.Columns["Barcode"].Visible = false;

            if (dgvTopProducts.Columns.Contains("Revenue"))
            {
                dgvTopProducts.Columns["Revenue"].DefaultCellStyle.Format = "N0";
                dgvTopProducts.Columns["Revenue"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvTopProducts.Columns.Contains("Quantity"))
                dgvTopProducts.Columns["Quantity"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
        }

        private void SetGridColumn(string name, string header, float weight, bool visible)
        {
            if (!dgvTopProducts.Columns.Contains(name))
                return;

            DataGridViewColumn column = dgvTopProducts.Columns[name];
            column.HeaderText = header;
            column.FillWeight = weight;
            column.Visible = visible;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.MinimumWidth = 40;
        }

        private void HourlyChartPanel_Paint(object sender, PaintEventArgs e)
        {
            // Tái sử dụng vùng biểu đồ hiện có để vẽ doanh thu theo từng ngày.
            // Dữ liệu được lấy từ reports/revenue-by-day (Stored Procedure),
            // hoặc từ reports/analytics nếu endpoint trên chưa sẵn sàng.
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            // Giữ biểu đồ theo giờ cho báo cáo đúng một ngày.
            if (!showDailyRevenueChart)
            {
                DrawHourlyRevenueChart(g);
                return;
            }

            Rectangle area = new Rectangle(
                48,
                12,
                Math.Max(10, hourlyChartPanel.ClientSize.Width - 62),
                Math.Max(10, hourlyChartPanel.ClientSize.Height - 42));

            List<DailyRevenueDto> points = dailyRevenue
                .OrderBy(x => x.Date)
                .ToList();

            decimal maxRevenue = points.Count == 0
                ? 0m
                : points.Max(x => x.Revenue);
            if (maxRevenue <= 0m)
                maxRevenue = 1m;

            using (Font axisFont = new Font("Segoe UI", 7F))
            using (Font smallFont = new Font("Segoe UI", 8F, FontStyle.Bold))
            using (Pen gridPen = new Pen(Color.FromArgb(226, 232, 240)))
            using (Brush axisBrush = new SolidBrush(muted))
            using (Brush barBrush = new SolidBrush(blue))
            using (Brush emptyBrush = new SolidBrush(muted))
            using (StringFormat centered = new StringFormat())
            {
                centered.Alignment = StringAlignment.Center;
                centered.LineAlignment = StringAlignment.Center;

                for (int i = 0; i <= 4; i++)
                {
                    float y = area.Top + area.Height * i / 4f;
                    g.DrawLine(gridPen, area.Left, y, area.Right, y);

                    decimal value = maxRevenue * (4 - i) / 4m;
                    g.DrawString(
                        FormatCompactMoney(value),
                        axisFont,
                        axisBrush,
                        new RectangleF(0, y - 7, 43, 14),
                        centered);
                }

                if (points.Count > 0)
                {
                    float slot = area.Width / (float)points.Count;
                    float barWidth = Math.Max(1f, Math.Min(22f, slot * 0.68f));
                    int labelStep = Math.Max(1, (int)Math.Ceiling(points.Count / 14.0));

                    for (int i = 0; i < points.Count; i++)
                    {
                        DailyRevenueDto point = points[i];
                        float height = (float)(point.Revenue / maxRevenue) * area.Height;
                        float x = area.Left + i * slot + (slot - barWidth) / 2f;
                        float y = area.Bottom - height;

                        if (point.Revenue > 0m)
                        {
                            g.FillRectangle(
                                barBrush,
                                x,
                                y,
                                barWidth,
                                Math.Max(1f, height));
                        }

                        // Khoảng ngày dài thì tự giãn nhãn để các mốc không chồng lên nhau.
                        if (i % labelStep == 0 || i == points.Count - 1)
                        {
                            float labelWidth = Math.Max(30f, Math.Min(54f, slot * labelStep));
                            float labelCenter = area.Left + i * slot + slot / 2f;
                            RectangleF labelRect = new RectangleF(
                                labelCenter - labelWidth / 2f,
                                area.Bottom + 4,
                                labelWidth,
                                22);

                            g.DrawString(
                                point.Date.ToString("dd/MM"),
                                axisFont,
                                axisBrush,
                                labelRect,
                                centered);
                        }
                    }
                }

                if (points.Count == 0 || points.All(x => x.Revenue <= 0m))
                {
                    g.DrawString(
                        "Chưa có doanh thu trong khoảng thời gian này.",
                        smallFont,
                        emptyBrush,
                        new RectangleF(
                            area.Left + 8,
                            area.Top + area.Height / 2f - 10,
                            Math.Max(10, area.Width - 16),
                            24),
                        centered);
                }
            }
        }

        private void DrawHourlyRevenueChart(Graphics g)
        {
            Rectangle area = new Rectangle(
                48,
                12,
                Math.Max(10, hourlyChartPanel.ClientSize.Width - 62),
                Math.Max(10, hourlyChartPanel.ClientSize.Height - 42));

            decimal maxRevenue = hourlyRevenue.Count == 0
                ? 0m
                : hourlyRevenue.Max(x => x.Revenue);
            if (maxRevenue <= 0m)
                maxRevenue = 1m;

            using (Font axisFont = new Font("Segoe UI", 7F))
            using (Font smallFont = new Font("Segoe UI", 8F, FontStyle.Bold))
            using (Pen gridPen = new Pen(Color.FromArgb(226, 232, 240)))
            using (Brush axisBrush = new SolidBrush(muted))
            using (Brush barBrush = new SolidBrush(blue))
            using (Brush emptyBrush = new SolidBrush(muted))
            using (StringFormat centered = new StringFormat())
            {
                centered.Alignment = StringAlignment.Center;
                centered.LineAlignment = StringAlignment.Center;

                for (int i = 0; i <= 4; i++)
                {
                    float y = area.Top + area.Height * i / 4f;
                    g.DrawLine(gridPen, area.Left, y, area.Right, y);
                    decimal value = maxRevenue * (4 - i) / 4m;
                    g.DrawString(FormatCompactMoney(value), axisFont, axisBrush,
                        new RectangleF(0, y - 7, 43, 14), centered);
                }

                List<HourlyRevenueDto> points = Enumerable.Range(0, 24)
                    .Select(hour => hourlyRevenue.FirstOrDefault(x => x.Hour == hour)
                        ?? new HourlyRevenueDto { Hour = hour, Revenue = 0m })
                    .OrderBy(x => x.Hour)
                    .ToList();

                float slot = area.Width / 24f;
                float barWidth = Math.Max(2f, slot * 0.66f);

                foreach (HourlyRevenueDto point in points)
                {
                    float height = (float)(point.Revenue / maxRevenue) * area.Height;
                    float x = area.Left + point.Hour * slot + (slot - barWidth) / 2f;
                    float y = area.Bottom - height;

                    if (point.Revenue > 0m)
                        g.FillRectangle(barBrush, x, y, barWidth, Math.Max(1f, height));

                    if (point.Hour % 2 == 0)
                    {
                        g.DrawString(point.Hour.ToString("00") + ":00", axisFont, axisBrush,
                            new RectangleF(area.Left + point.Hour * slot - slot * 0.35f,
                                area.Bottom + 4, slot * 1.7f, 22), centered);
                    }
                }

                if (hourlyRevenue.Count == 0 || hourlyRevenue.All(x => x.Revenue <= 0m))
                {
                    g.DrawString("Chưa có dữ liệu doanh thu theo giờ trong ngày này.",
                        smallFont, emptyBrush,
                        new RectangleF(area.Left + 8, area.Top + area.Height / 2f - 10,
                            Math.Max(10, area.Width - 16), 24), centered);
                }
            }
        }

        private void CategoryChartPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            if (revenueByCategory == null || revenueByCategory.Count == 0)
            {
                using (Font font = new Font("Segoe UI", 9F))
                using (Brush brush = new SolidBrush(muted))
                using (StringFormat centered = new StringFormat())
                {
                    centered.Alignment = StringAlignment.Center;
                    centered.LineAlignment = StringAlignment.Center;
                    g.DrawString("Chưa có dữ liệu doanh thu theo ngành hàng.",
                        font, brush, categoryChartPanel.ClientRectangle, centered);
                }
                return;
            }

            List<CategoryRevenueDto> rows = revenueByCategory
                .Where(x => x.Revenue > 0m)
                .OrderByDescending(x => x.Revenue)
                .Take(8).ToList();
            decimal total = rows.Sum(x => x.Revenue);

            if (rows.Count == 0 || total <= 0m)
            {
                using (Font font = new Font("Segoe UI", 9F))
                using (Brush brush = new SolidBrush(muted))
                using (StringFormat centered = new StringFormat())
                {
                    centered.Alignment = StringAlignment.Center;
                    centered.LineAlignment = StringAlignment.Center;
                    g.DrawString("Chưa có doanh thu trong kỳ.", font, brush,
                        categoryChartPanel.ClientRectangle, centered);
                }
                return;
            }

            int diameter = Math.Min(categoryChartPanel.ClientSize.Height - 24,
                Math.Max(100, categoryChartPanel.ClientSize.Width / 2 - 8));
            diameter = Math.Max(80, diameter);
            Rectangle pieRect = new Rectangle(8, Math.Max(5, (categoryChartPanel.ClientSize.Height - diameter) / 2),
                diameter, diameter);
            float startAngle = -90f;

            for (int i = 0; i < rows.Count; i++)
            {
                float sweep = (float)(rows[i].Revenue / total * 360m);
                using (Brush brush = new SolidBrush(chartColors[i % chartColors.Length]))
                using (Pen pen = new Pen(Color.White, 1.2f))
                {
                    g.FillPie(brush, pieRect, startAngle, sweep);
                    g.DrawPie(pen, pieRect, startAngle, sweep);
                }
                startAngle += sweep;
            }

            // Tạo lỗ donut ở giữa bằng màu nền.
            int hole = (int)(diameter * 0.52);
            Rectangle holeRect = new Rectangle(
                pieRect.Left + (diameter - hole) / 2,
                pieRect.Top + (diameter - hole) / 2, hole, hole);
            using (Brush white = new SolidBrush(Color.White))
                g.FillEllipse(white, holeRect);

            using (Font centerFont = new Font("Segoe UI", 8F, FontStyle.Bold))
            using (Brush centerBrush = new SolidBrush(ink))
            using (StringFormat center = new StringFormat())
            {
                center.Alignment = StringAlignment.Center;
                center.LineAlignment = StringAlignment.Center;
                g.DrawString("DOANH THU\n100%", centerFont, centerBrush, holeRect, center);
            }

            int legendX = pieRect.Right + 12;
            int legendWidth = Math.Max(80, categoryChartPanel.ClientSize.Width - legendX - 4);
            int legendY = 10;
            int rowHeight = Math.Min(26, Math.Max(19, (categoryChartPanel.ClientSize.Height - 12) / rows.Count));

            using (Font legendFont = new Font("Segoe UI", 7.5F))
            using (Brush textBrush = new SolidBrush(ink))
            using (Brush mutedBrush = new SolidBrush(muted))
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    int y = legendY + i * rowHeight;
                    using (Brush swatch = new SolidBrush(chartColors[i % chartColors.Length]))
                        g.FillRectangle(swatch, legendX, y + 3, 10, 10);

                    string name = rows[i].CategoryName ?? "Chưa phân loại";
                    string pct = (rows[i].Revenue / total * 100m).ToString("0.0") + "%";
                    RectangleF nameRect = new RectangleF(legendX + 15, y, Math.Max(30, legendWidth - 40), rowHeight);
                    RectangleF pctRect = new RectangleF(categoryChartPanel.ClientSize.Width - 35, y, 33, rowHeight);
                    g.DrawString(name, legendFont, textBrush, nameRect);
                    g.DrawString(pct, legendFont, mutedBrush, pctRect);
                }
            }
        }

        private string FormatCompactMoney(decimal value)
        {
            if (value >= 1000000000m) return (value / 1000000000m).ToString("0.#") + "B";
            if (value >= 1000000m) return (value / 1000000m).ToString("0.#") + "M";
            if (value >= 1000m) return (value / 1000m).ToString("0.#") + "K";
            return value.ToString("0");
        }
    }

    public class ReportAnalyticsDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
        public List<HourlyRevenueDto> RevenueByHour { get; set; } = new List<HourlyRevenueDto>();
        public List<DailyRevenueDto> RevenueByDay { get; set; } = new List<DailyRevenueDto>();
        public List<CategoryRevenueDto> RevenueByCategory { get; set; } = new List<CategoryRevenueDto>();
        public List<TopProductDto> TopProducts { get; set; } = new List<TopProductDto>();
    }

    public class HourlyRevenueDto
    {
        public int Hour { get; set; }
        public decimal Revenue { get; set; }
    }

    public class DailyRevenueDto
    {
        public DateTime Date { get; set; }
        public decimal Revenue { get; set; }
        public long OrderCount { get; set; }
    }

    public class CategoryRevenueDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
    }

    public class TopProductDto
    {
        public int Rank { get; set; }
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Unit { get; set; } = "SP";
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
    }
}
