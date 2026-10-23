using System;
using System.Drawing;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    /// <summary>Trang cài đặt hệ thống: hiển thị cấu hình kết nối và phiên đăng nhập thực tế.</summary>
    public class FormSettings : Form
    {
        private readonly Label connectionValue = new Label();
        private readonly Label checkedAt = new Label();
        private readonly Button checkButton = new Button();
        private readonly Color ink = Color.FromArgb(30, 41, 59);
        private readonly Color muted = Color.FromArgb(100, 116, 139);

        public FormSettings()
        {
            Text = "Cài đặt hệ thống"; BackColor = Color.FromArgb(245, 247, 251); Font = new Font("Segoe UI", 9F); Padding = new Padding(24);
            Controls.Add(new Label { Text = "CÀI ĐẶT HỆ THỐNG", Font = new Font("Segoe UI", 21F, FontStyle.Bold), ForeColor = ink, AutoSize = true, Location = new Point(24, 20) });
            Controls.Add(new Label { Text = "Kiểm tra kết nối API và thông tin phiên làm việc đang hoạt động", Font = new Font("Segoe UI", 10F), ForeColor = muted, AutoSize = true, Location = new Point(27, 60) });
            var sessionCard = CreateCard("PHIÊN ĐĂNG NHẬP", 24, 110, 430, 210);
            AddInfo(sessionCard, "Tài khoản", SessionManager.CurrentUsername, 18, 52);
            AddInfo(sessionCard, "Họ và tên", string.IsNullOrWhiteSpace(SessionManager.CurrentFullName) ? "Chưa khai báo" : SessionManager.CurrentFullName, 18, 90);
            AddInfo(sessionCard, "Vai trò", SessionManager.CurrentRole, 18, 128);
            Controls.Add(sessionCard);
            var connectionCard = CreateCard("KẾT NỐI DỊCH VỤ", 475, 110, 430, 210);
            AddInfo(connectionCard, "API đang sử dụng", ApiClientService.Client.BaseAddress == null ? "Chưa cấu hình" : ApiClientService.Client.BaseAddress.ToString(), 18, 52);
            connectionValue.Text = "Chưa kiểm tra"; connectionValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold); connectionValue.ForeColor = Color.FromArgb(217, 119, 6); connectionValue.Location = new Point(18, 104); connectionValue.Size = new Size(370, 28); connectionCard.Controls.Add(connectionValue);
            checkedAt.Text = ""; checkedAt.ForeColor = muted; checkedAt.Location = new Point(18, 136); checkedAt.AutoSize = true; connectionCard.Controls.Add(checkedAt);
            checkButton.Text = "Kiểm tra kết nối"; checkButton.Location = new Point(18, 162); checkButton.Size = new Size(170, 32); checkButton.BackColor = Color.FromArgb(13, 148, 136); checkButton.ForeColor = Color.White; checkButton.FlatStyle = FlatStyle.Flat; checkButton.FlatAppearance.BorderSize = 0; checkButton.Click += async (s, e) => await CheckConnectionAsync(); connectionCard.Controls.Add(checkButton);
            Controls.Add(connectionCard);
            var note = new Label { Text = "Lưu ý: các trường Hồ sơ cửa hàng và cấu hình POS có thể được bổ sung khi có bảng cấu hình tương ứng trong cơ sở dữ liệu. Trang này không lưu cài đặt giả vào bộ nhớ tạm.", ForeColor = muted, AutoSize = false, Size = new Size(850, 58), Location = new Point(26, 340), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            Controls.Add(note);
            Load += async (s, e) => await CheckConnectionAsync();
        }

        private Panel CreateCard(string title, int x, int y, int w, int h)
        {
            var p = new Panel { Location = new Point(x, y), Size = new Size(w, h), BackColor = Color.White, Anchor = AnchorStyles.Top | AnchorStyles.Left };
            p.Controls.Add(new Label { Text = title, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ink, AutoSize = true, Location = new Point(18, 16) }); return p;
        }

        private void AddInfo(Panel panel, string label, string value, int x, int y)
        {
            panel.Controls.Add(new Label { Text = label, Font = new Font("Segoe UI", 8.5F, FontStyle.Bold), ForeColor = muted, AutoSize = true, Location = new Point(x, y) });
            panel.Controls.Add(new Label { Text = value ?? "—", Font = new Font("Segoe UI", 10F), ForeColor = ink, AutoSize = true, Location = new Point(x, y + 16) });
        }

        private async Task CheckConnectionAsync()
        {
            checkButton.Enabled = false; connectionValue.Text = "Đang kiểm tra…"; connectionValue.ForeColor = Color.FromArgb(217, 119, 6);
            try
            {
                string json = await ApiClientService.GetDataWithTokenAsync("reports/dashboard-summary");
                using (JsonDocument.Parse(json)) { }
                connectionValue.Text = "●  Kết nối API thành công"; connectionValue.ForeColor = Color.FromArgb(5, 150, 105);
            }
            catch (Exception ex)
            {
                connectionValue.Text = "●  Kết nối thất bại"; connectionValue.ForeColor = Color.FromArgb(220, 38, 38);
                MessageBox.Show("Không thể kiểm tra API.\n" + ex.Message, "Kết nối hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { checkedAt.Text = "Kiểm tra lúc " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"); checkButton.Enabled = true; }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormSettings
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FormSettings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.ResumeLayout(false);

        }

        private void FormSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
