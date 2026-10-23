using System;
using System.Drawing;
using System.Globalization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormMainShell : Form
    {
        private Form _activeForm;

        // =========================================================
        // DASHBOARD
        // =========================================================

        private Panel panelDashboard;

        private Label lblRevenueValue;
        private Label lblOrdersValue;
        private Label lblLowStockValue;
        private Label lblCustomersValue;

        private Label lblAttentionOrders;
        private Label lblAttentionStock;
        private Label lblAttentionCustomers;

        private Label lblDashboardStatus;

        // =========================================================
        // MÀU GIAO DIỆN
        // =========================================================

        private readonly Color sidebarColor =
            Color.FromArgb(24, 30, 48);

        private readonly Color activeColor =
            Color.FromArgb(13, 148, 136);

        private readonly Color pageColor =
            Color.FromArgb(244, 247, 251);

        private readonly Color textColor =
            Color.FromArgb(35, 48, 68);

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public FormMainShell()
        {
            InitializeComponent();

            StyleMainWindow();
            CreateDashboard();
        }

        // =========================================================
        // STYLE FORM CHÍNH
        // =========================================================

        private void StyleMainWindow()
        {
            Text = "MiniMart | Quản lý bán hàng và tồn kho";

            StartPosition = FormStartPosition.CenterScreen;

            MinimumSize = new Size(1100, 650);

            BackColor = pageColor;

            panelSidebar.BackColor = sidebarColor;
            panelTopHeader.BackColor = Color.White;
            panelMainContent.BackColor = pageColor;

            // Logo
            lblLogo.Text = "MINIMART\r\nSAKURA";

            lblLogo.Font =
                new Font(
                    "Segoe UI",
                    15F,
                    FontStyle.Bold);

            lblLogo.ForeColor = Color.White;

            lblLogo.TextAlign =
                ContentAlignment.MiddleCenter;

            // Tiêu đề
            lblTitle.Font =
                new Font(
                    "Segoe UI",
                    14F,
                    FontStyle.Bold);

            lblTitle.ForeColor = textColor;

            // Thông tin user
            lblUserInfo.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Regular);

            lblUserInfo.ForeColor =
                Color.FromArgb(90, 102, 120);

            // Các nút menu
            StyleMenuButton(btnDashboard);
            StyleMenuButton(btnPOS);
            StyleMenuButton(btnCategory);
            StyleMenuButton(btnProduct);
            StyleMenuButton(btnCustomer);
            StyleMenuButton(btnInventory);
            StyleMenuButton(btnReports);
            StyleMenuButton(btnSettings);
            StyleMenuButton(btnUserManage);

            // Logout
            btnLogout.Dock = DockStyle.Fill;

            btnLogout.FlatStyle =
                FlatStyle.Flat;

            btnLogout.FlatAppearance.BorderSize = 0;

            btnLogout.BackColor =
                Color.FromArgb(35, 43, 63);

            btnLogout.ForeColor =
                Color.FromArgb(255, 220, 220);

            btnLogout.Font =
                new Font(
                    "Segoe UI",
                    9.5F,
                    FontStyle.Bold);

            btnLogout.Text =
                "🚪  Đăng xuất";

            btnLogout.TextAlign =
                ContentAlignment.MiddleLeft;

            btnLogout.Padding =
                new Padding(15, 0, 0, 0);

            btnLogout.Cursor =
                Cursors.Hand;

            btnLogout.UseVisualStyleBackColor = false;
        }

        private void StyleMenuButton(Button button)
        {
            if (button == null)
                return;

            button.Dock = DockStyle.Top;

            button.Height = 42;

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderSize = 0;

            button.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(39, 50, 74);

            button.BackColor =
                sidebarColor;

            button.ForeColor =
                Color.FromArgb(222, 229, 240);

            button.Font =
                new Font(
                    "Segoe UI",
                    9.5F,
                    FontStyle.Regular);

            button.TextAlign =
                ContentAlignment.MiddleLeft;

            button.Padding =
                new Padding(15, 0, 4, 0);

            button.Cursor =
                Cursors.Hand;

            button.UseVisualStyleBackColor = false;
        }

        // =========================================================
        // TẠO DASHBOARD
        // =========================================================

        private void CreateDashboard()
        {
            panelDashboard = new Panel();

            panelDashboard.Dock =
                DockStyle.Fill;

            panelDashboard.BackColor =
                pageColor;

            panelDashboard.AutoScroll = true;

            panelDashboard.Padding =
                new Padding(24);

            // =====================================================
            // TIÊU ĐỀ
            // =====================================================

            Label lblDashboardTitle =
                new Label();

            lblDashboardTitle.Text =
                "TỔNG QUAN ĐIỀU HÀNH";

            lblDashboardTitle.Font =
                new Font(
                    "Segoe UI",
                    22F,
                    FontStyle.Bold);

            lblDashboardTitle.ForeColor =
                textColor;

            lblDashboardTitle.AutoSize = true;

            lblDashboardTitle.Location =
                new Point(24, 20);

            // =====================================================
            // MÔ TẢ
            // =====================================================

            Label lblDashboardSubtitle =
                new Label();

            lblDashboardSubtitle.Text =
                "Theo dõi nhanh tình hình hoạt động của MiniMart trong ngày";

            lblDashboardSubtitle.Font =
                new Font(
                    "Segoe UI",
                    9.5F);

            lblDashboardSubtitle.ForeColor =
                Color.FromArgb(112, 124, 143);

            lblDashboardSubtitle.AutoSize = true;

            lblDashboardSubtitle.Location =
                new Point(27, 60);

            // =====================================================
            // NGÀY
            // =====================================================

            Label lblDashboardDate =
                new Label();

            lblDashboardDate.Text =
                DateTime.Now.ToString(
                    "dddd, dd/MM/yyyy",
                    new CultureInfo("vi-VN"));

            lblDashboardDate.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            lblDashboardDate.ForeColor =
                Color.FromArgb(76, 92, 116);

            lblDashboardDate.AutoSize = true;

            lblDashboardDate.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            lblDashboardDate.Location =
                new Point(850, 27);

            // =====================================================
            // ĐƯỜNG KẺ
            // =====================================================

            Panel headerLine =
                new Panel();

            headerLine.BackColor =
                Color.FromArgb(220, 226, 235);

            headerLine.Height = 1;

            headerLine.Location =
                new Point(24, 90);

            headerLine.Width = 1000;

            headerLine.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            // =====================================================
            // DOANH THU
            // =====================================================

            Panel revenuePanel =
                new Panel();

            revenuePanel.Location =
                new Point(24, 112);

            revenuePanel.Size =
                new Size(600, 155);

            revenuePanel.BackColor =
                Color.FromArgb(31, 83, 157);

            revenuePanel.Padding =
                new Padding(22);

            Label lblRevenueTitle =
                new Label();

            lblRevenueTitle.Text =
                "DOANH THU HÔM NAY";

            lblRevenueTitle.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);

            lblRevenueTitle.ForeColor =
                Color.FromArgb(220, 232, 250);

            lblRevenueTitle.AutoSize = true;

            lblRevenueTitle.Location =
                new Point(22, 18);

            lblRevenueValue =
                new Label();

            lblRevenueValue.Text =
                "Đang tải...";

            lblRevenueValue.Font =
                new Font(
                    "Segoe UI",
                    27F,
                    FontStyle.Bold);

            lblRevenueValue.ForeColor =
                Color.White;

            lblRevenueValue.AutoEllipsis = true;

            lblRevenueValue.Location =
                new Point(20, 45);

            lblRevenueValue.Size =
                new Size(550, 55);

            Label lblRevenueNote =
                new Label();

            lblRevenueNote.Text =
                "Dữ liệu được lấy trực tiếp từ hệ thống bán hàng";

            lblRevenueNote.Font =
                new Font(
                    "Segoe UI",
                    8.5F);

            lblRevenueNote.ForeColor =
                Color.FromArgb(205, 220, 242);

            lblRevenueNote.AutoSize = true;

            lblRevenueNote.Location =
                new Point(22, 116);

            revenuePanel.Controls.Add(
                lblRevenueTitle);

            revenuePanel.Controls.Add(
                lblRevenueValue);

            revenuePanel.Controls.Add(
                lblRevenueNote);

            // =====================================================
            // ĐƠN HÀNG
            // =====================================================

            Panel ordersPanel =
                CreateInfoCard(
                    "ĐƠN HÀNG",
                    "Đang tải...",
                    "Tổng số hóa đơn",
                    Color.FromArgb(16, 150, 105),
                    out lblOrdersValue);

            ordersPanel.Location =
                new Point(644, 112);

            // =====================================================
            // TỒN KHO
            // =====================================================

            Panel stockPanel =
                CreateInfoCard(
                    "TỒN KHO",
                    "Đang tải...",
                    "Sản phẩm cần chú ý",
                    Color.FromArgb(220, 137, 38),
                    out lblLowStockValue);

            stockPanel.Location =
                new Point(644, 198);

            // =====================================================
            // KHÁCH HÀNG
            // =====================================================

            Panel customerPanel =
                CreateInfoCard(
                    "KHÁCH HÀNG",
                    "Đang tải...",
                    "Tổng số khách hàng",
                    Color.FromArgb(124, 58, 237),
                    out lblCustomersValue);

            customerPanel.Location =
                new Point(644, 284);

            // =====================================================
            // CẦN QUAN TÂM
            // =====================================================

            Panel attentionPanel =
                new Panel();

            attentionPanel.Location =
                new Point(24, 292);

            attentionPanel.Size =
                new Size(600, 205);

            attentionPanel.BackColor =
                Color.White;

            attentionPanel.BorderStyle =
                BorderStyle.FixedSingle;

            Label attentionTitle =
                new Label();

            attentionTitle.Text =
                "CẦN QUAN TÂM";

            attentionTitle.Font =
                new Font(
                    "Segoe UI",
                    10.5F,
                    FontStyle.Bold);

            attentionTitle.ForeColor =
                textColor;

            attentionTitle.AutoSize = true;

            attentionTitle.Location =
                new Point(20, 16);

            Label attentionSubtitle =
                new Label();

            attentionSubtitle.Text =
                "Các chỉ số quan trọng để kiểm tra nhanh";

            attentionSubtitle.Font =
                new Font(
                    "Segoe UI",
                    8.5F);

            attentionSubtitle.ForeColor =
                Color.FromArgb(112, 124, 143);

            attentionSubtitle.AutoSize = true;

            attentionSubtitle.Location =
                new Point(20, 42);

            Panel rowOrders =
                CreateAttentionRow(
                    "●",
                    "Tổng số đơn hàng",
                    Color.FromArgb(16, 150, 105),
                    out lblAttentionOrders);

            rowOrders.Location =
                new Point(18, 75);

            Panel rowStock =
                CreateAttentionRow(
                    "●",
                    "Sản phẩm sắp hết hàng",
                    Color.FromArgb(220, 137, 38),
                    out lblAttentionStock);

            rowStock.Location =
                new Point(18, 115);

            Panel rowCustomers =
                CreateAttentionRow(
                    "●",
                    "Khách hàng đang quản lý",
                    Color.FromArgb(124, 58, 237),
                    out lblAttentionCustomers);

            rowCustomers.Location =
                new Point(18, 155);

            attentionPanel.Controls.Add(
                attentionTitle);

            attentionPanel.Controls.Add(
                attentionSubtitle);

            attentionPanel.Controls.Add(
                rowOrders);

            attentionPanel.Controls.Add(
                rowStock);

            attentionPanel.Controls.Add(
                rowCustomers);

            // =====================================================
            // THAO TÁC NHANH
            // =====================================================

            Panel quickPanel =
                new Panel();

            quickPanel.Location =
                new Point(644, 390);

            quickPanel.Size =
                new Size(390, 220);

            quickPanel.BackColor =
                Color.White;

            quickPanel.BorderStyle =
                BorderStyle.FixedSingle;

            Label quickTitle =
                new Label();

            quickTitle.Text =
                "THAO TÁC NHANH";

            quickTitle.Font =
                new Font(
                    "Segoe UI",
                    10.5F,
                    FontStyle.Bold);

            quickTitle.ForeColor =
                textColor;

            quickTitle.AutoSize = true;

            quickTitle.Location =
                new Point(20, 16);

            Label quickSubtitle =
                new Label();

            quickSubtitle.Text =
                "Truy cập nhanh các chức năng thường dùng";

            quickSubtitle.Font =
                new Font(
                    "Segoe UI",
                    8.5F);

            quickSubtitle.ForeColor =
                Color.FromArgb(112, 124, 143);

            quickSubtitle.AutoSize = true;

            quickSubtitle.Location =
                new Point(20, 42);

            Button quickPOS =
                CreateQuickButton(
                    "Bán hàng (POS)",
                    Color.FromArgb(31, 83, 157));

            quickPOS.Location =
                new Point(20, 75);

            quickPOS.Click +=
                btnPOS_Click;

            Button quickProduct =
                CreateQuickButton(
                    "Quản lý sản phẩm",
                    Color.FromArgb(16, 150, 105));

            quickProduct.Location =
                new Point(205, 75);

            quickProduct.Click +=
                btnProduct_Click;

            Button quickCustomer =
                CreateQuickButton(
                    "Quản lý khách hàng",
                    Color.FromArgb(124, 58, 237));

            quickCustomer.Location =
                new Point(20, 130);

            quickCustomer.Click +=
                btnCustomer_Click;

            Button quickInventory =
                CreateQuickButton(
                    "Kho và tồn kho",
                    Color.FromArgb(220, 137, 38));

            quickInventory.Location =
                new Point(205, 130);

            quickInventory.Click +=
                btnInventory_Click;

            quickPanel.Controls.Add(
                quickTitle);

            quickPanel.Controls.Add(
                quickSubtitle);

            quickPanel.Controls.Add(
                quickPOS);

            quickPanel.Controls.Add(
                quickProduct);

            quickPanel.Controls.Add(
                quickCustomer);

            quickPanel.Controls.Add(
                quickInventory);

            // =====================================================
            // TRẠNG THÁI HỆ THỐNG
            // =====================================================

            Panel statusPanel =
                new Panel();

            statusPanel.Location =
                new Point(24, 520);

            statusPanel.Size =
                new Size(600, 90);

            statusPanel.BackColor =
                Color.White;

            statusPanel.BorderStyle =
                BorderStyle.FixedSingle;

            Label statusTitle =
                new Label();

            statusTitle.Text =
                "TRẠNG THÁI HỆ THỐNG";

            statusTitle.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);

            statusTitle.ForeColor =
                textColor;

            statusTitle.AutoSize = true;

            statusTitle.Location =
                new Point(18, 13);

            lblDashboardStatus =
                new Label();

            lblDashboardStatus.Text =
                "Sẵn sàng kết nối API.";

            lblDashboardStatus.Font =
                new Font(
                    "Segoe UI",
                    8.5F);

            lblDashboardStatus.ForeColor =
                Color.FromArgb(80, 95, 115);

            lblDashboardStatus.AutoSize = true;

            lblDashboardStatus.Location =
                new Point(18, 43);

            statusPanel.Controls.Add(
                statusTitle);

            statusPanel.Controls.Add(
                lblDashboardStatus);

            // =====================================================
            // NÚT LÀM MỚI
            // =====================================================

            Button btnRefresh =
                new Button();

            btnRefresh.Text =
                "↻  Làm mới dữ liệu";

            btnRefresh.Size =
                new Size(170, 42);

            btnRefresh.Location =
                new Point(644, 630);

            btnRefresh.BackColor =
                activeColor;

            btnRefresh.ForeColor =
                Color.White;

            btnRefresh.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            btnRefresh.FlatStyle =
                FlatStyle.Flat;

            btnRefresh.FlatAppearance.BorderSize = 0;

            btnRefresh.Cursor =
                Cursors.Hand;

            btnRefresh.UseVisualStyleBackColor = false;

            btnRefresh.Click +=
                async delegate
                {
                    await LoadDashboardDataAsync();
                };

            // =====================================================
            // ADD CONTROL
            // =====================================================

            panelDashboard.Controls.Add(
                lblDashboardTitle);

            panelDashboard.Controls.Add(
                lblDashboardSubtitle);

            panelDashboard.Controls.Add(
                lblDashboardDate);

            panelDashboard.Controls.Add(
                headerLine);

            panelDashboard.Controls.Add(
                revenuePanel);

            panelDashboard.Controls.Add(
                ordersPanel);

            panelDashboard.Controls.Add(
                stockPanel);

            panelDashboard.Controls.Add(
                customerPanel);

            panelDashboard.Controls.Add(
                attentionPanel);

            panelDashboard.Controls.Add(
                quickPanel);

            panelDashboard.Controls.Add(
                statusPanel);

            panelDashboard.Controls.Add(
                btnRefresh);

            panelMainContent.Controls.Add(
                panelDashboard);
        }

        // =========================================================
        // TẠO INFO CARD
        // =========================================================

        private Panel CreateInfoCard(
            string title,
            string value,
            string description,
            Color accent,
            out Label valueLabel)
        {
            Panel panel =
                new Panel();

            panel.Size =
                new Size(390, 76);

            panel.BackColor =
                Color.White;

            panel.BorderStyle =
                BorderStyle.FixedSingle;

            Panel accentBar =
                new Panel();

            accentBar.Dock =
                DockStyle.Left;

            accentBar.Width = 5;

            accentBar.BackColor =
                accent;

            Label titleLabel =
                new Label();

            titleLabel.Text =
                title;

            titleLabel.Font =
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold);

            titleLabel.ForeColor =
                Color.FromArgb(104, 116, 134);

            titleLabel.AutoSize = true;

            titleLabel.Location =
                new Point(18, 10);

            valueLabel =
                new Label();

            valueLabel.Text =
                value;

            valueLabel.Font =
                new Font(
                    "Segoe UI",
                    16F,
                    FontStyle.Bold);

            valueLabel.ForeColor =
                accent;

            valueLabel.AutoEllipsis = true;

            valueLabel.Location =
                new Point(18, 30);

            valueLabel.Size =
                new Size(125, 32);

            Label descriptionLabel =
                new Label();

            descriptionLabel.Text =
                description;

            descriptionLabel.Font =
                new Font(
                    "Segoe UI",
                    8.5F);

            descriptionLabel.ForeColor =
                Color.FromArgb(130, 140, 155);

            descriptionLabel.AutoSize = true;

            descriptionLabel.Location =
                new Point(150, 35);

            panel.Controls.Add(
                accentBar);

            panel.Controls.Add(
                titleLabel);

            panel.Controls.Add(
                valueLabel);

            panel.Controls.Add(
                descriptionLabel);

            return panel;
        }

        // =========================================================
        // TẠO ATTENTION ROW
        // =========================================================

        private Panel CreateAttentionRow(
            string icon,
            string title,
            Color accent,
            out Label valueLabel)
        {
            Panel row =
                new Panel();

            row.Size =
                new Size(550, 32);

            row.BackColor =
                Color.FromArgb(
                    248,
                    250,
                    253);

            Label iconLabel =
                new Label();

            iconLabel.Text =
                icon;

            iconLabel.Font =
                new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold);

            iconLabel.ForeColor =
                accent;

            iconLabel.AutoSize = true;

            iconLabel.Location =
                new Point(10, 6);

            Label titleLabel =
                new Label();

            titleLabel.Text =
                title;

            titleLabel.Font =
                new Font(
                    "Segoe UI",
                    9F);

            titleLabel.ForeColor =
                textColor;

            titleLabel.AutoSize = true;

            titleLabel.Location =
                new Point(30, 7);

            valueLabel =
                new Label();

            valueLabel.Text =
                "0";

            valueLabel.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            valueLabel.ForeColor =
                accent;

            valueLabel.AutoSize = true;

            valueLabel.Location =
                new Point(430, 7);

            row.Controls.Add(
                iconLabel);

            row.Controls.Add(
                titleLabel);

            row.Controls.Add(
                valueLabel);

            return row;
        }

        // =========================================================
        // TẠO QUICK BUTTON
        // =========================================================

        private Button CreateQuickButton(
            string text,
            Color color)
        {
            Button button =
                new Button();

            button.Text =
                text;

            button.Size =
                new Size(165, 42);

            button.BackColor =
                Color.White;

            button.ForeColor =
                color;

            button.Font =
                new Font(
                    "Segoe UI",
                    8.5F,
                    FontStyle.Bold);

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderColor =
                Color.FromArgb(
                    215,
                    222,
                    232);

            button.FlatAppearance.BorderSize = 1;

            button.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(
                    245,
                    248,
                    252);

            button.Cursor =
                Cursors.Hand;

            button.UseVisualStyleBackColor = false;

            return button;
        }

        // =========================================================
        // LOAD DỮ LIỆU DASHBOARD
        // =========================================================

        private async Task LoadDashboardDataAsync()
        {
            if (lblRevenueValue == null)
                return;

            lblRevenueValue.Text =
                "Đang tải...";

            lblOrdersValue.Text =
                "Đang tải...";

            lblLowStockValue.Text =
                "Đang tải...";

            lblCustomersValue.Text =
                "Đang tải...";

            try
            {
                // =================================================
                // KIỂM TRA TOKEN
                // =================================================

                if (string.IsNullOrWhiteSpace(
                    SessionManager.JwtToken))
                {
                    lblDashboardStatus.Text =
                        "Không có phiên đăng nhập. Vui lòng đăng nhập lại.";

                    return;
                }

                ApiClientService.SetAuthenticationToken();

                // =================================================
                // 1. LẤY DỮ LIỆU DASHBOARD
                // =================================================

                string dashboardJson =
                    await ApiClientService.GetDataWithTokenAsync(
                        "Reports/dashboard-summary");

                if (string.IsNullOrWhiteSpace(
                    dashboardJson))
                {
                    throw new Exception(
                        "API Dashboard không trả về dữ liệu.");
                }

                int orders = 0;
                int lowStock = 0;
                decimal revenue = 0;

                using (
                    JsonDocument document =
                    JsonDocument.Parse(
                        dashboardJson))
                {
                    JsonElement root =
                        document.RootElement;

                    revenue =
                        ReadDecimal(
                            root,
                            "todayRevenue");

                    orders =
                        ReadInt(
                            root,
                            "totalOrders");

                    lowStock =
                        ReadInt(
                            root,
                            "lowStockProducts");
                }

                // =================================================
                // 2. HIỂN THỊ DOANH THU
                // =================================================

                lblRevenueValue.Text =
                    revenue.ToString(
                        "N0",
                        new CultureInfo("vi-VN"))
                    + " ₫";

                // =================================================
                // 3. HIỂN THỊ ĐƠN HÀNG
                // =================================================

                lblOrdersValue.Text =
                    orders.ToString("N0");

                lblAttentionOrders.Text =
                    orders.ToString("N0");

                // =================================================
                // 4. HIỂN THỊ TỒN KHO
                // =================================================

                lblLowStockValue.Text =
                    lowStock.ToString("N0");

                lblAttentionStock.Text =
                    lowStock.ToString("N0");

                // =================================================
                // 5. LẤY KHÁCH HÀNG TRỰC TIẾP
                //
                // Không sử dụng totalCustomers từ
                // Reports/dashboard-summary nữa.
                //
                // Lý do:
                // Dashboard trước đây có thể trả 0 trong khi
                // màn hình Quản lý khách hàng có 7.
                // =================================================

                int totalCustomers =
                    await GetTotalCustomersAsync();

                // =================================================
                // 6. HIỂN THỊ KHÁCH HÀNG
                // =================================================

                lblCustomersValue.Text =
                    totalCustomers.ToString("N0");

                lblAttentionCustomers.Text =
                    totalCustomers.ToString("N0");

                // =================================================
                // 7. TRẠNG THÁI
                // =================================================

                lblDashboardStatus.Text =
                    "Cập nhật thành công lúc "
                    + DateTime.Now.ToString(
                        "HH:mm:ss dd/MM/yyyy");
            }
            catch (UnauthorizedAccessException ex)
            {
                lblDashboardStatus.Text =
                    "Phiên đăng nhập không hợp lệ.";

                MessageBox.Show(
                    ex.Message,
                    "Lỗi xác thực",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                lblDashboardStatus.Text =
                    "Không thể lấy dữ liệu từ API.";

                MessageBox.Show(
                    "Không thể tải Dashboard.\n\n"
                    + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // LẤY TỔNG KHÁCH HÀNG
        // =========================================================

        private async Task<int> GetTotalCustomersAsync()
        {
            string customersJson =
                await ApiClientService.GetDataWithTokenAsync(
                    "customers");

            if (string.IsNullOrWhiteSpace(
                customersJson))
            {
                throw new Exception(
                    "API customers không trả về dữ liệu.");
            }

            using (
                JsonDocument document =
                JsonDocument.Parse(
                    customersJson))
            {
                JsonElement root =
                    document.RootElement;

                // =================================================
                // TRƯỜNG HỢP 1:
                //
                // [
                //    {...},
                //    {...}
                // ]
                // =================================================

                if (root.ValueKind ==
                    JsonValueKind.Array)
                {
                    return root.GetArrayLength();
                }

                // =================================================
                // TRƯỜNG HỢP 2:
                //
                // {
                //     "data": [...]
                // }
                // =================================================

                JsonElement data;

                if (root.TryGetProperty(
                    "data",
                    out data))
                {
                    if (data.ValueKind ==
                        JsonValueKind.Array)
                    {
                        return data.GetArrayLength();
                    }

                    // Một số API trả:
                    //
                    // "data": {
                    //     "items": [...]
                    // }
                    if (data.ValueKind ==
                        JsonValueKind.Object)
                    {
                        JsonElement dataItems;

                        if (data.TryGetProperty(
                            "items",
                            out dataItems) &&
                            dataItems.ValueKind ==
                            JsonValueKind.Array)
                        {
                            return dataItems.GetArrayLength();
                        }

                        JsonElement total;

                        if (data.TryGetProperty(
                            "total",
                            out total))
                        {
                            return ReadIntValue(total);
                        }

                        JsonElement totalCount;

                        if (data.TryGetProperty(
                            "totalCount",
                            out totalCount))
                        {
                            return ReadIntValue(
                                totalCount);
                        }
                    }
                }

                // =================================================
                // TRƯỜNG HỢP 3:
                //
                // {
                //     "items": [...]
                // }
                // =================================================

                JsonElement items;

                if (root.TryGetProperty(
                    "items",
                    out items))
                {
                    if (items.ValueKind ==
                        JsonValueKind.Array)
                    {
                        return items.GetArrayLength();
                    }
                }

                // =================================================
                // TRƯỜNG HỢP 4:
                //
                // {
                //     "total": 7
                // }
                // =================================================

                JsonElement totalRoot;

                if (root.TryGetProperty(
                    "total",
                    out totalRoot))
                {
                    return ReadIntValue(
                        totalRoot);
                }

                // =================================================
                // TRƯỜNG HỢP 5:
                //
                // {
                //     "totalCount": 7
                // }
                // =================================================

                JsonElement totalCountRoot;

                if (root.TryGetProperty(
                    "totalCount",
                    out totalCountRoot))
                {
                    return ReadIntValue(
                        totalCountRoot);
                }
            }

            return 0;
        }

        // =========================================================
        // ĐỌC INT VALUE
        // =========================================================

        private int ReadIntValue(
            JsonElement value)
        {
            if (value.ValueKind ==
                JsonValueKind.Number)
            {
                int number;

                if (value.TryGetInt32(
                    out number))
                {
                    return number;
                }

                decimal decimalNumber;

                if (value.TryGetDecimal(
                    out decimalNumber))
                {
                    return (int)decimalNumber;
                }
            }

            if (value.ValueKind ==
                JsonValueKind.String)
            {
                int number;

                if (int.TryParse(
                    value.GetString(),
                    out number))
                {
                    return number;
                }
            }

            return 0;
        }

        // =========================================================
        // ĐỌC DECIMAL
        // =========================================================

        private decimal ReadDecimal(
            JsonElement root,
            string name)
        {
            JsonElement value;

            if (root.TryGetProperty(
                name,
                out value))
            {
                if (value.ValueKind ==
                    JsonValueKind.Number)
                {
                    decimal number;

                    if (value.TryGetDecimal(
                        out number))
                    {
                        return number;
                    }

                    double doubleNumber;

                    if (value.TryGetDouble(
                        out doubleNumber))
                    {
                        return (decimal)doubleNumber;
                    }
                }

                if (value.ValueKind ==
                    JsonValueKind.String)
                {
                    decimal number;

                    if (decimal.TryParse(
                        value.GetString(),
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out number))
                    {
                        return number;
                    }

                    if (decimal.TryParse(
                        value.GetString(),
                        NumberStyles.Any,
                        new CultureInfo("vi-VN"),
                        out number))
                    {
                        return number;
                    }
                }
            }

            return 0;
        }

        // =========================================================
        // ĐỌC INT
        // =========================================================

        private int ReadInt(
            JsonElement root,
            string name)
        {
            JsonElement value;

            if (root.TryGetProperty(
                name,
                out value))
            {
                return ReadIntValue(value);
            }

            return 0;
        }

        // =========================================================
        // LOAD FORM
        // =========================================================

        private async void FormMainShell_Load(
            object sender,
            EventArgs e)
        {
            string role =
                SessionManager.CurrentRole ?? "";

            lblUserInfo.Text =
                "Nhân viên: "
                + (SessionManager.CurrentUsername ?? "")
                + "   |   Vai trò: "
                + role;

            ApplyRolePermissions(role);

            if (role.Equals(
                "ADMIN",
                StringComparison.OrdinalIgnoreCase))
            {
                ShowDashboard();

                await LoadDashboardDataAsync();
            }
            else
            {
                OpenDefaultScreenByRole(role);
            }
        }

        // =========================================================
        // PHÂN QUYỀN
        // =========================================================

        private void ApplyRolePermissions(
            string role)
        {
            string currentRole =
                (role ?? "").ToUpperInvariant();

            btnDashboard.Visible = false;
            btnPOS.Visible = false;
            btnCategory.Visible = false;
            btnProduct.Visible = false;
            btnCustomer.Visible = false;
            btnInventory.Visible = false;
            btnReports.Visible = false;
            btnSettings.Visible = false;
            btnUserManage.Visible = false;

            switch (currentRole)
            {
                case "ADMIN":

                    btnDashboard.Visible = true;
                    btnPOS.Visible = true;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    btnCustomer.Visible = true;
                    btnInventory.Visible = true;
                    btnReports.Visible = true;
                    btnSettings.Visible = true;
                    btnUserManage.Visible = true;

                    break;

                case "CASHIER":

                    btnPOS.Visible = true;
                    btnCustomer.Visible = true;

                    break;

                case "WAREHOUSE":

                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    btnInventory.Visible = true;

                    break;

                default:

                    MessageBox.Show(
                        "Tài khoản chưa được cấp quyền hợp lệ!",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    Close();

                    break;
            }
        }

        // =========================================================
        // HIỂN THỊ DASHBOARD
        // =========================================================

        private void ShowDashboard()
        {
            if (_activeForm != null)
            {
                _activeForm.Close();
                _activeForm.Dispose();
                _activeForm = null;
            }

            panelMainContent.Controls.Clear();

            panelMainContent.Controls.Add(
                panelDashboard);

            panelDashboard.Visible = true;

            panelDashboard.Dock =
                DockStyle.Fill;

            panelDashboard.BringToFront();

            lblTitle.Text =
                "DASHBOARD - TỔNG QUAN";

            HighlightActiveButton(
                btnDashboard);
        }

        // =========================================================
        // MỞ FORM CON
        // =========================================================

        private void OpenChildForm(
            Form childForm,
            string screenTitle,
            Button senderButton)
        {
            if (childForm == null)
                return;

            if (_activeForm != null)
            {
                _activeForm.Close();
                _activeForm.Dispose();
                _activeForm = null;
            }

            panelMainContent.Controls.Clear();

            if (panelDashboard != null)
            {
                panelDashboard.Visible = false;
            }

            _activeForm =
                childForm;

            childForm.TopLevel =
                false;

            childForm.FormBorderStyle =
                FormBorderStyle.None;

            childForm.Dock =
                DockStyle.Fill;

            panelMainContent.Controls.Add(
                childForm);

            panelMainContent.Tag =
                childForm;

            lblTitle.Text =
                screenTitle;

            HighlightActiveButton(
                senderButton);

            childForm.Show();

            childForm.BringToFront();
        }

        // =========================================================
        // HIGHLIGHT MENU
        // =========================================================

        private void HighlightActiveButton(
            Button activeButton)
        {
            Button[] buttons =
            {
                btnDashboard,
                btnPOS,
                btnCategory,
                btnProduct,
                btnCustomer,
                btnInventory,
                btnReports,
                btnSettings,
                btnUserManage
            };

            foreach (Button button in buttons)
            {
                if (button == null)
                    continue;

                button.BackColor =
                    sidebarColor;

                button.ForeColor =
                    Color.FromArgb(
                        222,
                        229,
                        240);

                button.Font =
                    new Font(
                        "Segoe UI",
                        9.5F,
                        FontStyle.Regular);
            }

            if (activeButton != null)
            {
                activeButton.BackColor =
                    activeColor;

                activeButton.ForeColor =
                    Color.White;

                activeButton.Font =
                    new Font(
                        "Segoe UI",
                        9.5F,
                        FontStyle.Bold);
            }
        }

        // =========================================================
        // FORM MẶC ĐỊNH THEO ROLE
        // =========================================================

        private void OpenDefaultScreenByRole(
            string role)
        {
            switch (
                (role ?? "").ToUpperInvariant())
            {
                case "ADMIN":

                    ShowDashboard();

                    break;

                case "WAREHOUSE":

                    OpenChildForm(
                        new FormInventoryManagement(),
                        "KHO VÀ TỒN KHO",
                        btnInventory);

                    break;

                case "CASHIER":

                    OpenChildForm(
                        new FormPOS(),
                        "BÁN HÀNG - POS",
                        btnPOS);

                    break;
            }
        }

        // =========================================================
        // DASHBOARD BUTTON
        // =========================================================

        private async void btnDashboard_Click(
            object sender,
            EventArgs e)
        {
            ShowDashboard();

            await LoadDashboardDataAsync();
        }

        // =========================================================
        // POS
        // =========================================================

        private void btnPOS_Click(
            object sender,
            EventArgs e)
        {
            OpenChildForm(
                new FormPOS(),
                "BÁN HÀNG - POS",
                btnPOS);
        }

        // =========================================================
        // CATEGORY
        // =========================================================

        private void btnCategory_Click(
            object sender,
            EventArgs e)
        {
            OpenChildForm(
                new FormCategoryManagement(),
                "QUẢN LÝ DANH MỤC NHÓM HÀNG",
                btnCategory);
        }

        // =========================================================
        // PRODUCT
        // =========================================================

        private void btnProduct_Click(
            object sender,
            EventArgs e)
        {
            OpenChildForm(
                new FormProductManagement(),
                "QUẢN LÝ SẢN PHẨM VÀ KHO HÀNG",
                btnProduct);
        }

        // =========================================================
        // CUSTOMER
        // =========================================================

        private void btnCustomer_Click(
            object sender,
            EventArgs e)
        {
            OpenChildForm(
                new FormCustomerManagement(),
                "QUẢN LÝ KHÁCH HÀNG",
                btnCustomer);
        }

        // =========================================================
        // INVENTORY
        // =========================================================

        private void btnInventory_Click(
            object sender,
            EventArgs e)
        {
            OpenChildForm(
                new FormInventoryManagement(),
                "KHO VÀ TỒN KHO",
                btnInventory);
        }

        // =========================================================
        // SETTINGS
        // =========================================================

        private void btnSettings_Click(
            object sender,
            EventArgs e)
        {
            if (!string.Equals(
                SessionManager.CurrentRole,
                "ADMIN",
                StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Bạn không có quyền truy cập cài đặt hệ thống.",
                    "Từ chối truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);

                return;
            }

            OpenChildForm(
                new FormSettings(),
                "CÀI ĐẶT HỆ THỐNG",
                btnSettings);
        }

        // =========================================================
        // REPORTS
        // =========================================================

        private void btnReports_Click(
            object sender,
            EventArgs e)
        {
            if (!string.Equals(
                SessionManager.CurrentRole,
                "ADMIN",
                StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Bạn không có quyền xem báo cáo doanh thu!",
                    "Từ chối truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);

                return;
            }

            OpenChildForm(
                new FormQuickReport(),
                "BÁO CÁO DOANH THU",
                btnReports);
        }

        // =========================================================
        // USER MANAGEMENT
        // =========================================================

        private void btnUserManage_Click(
            object sender,
            EventArgs e)
        {
            if (!string.Equals(
                SessionManager.CurrentRole,
                "ADMIN",
                StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Bạn không có quyền quản trị tài khoản!",
                    "Từ chối truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);

                return;
            }

            OpenChildForm(
                new FormUserManagement(),
                "QUẢN TRỊ TÀI KHOẢN",
                btnUserManage);
        }

        // =========================================================
        // LOGOUT
        // =========================================================

        private void btnLogout_Click(
            object sender,
            EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "Bạn có chắc chắn muốn đăng xuất?",
                    "Xác nhận đăng xuất",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result ==
                DialogResult.Yes)
            {
                SessionManager.ClearSession();

                Close();
            }
        }

        // =========================================================
        // LOGO
        // =========================================================

        private void lblLogo_Click(
            object sender,
            EventArgs e)
        {
            string role =
                SessionManager.CurrentRole ?? "";

            if (role.Equals(
                "ADMIN",
                StringComparison.OrdinalIgnoreCase))
            {
                ShowDashboard();
            }
        }
    }
}