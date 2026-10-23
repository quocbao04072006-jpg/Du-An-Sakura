namespace MiniSupermarket.WinForms
{
    partial class FormMainShell
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panelUserFooter = new System.Windows.Forms.Panel();

            this.btnLogout = new System.Windows.Forms.Button();

            this.btnUserManage = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnProduct = new System.Windows.Forms.Button();
            this.btnCategory = new System.Windows.Forms.Button();
            this.btnPOS = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();

            this.lblLogo = new System.Windows.Forms.Label();

            this.panelTopHeader = new System.Windows.Forms.Panel();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();

            this.panelMainContent = new System.Windows.Forms.Panel();

            this.panelSidebar.SuspendLayout();
            this.panelUserFooter.SuspendLayout();
            this.panelTopHeader.SuspendLayout();
            this.SuspendLayout();

            // =====================================================
            // panelSidebar
            // =====================================================

            this.panelSidebar.BackColor =
                System.Drawing.Color.FromArgb(
                    24,
                    30,
                    48);

            this.panelSidebar.Controls.Add(
                this.panelUserFooter);

            this.panelSidebar.Controls.Add(
                this.btnUserManage);

            this.panelSidebar.Controls.Add(
                this.btnSettings);

            this.panelSidebar.Controls.Add(
                this.btnReports);

            this.panelSidebar.Controls.Add(
                this.btnInventory);

            this.panelSidebar.Controls.Add(
                this.btnCustomer);

            this.panelSidebar.Controls.Add(
                this.btnProduct);

            this.panelSidebar.Controls.Add(
                this.btnCategory);

            this.panelSidebar.Controls.Add(
                this.btnPOS);

            this.panelSidebar.Controls.Add(
                this.btnDashboard);

            this.panelSidebar.Controls.Add(
                this.lblLogo);

            this.panelSidebar.Dock =
                System.Windows.Forms.DockStyle.Left;

            this.panelSidebar.Location =
                new System.Drawing.Point(0, 0);

            this.panelSidebar.Name =
                "panelSidebar";

            this.panelSidebar.Size =
                new System.Drawing.Size(190, 700);

            this.panelSidebar.TabIndex = 0;

            // =====================================================
            // panelUserFooter
            // =====================================================

            this.panelUserFooter.Controls.Add(
                this.btnLogout);

            this.panelUserFooter.Dock =
                System.Windows.Forms.DockStyle.Bottom;

            this.panelUserFooter.Location =
                new System.Drawing.Point(0, 650);

            this.panelUserFooter.Name =
                "panelUserFooter";

            this.panelUserFooter.Size =
                new System.Drawing.Size(190, 50);

            this.panelUserFooter.TabIndex = 10;

            // =====================================================
            // btnLogout
            // =====================================================

            this.btnLogout.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.btnLogout.FlatAppearance.BorderSize = 0;

            this.btnLogout.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnLogout.ForeColor =
                System.Drawing.Color.White;

            this.btnLogout.Location =
                new System.Drawing.Point(0, 0);

            this.btnLogout.Name =
                "btnLogout";

            this.btnLogout.Size =
                new System.Drawing.Size(190, 50);

            this.btnLogout.TabIndex = 0;

            this.btnLogout.Text =
                "🚪  Đăng xuất";

            this.btnLogout.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            this.btnLogout.UseVisualStyleBackColor = false;

            this.btnLogout.Click +=
                new System.EventHandler(
                    this.btnLogout_Click);

            // =====================================================
            // btnUserManage
            // =====================================================

            this.btnUserManage.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.btnUserManage.FlatAppearance.BorderSize = 0;

            this.btnUserManage.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnUserManage.ForeColor =
                System.Drawing.Color.White;

            this.btnUserManage.Location =
                new System.Drawing.Point(0, 407);

            this.btnUserManage.Name =
                "btnUserManage";

            this.btnUserManage.Size =
                new System.Drawing.Size(190, 42);

            this.btnUserManage.TabIndex = 9;

            this.btnUserManage.Text =
                "🛡  Quản trị tài khoản";

            this.btnUserManage.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            this.btnUserManage.UseVisualStyleBackColor = false;

            this.btnUserManage.Click +=
                new System.EventHandler(
                    this.btnUserManage_Click);

            // =====================================================
            // btnSettings
            // =====================================================

            this.btnSettings.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.btnSettings.FlatAppearance.BorderSize = 0;

            this.btnSettings.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnSettings.ForeColor =
                System.Drawing.Color.White;

            this.btnSettings.Location =
                new System.Drawing.Point(0, 365);

            this.btnSettings.Name =
                "btnSettings";

            this.btnSettings.Size =
                new System.Drawing.Size(190, 42);

            this.btnSettings.TabIndex = 8;

            this.btnSettings.Text =
                "⚙  Cài đặt hệ thống";

            this.btnSettings.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            this.btnSettings.UseVisualStyleBackColor = false;

            this.btnSettings.Click +=
                new System.EventHandler(
                    this.btnSettings_Click);

            // =====================================================
            // btnReports
            // =====================================================

            this.btnReports.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.btnReports.FlatAppearance.BorderSize = 0;

            this.btnReports.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnReports.ForeColor =
                System.Drawing.Color.White;

            this.btnReports.Location =
                new System.Drawing.Point(0, 323);

            this.btnReports.Name =
                "btnReports";

            this.btnReports.Size =
                new System.Drawing.Size(190, 42);

            this.btnReports.TabIndex = 7;

            this.btnReports.Text =
                "📊  Báo cáo doanh thu";

            this.btnReports.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            this.btnReports.UseVisualStyleBackColor = false;

            this.btnReports.Click +=
                new System.EventHandler(
                    this.btnReports_Click);

            // =====================================================
            // btnInventory
            // =====================================================

            this.btnInventory.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.btnInventory.FlatAppearance.BorderSize = 0;

            this.btnInventory.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnInventory.ForeColor =
                System.Drawing.Color.White;

            this.btnInventory.Location =
                new System.Drawing.Point(0, 281);

            this.btnInventory.Name =
                "btnInventory";

            this.btnInventory.Size =
                new System.Drawing.Size(190, 42);

            this.btnInventory.TabIndex = 6;

            this.btnInventory.Text =
                "▦  Kho & tồn kho";

            this.btnInventory.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            this.btnInventory.UseVisualStyleBackColor = false;

            this.btnInventory.Click +=
                new System.EventHandler(
                    this.btnInventory_Click);

            // =====================================================
            // btnCustomer
            // =====================================================

            this.btnCustomer.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.btnCustomer.FlatAppearance.BorderSize = 0;

            this.btnCustomer.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnCustomer.ForeColor =
                System.Drawing.Color.White;

            this.btnCustomer.Location =
                new System.Drawing.Point(0, 239);

            this.btnCustomer.Name =
                "btnCustomer";

            this.btnCustomer.Size =
                new System.Drawing.Size(190, 42);

            this.btnCustomer.TabIndex = 5;

            this.btnCustomer.Text =
                "👥  Quản lý khách hàng";

            this.btnCustomer.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            this.btnCustomer.UseVisualStyleBackColor = false;

            this.btnCustomer.Click +=
                new System.EventHandler(
                    this.btnCustomer_Click);

            // =====================================================
            // btnProduct
            // =====================================================

            this.btnProduct.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.btnProduct.FlatAppearance.BorderSize = 0;

            this.btnProduct.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnProduct.ForeColor =
                System.Drawing.Color.White;

            this.btnProduct.Location =
                new System.Drawing.Point(0, 197);

            this.btnProduct.Name =
                "btnProduct";

            this.btnProduct.Size =
                new System.Drawing.Size(190, 42);

            this.btnProduct.TabIndex = 4;

            this.btnProduct.Text =
                "📦  Quản lý sản phẩm";

            this.btnProduct.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            this.btnProduct.UseVisualStyleBackColor = false;

            this.btnProduct.Click +=
                new System.EventHandler(
                    this.btnProduct_Click);

            // =====================================================
            // btnCategory
            // =====================================================

            this.btnCategory.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.btnCategory.FlatAppearance.BorderSize = 0;

            this.btnCategory.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnCategory.ForeColor =
                System.Drawing.Color.White;

            this.btnCategory.Location =
                new System.Drawing.Point(0, 155);

            this.btnCategory.Name =
                "btnCategory";

            this.btnCategory.Size =
                new System.Drawing.Size(190, 42);

            this.btnCategory.TabIndex = 3;

            this.btnCategory.Text =
                "📁  Quản lý danh mục";

            this.btnCategory.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            this.btnCategory.UseVisualStyleBackColor = false;

            this.btnCategory.Click +=
                new System.EventHandler(
                    this.btnCategory_Click);

            // =====================================================
            // btnPOS
            // =====================================================

            this.btnPOS.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.btnPOS.FlatAppearance.BorderSize = 0;

            this.btnPOS.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnPOS.ForeColor =
                System.Drawing.Color.White;

            this.btnPOS.Location =
                new System.Drawing.Point(0, 113);

            this.btnPOS.Name =
                "btnPOS";

            this.btnPOS.Size =
                new System.Drawing.Size(190, 42);

            this.btnPOS.TabIndex = 2;

            this.btnPOS.Text =
                "🛒  Bán hàng (POS)";

            this.btnPOS.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            this.btnPOS.UseVisualStyleBackColor = false;

            this.btnPOS.Click +=
                new System.EventHandler(
                    this.btnPOS_Click);

            // =====================================================
            // btnDashboard
            // =====================================================

            this.btnDashboard.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.btnDashboard.FlatAppearance.BorderSize = 0;

            this.btnDashboard.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnDashboard.ForeColor =
                System.Drawing.Color.White;

            this.btnDashboard.Location =
                new System.Drawing.Point(0, 71);

            this.btnDashboard.Name =
                "btnDashboard";

            this.btnDashboard.Size =
                new System.Drawing.Size(190, 42);

            this.btnDashboard.TabIndex = 1;

            this.btnDashboard.Text =
                "▣  Dashboard";

            this.btnDashboard.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            this.btnDashboard.UseVisualStyleBackColor = false;

            this.btnDashboard.Click +=
                new System.EventHandler(
                    this.btnDashboard_Click);

            // =====================================================
            // lblLogo
            // =====================================================

            this.lblLogo.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.lblLogo.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    15F,
                    System.Drawing.FontStyle.Bold);

            this.lblLogo.ForeColor =
                System.Drawing.Color.White;

            this.lblLogo.Location =
                new System.Drawing.Point(0, 0);

            this.lblLogo.Name =
                "lblLogo";

            this.lblLogo.Size =
                new System.Drawing.Size(190, 71);

            this.lblLogo.TabIndex = 0;

            this.lblLogo.Text =
                "MINIMART\r\nSAKURA";

            this.lblLogo.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            this.lblLogo.Click +=
                new System.EventHandler(
                    this.lblLogo_Click);

            // =====================================================
            // panelTopHeader
            // =====================================================

            this.panelTopHeader.BackColor =
                System.Drawing.Color.White;

            this.panelTopHeader.Controls.Add(
                this.lblUserInfo);

            this.panelTopHeader.Controls.Add(
                this.lblTitle);

            this.panelTopHeader.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.panelTopHeader.Location =
                new System.Drawing.Point(190, 0);

            this.panelTopHeader.Name =
                "panelTopHeader";

            this.panelTopHeader.Size =
                new System.Drawing.Size(1090, 58);

            this.panelTopHeader.TabIndex = 1;

            // =====================================================
            // lblUserInfo
            // =====================================================

            this.lblUserInfo.Anchor =
                (
                    System.Windows.Forms.AnchorStyles.Top |
                    System.Windows.Forms.AnchorStyles.Right
                );

            this.lblUserInfo.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F);

            this.lblUserInfo.Location =
                new System.Drawing.Point(
                    700,
                    17);

            this.lblUserInfo.Name =
                "lblUserInfo";

            this.lblUserInfo.Size =
                new System.Drawing.Size(
                    370,
                    25);

            this.lblUserInfo.TabIndex = 1;

            this.lblUserInfo.Text =
                "Nhân viên: ... | Vai trò: ...";

            this.lblUserInfo.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            // =====================================================
            // lblTitle
            // =====================================================

            this.lblTitle.AutoSize = true;

            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    14F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitle.Location =
                new System.Drawing.Point(
                    18,
                    16);

            this.lblTitle.Name =
                "lblTitle";

            this.lblTitle.Size =
                new System.Drawing.Size(
                    300,
                    25);

            this.lblTitle.TabIndex = 0;

            this.lblTitle.Text =
                "DASHBOARD - TỔNG QUAN";

            // =====================================================
            // panelMainContent
            // =====================================================

            this.panelMainContent.BackColor =
                System.Drawing.Color.FromArgb(
                    244,
                    247,
                    251);

            this.panelMainContent.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.panelMainContent.Location =
                new System.Drawing.Point(
                    190,
                    58);

            this.panelMainContent.Name =
                "panelMainContent";

            this.panelMainContent.Size =
                new System.Drawing.Size(
                    1090,
                    642);

            this.panelMainContent.TabIndex = 2;

            // =====================================================
            // FormMainShell
            // =====================================================

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(
                    7F,
                    15F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize =
                new System.Drawing.Size(
                    1280,
                    700);

            this.Controls.Add(
                this.panelMainContent);

            this.Controls.Add(
                this.panelTopHeader);

            this.Controls.Add(
                this.panelSidebar);

            this.Name =
                "FormMainShell";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text =
                "MiniMart | Quản lý bán hàng & tồn kho";

            this.Load +=
                new System.EventHandler(
                    this.FormMainShell_Load);

            this.panelSidebar.ResumeLayout(false);

            this.panelUserFooter.ResumeLayout(false);

            this.panelTopHeader.ResumeLayout(false);

            this.panelTopHeader.PerformLayout();

            this.ResumeLayout(false);
        }

        #endregion

        // =========================================================
        // SIDEBAR
        // =========================================================

        private System.Windows.Forms.Panel panelSidebar;

        private System.Windows.Forms.Label lblLogo;

        private System.Windows.Forms.Button btnDashboard;

        private System.Windows.Forms.Button btnPOS;

        private System.Windows.Forms.Button btnCategory;

        private System.Windows.Forms.Button btnProduct;

        private System.Windows.Forms.Button btnCustomer;

        private System.Windows.Forms.Button btnInventory;

        private System.Windows.Forms.Button btnReports;

        private System.Windows.Forms.Button btnSettings;

        private System.Windows.Forms.Button btnUserManage;

        private System.Windows.Forms.Panel panelUserFooter;

        private System.Windows.Forms.Button btnLogout;

        // =========================================================
        // HEADER
        // =========================================================

        private System.Windows.Forms.Panel panelTopHeader;

        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblUserInfo;

        // =========================================================
        // CONTENT
        // =========================================================

        private System.Windows.Forms.Panel panelMainContent;
    }
}