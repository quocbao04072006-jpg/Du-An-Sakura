namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
    {
        private System.ComponentModel.IContainer components = null;

        // =========================================================
        // KHAI BÁO CONTROL
        // =========================================================

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;

        private System.Windows.Forms.Panel panelMain;

        private System.Windows.Forms.Panel panelList;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.Label lblListDescription;
        private System.Windows.Forms.DataGridView dgvUsers;

        private System.Windows.Forms.Panel panelSelected;
        private System.Windows.Forms.Label lblSelectedTitle;
        private System.Windows.Forms.Label lblSelectedUser;
        private System.Windows.Forms.Label lblSelectedName;
        private System.Windows.Forms.Label lblSelectedRole;
        private System.Windows.Forms.Label lblSelectedStatus;

        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.Label lblAddTitle;
        private System.Windows.Forms.Label lblAddDescription;

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;

        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;

        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;

        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cboRole;

        private System.Windows.Forms.Button btnAddUser;

        // =========================================================
        // DISPOSE
        // =========================================================

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // =========================================================
        // INITIALIZE COMPONENT
        // =========================================================

        private void InitializeComponent()
        {
            this.components =
                new System.ComponentModel.Container();

            this.panelHeader =
                new System.Windows.Forms.Panel();

            this.lblTitle =
                new System.Windows.Forms.Label();

            this.lblSubTitle =
                new System.Windows.Forms.Label();

            this.panelMain =
                new System.Windows.Forms.Panel();

            this.panelList =
                new System.Windows.Forms.Panel();

            this.lblListTitle =
                new System.Windows.Forms.Label();

            this.lblListDescription =
                new System.Windows.Forms.Label();

            this.dgvUsers =
                new System.Windows.Forms.DataGridView();

            this.panelSelected =
                new System.Windows.Forms.Panel();

            this.lblSelectedTitle =
                new System.Windows.Forms.Label();

            this.lblSelectedUser =
                new System.Windows.Forms.Label();

            this.lblSelectedName =
                new System.Windows.Forms.Label();

            this.lblSelectedRole =
                new System.Windows.Forms.Label();

            this.lblSelectedStatus =
                new System.Windows.Forms.Label();

            this.panelAdd =
                new System.Windows.Forms.Panel();

            this.lblAddTitle =
                new System.Windows.Forms.Label();

            this.lblAddDescription =
                new System.Windows.Forms.Label();

            this.lblUsername =
                new System.Windows.Forms.Label();

            this.txtUsername =
                new System.Windows.Forms.TextBox();

            this.lblPassword =
                new System.Windows.Forms.Label();

            this.txtPassword =
                new System.Windows.Forms.TextBox();

            this.lblFullName =
                new System.Windows.Forms.Label();

            this.txtFullName =
                new System.Windows.Forms.TextBox();

            this.lblRole =
                new System.Windows.Forms.Label();

            this.cboRole =
                new System.Windows.Forms.ComboBox();

            this.btnAddUser =
                new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvUsers)).BeginInit();

            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelList.SuspendLayout();
            this.panelSelected.SuspendLayout();
            this.panelAdd.SuspendLayout();

            this.SuspendLayout();

            // =====================================================
            // FORM
            // =====================================================

            this.AutoScaleDimensions =
                new System.Drawing.SizeF(8F, 16F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.BackColor =
                System.Drawing.Color.FromArgb(
                    245,
                    247,
                    250);

            this.ClientSize =
                new System.Drawing.Size(
                    1180,
                    720);

            this.MinimumSize =
                new System.Drawing.Size(
                    1050,
                    650);

            this.Name =
                "FormUserManagement";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text =
                "Quản lý tài khoản người dùng";

            // =====================================================
            // HEADER
            // =====================================================

            this.panelHeader.BackColor =
                System.Drawing.Color.FromArgb(
                    31,
                    41,
                    55);

            this.panelHeader.Dock =
                System.Windows.Forms.DockStyle.Top;

            this.panelHeader.Height = 85;

            this.panelHeader.Location =
                new System.Drawing.Point(
                    0,
                    0);

            this.panelHeader.Name =
                "panelHeader";

            this.panelHeader.TabIndex = 0;

            // =====================================================
            // HEADER TITLE
            // =====================================================

            this.lblTitle.AutoSize = true;

            this.lblTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    19F,
                    System.Drawing.FontStyle.Bold);

            this.lblTitle.ForeColor =
                System.Drawing.Color.White;

            this.lblTitle.Location =
                new System.Drawing.Point(
                    28,
                    13);

            this.lblTitle.Name =
                "lblTitle";

            this.lblTitle.Text =
                "QUẢN LÝ TÀI KHOẢN";

            // =====================================================
            // HEADER SUBTITLE
            // =====================================================

            this.lblSubTitle.AutoSize = true;

            this.lblSubTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F);

            this.lblSubTitle.ForeColor =
                System.Drawing.Color.FromArgb(
                    203,
                    213,
                    225);

            this.lblSubTitle.Location =
                new System.Drawing.Point(
                    31,
                    54);

            this.lblSubTitle.Name =
                "lblSubTitle";

            this.lblSubTitle.Text =
                "Quản lý người dùng, vai trò và trạng thái hoạt động";

            // Thêm label vào Header
            this.panelHeader.Controls.Add(
                this.lblTitle);

            this.panelHeader.Controls.Add(
                this.lblSubTitle);

            // =====================================================
            // MAIN
            // =====================================================

            this.panelMain.Dock =
                System.Windows.Forms.DockStyle.Fill;

            this.panelMain.Location =
                new System.Drawing.Point(
                    0,
                    85);

            this.panelMain.Name =
                "panelMain";

            this.panelMain.Padding =
                new System.Windows.Forms.Padding(
                    18);

            this.panelMain.Size =
                new System.Drawing.Size(
                    1180,
                    635);

            this.panelMain.TabIndex = 1;

            // =====================================================
            // PANEL LIST
            // =====================================================

            this.panelList.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left;

            this.panelList.BackColor =
                System.Drawing.Color.White;

            this.panelList.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.panelList.Location =
                new System.Drawing.Point(
                    18,
                    18);

            this.panelList.Name =
                "panelList";

            this.panelList.Size =
                new System.Drawing.Size(
                    755,
                    420);

            this.panelList.TabIndex = 0;

            // =====================================================
            // LIST TITLE
            // =====================================================

            this.lblListTitle.AutoSize = true;

            this.lblListTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    13F,
                    System.Drawing.FontStyle.Bold);

            this.lblListTitle.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    41,
                    55);

            this.lblListTitle.Location =
                new System.Drawing.Point(
                    18,
                    14);

            this.lblListTitle.Name =
                "lblListTitle";

            this.lblListTitle.Text =
                "Danh sách người dùng";

            // =====================================================
            // LIST DESCRIPTION
            // =====================================================

            this.lblListDescription.AutoSize = true;

            this.lblListDescription.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F);

            this.lblListDescription.ForeColor =
                System.Drawing.Color.Gray;

            this.lblListDescription.Location =
                new System.Drawing.Point(
                    20,
                    43);

            this.lblListDescription.Name =
                "lblListDescription";

            this.lblListDescription.Text =
                "Tick trạng thái để khóa hoặc mở tài khoản.";

            // =====================================================
            // DATAGRIDVIEW
            // =====================================================

            this.dgvUsers.AllowUserToAddRows =
                false;

            this.dgvUsers.AllowUserToDeleteRows =
                false;

            this.dgvUsers.AllowUserToResizeRows =
                false;

            this.dgvUsers.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvUsers.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvUsers.BorderStyle =
                System.Windows.Forms.BorderStyle.None;

            this.dgvUsers.CellBorderStyle =
                System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;

            this.dgvUsers.ColumnHeadersBorderStyle =
                System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            this.dgvUsers.ColumnHeadersHeight = 38;

            this.dgvUsers.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            this.dgvUsers.ColumnHeadersDefaultCellStyle =
                new System.Windows.Forms.DataGridViewCellStyle
                {
                    Alignment =
                        System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,

                    BackColor =
                        System.Drawing.Color.FromArgb(
                            241,
                            245,
                            249),

                    Font =
                        new System.Drawing.Font(
                            "Segoe UI",
                            9.5F,
                            System.Drawing.FontStyle.Bold),

                    ForeColor =
                        System.Drawing.Color.FromArgb(
                            31,
                            41,
                            55),

                    SelectionBackColor =
                        System.Drawing.Color.FromArgb(
                            241,
                            245,
                            249),

                    SelectionForeColor =
                        System.Drawing.Color.FromArgb(
                            31,
                            41,
                            55)
                };

            this.dgvUsers.DefaultCellStyle =
                new System.Windows.Forms.DataGridViewCellStyle
                {
                    Alignment =
                        System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft,

                    BackColor =
                        System.Drawing.Color.White,

                    Font =
                        new System.Drawing.Font(
                            "Segoe UI",
                            9.5F),

                    ForeColor =
                        System.Drawing.Color.FromArgb(
                            55,
                            65,
                            81),

                    SelectionBackColor =
                        System.Drawing.Color.FromArgb(
                            219,
                            234,
                            254),

                    SelectionForeColor =
                        System.Drawing.Color.FromArgb(
                            30,
                            41,
                            59),

                    Padding =
                        new System.Windows.Forms.Padding(
                            5)
                };

            this.dgvUsers.EnableHeadersVisualStyles =
                false;

            this.dgvUsers.GridColor =
                System.Drawing.Color.FromArgb(
                    226,
                    232,
                    240);

            this.dgvUsers.Location =
                new System.Drawing.Point(
                    18,
                    72);

            this.dgvUsers.MultiSelect =
                false;

            this.dgvUsers.Name =
                "dgvUsers";

            this.dgvUsers.ReadOnly = false;

            this.dgvUsers.RowHeadersVisible =
                false;

            this.dgvUsers.RowTemplate.Height = 38;

            this.dgvUsers.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvUsers.Size =
                new System.Drawing.Size(
                    717,
                    328);

            this.dgvUsers.TabIndex = 2;

            // Thêm control vào Panel List
            this.panelList.Controls.Add(
                this.dgvUsers);

            this.panelList.Controls.Add(
                this.lblListDescription);

            this.panelList.Controls.Add(
                this.lblListTitle);

            // =====================================================
            // PANEL SELECTED
            // =====================================================

            this.panelSelected.BackColor =
                System.Drawing.Color.White;

            this.panelSelected.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.panelSelected.Location =
                new System.Drawing.Point(
                    18,
                    453);

            this.panelSelected.Name =
                "panelSelected";

            this.panelSelected.Size =
                new System.Drawing.Size(
                    755,
                    145);

            this.panelSelected.TabIndex = 1;

            // =====================================================
            // SELECTED TITLE
            // =====================================================

            this.lblSelectedTitle.AutoSize = true;

            this.lblSelectedTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    11F,
                    System.Drawing.FontStyle.Bold);

            this.lblSelectedTitle.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    41,
                    55);

            this.lblSelectedTitle.Location =
                new System.Drawing.Point(
                    18,
                    13);

            this.lblSelectedTitle.Text =
                "Thông tin tài khoản đang chọn";

            // =====================================================
            // SELECTED USER
            // =====================================================

            this.lblSelectedUser.AutoSize = true;

            this.lblSelectedUser.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblSelectedUser.Location =
                new System.Drawing.Point(
                    20,
                    48);

            this.lblSelectedUser.Text =
                "Tài khoản: -";

            // =====================================================
            // SELECTED NAME
            // =====================================================

            this.lblSelectedName.AutoSize = true;

            this.lblSelectedName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.lblSelectedName.Location =
                new System.Drawing.Point(
                    270,
                    48);

            this.lblSelectedName.Text =
                "Họ tên: -";

            // =====================================================
            // SELECTED ROLE
            // =====================================================

            this.lblSelectedRole.AutoSize = true;

            this.lblSelectedRole.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.lblSelectedRole.Location =
                new System.Drawing.Point(
                    20,
                    88);

            this.lblSelectedRole.Text =
                "Vai trò: -";

            // =====================================================
            // SELECTED STATUS
            // =====================================================

            this.lblSelectedStatus.AutoSize = true;

            this.lblSelectedStatus.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            this.lblSelectedStatus.ForeColor =
                System.Drawing.Color.FromArgb(
                    22,
                    163,
                    74);

            this.lblSelectedStatus.Location =
                new System.Drawing.Point(
                    270,
                    88);

            this.lblSelectedStatus.Text =
                "Trạng thái: -";

            // Thêm controls
            this.panelSelected.Controls.Add(
                this.lblSelectedTitle);

            this.panelSelected.Controls.Add(
                this.lblSelectedUser);

            this.panelSelected.Controls.Add(
                this.lblSelectedName);

            this.panelSelected.Controls.Add(
                this.lblSelectedRole);

            this.panelSelected.Controls.Add(
                this.lblSelectedStatus);

            // =====================================================
            // PANEL ADD USER
            // =====================================================

            this.panelAdd.BackColor =
                System.Drawing.Color.White;

            this.panelAdd.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.panelAdd.Location =
                new System.Drawing.Point(
                    793,
                    18);

            this.panelAdd.Name =
                "panelAdd";

            this.panelAdd.Size =
                new System.Drawing.Size(
                    350,
                    580);

            this.panelAdd.TabIndex = 2;

            // =====================================================
            // ADD TITLE
            // =====================================================

            this.lblAddTitle.AutoSize = true;

            this.lblAddTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    14F,
                    System.Drawing.FontStyle.Bold);

            this.lblAddTitle.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    41,
                    55);

            this.lblAddTitle.Location =
                new System.Drawing.Point(
                    22,
                    18);

            this.lblAddTitle.Text =
                "Tạo tài khoản mới";

            // =====================================================
            // ADD DESCRIPTION
            // =====================================================

            this.lblAddDescription.AutoSize = false;

            this.lblAddDescription.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F);

            this.lblAddDescription.ForeColor =
                System.Drawing.Color.Gray;

            this.lblAddDescription.Location =
                new System.Drawing.Point(
                    24,
                    51);

            this.lblAddDescription.Size =
                new System.Drawing.Size(
                    300,
                    40);

            this.lblAddDescription.Text =
                "Nhập thông tin bên dưới để thêm "
                + "người dùng vào hệ thống.";

            // =====================================================
            // USERNAME LABEL
            // =====================================================

            this.lblUsername.AutoSize = true;

            this.lblUsername.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            this.lblUsername.Location =
                new System.Drawing.Point(
                    24,
                    105);

            this.lblUsername.Text =
                "Tên đăng nhập";

            // =====================================================
            // USERNAME TEXTBOX
            // =====================================================

            this.txtUsername.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.txtUsername.Location =
                new System.Drawing.Point(
                    24,
                    130);

            this.txtUsername.Name =
                "txtUsername";

            this.txtUsername.Size =
                new System.Drawing.Size(
                    300,
                    30);

            this.txtUsername.TabIndex = 1;

            // =====================================================
            // PASSWORD LABEL
            // =====================================================

            this.lblPassword.AutoSize = true;

            this.lblPassword.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            this.lblPassword.Location =
                new System.Drawing.Point(
                    24,
                    176);

            this.lblPassword.Text =
                "Mật khẩu";

            // =====================================================
            // PASSWORD TEXTBOX
            // =====================================================

            this.txtPassword.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.txtPassword.Location =
                new System.Drawing.Point(
                    24,
                    201);

            this.txtPassword.Name =
                "txtPassword";

            this.txtPassword.PasswordChar =
                '*';

            this.txtPassword.Size =
                new System.Drawing.Size(
                    300,
                    30);

            this.txtPassword.TabIndex = 2;

            // =====================================================
            // FULL NAME LABEL
            // =====================================================

            this.lblFullName.AutoSize = true;

            this.lblFullName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            this.lblFullName.Location =
                new System.Drawing.Point(
                    24,
                    247);

            this.lblFullName.Text =
                "Họ và tên";

            // =====================================================
            // FULL NAME TEXTBOX
            // =====================================================

            this.txtFullName.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.txtFullName.Location =
                new System.Drawing.Point(
                    24,
                    272);

            this.txtFullName.Name =
                "txtFullName";

            this.txtFullName.Size =
                new System.Drawing.Size(
                    300,
                    30);

            this.txtFullName.TabIndex = 3;

            // =====================================================
            // ROLE LABEL
            // =====================================================

            this.lblRole.AutoSize = true;

            this.lblRole.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9.5F,
                    System.Drawing.FontStyle.Bold);

            this.lblRole.Location =
                new System.Drawing.Point(
                    24,
                    318);

            this.lblRole.Text =
                "Vai trò";

            // =====================================================
            // ROLE COMBOBOX
            // =====================================================

            this.cboRole.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cboRole.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);

            this.cboRole.FormattingEnabled = true;

            this.cboRole.Location =
                new System.Drawing.Point(
                    24,
                    343);

            this.cboRole.Name =
                "cboRole";

            this.cboRole.Size =
                new System.Drawing.Size(
                    300,
                    31);

            this.cboRole.TabIndex = 4;

            // =====================================================
            // ADD BUTTON
            // =====================================================

            this.btnAddUser.BackColor =
                System.Drawing.Color.FromArgb(
                    37,
                    99,
                    235);

            this.btnAddUser.FlatAppearance.BorderSize =
                0;

            this.btnAddUser.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            this.btnAddUser.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10.5F,
                    System.Drawing.FontStyle.Bold);

            this.btnAddUser.ForeColor =
                System.Drawing.Color.White;

            this.btnAddUser.Location =
                new System.Drawing.Point(
                    24,
                    405);

            this.btnAddUser.Name =
                "btnAddUser";

            this.btnAddUser.Size =
                new System.Drawing.Size(
                    300,
                    48);

            this.btnAddUser.TabIndex = 5;

            this.btnAddUser.Text =
                "TẠO TÀI KHOẢN";

            this.btnAddUser.UseVisualStyleBackColor =
                false;

            this.btnAddUser.Click +=
                new System.EventHandler(
                    this.btnAddUser_Click);

            // =====================================================
            // ADD CONTROLS TO PANEL ADD
            // =====================================================

            this.panelAdd.Controls.Add(
                this.lblAddTitle);

            this.panelAdd.Controls.Add(
                this.lblAddDescription);

            this.panelAdd.Controls.Add(
                this.lblUsername);

            this.panelAdd.Controls.Add(
                this.txtUsername);

            this.panelAdd.Controls.Add(
                this.lblPassword);

            this.panelAdd.Controls.Add(
                this.txtPassword);

            this.panelAdd.Controls.Add(
                this.lblFullName);

            this.panelAdd.Controls.Add(
                this.txtFullName);

            this.panelAdd.Controls.Add(
                this.lblRole);

            this.panelAdd.Controls.Add(
                this.cboRole);

            this.panelAdd.Controls.Add(
                this.btnAddUser);

            // =====================================================
            // ADD PANELS TO MAIN
            // =====================================================

            this.panelMain.Controls.Add(
                this.panelList);

            this.panelMain.Controls.Add(
                this.panelSelected);

            this.panelMain.Controls.Add(
                this.panelAdd);

            // =====================================================
            // QUAN TRỌNG:
            // ADD MAIN + HEADER VÀO FORM
            // =====================================================

            this.Controls.Add(
                this.panelMain);

            this.Controls.Add(
                this.panelHeader);

            // =====================================================
            // FORM LOAD EVENT
            // =====================================================

            this.Load +=
                new System.EventHandler(
                    this.FormUserManagement_Load);

            // =====================================================
            // RESUME LAYOUT
            // =====================================================

            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();

            this.panelMain.ResumeLayout(false);

            this.panelList.ResumeLayout(false);
            this.panelList.PerformLayout();

            this.panelSelected.ResumeLayout(false);
            this.panelSelected.PerformLayout();

            this.panelAdd.ResumeLayout(false);
            this.panelAdd.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvUsers)).EndInit();

            this.ResumeLayout(false);
        }

        #endregion
    }
}