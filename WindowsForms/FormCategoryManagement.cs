using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormCategoryManagement : Form
    {
        private const int DefaultPageSize = 10;

        private List<CategoryDto> allCategories = new List<CategoryDto>();
        private int currentPage = 1;
        private int pageSize = DefaultPageSize;
        private int totalPages = 1;
        private bool isLoading = false;

        public FormCategoryManagement()
        {
            InitializeComponent();

            cboPageSize.Items.Clear();
            cboPageSize.Items.Add("5");
            cboPageSize.Items.Add("10");
            cboPageSize.Items.Add("20");
            cboPageSize.Items.Add("50");
            cboPageSize.SelectedItem = "10";
        }

        private HttpClient CreateClient()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => true;

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7198/api/");

            if (!string.IsNullOrWhiteSpace(SessionManager.JwtToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
            }

            return client;
        }

        private async void FormCategoryManagement_Load(object sender, EventArgs e)
        {
            await LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            if (isLoading)
                return;

            isLoading = true;
            SetActionButtons(false);

            try
            {
                using (HttpClient client = CreateClient())
                {
                    var data = await client.GetFromJsonAsync<List<CategoryDto>>("categories");
                    allCategories = data ?? new List<CategoryDto>();
                }

                currentPage = 1;
                UpdatePagination();
                lblStatus.Text = "Đã tải " + allCategories.Count + " danh mục.";
            }
            catch (Exception ex)
            {
                allCategories = new List<CategoryDto>();
                currentPage = 1;
                UpdatePagination();

                MessageBox.Show(
                    "Không thể tải danh mục.\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
                SetActionButtons(true);
            }
        }

        private List<CategoryDto> GetFilteredData()
        {
            string keyword = txtKeyword.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
                return allCategories.ToList();

            return allCategories.Where(x =>
                x.CategoryId.ToString().IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                (x.CategoryName ?? "").IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                (x.Description ?? "").IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
            ).ToList();
        }

        private void UpdatePagination()
        {
            List<CategoryDto> filtered = GetFilteredData();

            if (pageSize <= 0)
                pageSize = DefaultPageSize;

            totalPages = Math.Max(
                1,
                (int)Math.Ceiling(filtered.Count / (double)pageSize));

            if (currentPage < 1)
                currentPage = 1;

            if (currentPage > totalPages)
                currentPage = totalPages;

            List<CategoryDto> pageData = filtered
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            dgvCategories.DataSource = null;
            dgvCategories.DataSource = pageData;

            FormatGrid();

            int from = filtered.Count == 0
                ? 0
                : (currentPage - 1) * pageSize + 1;

            int to = Math.Min(currentPage * pageSize, filtered.Count);

            lblPage.Text = "Trang " + currentPage + " / " + totalPages;
            lblPageInfo.Text =
                "Hiển thị " + from + " - " + to +
                " / " + filtered.Count + " danh mục";

            btnFirst.Enabled = currentPage > 1;
            btnPrevious.Enabled = currentPage > 1;
            btnNext.Enabled = currentPage < totalPages;
            btnLast.Enabled = currentPage < totalPages;
        }

        private void FormatGrid()
        {
            if (dgvCategories.Columns.Contains("CategoryId"))
                dgvCategories.Columns["CategoryId"].HeaderText = "Mã danh mục";

            if (dgvCategories.Columns.Contains("CategoryName"))
                dgvCategories.Columns["CategoryName"].HeaderText = "Tên danh mục";

            if (dgvCategories.Columns.Contains("Description"))
                dgvCategories.Columns["Description"].HeaderText = "Mô tả";
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            UpdatePagination();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
                currentPage--;

            UpdatePagination();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
                currentPage++;

            UpdatePagination();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentPage = totalPages;
            UpdatePagination();
        }

        private void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPageSize.SelectedItem == null)
                return;

            int.TryParse(cboPageSize.SelectedItem.ToString(), out pageSize);

            if (pageSize <= 0)
                pageSize = DefaultPageSize;

            currentPage = 1;
            UpdatePagination();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            UpdatePagination();
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
            ClearInputs();
            await LoadCategoriesAsync();
        }

        private void txtKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearch.PerformClick();
            }
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvCategories.Rows[e.RowIndex];

            txtId.Text = Convert.ToString(row.Cells["CategoryId"].Value);
            txtCategoryName.Text = Convert.ToString(row.Cells["CategoryName"].Value);
            txtDescription.Text = Convert.ToString(row.Cells["Description"].Value);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show(
                    "Tên danh mục không được để trống!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCategoryName.Focus();
                return false;
            }

            return true;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (isLoading || !ValidateInput())
                return;

            try
            {
                isLoading = true;
                SetActionButtons(false);

                using (HttpClient client = CreateClient())
                {
                    var request = new
                    {
                        CategoryName = txtCategoryName.Text.Trim(),
                        Description = txtDescription.Text.Trim()
                    };

                    HttpResponseMessage response =
                        await client.PostAsJsonAsync("categories", request);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(
                            await response.Content.ReadAsStringAsync(),
                            "Không thể thêm",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }

                MessageBox.Show("Thêm danh mục thành công!");

                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi thêm danh mục:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
                SetActionButtons(true);
            }

            await LoadCategoriesAsync();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (isLoading || !ValidateInput())
                return;

            int id;

            if (!int.TryParse(txtId.Text, out id) || id <= 0)
            {
                MessageBox.Show(
                    "Hãy chọn danh mục cần cập nhật!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                isLoading = true;
                SetActionButtons(false);

                using (HttpClient client = CreateClient())
                {
                    var request = new
                    {
                        CategoryId = id,
                        CategoryName = txtCategoryName.Text.Trim(),
                        Description = txtDescription.Text.Trim()
                    };

                    HttpResponseMessage response =
                        await client.PutAsJsonAsync("categories/" + id, request);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(
                            await response.Content.ReadAsStringAsync(),
                            "Không thể cập nhật",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }

                MessageBox.Show("Cập nhật danh mục thành công!");
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi cập nhật:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
                SetActionButtons(true);
            }

            await LoadCategoriesAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (isLoading)
                return;

            int id;

            if (!int.TryParse(txtId.Text, out id) || id <= 0)
            {
                MessageBox.Show(
                    "Hãy chọn danh mục cần xóa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa danh mục này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                isLoading = true;
                SetActionButtons(false);

                using (HttpClient client = CreateClient())
                {
                    HttpResponseMessage response =
                        await client.DeleteAsync("categories/" + id);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(
                            await response.Content.ReadAsStringAsync(),
                            "Không thể xóa",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }

                MessageBox.Show("Xóa danh mục thành công!");
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi xóa:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
                SetActionButtons(true);
            }

            await LoadCategoriesAsync();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtCategoryName.Clear();
            txtDescription.Clear();
            dgvCategories.ClearSelection();
        }

        private void SetActionButtons(bool enabled)
        {
            btnAdd.Enabled = enabled;
            btnUpdate.Enabled = enabled;
            btnDelete.Enabled = enabled;
            btnSearch.Enabled = enabled;
            btnLoad.Enabled = enabled;
        }
    }

    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
