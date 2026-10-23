using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        private List<CustomerDto> allCustomers = new List<CustomerDto>();
        private CustomerDto selectedCustomer;

        private readonly Color ink = Color.FromArgb(30, 41, 59);
        private readonly Color muted = Color.FromArgb(100, 116, 139);
        private readonly Color teal = Color.FromArgb(13, 148, 136);
        private Button btnHistory;

        public FormCustomerManagement()
        {
            InitializeComponent();

            CreateHistoryButton();

            ApplyRuntimeStyles();
            WireEvents();
        }

        private void CreateHistoryButton()
        {
            btnHistory = new Button();

            btnHistory.Name = "btnHistory";
            btnHistory.Text = "Lịch sử giao dịch";
            btnHistory.Size = new Size(125, 28);
            btnHistory.Location = new Point(645, 68);
            btnHistory.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnHistory.TabIndex = 6;
            btnHistory.Cursor = Cursors.Hand;
            btnHistory.FlatStyle = FlatStyle.Flat;
            btnHistory.FlatAppearance.BorderSize = 0;

            panelTop.Controls.Add(btnHistory);
            btnHistory.BringToFront();
        }

        private void WireEvents()
        {
            Load += FormCustomerManagement_Load;

            btnLoad.Click += btnLoad_Click;
            btnHistory.Click += btnHistory_Click;
            btnSearch.Click += btnSearch_Click;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;

            dgvCustomers.CellClick += dgvCustomers_CellClick;
            dgvCustomers.CellFormatting += dgvCustomers_CellFormatting;
            dgvCustomers.CellToolTipTextNeeded += dgvCustomers_CellToolTipTextNeeded;

            txtSearch.KeyDown += txtSearch_KeyDown;
        }

        // =========================================================
        // GIAO DIỆN
        // =========================================================

        private void ApplyRuntimeStyles()
        {
            BackColor = Color.FromArgb(245, 247, 251);
            ForeColor = ink;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            txtCustomerId.ReadOnly = true;
            txtCustomerId.BackColor = Color.FromArgb(248, 250, 252);

            txtRewardPoints.Text = "0";

            // Hạng khách hàng
            cboMembershipRank.Items.Clear();
            cboMembershipRank.Items.Add("Chuẩn");
            cboMembershipRank.Items.Add("Bạc");
            cboMembershipRank.Items.Add("Vàng");
            cboMembershipRank.Items.Add("VIP");
            cboMembershipRank.SelectedIndex = 0;
            cboMembershipRank.DropDownStyle = ComboBoxStyle.DropDownList;

            // Ngày tham gia
            dtpCreatedAt.Format = DateTimePickerFormat.Custom;
            dtpCreatedAt.CustomFormat = "dd/MM/yyyy";
            dtpCreatedAt.ShowCheckBox = true;
            dtpCreatedAt.Value = DateTime.Today;
            dtpCreatedAt.Checked = true;

            // DataGridView
            dgvCustomers.AutoGenerateColumns = true;
            dgvCustomers.ColumnHeadersVisible = true;
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.Visible = true;

            dgvCustomers.ReadOnly = true;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;

            dgvCustomers.MultiSelect = false;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvCustomers.EnableHeadersVisualStyles = false;

            dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(30, 41, 59);

            dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgvCustomers.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            dgvCustomers.DefaultCellStyle.BackColor = Color.White;
            dgvCustomers.DefaultCellStyle.ForeColor = ink;

            dgvCustomers.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(219, 234, 254);

            dgvCustomers.DefaultCellStyle.SelectionForeColor = ink;

            dgvCustomers.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 250, 252);

            dgvCustomers.RowTemplate.Height = 32;
            dgvCustomers.ColumnHeadersHeight = 38;

            dgvCustomers.GridColor = Color.FromArgb(226, 232, 240);

            dgvCustomers.BorderStyle = BorderStyle.None;
            dgvCustomers.BackgroundColor = Color.White;

            StyleButton(
                btnSearch,
                "Tìm kiếm",
                Color.FromArgb(37, 99, 235));

            StyleButton(
                btnLoad,
                "↻  Làm mới",
                Color.FromArgb(71, 85, 105));

            StyleButton(
                btnHistory,
                "Lịch sử giao dịch",
                Color.FromArgb(124, 58, 237));

            StyleButton(
                btnAdd,
                "+ Thêm",
                teal);

            StyleButton(
                btnUpdate,
                "Cập nhật",
                Color.FromArgb(37, 99, 235));

            StyleButton(
                btnDelete,
                "Xóa",
                Color.FromArgb(220, 38, 38));

            SetStatus("Sẵn sàng đồng bộ dữ liệu khách hàng.");
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

            button.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        private void SetStatus(string message)
        {
            if (customerStatus != null &&
                !customerStatus.IsDisposed)
            {
                customerStatus.Text = message;
            }
        }

        // =========================================================
        // LOAD DỮ LIỆU
        // =========================================================

        private async void FormCustomerManagement_Load(
            object sender,
            EventArgs e)
        {
            await LoadCustomersAsync();
        }

        private async Task LoadCustomersAsync()
        {
            try
            {
                SetStatus("Đang tải danh sách khách hàng...");

                ApiClientService.SetAuthenticationToken();

                List<CustomerDto> customers =
                    await ApiClientService.Client
                        .GetFromJsonAsync<List<CustomerDto>>("customers");

                allCustomers =
                    customers ?? new List<CustomerDto>();

                BindCustomers(allCustomers);

                UpdateCustomerKpis();

                if (allCustomers.Count == 0)
                {
                    SetStatus(
                        "API kết nối thành công nhưng chưa có khách hàng.");
                }
                else
                {
                    SetStatus(
                        "Đã đồng bộ " +
                        allCustomers.Count.ToString("N0") +
                        " khách hàng · " +
                        DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));
                }
            }
            catch (Exception ex)
            {
                SetStatus("Không thể tải dữ liệu khách hàng.");

                MessageBox.Show(
                    "Không thể tải danh sách khách hàng.\n\n" +
                    ex.Message,
                    "Lỗi kết nối API",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BindCustomers(
            IEnumerable<CustomerDto> customers)
        {
            List<CustomerDto> rows =
                customers == null
                    ? new List<CustomerDto>()
                    : customers.ToList();

            dgvCustomers.SuspendLayout();

            try
            {
                dgvCustomers.DataSource = null;

                dgvCustomers.Columns.Clear();

                dgvCustomers.AutoGenerateColumns = true;

                dgvCustomers.ColumnHeadersVisible = true;
                dgvCustomers.Visible = true;

                dgvCustomers.DataSource = rows;

                ConfigureCustomerGridColumns();

                dgvCustomers.Refresh();
            }
            finally
            {
                dgvCustomers.ResumeLayout();
            }

            selectedCustomer = null;

            ClearDetailFields(false);

            dgvCustomers.ClearSelection();
        }

        private void ConfigureCustomerGridColumns()
        {
            SetColumn(
                "CustomerId",
                "Mã KH",
                true,
                55F);

            SetColumn(
                "CustomerName",
                "Họ tên",
                true,
                125F);

            SetColumn(
                "PhoneNumber",
                "SĐT",
                true,
                100F);

            SetColumn(
                "Address",
                "Địa chỉ",
                true,
                145F);

            SetColumn(
                "RewardPoints",
                "Điểm",
                true,
                65F);

            SetColumn(
                "MembershipRank",
                "Hạng",
                true,
                75F);

            SetColumn(
                "CreatedAt",
                "Ngày tham gia",
                true,
                105F);

            SetDisplayIndex("CustomerId", 0);
            SetDisplayIndex("CustomerName", 1);
            SetDisplayIndex("PhoneNumber", 2);
            SetDisplayIndex("Address", 3);
            SetDisplayIndex("RewardPoints", 4);
            SetDisplayIndex("MembershipRank", 5);
            SetDisplayIndex("CreatedAt", 6);

            if (dgvCustomers.Columns.Contains("Address"))
            {
                dgvCustomers.Columns["Address"]
                    .DefaultCellStyle.NullValue = "—";
            }

            if (dgvCustomers.Columns.Contains("CreatedAt"))
            {
                dgvCustomers.Columns["CreatedAt"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvCustomers.Columns["CreatedAt"]
                    .DefaultCellStyle.NullValue = "—";
            }

            if (dgvCustomers.Columns.Contains("RewardPoints"))
            {
                dgvCustomers.Columns["RewardPoints"]
                    .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

                dgvCustomers.Columns["RewardPoints"]
                    .DefaultCellStyle.Format = "N0";
            }
        }

        private void SetColumn(
            string propertyName,
            string headerText,
            bool visible,
            float fillWeight)
        {
            if (!dgvCustomers.Columns.Contains(propertyName))
                return;

            DataGridViewColumn column =
                dgvCustomers.Columns[propertyName];

            column.HeaderText = headerText;
            column.Visible = visible;
            column.FillWeight = fillWeight;
            column.MinimumWidth = 45;

            column.SortMode =
                DataGridViewColumnSortMode.Automatic;
        }

        private void SetDisplayIndex(
            string propertyName,
            int displayIndex)
        {
            if (dgvCustomers.Columns.Contains(propertyName))
            {
                dgvCustomers.Columns[propertyName]
                    .DisplayIndex = displayIndex;
            }
        }

        // =========================================================
        // KPI
        // =========================================================

        private void UpdateCustomerKpis()
        {
            lblTotalCustomersValue.Text =
                allCustomers.Count.ToString("N0");

            DateTime monthStart =
                new DateTime(
                    DateTime.Today.Year,
                    DateTime.Today.Month,
                    1);

            DateTime nextMonth =
                monthStart.AddMonths(1);

            int newCustomers =
                allCustomers.Count(c =>
                    c.CreatedAt.HasValue &&
                    c.CreatedAt.Value >= monthStart &&
                    c.CreatedAt.Value < nextMonth);

            lblNewCustomersValue.Text =
                newCustomers.ToString("N0");

            lblTotalPointsValue.Text =
                allCustomers
                    .Sum(c => c.RewardPoints)
                    .ToString("N0");

            int vipCount =
                allCustomers.Count(c =>
                    string.Equals(
                        c.MembershipRank,
                        "VIP",
                        StringComparison.OrdinalIgnoreCase));

            lblVipCustomersValue.Text =
                vipCount.ToString("N0");
        }

        // =========================================================
        // TÌM KIẾM
        // =========================================================

        private async void btnSearch_Click(
            object sender,
            EventArgs e)
        {
            await SearchAsync();
        }

        private async void txtSearch_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                await SearchAsync();
            }
        }

        private async Task SearchAsync()
        {
            string keyword =
                (txtSearch.Text ?? string.Empty).Trim();

            if (keyword.Length == 0)
            {
                BindCustomers(allCustomers);

                SetStatus(
                    "Hiển thị toàn bộ " +
                    allCustomers.Count.ToString("N0") +
                    " khách hàng.");

                return;
            }

            try
            {
                SetStatus("Đang tìm kiếm khách hàng...");

                ApiClientService.SetAuthenticationToken();

                string requestUri =
                    "customers/search?keyword=" +
                    Uri.EscapeDataString(keyword);

                List<CustomerDto> result =
                    await ApiClientService.Client
                        .GetFromJsonAsync<List<CustomerDto>>(
                            requestUri);

                List<CustomerDto> matches =
                    result ?? new List<CustomerDto>();

                BindCustomers(matches);

                SetStatus(
                    "Tìm thấy " +
                    matches.Count.ToString("N0") +
                    " khách hàng phù hợp.");
            }
            catch (Exception ex)
            {
                SetStatus("Tìm kiếm thất bại.");

                MessageBox.Show(
                    "Lỗi tìm kiếm khách hàng.\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void btnLoad_Click(
            object sender,
            EventArgs e)
        {
            txtSearch.Clear();

            await LoadCustomersAsync();
        }

        // =========================================================
        // THÊM KHÁCH HÀNG
        // =========================================================

        private async void btnAdd_Click(
            object sender,
            EventArgs e)
        {
            string name =
                (txtCustomerName.Text ?? string.Empty).Trim();

            string phone =
                (txtPhoneNumber.Text ?? string.Empty).Trim();

            string address =
                (txtAddress.Text ?? string.Empty).Trim();

            int points;

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show(
                    "Tên khách hàng và số điện thoại không được để trống.",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                    txtRewardPoints.Text,
                    out points) ||
                points < 0)
            {
                MessageBox.Show(
                    "Điểm tích lũy phải là số nguyên không âm.",
                    "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtRewardPoints.Focus();

                return;
            }

            string membershipRank =
                GetSelectedMembershipRank();

            try
            {
                ApiClientService.SetAuthenticationToken();

                var payload = new
                {
                    CustomerName = name,
                    PhoneNumber = phone,
                    Address = address,
                    RewardPoints = points,

                    // Lấy đúng hạng đang chọn trên giao diện.
                    MembershipRank = membershipRank,

                    CreatedAt =
                        dtpCreatedAt.Checked
                            ? (DateTime?)dtpCreatedAt.Value.Date
                            : null
                };

                var response =
                    await ApiClientService.Client
                        .PostAsJsonAsync(
                            "customers",
                            payload);

                if (!response.IsSuccessStatusCode)
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Thêm khách hàng thất bại.\n\n" +
                        error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show(
                    "Thêm khách hàng thành công.",
                    "Hoàn tất",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadCustomersAsync();

                ClearDetailFields(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể thêm khách hàng.\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CẬP NHẬT KHÁCH HÀNG
        // =========================================================

        private async void btnUpdate_Click(
            object sender,
            EventArgs e)
        {
            int customerId;
            int points;

            if (!int.TryParse(
                    txtCustomerId.Text,
                    out customerId))
            {
                MessageBox.Show(
                    "Vui lòng chọn khách hàng cần cập nhật trong bảng.",
                    "Chưa chọn khách hàng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(
                    txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(
                    txtPhoneNumber.Text))
            {
                MessageBox.Show(
                    "Tên khách hàng và số điện thoại không được để trống.",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(
                    txtRewardPoints.Text,
                    out points) ||
                points < 0)
            {
                MessageBox.Show(
                    "Điểm tích lũy phải là số nguyên không âm.",
                    "Dữ liệu không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtRewardPoints.Focus();

                return;
            }

            // =====================================================
            // ĐÂY LÀ PHẦN ĐÃ FIX LỖI HẠNG
            // =====================================================

            string selectedRank =
                GetSelectedMembershipRank();

            if (string.IsNullOrWhiteSpace(selectedRank))
            {
                selectedRank = "Chuẩn";
            }

            DateTime? submittedCreatedAt =
                dtpCreatedAt.Checked
                    ? (DateTime?)dtpCreatedAt.Value.Date
                    : null;

            try
            {
                ApiClientService.SetAuthenticationToken();

                // QUAN TRỌNG:
                // Không lấy MembershipRank từ original nữa.
                // Lấy trực tiếp hạng mà người dùng vừa chọn.
                var payload = new
                {
                    CustomerId = customerId,

                    CustomerName =
                        txtCustomerName.Text.Trim(),

                    PhoneNumber =
                        txtPhoneNumber.Text.Trim(),

                    Address =
                        (txtAddress.Text ?? string.Empty).Trim(),

                    RewardPoints = points,

                    MembershipRank = selectedRank,

                    CreatedAt = submittedCreatedAt
                };

                var response =
                    await ApiClientService.Client
                        .PutAsJsonAsync(
                            "customers/" + customerId,
                            payload);

                if (!response.IsSuccessStatusCode)
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Cập nhật khách hàng thất bại.\n\n" +
                        error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                // =================================================
                // ĐỌC LẠI TỪ API ĐỂ KIỂM TRA DB THỰC SỰ ĐÃ LƯU
                // =================================================

                await LoadCustomersAsync();

                CustomerDto saved =
                    allCustomers.FirstOrDefault(
                        c => c.CustomerId == customerId);

                if (saved == null)
                {
                    MessageBox.Show(
                        "API đã nhận yêu cầu cập nhật nhưng không đọc lại được khách hàng.\n\n" +
                        "Hãy bấm Làm mới để kiểm tra lại.",
                        "Cần kiểm tra lại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                SelectCustomerById(customerId);

                // =================================================
                // KIỂM TRA HẠNG
                // =================================================

                string savedRank =
                    string.IsNullOrWhiteSpace(
                        saved.MembershipRank)
                        ? "Chuẩn"
                        : saved.MembershipRank.Trim();

                if (!string.Equals(
                        savedRank,
                        selectedRank,
                        StringComparison.OrdinalIgnoreCase))
                {
                    // API đã ghi đè hạng người dùng chọn.
                    MessageBox.Show(
                        "Thông tin khách hàng đã được gửi lên API,\n" +
                        "nhưng hạng đọc lại từ API không giống hạng bạn chọn.\n\n" +

                        "Hạng bạn chọn: " +
                        selectedRank +
                        "\n" +

                        "Hạng API trả về: " +
                        savedRank +
                        "\n\n" +

                        "Điều này có nghĩa backend/API đang tự động ghi đè MembershipRank.\n" +
                        "Nếu muốn cho phép chỉnh hạng thủ công, cần sửa CustomersController/Service ở Web API.",
                        "Hạng chưa được đồng bộ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                MessageBox.Show(
                    "Cập nhật khách hàng thành công.\n\n" +
                    "Hạng: " + savedRank +
                    "\n" +
                    "Điểm: " +
                    saved.RewardPoints.ToString("N0"),
                    "Hoàn tất",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể cập nhật khách hàng.\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // LẤY HẠNG ĐANG CHỌN
        // =========================================================

        private string GetSelectedMembershipRank()
        {
            if (cboMembershipRank == null)
                return "Chuẩn";

            string value =
                cboMembershipRank.SelectedItem == null
                    ? string.Empty
                    : cboMembershipRank.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(value))
                return "Chuẩn";

            return value.Trim();
        }

        // =========================================================
        // XÓA
        // =========================================================

        private async void btnDelete_Click(
            object sender,
            EventArgs e)
        {
            int customerId;

            if (!int.TryParse(
                    txtCustomerId.Text,
                    out customerId))
            {
                MessageBox.Show(
                    "Vui lòng chọn khách hàng cần xóa trong bảng.",
                    "Chưa chọn khách hàng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult confirm =
                MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa khách hàng có mã " +
                    customerId +
                    "?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                ApiClientService.SetAuthenticationToken();

                var response =
                    await ApiClientService.Client
                        .DeleteAsync(
                            "customers/" + customerId);

                if (!response.IsSuccessStatusCode)
                {
                    string error =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Xóa khách hàng thất bại.\n\n" +
                        error,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show(
                    "Xóa khách hàng thành công.",
                    "Hoàn tất",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadCustomersAsync();

                ClearDetailFields(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa khách hàng.\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CLICK KHÁCH HÀNG
        // =========================================================

        private void dgvCustomers_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.RowIndex >= dgvCustomers.Rows.Count)
            {
                return;
            }

            CustomerDto customer =
                dgvCustomers.Rows[e.RowIndex]
                    .DataBoundItem as CustomerDto;

            if (customer == null)
                return;

            ShowCustomerDetails(customer);
        }

        private void SelectCustomerById(
            int customerId)
        {
            for (int i = 0;
                 i < dgvCustomers.Rows.Count;
                 i++)
            {
                DataGridViewRow row =
                    dgvCustomers.Rows[i];

                CustomerDto customer =
                    row.DataBoundItem as CustomerDto;

                if (customer == null ||
                    customer.CustomerId != customerId)
                {
                    continue;
                }

                dgvCustomers.ClearSelection();

                row.Selected = true;

                if (dgvCustomers.Columns.Contains(
                        "CustomerId"))
                {
                    dgvCustomers.CurrentCell =
                        row.Cells["CustomerId"];
                }

                ShowCustomerDetails(customer);

                return;
            }
        }

        // =========================================================
        // HIỂN THỊ CHI TIẾT
        // =========================================================

        private void ShowCustomerDetails(
            CustomerDto customer)
        {
            if (customer == null)
                return;

            selectedCustomer = customer;

            txtCustomerId.Text =
                customer.CustomerId.ToString();

            txtCustomerName.Text =
                customer.CustomerName ?? string.Empty;

            txtPhoneNumber.Text =
                customer.PhoneNumber ?? string.Empty;

            txtAddress.Text =
                customer.Address ?? string.Empty;

            txtRewardPoints.Text =
                customer.RewardPoints.ToString();

            // =====================================================
            // FIX QUAN TRỌNG:
            // LẤY HẠNG TỪ DATABASE VÀ ĐƯA VÀO COMBOBOX
            // =====================================================

            string rank =
                string.IsNullOrWhiteSpace(
                    customer.MembershipRank)
                    ? "Chuẩn"
                    : customer.MembershipRank.Trim();

            int rankIndex =
                cboMembershipRank.Items.IndexOf(rank);

            if (rankIndex >= 0)
            {
                cboMembershipRank.SelectedIndex =
                    rankIndex;
            }
            else
            {
                cboMembershipRank.SelectedIndex = 0;
            }

            // Ngày tham gia
            if (customer.CreatedAt.HasValue)
            {
                DateTime createdAt =
                    customer.CreatedAt.Value.Date;

                if (createdAt < dtpCreatedAt.MinDate)
                    createdAt = dtpCreatedAt.MinDate;

                if (createdAt > dtpCreatedAt.MaxDate)
                    createdAt = dtpCreatedAt.MaxDate;

                dtpCreatedAt.Value = createdAt;
                dtpCreatedAt.Checked = true;
            }
            else
            {
                dtpCreatedAt.Value = DateTime.Today;
                dtpCreatedAt.Checked = false;
            }
        }

        // =========================================================
        // TOOLTIP
        // =========================================================

        private void dgvCustomers_CellToolTipTextNeeded(
            object sender,
            DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.RowIndex >= dgvCustomers.Rows.Count ||
                e.ColumnIndex < 0)
            {
                return;
            }

            DataGridViewRow row =
                dgvCustomers.Rows[e.RowIndex];

            CustomerDto customer =
                row.DataBoundItem as CustomerDto;

            if (customer == null)
                return;

            string columnName =
                dgvCustomers.Columns[e.ColumnIndex].Name;

            if (columnName == "Address")
            {
                e.ToolTipText =
                    customer.Address ?? string.Empty;
            }
            else if (columnName == "CustomerName")
            {
                e.ToolTipText =
                    customer.CustomerName ?? string.Empty;
            }
        }

        // =========================================================
        // FORMAT BẢNG
        // =========================================================

        private void dgvCustomers_CellFormatting(
            object sender,
            DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.RowIndex >= dgvCustomers.Rows.Count)
            {
                return;
            }

            CustomerDto customer =
                dgvCustomers.Rows[e.RowIndex]
                    .DataBoundItem as CustomerDto;

            if (customer == null)
                return;

            string rank =
                customer.MembershipRank ?? "Chuẩn";

            if (string.Equals(
                    rank,
                    "VIP",
                    StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor =
                    Color.FromArgb(245, 243, 255);

                e.CellStyle.ForeColor =
                    Color.FromArgb(91, 33, 182);
            }
            else if (string.Equals(
                    rank,
                    "Vàng",
                    StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor =
                    Color.FromArgb(255, 251, 235);

                e.CellStyle.ForeColor =
                    Color.FromArgb(161, 98, 7);
            }
            else if (string.Equals(
                    rank,
                    "Bạc",
                    StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor =
                    Color.FromArgb(241, 245, 249);

                e.CellStyle.ForeColor =
                    Color.FromArgb(71, 85, 105);
            }
            else
            {
                if (e.RowIndex % 2 == 0)
                {
                    e.CellStyle.BackColor =
                        Color.White;
                }
                else
                {
                    e.CellStyle.BackColor =
                        Color.FromArgb(248, 250, 252);
                }

                e.CellStyle.ForeColor = ink;
            }
        }

        // =========================================================
        // LỊCH SỬ GIAO DỊCH
        // =========================================================

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (selectedCustomer == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn một khách hàng trong danh sách trước.",
                    "Chưa chọn khách hàng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            using (FormCustomerTransactionHistory historyForm =
                   new FormCustomerTransactionHistory(
                       selectedCustomer.CustomerId,
                       selectedCustomer.CustomerName,
                       selectedCustomer.PhoneNumber,
                       selectedCustomer.MembershipRank,
                       selectedCustomer.RewardPoints))
            {
                historyForm.ShowDialog(this);
            }
        }

        // =========================================================
        // XÓA FORM
        // =========================================================

        private void ClearDetailFields(
            bool clearSearch)
        {
            selectedCustomer = null;

            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();

            txtRewardPoints.Text = "0";

            cboMembershipRank.SelectedIndex = 0;

            dtpCreatedAt.Value =
                DateTime.Today;

            dtpCreatedAt.Checked = true;

            if (clearSearch)
                txtSearch.Clear();

            if (dgvCustomers != null)
                dgvCustomers.ClearSelection();
        }
    }

    // =============================================================
    // DTO KHÁCH HÀNG
    // =============================================================

    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public int RewardPoints { get; set; }

        public string MembershipRank { get; set; }

        public DateTime? CreatedAt { get; set; }

        public CustomerDto()
        {
            CustomerName = string.Empty;
            PhoneNumber = string.Empty;
            Address = string.Empty;
            MembershipRank = "Chuẩn";
        }
    }
}