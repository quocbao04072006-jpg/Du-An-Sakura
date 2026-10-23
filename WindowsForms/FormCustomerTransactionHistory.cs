using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerTransactionHistory : Form
    {
        private readonly int customerId;
        private readonly string customerName;
        private readonly string customerPhone;
        private readonly string customerRank;
        private readonly int customerPoints;

        private List<CustomerTransactionDto> transactions =
            new List<CustomerTransactionDto>();

        private readonly Color ink = Color.FromArgb(30, 41, 59);
        private readonly Color muted = Color.FromArgb(100, 116, 139);
        private readonly Color purple = Color.FromArgb(124, 58, 237);
        private readonly Color blue = Color.FromArgb(37, 99, 235);

        public FormCustomerTransactionHistory(
            int customerId,
            string customerName,
            string customerPhone,
            string customerRank,
            int customerPoints)
        {
            InitializeComponent();

            this.customerId = customerId;
            this.customerName = customerName ?? string.Empty;
            this.customerPhone = customerPhone ?? string.Empty;
            this.customerRank = string.IsNullOrWhiteSpace(customerRank)
                ? "Chuẩn"
                : customerRank.Trim();
            this.customerPoints = customerPoints;

            ApplyRuntimeStyles();
            ShowCustomerInformation();
        }

        private void ApplyRuntimeStyles()
        {
            BackColor = Color.FromArgb(245, 247, 251);
            ForeColor = ink;
            Font = new Font("Segoe UI", 9F);

            dgvTransactions.AutoGenerateColumns = false;
            dgvTransactions.AllowUserToAddRows = false;
            dgvTransactions.AllowUserToDeleteRows = false;
            dgvTransactions.ReadOnly = true;
            dgvTransactions.MultiSelect = false;
            dgvTransactions.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvTransactions.RowHeadersVisible = false;
            dgvTransactions.EnableHeadersVisualStyles = false;
            dgvTransactions.ColumnHeadersDefaultCellStyle.BackColor = ink;
            dgvTransactions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTransactions.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvTransactions.DefaultCellStyle.Font =
                new Font("Segoe UI", 9F);
            dgvTransactions.DefaultCellStyle.ForeColor = ink;
            dgvTransactions.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(224, 231, 255);
            dgvTransactions.DefaultCellStyle.SelectionForeColor = ink;
            dgvTransactions.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 250, 252);
            dgvTransactions.GridColor = Color.FromArgb(226, 232, 240);
            dgvTransactions.BackgroundColor = Color.White;
            dgvTransactions.BorderStyle = BorderStyle.None;
            dgvTransactions.RowTemplate.Height = 30;

            dgvTransactionDetails.AutoGenerateColumns = false;
            dgvTransactionDetails.AllowUserToAddRows = false;
            dgvTransactionDetails.AllowUserToDeleteRows = false;
            dgvTransactionDetails.ReadOnly = true;
            dgvTransactionDetails.RowHeadersVisible = false;
            dgvTransactionDetails.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvTransactionDetails.EnableHeadersVisualStyles = false;
            dgvTransactionDetails.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(51, 65, 85);
            dgvTransactionDetails.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
            dgvTransactionDetails.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvTransactionDetails.DefaultCellStyle.ForeColor = ink;
            dgvTransactionDetails.GridColor =
                Color.FromArgb(226, 232, 240);
            dgvTransactionDetails.BackgroundColor = Color.White;
            dgvTransactionDetails.BorderStyle = BorderStyle.None;
            dgvTransactionDetails.RowTemplate.Height = 28;

            StyleButton(
                btnRefresh,
                "↻  Làm mới",
                blue);

            StyleButton(
                btnClose,
                "Đóng",
                Color.FromArgb(71, 85, 105));

            ConfigureTransactionGrid();
            ConfigureDetailGrid();

            dgvTransactions.SelectionChanged +=
                dgvTransactions_SelectionChanged;

            btnRefresh.Click += btnRefresh_Click;
            btnClose.Click += btnClose_Click;

            Shown += FormCustomerTransactionHistory_Shown;
        }

        private void StyleButton(
            Button button,
            string text,
            Color background)
        {
            button.Text = text;
            button.BackColor = background;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font(
                "Segoe UI",
                9F,
                FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        private void ShowCustomerInformation()
        {
            lblCustomerNameValue.Text =
                string.IsNullOrWhiteSpace(customerName)
                    ? "Chưa có tên"
                    : customerName;

            lblCustomerIdValue.Text =
                "Mã KH: " + customerId;

            lblCustomerPhoneValue.Text =
                "SĐT: " +
                (string.IsNullOrWhiteSpace(customerPhone)
                    ? "Chưa có"
                    : customerPhone);

            lblCustomerRankValue.Text =
                "Hạng: " + customerRank;

            lblCustomerPointsValue.Text =
                customerPoints.ToString("N0");

            lblTransactionCountValue.Text = "0";
            lblTotalSpentValue.Text = "0 đ";
            lblAverageValue.Text = "0 đ";
        }

        private void ConfigureTransactionGrid()
        {
            dgvTransactions.Columns.Clear();

            dgvTransactions.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colOrderId",
                    HeaderText = "Mã giao dịch",
                    DataPropertyName = "OrderId",
                    Width = 95
                });

            dgvTransactions.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colOrderDate",
                    HeaderText = "Ngày giao dịch",
                    DataPropertyName = "OrderDate",
                    Width = 145,
                    DefaultCellStyle =
                    {
                        Format = "dd/MM/yyyy HH:mm"
                    }
                });

            dgvTransactions.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colTotalAmount",
                    HeaderText = "Tổng tiền",
                    DataPropertyName = "TotalAmount",
                    Width = 135,
                    DefaultCellStyle =
                    {
                        Format = "N0",
                        Alignment =
                            DataGridViewContentAlignment.MiddleRight
                    }
                });

            dgvTransactions.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colPaymentMethod",
                    HeaderText = "Thanh toán",
                    DataPropertyName = "PaymentMethod",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill
                });
        }

        private void ConfigureDetailGrid()
        {
            dgvTransactionDetails.Columns.Clear();

            dgvTransactionDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colProductId",
                    HeaderText = "Mã SP",
                    DataPropertyName = "ProductId",
                    Width = 70
                });

            dgvTransactionDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colProductName",
                    HeaderText = "Tên sản phẩm",
                    DataPropertyName = "ProductName",
                    AutoSizeMode =
                        DataGridViewAutoSizeColumnMode.Fill,
                    MinimumWidth = 160
                });

            dgvTransactionDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colQuantity",
                    HeaderText = "SL",
                    DataPropertyName = "Quantity",
                    Width = 55
                });

            dgvTransactionDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colUnitPrice",
                    HeaderText = "Đơn giá",
                    DataPropertyName = "UnitPrice",
                    Width = 105,
                    DefaultCellStyle =
                    {
                        Format = "N0",
                        Alignment =
                            DataGridViewContentAlignment.MiddleRight
                    }
                });

            dgvTransactionDetails.Columns.Add(
                new DataGridViewTextBoxColumn
                {
                    Name = "colLineTotal",
                    HeaderText = "Thành tiền",
                    DataPropertyName = "LineTotal",
                    Width = 115,
                    DefaultCellStyle =
                    {
                        Format = "N0",
                        Alignment =
                            DataGridViewContentAlignment.MiddleRight
                    }
                });
        }

        private async void FormCustomerTransactionHistory_Shown(
            object sender,
            EventArgs e)
        {
            await LoadTransactionHistoryAsync();
        }

        private async void btnRefresh_Click(
            object sender,
            EventArgs e)
        {
            await LoadTransactionHistoryAsync();
        }

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }

        private async Task LoadTransactionHistoryAsync()
        {
            btnRefresh.Enabled = false;
            SetStatus("Đang tải lịch sử giao dịch...");

            try
            {
                ApiClientService.SetAuthenticationToken();

                string endpoint =
                    "orders/customer/" + customerId;

                using (var response =
                    await ApiClientService.Client.GetAsync(endpoint))
                {
                    string json =
                        await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        MessageBox.Show(
                            "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.",
                            "Chưa xác thực",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        Close();
                        return;
                    }

                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        MessageBox.Show(
                            "Tài khoản hiện tại không có quyền xem lịch sử giao dịch.",
                            "Không có quyền",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(
                            "Không tải được lịch sử giao dịch.\n\n" +
                            "HTTP " +
                            (int)response.StatusCode +
                            "\n" +
                            ExtractApiMessage(json),
                            "Lỗi API",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    transactions =
                        System.Text.Json.JsonSerializer
                            .Deserialize<List<CustomerTransactionDto>>(
                                json,
                                new System.Text.Json.JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                })
                        ?? new List<CustomerTransactionDto>();
                }

                BindTransactionHistory();

                SetStatus(
                    "Đã đồng bộ " +
                    transactions.Count.ToString("N0") +
                    " giao dịch · " +
                    DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));
            }
            catch (Exception ex)
            {
                SetStatus("Không thể tải lịch sử giao dịch.");

                MessageBox.Show(
                    "Không thể tải lịch sử giao dịch.\n\n" +
                    ex.Message,
                    "Lỗi kết nối API",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    btnRefresh.Enabled = true;
            }
        }

        private void BindTransactionHistory()
        {
            dgvTransactions.DataSource = null;
            dgvTransactions.DataSource = transactions;

            UpdateSummary();

            dgvTransactionDetails.DataSource = null;
            lblSelectedTransaction.Text =
                "Chưa chọn giao dịch";

            if (transactions.Count > 0)
            {
                dgvTransactions.ClearSelection();

                dgvTransactions.Rows[0].Selected = true;

                ShowTransactionDetails(transactions[0]);
            }
            else
            {
                lblSelectedTransaction.Text =
                    "Khách hàng chưa có giao dịch";
            }
        }

        private void UpdateSummary()
        {
            int count = transactions.Count;

            decimal total =
                transactions.Sum(t => t.TotalAmount);

            decimal average =
                count == 0
                    ? 0m
                    : total / count;

            lblTransactionCountValue.Text =
                count.ToString("N0");

            lblTotalSpentValue.Text =
                total.ToString("N0") + " đ";

            lblAverageValue.Text =
                average.ToString("N0") + " đ";
        }

        private void dgvTransactions_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (dgvTransactions.CurrentRow == null)
                return;

            CustomerTransactionDto transaction =
                dgvTransactions.CurrentRow.DataBoundItem
                    as CustomerTransactionDto;

            if (transaction == null)
                return;

            ShowTransactionDetails(transaction);
        }

        private void ShowTransactionDetails(
            CustomerTransactionDto transaction)
        {
            lblSelectedTransaction.Text =
                "Giao dịch #" +
                transaction.OrderId +
                " · " +
                transaction.OrderDate.ToString("dd/MM/yyyy HH:mm") +
                " · " +
                transaction.TotalAmount.ToString("N0") +
                " đ";

            dgvTransactionDetails.DataSource = null;
            dgvTransactionDetails.DataSource =
                transaction.Items ??
                new List<CustomerTransactionItemDto>();
        }

        private void SetStatus(string message)
        {
            lblStatus.Text = message;
        }

        private string ExtractApiMessage(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return "Máy chủ không trả về nội dung chi tiết.";

            try
            {
                using (var document =
                       System.Text.Json.JsonDocument.Parse(json))
                {
                    System.Text.Json.JsonElement message;

                    if (document.RootElement.TryGetProperty(
                            "message",
                            out message))
                    {
                        return message.GetString() ??
                               "Máy chủ không trả về nội dung chi tiết.";
                    }
                }
            }
            catch
            {
                // Nếu response không phải JSON thì hiển thị nội dung thô.
            }

            return json;
        }
    }

    public class CustomerTransactionDto
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentMethod { get; set; } =
            "Tiền mặt";

        public List<CustomerTransactionItemDto> Items { get; set; } =
            new List<CustomerTransactionItemDto>();
    }

    public class CustomerTransactionItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } =
            string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }
    }
}
