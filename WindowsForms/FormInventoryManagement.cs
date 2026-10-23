using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    /// <summary>
    /// Màn hình quản lý kho. Giao diện nằm ở FormInventoryManagement.Designer.cs.
    /// Tương thích project WinForms .NET Framework 4.7.2 / C# 7.3.
    /// </summary>
    public partial class FormInventoryManagement : Form
    {
        private readonly Color ink = Color.FromArgb(30, 41, 59);
        private readonly Color muted = Color.FromArgb(100, 116, 139);
        private readonly Color teal = Color.FromArgb(13, 148, 136);
        private List<InventoryProduct> products = new List<InventoryProduct>();
        private bool loadingCategories;

        public FormInventoryManagement()
        {
            InitializeComponent();
            ConfigureContextMenu();
            WireEvents();
            SetNoSelection();
        }

        private void WireEvents()
        {
            this.Load += FormInventoryManagement_Load;
            searchBox.TextChanged += delegate { ApplyFilters(); };
            stockFilter.SelectedIndexChanged += delegate { ApplyFilters(); };
            categoryFilter.SelectedIndexChanged += delegate
            {
                if (!loadingCategories) ApplyFilters();
            };
            refreshButton.Click += async delegate { await LoadProductsAsync(); };
            addStockButton.Click += async delegate { await AdjustStockAsync(true); };
            removeStockButton.Click += async delegate { await AdjustStockAsync(false); };
            grid.SelectionChanged += delegate { ShowSelectedProduct(); };
            grid.CellFormatting += Grid_CellFormatting;
            grid.MouseDown += Grid_MouseDown;
        }

        private void ConfigureContextMenu()
        {
            rowMenu.Items.Clear();

            ToolStripMenuItem addItem = new ToolStripMenuItem("Nhập bổ sung tồn kho");
            addItem.Click += async delegate { await AdjustStockAsync(true); };

            ToolStripMenuItem removeItem = new ToolStripMenuItem("Xuất / giảm tồn kho");
            removeItem.Click += async delegate { await AdjustStockAsync(false); };

            rowMenu.Items.Add(addItem);
            rowMenu.Items.Add(removeItem);
            grid.ContextMenuStrip = rowMenu;
        }

        private async void FormInventoryManagement_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
        }

        private void Grid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            DataGridView.HitTestInfo hit = grid.HitTest(e.X, e.Y);
            if (hit.RowIndex < 0 || hit.RowIndex >= grid.Rows.Count) return;

            grid.ClearSelection();
            grid.Rows[hit.RowIndex].Selected = true;
            if (grid.Rows[hit.RowIndex].Cells.Count > 0)
            {
                grid.CurrentCell = grid.Rows[hit.RowIndex].Cells[0];
            }
            ShowSelectedProduct();
        }

        private async Task LoadProductsAsync()
        {
            refreshButton.Enabled = false;
            Cursor previousCursor = Cursor;
            Cursor = Cursors.WaitCursor;

            try
            {
                status.Text = "Đang đồng bộ dữ liệu tồn kho...";
                string json = await ApiClientService.GetDataWithTokenAsync("products");

                List<InventoryProduct> loadedProducts = new List<InventoryProduct>();
                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    if (document.RootElement.ValueKind != JsonValueKind.Array)
                    {
                        throw new InvalidOperationException("API products không trả về danh sách JSON như mong đợi.");
                    }

                    foreach (JsonElement element in document.RootElement.EnumerateArray())
                    {
                        JsonElement categoryElement;
                        string categoryName = "Chưa phân loại";

                        if (TryGetPropertyIgnoreCase(element, "category", out categoryElement) &&
                            categoryElement.ValueKind == JsonValueKind.Object)
                        {
                            string nestedCategoryName = ReadString(categoryElement, "categoryName");
                            if (!string.IsNullOrWhiteSpace(nestedCategoryName))
                                categoryName = nestedCategoryName;
                        }

                        // Hỗ trợ cả trường CategoryName nếu API trả DTO phẳng.
                        string flatCategoryName = ReadString(element, "categoryName");
                        if (!string.IsNullOrWhiteSpace(flatCategoryName))
                            categoryName = flatCategoryName;

                        loadedProducts.Add(new InventoryProduct
                        {
                            ProductId = ReadInt(element, "productId"),
                            Barcode = ReadString(element, "barcode"),
                            ProductName = ReadString(element, "productName"),
                            CategoryId = ReadInt(element, "categoryId"),
                            CategoryName = categoryName,
                            StockQuantity = ReadInt(element, "stockQuantity"),
                            Price = ReadDecimal(element, "price")
                        });
                    }
                }

                products = loadedProducts;
                FillCategoryFilter();

                // KPI đồng bộ theo cùng tập Products mà API/web đọc từ database.
                totalValue.Text = products.Count.ToString("N0");
                long totalQuantity = products.Sum(p => (long)p.StockQuantity);
                totalStockValue.Text = totalQuantity.ToString("N0") + " SP";
                lowValue.Text = products.Count(p => p.StockQuantity > 0 && p.StockQuantity <= 10).ToString("N0");
                outValue.Text = products.Count(p => p.StockQuantity <= 0).ToString("N0");

                ApplyFilters();
                status.Text = "Đồng bộ thành công · " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            }
            catch (UnauthorizedAccessException ex)
            {
                products = new List<InventoryProduct>();
                grid.DataSource = null;
                SetNoSelection();
                status.Text = "Phiên đăng nhập không hợp lệ.";
                MessageBox.Show(ex.Message, "Cần đăng nhập lại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                status.Text = "Không thể tải dữ liệu kho.";
                MessageBox.Show("Không thể tải dữ liệu tồn kho.\n" + ex.Message,
                    "Lỗi kết nối API", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = previousCursor;
                refreshButton.Enabled = true;
            }
        }

        private void FillCategoryFilter()
        {
            string previousSelection = categoryFilter.SelectedItem == null
                ? "Tất cả ngành hàng"
                : categoryFilter.SelectedItem.ToString();

            loadingCategories = true;
            categoryFilter.BeginUpdate();
            try
            {
                categoryFilter.Items.Clear();
                categoryFilter.Items.Add("Tất cả ngành hàng");

                foreach (string categoryName in products
                    .Select(p => p.CategoryName)
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(name => name))
                {
                    categoryFilter.Items.Add(categoryName);
                }

                int previousIndex = categoryFilter.Items.IndexOf(previousSelection);
                categoryFilter.SelectedIndex = previousIndex >= 0 ? previousIndex : 0;
            }
            finally
            {
                categoryFilter.EndUpdate();
                loadingCategories = false;
            }
        }

        private void ApplyFilters()
        {
            IEnumerable<InventoryProduct> query = products;
            string keyword = (searchBox.Text ?? string.Empty).Trim();

            if (keyword.Length > 0)
            {
                query = query.Where(p =>
                    ContainsText(p.ProductName, keyword) ||
                    ContainsText(p.Barcode, keyword) ||
                    ContainsText(p.CategoryName, keyword));
            }

            if (stockFilter.SelectedIndex == 1)
                query = query.Where(p => p.StockQuantity > 0 && p.StockQuantity <= 10);
            else if (stockFilter.SelectedIndex == 2)
                query = query.Where(p => p.StockQuantity <= 0);
            else if (stockFilter.SelectedIndex == 3)
                query = query.Where(p => p.StockQuantity > 10);

            if (categoryFilter.SelectedIndex > 0 && categoryFilter.SelectedItem != null)
            {
                string chosenCategory = categoryFilter.SelectedItem.ToString();
                query = query.Where(p => string.Equals(p.CategoryName, chosenCategory, StringComparison.OrdinalIgnoreCase));
            }

            List<InventoryProduct> filteredProducts = query
                .OrderBy(p => p.StockQuantity)
                .ThenBy(p => p.ProductName)
                .ToList();

            grid.DataSource = null;
            grid.DataSource = filteredProducts;

            if (grid.Rows.Count > 0)
            {
                grid.ClearSelection();
                grid.Rows[0].Selected = true;
                if (grid.Columns.Count > 0) grid.CurrentCell = grid.Rows[0].Cells[0];
            }
            else
            {
                SetNoSelection();
            }

            ShowSelectedProduct();
        }

        private static bool ContainsText(string source, string keyword)
        {
            return !string.IsNullOrEmpty(source) &&
                   source.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= grid.Rows.Count) return;

            InventoryProduct item = grid.Rows[e.RowIndex].DataBoundItem as InventoryProduct;
            if (item == null) return;

            if (item.StockQuantity <= 0)
            {
                e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
            }
            else if (item.StockQuantity <= 10)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 247, 237);
                e.CellStyle.ForeColor = Color.FromArgb(146, 64, 14);
            }
            else
            {
                e.CellStyle.BackColor = e.RowIndex % 2 == 0 ? Color.White : Color.FromArgb(249, 250, 251);
                e.CellStyle.ForeColor = ink;
            }
        }

        private void ShowSelectedProduct()
        {
            InventoryProduct item = grid.CurrentRow == null
                ? null
                : grid.CurrentRow.DataBoundItem as InventoryProduct;

            if (item == null)
            {
                SetNoSelection();
                return;
            }

            selectedName.Text = string.IsNullOrWhiteSpace(item.ProductName)
                ? "Sản phẩm chưa có tên"
                : item.ProductName;
            selectedCategory.Text = "Ngành hàng: " + (item.CategoryName ?? "Chưa phân loại");
            selectedStock.Text = item.StockQuantity.ToString("N0") + " SP";
            selectedStock.ForeColor = item.StockQuantity <= 0
                ? Color.FromArgb(220, 38, 38)
                : item.StockQuantity <= 10
                    ? Color.FromArgb(217, 119, 6)
                    : teal;

            addStockButton.Enabled = true;
            removeStockButton.Enabled = true;
        }

        private void SetNoSelection()
        {
            selectedName.Text = "Chọn một sản phẩm";
            selectedCategory.Text = "Ngành hàng: —";
            selectedStock.Text = "—";
            selectedStock.ForeColor = teal;
            addStockButton.Enabled = false;
            removeStockButton.Enabled = false;
        }

        private async Task AdjustStockAsync(bool increase)
        {
            InventoryProduct item = grid.CurrentRow == null
                ? null
                : grid.CurrentRow.DataBoundItem as InventoryProduct;

            if (item == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm trước.", "Chưa chọn sản phẩm",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (StockQuantityDialog dialog = new StockQuantityDialog(
                increase ? "Nhập bổ sung tồn kho" : "Xuất / giảm tồn kho",
                increase ? "Số lượng nhập thêm" : "Số lượng cần giảm"))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                int delta = increase ? dialog.Quantity : -dialog.Quantity;
                int newQuantity = item.StockQuantity + delta;
                if (newQuantity < 0)
                {
                    MessageBox.Show("Số lượng giảm không được lớn hơn số lượng tồn hiện tại.",
                        "Không đủ tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    status.Text = "Đang cập nhật tồn kho...";
                    addStockButton.Enabled = false;
                    removeStockButton.Enabled = false;

                    // API hiện có hỗ trợ PUT api/products/{id}; gửi đủ các trường Product.
                    // Dùng PostAsync/PutAsync + JSON tường minh để tương thích project .NET Framework.
                    var payload = new
                    {
                        ProductId = item.ProductId,
                        Barcode = item.Barcode ?? string.Empty,
                        ProductName = item.ProductName ?? string.Empty,
                        Price = item.Price,
                        StockQuantity = newQuantity,
                        CategoryId = item.CategoryId
                    };

                    string payloadJson = JsonSerializer.Serialize(payload);
                    using (StringContent content = new StringContent(payloadJson, Encoding.UTF8, "application/json"))
                    using (HttpResponseMessage response = await ApiClientService.Client.PutAsync(
                        "products/" + item.ProductId, content))
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode)
                        {
                            status.Text = "Cập nhật tồn kho thất bại.";
                            MessageBox.Show("Không thể cập nhật tồn kho.\n" + responseBody,
                                "Cập nhật thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    await LoadProductsAsync();
                    status.Text = "Đã cập nhật tồn kho thành công.";
                    MessageBox.Show("Đã cập nhật tồn kho thành công.", "Hoàn tất",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    status.Text = "Lỗi cập nhật tồn kho.";
                    MessageBox.Show("Lỗi cập nhật tồn kho: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    addStockButton.Enabled = true;
                    removeStockButton.Enabled = true;
                    ShowSelectedProduct();
                }
            }
        }

        private static bool TryGetPropertyIgnoreCase(JsonElement element, string propertyName, out JsonElement value)
        {
            foreach (JsonProperty property in element.EnumerateObject())
            {
                if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
                {
                    value = property.Value;
                    return true;
                }
            }
            value = default(JsonElement);
            return false;
        }

        private static int ReadInt(JsonElement element, string propertyName)
        {
            JsonElement value;
            int result;
            if (TryGetPropertyIgnoreCase(element, propertyName, out value) &&
                value.ValueKind == JsonValueKind.Number && value.TryGetInt32(out result))
                return result;
            return 0;
        }

        private static decimal ReadDecimal(JsonElement element, string propertyName)
        {
            JsonElement value;
            decimal result;
            if (TryGetPropertyIgnoreCase(element, propertyName, out value) &&
                value.ValueKind == JsonValueKind.Number && value.TryGetDecimal(out result))
                return result;
            return 0m;
        }

        private static string ReadString(JsonElement element, string propertyName)
        {
            JsonElement value;
            if (TryGetPropertyIgnoreCase(element, propertyName, out value) && value.ValueKind == JsonValueKind.String)
                return value.GetString() ?? string.Empty;
            return string.Empty;
        }

        public sealed class InventoryProduct
        {
            public int ProductId { get; set; }
            public string Barcode { get; set; }
            public string ProductName { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int StockQuantity { get; set; }
            public decimal Price { get; set; }
        }

        private sealed class StockQuantityDialog : Form
        {
            private readonly NumericUpDown quantityInput;

            public int Quantity
            {
                get { return decimal.ToInt32(quantityInput.Value); }
            }

            public StockQuantityDialog(string title, string prompt)
            {
                Text = title;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                ClientSize = new Size(360, 160);
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                BackColor = Color.White;
                Font = new Font("Segoe UI", 9F);

                Label promptLabel = new Label();
                promptLabel.Text = prompt;
                promptLabel.Location = new Point(18, 16);
                promptLabel.Size = new Size(320, 25);
                promptLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                promptLabel.TextAlign = ContentAlignment.MiddleLeft;

                quantityInput = new NumericUpDown();
                quantityInput.Location = new Point(18, 48);
                quantityInput.Size = new Size(320, 28);
                quantityInput.Minimum = 1;
                quantityInput.Maximum = 100000;
                quantityInput.Value = 1;
                quantityInput.ThousandsSeparator = true;

                System.Windows.Forms.Button okButton = new System.Windows.Forms.Button();
                okButton.Text = "Xác nhận";
                okButton.DialogResult = DialogResult.OK;
                okButton.Location = new Point(158, 100);
                okButton.Size = new Size(86, 32);
                okButton.BackColor = Color.FromArgb(13, 148, 136);
                okButton.ForeColor = Color.White;
                okButton.FlatStyle = FlatStyle.Flat;
                okButton.FlatAppearance.BorderSize = 0;

                System.Windows.Forms.Button cancelButton = new System.Windows.Forms.Button();
                cancelButton.Text = "Hủy";
                cancelButton.DialogResult = DialogResult.Cancel;
                cancelButton.Location = new Point(252, 100);
                cancelButton.Size = new Size(86, 32);
                cancelButton.FlatStyle = FlatStyle.Flat;

                Controls.Add(promptLabel);
                Controls.Add(quantityInput);
                Controls.Add(okButton);
                Controls.Add(cancelButton);
                AcceptButton = okButton;
                CancelButton = cancelButton;
            }
        }

        private void stockFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
