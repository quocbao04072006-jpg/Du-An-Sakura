using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        private bool _isLoadingUsers;

        public FormUserManagement()
        {
            InitializeComponent();

            // Danh sách vai trò
            cboRole.Items.AddRange(new string[]
            {
                "Admin",
                "Cashier",
                "Warehouse"
            });

            cboRole.SelectedIndex = 1;

            // Xử lý checkbox IsActive
            dgvUsers.CurrentCellDirtyStateChanged +=
                dgvUsers_CurrentCellDirtyStateChanged;

            dgvUsers.CellValueChanged +=
                dgvUsers_CellValueChanged;

            // Khi chọn một tài khoản
            dgvUsers.SelectionChanged +=
                dgvUsers_SelectionChanged;
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private async void FormUserManagement_Load(
            object sender,
            EventArgs e)
        {
            await LoadUsersAsync();
        }

        // =========================================================
        // LOAD DANH SÁCH USER
        // =========================================================

        private async Task LoadUsersAsync()
        {
            _isLoadingUsers = true;

            try
            {
                var users =
                    await ApiClientService.Client
                    .GetFromJsonAsync<List<UserDto>>("users");

                dgvUsers.DataSource = null;
                dgvUsers.DataSource =
                    users ?? new List<UserDto>();

                // Đổi tên các cột
                if (dgvUsers.Columns[nameof(UserDto.Id)] != null)
                {
                    dgvUsers.Columns[nameof(UserDto.Id)]
                        .HeaderText = "Mã";
                }

                if (dgvUsers.Columns[nameof(UserDto.Username)] != null)
                {
                    dgvUsers.Columns[nameof(UserDto.Username)]
                        .HeaderText = "Tên đăng nhập";
                }

                if (dgvUsers.Columns[nameof(UserDto.FullName)] != null)
                {
                    dgvUsers.Columns[nameof(UserDto.FullName)]
                        .HeaderText = "Họ và tên";
                }

                if (dgvUsers.Columns[nameof(UserDto.Role)] != null)
                {
                    dgvUsers.Columns[nameof(UserDto.Role)]
                        .HeaderText = "Vai trò";
                }

                if (dgvUsers.Columns[nameof(UserDto.IsActive)] != null)
                {
                    dgvUsers.Columns[nameof(UserDto.IsActive)]
                        .HeaderText = "Hoạt động";
                }

                // Không cho sửa thông tin trực tiếp
                if (dgvUsers.Columns[nameof(UserDto.Id)] != null)
                    dgvUsers.Columns[nameof(UserDto.Id)].ReadOnly = true;

                if (dgvUsers.Columns[nameof(UserDto.Username)] != null)
                    dgvUsers.Columns[nameof(UserDto.Username)].ReadOnly = true;

                if (dgvUsers.Columns[nameof(UserDto.FullName)] != null)
                    dgvUsers.Columns[nameof(UserDto.FullName)].ReadOnly = true;

                if (dgvUsers.Columns[nameof(UserDto.Role)] != null)
                    dgvUsers.Columns[nameof(UserDto.Role)].ReadOnly = true;

                // Chỉ checkbox IsActive được phép thay đổi
                if (dgvUsers.Columns[nameof(UserDto.IsActive)] != null)
                    dgvUsers.Columns[nameof(UserDto.IsActive)].ReadOnly = false;

                // Chọn dòng đầu tiên
                if (dgvUsers.Rows.Count > 0)
                {
                    dgvUsers.ClearSelection();
                    dgvUsers.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi lấy danh sách tài khoản:\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingUsers = false;
            }
        }

        // =========================================================
        // CHỌN USER
        // =========================================================

        private void dgvUsers_SelectionChanged(
            object sender,
            EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
                return;

            var user =
                dgvUsers.CurrentRow.DataBoundItem as UserDto;

            if (user == null)
                return;

            lblSelectedUser.Text =
                "Tài khoản: " + user.Username;

            lblSelectedName.Text =
                "Họ tên: " + user.FullName;

            lblSelectedRole.Text =
                "Vai trò: " + user.Role;

            if (user.IsActive)
            {
                lblSelectedStatus.Text =
                    "Trạng thái: Đang hoạt động";

                lblSelectedStatus.ForeColor =
                    System.Drawing.Color.FromArgb(22, 163, 74);
            }
            else
            {
                lblSelectedStatus.Text =
                    "Trạng thái: Đã khóa";

                lblSelectedStatus.ForeColor =
                    System.Drawing.Color.FromArgb(220, 38, 38);
            }
        }

        // =========================================================
        // CHECKBOX COMMIT NGAY
        // =========================================================

        private void dgvUsers_CurrentCellDirtyStateChanged(
            object sender,
            EventArgs e)
        {
            if (dgvUsers.IsCurrentCellDirty &&
                dgvUsers.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvUsers.CommitEdit(
                    DataGridViewDataErrorContexts.Commit);
            }
        }

        // =========================================================
        // CẬP NHẬT ISACTIVE
        // =========================================================

        private async void dgvUsers_CellValueChanged(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (_isLoadingUsers ||
                e.RowIndex < 0 ||
                e.ColumnIndex < 0)
            {
                return;
            }

            var column =
                dgvUsers.Columns[e.ColumnIndex];

            if (column.DataPropertyName !=
                    nameof(UserDto.IsActive) &&
                column.Name !=
                    nameof(UserDto.IsActive))
            {
                return;
            }

            var user =
                dgvUsers.Rows[e.RowIndex]
                .DataBoundItem as UserDto;

            if (user == null)
                return;

            bool isActive = Convert.ToBoolean(
                dgvUsers.Rows[e.RowIndex]
                .Cells[e.ColumnIndex]
                .Value ?? false);

            try
            {
                var response =
                    await ApiClientService.Client.PutAsJsonAsync(
                        $"users/{user.Id}/active",
                        new UpdateUserActiveRequest
                        {
                            IsActive = isActive
                        });

                if (!response.IsSuccessStatusCode)
                {
                    string detail =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Không lưu được trạng thái tài khoản.\n\n" +
                        "HTTP: " +
                        (int)response.StatusCode +
                        "\n\n" +
                        detail,
                        "Cập nhật thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    await LoadUsersAsync();
                    return;
                }

                // Load lại từ API để đồng bộ DB
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi cập nhật trạng thái tài khoản:\n\n" +
                    ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                await LoadUsersAsync();
            }
        }

        // =========================================================
        // TẠO TÀI KHOẢN
        // =========================================================

        private async void btnAddUser_Click(
            object sender,
            EventArgs e)
        {
            // Kiểm tra username
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên đăng nhập!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUsername.Focus();
                return;
            }

            // Kiểm tra password
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPassword.Focus();
                return;
            }

            // Tạo object gửi API
            var newUser = new
            {
                Username = txtUsername.Text.Trim(),

                Password = txtPassword.Text.Trim(),

                FullName = txtFullName.Text.Trim(),

                Role = cboRole.SelectedItem?.ToString()
                       ?? "Cashier"
            };

            try
            {
                var response =
                    await ApiClientService.Client.PostAsJsonAsync(
                        "users",
                        newUser);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Tạo tài khoản mới thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Load lại danh sách
                    await LoadUsersAsync();

                    // Xóa form
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtFullName.Clear();

                    cboRole.SelectedIndex = 1;

                    txtUsername.Focus();
                }
                else
                {
                    string detail =
                        await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        "Không tạo được tài khoản.\n\n" +
                        detail,
                        "Thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tạo tài khoản:\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    // =============================================================
    // USER DTO
    // =============================================================

    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    // =============================================================
    // REQUEST UPDATE ACTIVE
    // =============================================================

    public class UpdateUserActiveRequest
    {
        public bool IsActive { get; set; }
    }
}