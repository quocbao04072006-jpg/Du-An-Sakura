
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            txtPass.KeyDown += TxtPass_KeyDown;
            txtUser.KeyDown += TxtUser_KeyDown;

            ApplySakuraTheme();
        }

        private void ApplySakuraTheme()
        {
            // Bảng màu Sakura hiện đại, trung tính và nam tính
            Color navy = Color.FromArgb(31, 41, 55);
            Color navyLight = Color.FromArgb(51, 65, 85);
            Color background = Color.FromArgb(241, 245, 249);
            Color white = Color.White;
            Color muted = Color.FromArgb(100, 116, 139);
            Color border = Color.FromArgb(203, 213, 225);
            Color red = Color.FromArgb(196, 52, 68);
            Color redHover = Color.FromArgb(158, 35, 52);

            Text = "MiniMart Sakura - Đăng nhập";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            ClientSize = new Size(900, 600);
            MaximizeBox = false;
            BackColor = background;
            Font = new Font("Segoe UI", 10F);

            panelMain.Dock = DockStyle.Fill;
            panelMain.BackColor = background;

            // Thẻ đăng nhập
            panelLogin.Size = new Size(390, 475);
            panelLogin.Location = new Point(255, 45);
            panelLogin.BackColor = white;
            panelLogin.BorderStyle = BorderStyle.FixedSingle;

            // Logo
            lblLogo.Text = "S";
            lblLogo.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            lblLogo.ForeColor = white;
            lblLogo.BackColor = navy;
            lblLogo.Size = new Size(65, 65);
            lblLogo.Location = new Point(161, 20);
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            // Tiêu đề
            lblTitle.Text = "MiniMart Sakura";
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = navy;
            lblTitle.AutoSize = false;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Location = new Point(25, 91);
            lblTitle.Size = new Size(338, 42);

            lblSubTitle.Text = "Hệ thống quản lý siêu thị mini";
            lblSubTitle.Font = new Font("Segoe UI", 10F);
            lblSubTitle.ForeColor = muted;
            lblSubTitle.AutoSize = false;
            lblSubTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblSubTitle.Location = new Point(25, 132);
            lblSubTitle.Size = new Size(338, 25);

            lblWelcome.Text = "Đăng nhập vào hệ thống";
            lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblWelcome.ForeColor = navy;
            lblWelcome.Location = new Point(30, 174);
            lblWelcome.AutoSize = true;

            // Tài khoản
            lblUser.Text = "TÊN TÀI KHOẢN";
            lblUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUser.ForeColor = navyLight;
            lblUser.Location = new Point(30, 213);

            txtUser.Location = new Point(30, 236);
            txtUser.Size = new Size(328, 31);
            txtUser.Font = new Font("Segoe UI", 11F);
            txtUser.BorderStyle = BorderStyle.FixedSingle;
            txtUser.BackColor = Color.FromArgb(248, 250, 252);
            txtUser.ForeColor = navy;

            // Mật khẩu
            lblPass.Text = "MẬT KHẨU";
            lblPass.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPass.ForeColor = navyLight;
            lblPass.Location = new Point(30, 280);

            txtPass.Location = new Point(30, 303);
            txtPass.Size = new Size(328, 31);
            txtPass.Font = new Font("Segoe UI", 11F);
            txtPass.BorderStyle = BorderStyle.FixedSingle;
            txtPass.BackColor = Color.FromArgb(248, 250, 252);
            txtPass.ForeColor = navy;
            txtPass.UseSystemPasswordChar = true;

            // Nút đăng nhập
            btnLogin.Text = "ĐĂNG NHẬP HỆ THỐNG";
            btnLogin.Location = new Point(30, 355);
            btnLogin.Size = new Size(328, 44);
            btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogin.BackColor = red;
            btnLogin.ForeColor = white;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = redHover;
            btnLogin.FlatAppearance.MouseDownBackColor = redHover;
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Cursor = Cursors.Hand;

            // Chân trang
            lblFooter.Text = "MINIMART SAKURA  |  QUẢN LÝ BÁN HÀNG";
            lblFooter.Font = new Font("Segoe UI", 8.5F);
            lblFooter.ForeColor = muted;
            lblFooter.AutoSize = false;
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            lblFooter.Location = new Point(20, 421);
            lblFooter.Size = new Size(350, 25);

            AcceptButton = btnLogin;
        }

        private void TxtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtPass.Focus();
            }
        }

        private void TxtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin.PerformClick();
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên tài khoản!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUser.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPass.Focus();
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "ĐANG ĐĂNG NHẬP...";

            try
            {
                bool loginSuccess =
                    await ApiClientService.LoginAsync(username, password);

                if (!loginSuccess)
                {
                    MessageBox.Show(
                        ApiClientService.LastLoginError,
                        "Đăng nhập thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    txtPass.Clear();
                    txtPass.Focus();
                    return;
                }

                MessageBox.Show(
                    "Đăng nhập thành công!\n\nQuyền: " +
                    SessionManager.CurrentRole,
                    "Đăng nhập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Hide();

                using (FormMainShell mainForm = new FormMainShell())
                {
                    mainForm.ShowDialog();
                }

                if (string.IsNullOrWhiteSpace(SessionManager.JwtToken))
                {
                    Show();
                    txtPass.Clear();
                    txtUser.Focus();
                    return;
                }

                Close();
            }
            catch (Exception ex)
            {
                SessionManager.ClearSession();

                MessageBox.Show(
                    "Không thể đăng nhập hoặc kết nối Web API.\n\n" +
                    ex.Message +
                    "\n\nHãy kiểm tra API đang chạy.",
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                {
                    btnLogin.Enabled = true;
                    btnLogin.Text = "ĐĂNG NHẬP HỆ THỐNG";
                    btnLogin.BackColor = Color.FromArgb(196, 52, 68);
                }
            }
        }

        private void lblSubTitle_Click(object sender, EventArgs e)
        {
        }

        private void lblFooter_Click(object sender, EventArgs e)
        {
        }
    }
}
