
namespace MiniSupermarket.WinForms
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelLogin;

        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;

        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPass;

        private System.Windows.Forms.Button btnLogin;

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblFooter;

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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelLogin = new System.Windows.Forms.Panel();

            this.lblLogo = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();

            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();

            this.lblPass = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();

            this.btnLogin = new System.Windows.Forms.Button();
            this.lblFooter = new System.Windows.Forms.Label();

            this.panelMain.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.SuspendLayout();

            // panelMain
            this.panelMain.BackColor =
                System.Drawing.Color.FromArgb(241, 245, 249);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(900, 600);
            this.panelMain.TabIndex = 0;
            this.panelMain.Controls.Add(this.panelLogin);

            // panelLogin
            this.panelLogin.BackColor = System.Drawing.Color.White;
            this.panelLogin.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLogin.Location = new System.Drawing.Point(255, 45);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(390, 475);
            this.panelLogin.TabIndex = 0;

            // lblLogo
            this.lblLogo.BackColor =
                System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblLogo.Font = new System.Drawing.Font(
                "Segoe UI", 25F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(161, 20);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(65, 65);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "S";
            this.lblLogo.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblTitle.Location = new System.Drawing.Point(25, 91);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(338, 42);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "MiniMart Sakura";
            this.lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // lblSubTitle
            this.lblSubTitle.AutoSize = false;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubTitle.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSubTitle.Location = new System.Drawing.Point(25, 132);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(338, 25);
            this.lblSubTitle.TabIndex = 2;
            this.lblSubTitle.Text = "Hệ thống quản lý siêu thị mini";
            this.lblSubTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSubTitle.Click +=
                new System.EventHandler(this.lblSubTitle_Click);

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font(
                "Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor =
                System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblWelcome.Location = new System.Drawing.Point(30, 174);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(0, 21);
            this.lblWelcome.TabIndex = 3;
            this.lblWelcome.Text = "Đăng nhập vào hệ thống";

            // lblUser
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font(
                "Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor =
                System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblUser.Location = new System.Drawing.Point(30, 213);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(0, 15);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "TÊN TÀI KHOẢN";

            // txtUser
            this.txtUser.BackColor =
                System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtUser.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtUser.ForeColor =
                System.Drawing.Color.FromArgb(31, 41, 55);
            this.txtUser.Location = new System.Drawing.Point(30, 236);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(328, 27);
            this.txtUser.TabIndex = 0;

            // lblPass
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font(
                "Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPass.ForeColor =
                System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblPass.Location = new System.Drawing.Point(30, 280);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(0, 15);
            this.lblPass.TabIndex = 5;
            this.lblPass.Text = "MẬT KHẨU";

            // txtPass
            this.txtPass.BackColor =
                System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtPass.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPass.ForeColor =
                System.Drawing.Color.FromArgb(31, 41, 55);
            this.txtPass.Location = new System.Drawing.Point(30, 303);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(328, 27);
            this.txtPass.TabIndex = 1;
            this.txtPass.UseSystemPasswordChar = true;

            // btnLogin
            this.btnLogin.BackColor =
                System.Drawing.Color.FromArgb(196, 52, 68);
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseDownBackColor =
                System.Drawing.Color.FromArgb(158, 35, 52);
            this.btnLogin.FlatAppearance.MouseOverBackColor =
                System.Drawing.Color.FromArgb(158, 35, 52);
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font(
                "Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(30, 355);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(328, 44);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "ĐĂNG NHẬP HỆ THỐNG";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Click +=
                new System.EventHandler(this.btnLogin_Click);

            // lblFooter
            this.lblFooter.AutoSize = false;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFooter.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblFooter.Location = new System.Drawing.Point(20, 421);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(350, 25);
            this.lblFooter.TabIndex = 6;
            this.lblFooter.Text =
                "MINIMART SAKURA  |  QUẢN LÝ BÁN HÀNG";
            this.lblFooter.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFooter.Click +=
                new System.EventHandler(this.lblFooter_Click);

            // Add controls to panelLogin
            this.panelLogin.Controls.Add(this.lblLogo);
            this.panelLogin.Controls.Add(this.lblTitle);
            this.panelLogin.Controls.Add(this.lblSubTitle);
            this.panelLogin.Controls.Add(this.lblWelcome);
            this.panelLogin.Controls.Add(this.lblUser);
            this.panelLogin.Controls.Add(this.txtUser);
            this.panelLogin.Controls.Add(this.lblPass);
            this.panelLogin.Controls.Add(this.txtPass);
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Controls.Add(this.lblFooter);

            // FormLogin
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor =
                System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiniMart Sakura - Đăng nhập";

            this.panelMain.ResumeLayout(false);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
