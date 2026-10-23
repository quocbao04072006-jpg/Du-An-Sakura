using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        private static readonly List<CartItemDto> _cart =
            new List<CartItemDto>();

        private bool isSearchingBarcode = false;
        private bool isCheckingOut = false;
        private ComboBox cboPaymentMethod;
        private Panel posHeaderPanel;
        private Label lblHeaderTitle;
        private Label lblHeaderSubtitle;
        private Label[] statValues;
        private Label[] statCaptions;
        private Timer customerLookupTimer;
        private string lastCustomerLookupPhone = string.Empty;
        private string resolvedCustomerName = string.Empty;
        private string resolvedCustomerRank = string.Empty;

        public FormPOS()
        {
            InitializeComponent();
            SetupCartGrid();
            SetupPaymentControls();
            ApplyPosTheme();
            KeyPreview = true;
            KeyDown += FormPOS_KeyDown;
            Resize += (sender, args) => ApplyResponsivePosLayout();
            Load += FormPOS_Load;
            dgvCart.CellEndEdit += dgvCart_CellEndEdit;
            dgvCart.CellContentClick += dgvCart_CellContentClick;
            dgvCart.DataError += (sender, args) => { args.ThrowException = false; };

            customerLookupTimer = new Timer { Interval = 450 };
            customerLookupTimer.Tick += async (sender, args) =>
            {
                customerLookupTimer.Stop();
                await LookupCustomerByPhoneAsync();
            };
            txtCustomerPhone.TextChanged += txtCustomerPhone_TextChanged;
        }

        private void SetupPaymentControls()
        {
            panelRight.BackColor = Color.White;
            panelRight.Padding = new Padding(18, 16, 18, 16);

            lblCustPhoneTitle.Text = "KHÁCH HÀNG";
            lblCustPhoneTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCustPhoneTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblCustomerName.Text = "Khách vãng lai";

            lblTotalTitle.Text = "TỔNG THANH TOÁN";
            lblTotalTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalTitle.ForeColor = Color.FromArgb(51, 65, 85);
            lblTotalAmount.Font = new Font("Segoe UI", 23F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.FromArgb(220, 53, 69);

            lblCashTitle.Text = "TIỀN KHÁCH ĐƯA";
            lblChangeTitle.Text = "TIỀN THỪA TRẢ";
            lblCashTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblChangeTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCashTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblChangeTitle.ForeColor = Color.FromArgb(71, 85, 105);

            txtCustomerPhone.BorderStyle = BorderStyle.FixedSingle;
            txtCashReceived.BorderStyle = BorderStyle.FixedSingle;
            txtCustomerPhone.Font = new Font("Segoe UI", 10F);
            txtCashReceived.Font = new Font("Segoe UI", 11F);

            Label paymentTitle = new Label
            {
                Name = "lblPaymentMethodTitle",
                Text = "PHƯƠNG THỨC THANH TOÁN",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(15, 344),
                Size = new Size(270, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            cboPaymentMethod = new ComboBox
            {
                Name = "cboPaymentMethod",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(15, 365),
                Size = new Size(248, 30),
                FlatStyle = FlatStyle.Flat,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            cboPaymentMethod.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ POS" });
            cboPaymentMethod.SelectedIndex = 0;
            cboPaymentMethod.SelectedIndexChanged += (sender, args) => UpdatePaymentMode();
            panelRight.Controls.Add(paymentTitle);
            panelRight.Controls.Add(cboPaymentMethod);

            lblTotalTitle.Location = new Point(15, 145);
            lblTotalAmount.Location = new Point(15, 172);
            lblCashTitle.Location = new Point(15, 222);
            txtCashReceived.Location = new Point(15, 246);
            lblChangeTitle.Location = new Point(15, 282);
            lblChange.Location = new Point(15, 305);

            btnCheckout.Text = "THANH TOÁN & IN HÓA ĐƠN (F12)";
            btnCheckout.Location = new Point(15, 405);
            btnRemoveSelected.Location = new Point(15, 456);
            btnClearCart.Location = new Point(15, 506);
            btnCheckout.Height = 44;
            btnRemoveSelected.Height = 40;
            btnClearCart.Height = 40;

            ApplyResponsivePosLayout();
            UpdatePaymentMode();
        }

        private void ApplyPosTheme()
        {
            BackColor = Color.FromArgb(241, 245, 249);
            Font = new Font("Segoe UI", 9.5F);
            Text = "MiniMart | Quầy thu ngân & bán hàng";
            MinimumSize = new Size(1000, 680);

            panelLeft.BackColor = Color.FromArgb(248, 250, 252);
            panelRight.BackColor = Color.White;
            panelLeft.Dock = DockStyle.None;
            panelRight.Dock = DockStyle.None;
            panelLeft.BorderStyle = BorderStyle.FixedSingle;
            panelRight.BorderStyle = BorderStyle.FixedSingle;
            BuildPosHeader();

            lblBarcode.Text = "QUÉT MÃ VẠCH";
            lblBarcode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBarcode.ForeColor = Color.FromArgb(30, 41, 59);
            txtBarcode.Font = new Font("Segoe UI", 12F);
            txtBarcode.BorderStyle = BorderStyle.FixedSingle;

            dgvCart.BackgroundColor = Color.White;
            dgvCart.BorderStyle = BorderStyle.None;
            dgvCart.GridColor = Color.FromArgb(226, 232, 240);
            dgvCart.EnableHeadersVisualStyles = false;
            dgvCart.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 64, 175);
            dgvCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvCart.ColumnHeadersHeight = 38;
            dgvCart.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvCart.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            dgvCart.DefaultCellStyle.BackColor = Color.White;
            dgvCart.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvCart.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            dgvCart.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvCart.RowTemplate.Height = 34;
            dgvCart.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCart.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            StyleActionButton(btnCheckout, Color.FromArgb(5, 150, 105));
            StyleActionButton(btnRemoveSelected, Color.FromArgb(217, 119, 6));
            StyleActionButton(btnClearCart, Color.FromArgb(185, 28, 28));

            lblCustomerName.ForeColor = Color.FromArgb(100, 116, 139);
            lblChange.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblChange.ForeColor = Color.FromArgb(15, 23, 42);
            ApplyResponsivePosLayout();
        }

        private void BuildPosHeader()
        {
            posHeaderPanel = new Panel
            {
                Name = "posHeaderPanel",
                BackColor = Color.FromArgb(241, 245, 249),
                Height = 132,
                Dock = DockStyle.None
            };

            lblHeaderTitle = new Label
            {
                Text = "THU NGÂN & BÁN HÀNG",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(18, 10),
                AutoSize = true
            };
            lblHeaderSubtitle = new Label
            {
                Text = "Quét mã vạch · Quản lý giỏ hàng · Thanh toán nhanh",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(20, 37),
                AutoSize = true
            };
            posHeaderPanel.Controls.Add(lblHeaderTitle);
            posHeaderPanel.Controls.Add(lblHeaderSubtitle);

            string[] captions = { "SẢN PHẨM TRONG GIỎ", "SỐ DÒNG HÀNG", "TẠM TÍNH", "PHƯƠNG THỨC" };
            string[] values = { "0 SP", "0 dòng", "0 đ", "Tiền mặt" };
            statValues = new Label[4];
            statCaptions = new Label[4];
            for (int i = 0; i < 4; i++)
            {
                Panel card = new Panel
                {
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Height = 66,
                    Tag = i
                };
                statCaptions[i] = new Label
                {
                    Text = captions[i],
                    Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    Location = new Point(10, 8),
                    AutoSize = true
                };
                statValues[i] = new Label
                {
                    Text = values[i],
                    Font = new Font("Segoe UI", i == 2 ? 13F : 12F, FontStyle.Bold),
                    ForeColor = i == 2 ? Color.FromArgb(5, 150, 105) : Color.FromArgb(30, 41, 59),
                    Location = new Point(10, 31),
                    AutoSize = true
                };
                card.Controls.Add(statCaptions[i]);
                card.Controls.Add(statValues[i]);
                posHeaderPanel.Controls.Add(card);
            }
            Controls.Add(posHeaderPanel);
            posHeaderPanel.BringToFront();
        }

        private void UpdatePosStats()
        {
            if (statValues == null || statValues.Length < 4) return;
            statValues[0].Text = _cart.Sum(x => x.Quantity) + " SP";
            statValues[1].Text = _cart.Count + " dòng";
            statValues[2].Text = _cart.Sum(x => x.TotalPrice).ToString("N0") + " đ";
            statValues[3].Text = cboPaymentMethod == null || cboPaymentMethod.SelectedItem == null
                ? "Tiền mặt" : cboPaymentMethod.SelectedItem.ToString();
        }

        private static void StyleActionButton(Button button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.Height = 42;
        }

        private void UpdatePaymentMode()
        {
            string method = cboPaymentMethod == null || cboPaymentMethod.SelectedItem == null
                ? "Tiền mặt" : cboPaymentMethod.SelectedItem.ToString();
            bool cashMode = string.Equals(method, "Tiền mặt", StringComparison.OrdinalIgnoreCase);

            txtCashReceived.Enabled = cashMode;
            if (!cashMode) txtCashReceived.Clear();

            lblCashTitle.Text = cashMode ? "Tiền khách đưa:" : "Tiền mặt (không áp dụng):";
            lblChangeTitle.Text = cashMode ? "Tiền thừa trả:" : "Trạng thái thanh toán:";
            if (cashMode) CalculateChange();
            else CalculateChange(); // Also updates the hint for transfer/card modes.

            UpdatePosStats();
            if (btnCheckout != null)
            {
                btnCheckout.Text = cashMode
                    ? "THANH TOÁN & IN HÓA ĐƠN (F12)"
                    : "XÁC NHẬN THANH TOÁN (F12)";
            }
        }

        private void ApplyResponsivePosLayout()
        {
            if (panelLeft == null || panelRight == null || dgvCart == null || ClientSize.Width <= 0)
                return;

            const int headerHeight = 132;
            int rightWidth = Math.Max(320, (int)(ClientSize.Width * 0.29));
            rightWidth = Math.Min(rightWidth, 430);
            int leftWidth = Math.Max(420, ClientSize.Width - rightWidth);
            int contentHeight = Math.Max(300, ClientSize.Height - headerHeight);

            if (posHeaderPanel != null)
            {
                posHeaderPanel.SetBounds(0, 0, ClientSize.Width, headerHeight);
                int cardGap = 10;
                int cardStartX = 18;
                int available = Math.Max(400, ClientSize.Width - 36);
                int cardWidth = Math.Max(120, (available - cardGap * 3) / 4);
                for (int i = 0; i < posHeaderPanel.Controls.Count; i++)
                {
                    Control control = posHeaderPanel.Controls[i];
                    if (control is Panel card)
                    {
                        int index = Convert.ToInt32(card.Tag);
                        card.SetBounds(cardStartX + index * (cardWidth + cardGap), 58, cardWidth, 66);
                    }
                }
            }

            panelLeft.SetBounds(0, headerHeight, leftWidth, contentHeight);
            panelRight.SetBounds(leftWidth, headerHeight, ClientSize.Width - leftWidth, contentHeight);

            int leftContentWidth = Math.Max(300, panelLeft.ClientSize.Width - 30);
            lblBarcode.Location = new Point(15, 14);
            txtBarcode.Location = new Point(15, 39);
            txtBarcode.Width = leftContentWidth;
            txtBarcode.Height = 34;
            dgvCart.Location = new Point(15, 84);
            dgvCart.Width = leftContentWidth;
            dgvCart.Height = Math.Max(180, panelLeft.ClientSize.Height - 100);

            int rightControlWidth = Math.Max(220, panelRight.ClientSize.Width - 30);
            Control[] rightControls = new Control[]
            {
                txtCustomerPhone, txtCashReceived, cboPaymentMethod,
                btnCheckout, btnRemoveSelected, btnClearCart,
                lblTotalAmount, lblChange
            };
            foreach (Control control in rightControls)
            {
                if (control != null)
                {
                    control.Width = rightControlWidth;
                    control.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                }
            }
        }

        private void FormPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (cboPaymentMethod == null) return;
            if (e.KeyCode == Keys.F8)
            {
                cboPaymentMethod.SelectedItem = "Tiền mặt";
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F9)
            {
                cboPaymentMethod.SelectedItem = "Chuyển khoản";
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F10)
            {
                cboPaymentMethod.SelectedItem = "Thẻ POS";
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F12)
            {
                btnCheckout.PerformClick();
                e.Handled = true;
            }
        }

        private void FormPOS_Load(object sender, EventArgs e)
        {
            UpdateCartDisplay();
            UpdatePaymentMode();
            txtBarcode.Focus();
        }

        // =====================================================
        // 1. THIẾT LẬP BẢNG GIỎ HÀNG
        // =====================================================

        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();
            dgvCart.AllowUserToAddRows = false;
            dgvCart.ReadOnly = false;
            dgvCart.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dgvCart.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            // Cho phép chọn nhiều sản phẩm cùng lúc.
            dgvCart.MultiSelect = true;
            dgvCart.RowHeadersVisible = false;

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductId",
                DataPropertyName = "ProductId",
                HeaderText = "Mã SP",
                Width = 75,
                ReadOnly = true
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProductName",
                DataPropertyName = "ProductName",
                HeaderText = "Tên sản phẩm",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 100,
                ReadOnly = true
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnitPrice",
                DataPropertyName = "UnitPrice",
                HeaderText = "Đơn giá",
                Width = 105,
                ReadOnly = true,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            // Bộ điều khiển số lượng dạng nút giảm / số lượng / tăng.
            dgvCart.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colDecrease",
                HeaderText = "",
                Text = "−",
                UseColumnTextForButtonValue = true,
                Width = 34,
                FlatStyle = FlatStyle.Flat,
                ReadOnly = true,
                DefaultCellStyle =
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(241, 245, 249),
                    ForeColor = Color.FromArgb(51, 65, 85),
                    SelectionBackColor = Color.FromArgb(226, 232, 240),
                    SelectionForeColor = Color.FromArgb(51, 65, 85),
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Padding = new Padding(0)
                }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colQuantity",
                DataPropertyName = "Quantity",
                HeaderText = "SL",
                Width = 48,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle =
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(239, 246, 255),
                    ForeColor = Color.FromArgb(30, 64, 175),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    SelectionBackColor = Color.FromArgb(219, 234, 254),
                    SelectionForeColor = Color.FromArgb(30, 64, 175)
                }
            });

            dgvCart.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colIncrease",
                HeaderText = "",
                Text = "+",
                UseColumnTextForButtonValue = true,
                Width = 34,
                FlatStyle = FlatStyle.Flat,
                ReadOnly = true,
                DefaultCellStyle =
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(220, 252, 231),
                    ForeColor = Color.FromArgb(21, 128, 61),
                    SelectionBackColor = Color.FromArgb(187, 247, 208),
                    SelectionForeColor = Color.FromArgb(21, 128, 61),
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Padding = new Padding(0)
                }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotalPrice",
                DataPropertyName = "TotalPrice",
                HeaderText = "Thành tiền",
                Width = 125,
                ReadOnly = true,
                DefaultCellStyle = { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        // =====================================================
        // 2. QUÉT BARCODE
        // =====================================================

        private async void txtBarcode_KeyDown(
            object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.Handled = true;
            e.SuppressKeyPress = true;

            if (isSearchingBarcode || isCheckingOut)
                return;

            string barcode = txtBarcode.Text.Trim();

            if (string.IsNullOrWhiteSpace(barcode))
            {
                MessageBox.Show(
                    "Vui lòng nhập hoặc quét mã vạch.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtBarcode.Focus();
                return;
            }

            txtBarcode.Clear();

            await AddProductToCartByBarcodeAsync(barcode);

            if (!IsDisposed && txtBarcode.CanFocus)
                txtBarcode.Focus();
        }

        private async Task AddProductToCartByBarcodeAsync(
            string barcode)
        {
            if (isSearchingBarcode || isCheckingOut)
                return;

            isSearchingBarcode = true;

            try
            {
                ApiClientService.SetAuthenticationToken();

                string encodedBarcode =
                    Uri.EscapeDataString(barcode);

                using (HttpResponseMessage response =
                    await ApiClientService.Client.GetAsync(
                        "products/barcode/" + encodedBarcode))
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        MessageBox.Show(
                            "Không tìm thấy sản phẩm có mã vạch: "
                            + barcode,
                            "Không tìm thấy sản phẩm",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        MessageBox.Show(
                            "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.",
                            "Chưa xác thực",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    if (response.StatusCode == HttpStatusCode.Forbidden)
                    {
                        MessageBox.Show(
                            "Tài khoản không có quyền tra cứu sản phẩm.",
                            "Không có quyền",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        string error =
                            await response.Content.ReadAsStringAsync();

                        MessageBox.Show(
                            "API tra cứu mã vạch bị lỗi.\nHTTP: "
                            + (int)response.StatusCode
                            + "\nChi tiết: " + error,
                            "Lỗi API",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    ProductDto product =
                        await response.Content.ReadFromJsonAsync<ProductDto>();

                    if (product == null || product.ProductId <= 0)
                    {
                        MessageBox.Show(
                            "Dữ liệu sản phẩm không hợp lệ.",
                            "Lỗi dữ liệu",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    if (product.StockQuantity <= 0)
                    {
                        MessageBox.Show(
                            "Sản phẩm \"" + product.ProductName
                            + "\" đã hết hàng.",
                            "Hết hàng",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    CartItemDto existingItem = _cart.FirstOrDefault(
                        x => x.ProductId == product.ProductId);

                    int quantityInCart = existingItem == null
                        ? 0
                        : existingItem.Quantity;

                    if (quantityInCart >= product.StockQuantity)
                    {
                        MessageBox.Show(
                            "Số lượng trong giỏ đã đạt tồn kho hiện tại: "
                            + product.StockQuantity,
                            "Không đủ tồn kho",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    if (existingItem != null)
                    {
                        existingItem.StockQuantity = product.StockQuantity;
                        existingItem.Quantity++;
                    }
                    else
                    {
                        _cart.Add(new CartItemDto
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            UnitPrice = product.Price,
                            Quantity = 1,
                            StockQuantity = product.StockQuantity
                        });
                    }

                    UpdateCartDisplay();
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    "Không kết nối được API.\n" + ex.Message,
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (System.Text.Json.JsonException ex)
            {
                MessageBox.Show(
                    "Dữ liệu sản phẩm trả về không đúng định dạng.\n"
                    + ex.Message,
                    "Lỗi dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tra cứu mã vạch:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                isSearchingBarcode = false;
            }
        }

        // =====================================================
        // 3. CẬP NHẬT GIỎ HÀNG
        // =====================================================

        private void UpdateCartDisplay()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart.ToList();

            decimal total = _cart.Sum(x => x.TotalPrice);
            lblTotalAmount.Text = $"{total:N0} đ";
            lblTotalTitle.Text = $"TỔNG THANH TOÁN  ·  {_cart.Sum(x => x.Quantity)} SP";
            UpdatePosStats();

            CalculateChange();
        }

        private void dgvCart_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Cot so luong da duoc khoa; dieu chinh bang hai nut +/- trong tung dong.
        }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string columnName = dgvCart.Columns[e.ColumnIndex].Name;
            if (columnName != "colDecrease" && columnName != "colIncrease")
                return;

            if (!int.TryParse(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["colProductId"].Value), out int productId))
                return;

            CartItemDto item = _cart.FirstOrDefault(x => x.ProductId == productId);
            if (item == null)
                return;

            if (columnName == "colIncrease")
            {
                if (item.StockQuantity > 0 && item.Quantity >= item.StockQuantity)
                {
                    MessageBox.Show(
                        "Số lượng đã đạt mức tồn kho hiện tại (" + item.StockQuantity + ").",
                        "Không đủ tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                item.Quantity++;
            }
            else
            {
                // Không cho số lượng thấp hơn 1; dùng nút xóa sản phẩm nếu muốn bỏ khỏi giỏ.
                if (item.Quantity <= 1)
                    return;
                item.Quantity--;
            }

            UpdateCartDisplay();
        }

        private void txtCustomerPhone_TextChanged(object sender, EventArgs e)
        {
            string phone = (txtCustomerPhone.Text ?? string.Empty).Trim();
            customerLookupTimer?.Stop();
            lastCustomerLookupPhone = string.Empty;
            resolvedCustomerName = string.Empty;
            resolvedCustomerRank = string.Empty;

            if (phone.Length == 0)
            {
                lblCustomerName.Text = "Khách vãng lai";
                lblCustomerName.ForeColor = Color.FromArgb(100, 116, 139);
                return;
            }

            lblCustomerName.Text = "Đang tìm khách hàng...";
            lblCustomerName.ForeColor = Color.FromArgb(37, 99, 235);
            if (phone.Count(char.IsDigit) >= 9)
                customerLookupTimer?.Start();
        }

        private async Task LookupCustomerByPhoneAsync()
        {
            string phone = (txtCustomerPhone.Text ?? string.Empty).Trim();
            if (phone.Length == 0 || phone.Count(char.IsDigit) < 9 || isCheckingOut)
                return;

            try
            {
                ApiClientService.SetAuthenticationToken();
                string uri = "customers/search?keyword=" + Uri.EscapeDataString(phone);
                using (HttpResponseMessage response = await ApiClientService.Client.GetAsync(uri))
                {
                    if (!response.IsSuccessStatusCode) return;
                    string json = await response.Content.ReadAsStringAsync();
                    using (JsonDocument doc = JsonDocument.Parse(json))
                    {
                        if (doc.RootElement.ValueKind != JsonValueKind.Array) return;
                        foreach (JsonElement customer in doc.RootElement.EnumerateArray())
                        {
                            string foundPhone = ReadJsonString(customer, "PhoneNumber", "phoneNumber", "phone");
                            if (NormalizePhone(foundPhone) != NormalizePhone(phone)) continue;

                            resolvedCustomerName = ReadJsonString(customer, "CustomerName", "customerName", "name");
                            resolvedCustomerRank = ReadJsonString(customer, "MembershipRank", "membershipRank", "rank");
                            lastCustomerLookupPhone = phone;
                            if (!string.IsNullOrWhiteSpace(resolvedCustomerName))
                            {
                                string rankText = string.IsNullOrWhiteSpace(resolvedCustomerRank) ? "Thành viên" : "Hạng " + resolvedCustomerRank;
                                lblCustomerName.Text = resolvedCustomerName + " · " + rankText;
                                lblCustomerName.ForeColor = Color.FromArgb(13, 148, 136);
                            }
                            return;
                        }
                    }
                }

                // Chưa tìm thấy số điện thoại chính xác trong hệ thống.
                if (phone == (txtCustomerPhone.Text ?? string.Empty).Trim())
                {
                    lblCustomerName.Text = "Chưa có thông tin khách hàng";
                    lblCustomerName.ForeColor = Color.FromArgb(217, 119, 6);
                }
            }
            catch
            {
                // Không chặn thao tác bán hàng nếu API tra cứu khách hàng lỗi.
                if (!IsDisposed && phone == (txtCustomerPhone.Text ?? string.Empty).Trim())
                {
                    lblCustomerName.Text = "Không tra cứu được khách hàng";
                    lblCustomerName.ForeColor = Color.FromArgb(100, 116, 139);
                }
            }
        }

        private static string ReadJsonString(JsonElement element, params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                if (element.TryGetProperty(propertyName, out JsonElement value) && value.ValueKind == JsonValueKind.String)
                    return value.GetString() ?? string.Empty;
                foreach (JsonProperty property in element.EnumerateObject())
                {
                    if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase) && property.Value.ValueKind == JsonValueKind.String)
                        return property.Value.GetString() ?? string.Empty;
                }
            }
            return string.Empty;
        }

        private static string NormalizePhone(string value)
        {
            return new string((value ?? string.Empty).Where(char.IsDigit).ToArray());
        }

        // =====================================================
        // 4. XÓA MỘT HOẶC NHIỀU SẢN PHẨM ĐÃ CHỌN
        // =====================================================

        private void btnRemoveSelected_Click(
            object sender, EventArgs e)
        {
            RemoveSelectedCartItems();
        }

        private void RemoveSelectedCartItems()
        {
            if (isSearchingBarcode || isCheckingOut)
            {
                MessageBox.Show(
                    "Hệ thống đang xử lý. Vui lòng đợi.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn một hoặc nhiều sản phẩm cần xóa.\n\n"
                    + "Giữ Ctrl và nhấp vào các dòng để chọn nhiều sản phẩm.",
                    "Chưa chọn sản phẩm",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã các sản phẩm được chọn trước khi cập nhật bảng.
            HashSet<int> selectedIds = new HashSet<int>();

            foreach (DataGridViewRow row in dgvCart.SelectedRows)
            {
                object value = row.Cells["colProductId"].Value;

                if (value != null &&
                    int.TryParse(value.ToString(), out int productId))
                {
                    selectedIds.Add(productId);
                }
            }

            List<CartItemDto> selectedItems = _cart
                .Where(x => selectedIds.Contains(x.ProductId))
                .ToList();

            if (selectedItems.Count == 0)
            {
                MessageBox.Show(
                    "Không tìm thấy sản phẩm được chọn.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string names = string.Join(
                "\n",
                selectedItems.Select(x =>
                    "- " + x.ProductName + " (SL: " + x.Quantity + ")"));

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa " + selectedItems.Count
                + " sản phẩm sau khỏi giỏ hàng?\n\n"
                + names,
                "Xác nhận xóa sản phẩm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            foreach (CartItemDto item in selectedItems)
            {
                _cart.Remove(item);
            }

            UpdateCartDisplay();

            MessageBox.Show(
                "Đã xóa " + selectedItems.Count
                + " sản phẩm khỏi giỏ hàng.",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtBarcode.Focus();
        }

        // =====================================================
        // 5. HỦY TOÀN BỘ GIỎ HÀNG
        // =====================================================

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (isSearchingBarcode || isCheckingOut)
            {
                MessageBox.Show(
                    "Hệ thống đang xử lý. Vui lòng đợi.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (_cart.Count == 0)
            {
                MessageBox.Show(
                    "Giỏ hàng đang trống.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn hủy TOÀN BỘ giỏ hàng không?",
                "Xác nhận hủy giỏ hàng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            _cart.Clear();
            UpdateCartDisplay();

            txtCashReceived.Clear();
            txtCustomerPhone.Clear();
            lblCustomerName.Text = "Khách vãng lai";
            lblCustomerName.ForeColor = Color.FromArgb(100, 116, 139);
            resolvedCustomerName = string.Empty;
            resolvedCustomerRank = string.Empty;
            lastCustomerLookupPhone = string.Empty;
            txtBarcode.Clear();
            txtBarcode.Focus();

            MessageBox.Show(
                "Đã hủy toàn bộ giỏ hàng thành công!",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =====================================================
        // 6. TÍNH TIỀN THỪA
        // =====================================================

        private void txtCashReceived_TextChanged(
            object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void CalculateChange()
        {
            if (lblChange == null || txtCashReceived == null)
                return;

            string paymentMethod = cboPaymentMethod == null || cboPaymentMethod.SelectedItem == null
                ? "Tiền mặt" : cboPaymentMethod.SelectedItem.ToString();
            if (!string.Equals(paymentMethod, "Tiền mặt", StringComparison.OrdinalIgnoreCase))
            {
                lblChangeTitle.Text = "Trạng thái thanh toán:";
                lblChange.Text = _cart.Count > 0 ? "Cần xác nhận giao dịch" : "Không áp dụng";
                lblChange.ForeColor = _cart.Count > 0
                    ? Color.FromArgb(217, 119, 6) : Color.FromArgb(71, 85, 105);
                return;
            }

            decimal total = _cart.Sum(x => x.TotalPrice);

            if (string.IsNullOrWhiteSpace(txtCashReceived.Text))
            {
                lblChange.Text = total > 0 ? "Chưa đủ tiền!" : "0 đ";
                lblChange.ForeColor =
                    total > 0 ? Color.Red : Color.Black;
                return;
            }

            if (decimal.TryParse(
                txtCashReceived.Text.Trim(),
                out decimal cashReceived))
            {
                decimal change = cashReceived - total;

                if (change >= 0)
                {
                    lblChange.Text = $"{change:N0} đ";
                    lblChange.ForeColor = Color.Black;
                }
                else
                {
                    lblChange.Text = "Chưa đủ tiền!";
                    lblChange.ForeColor = Color.Red;
                }
            }
            else
            {
                lblChange.Text = "Số tiền không hợp lệ";
                lblChange.ForeColor = Color.Red;
            }
        }

        // =====================================================
        // 7. THANH TOÁN
        // =====================================================

        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (isCheckingOut || isSearchingBarcode) return;

            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = _cart.Sum(x => x.TotalPrice);
            string paymentMethod = cboPaymentMethod == null || cboPaymentMethod.SelectedItem == null
                ? "Tiền mặt" : cboPaymentMethod.SelectedItem.ToString();
            bool cashMode = string.Equals(paymentMethod, "Tiền mặt", StringComparison.OrdinalIgnoreCase);
            decimal cashReceived = 0m;

            if (cashMode)
            {
                if (!decimal.TryParse((txtCashReceived.Text ?? string.Empty).Trim(), out cashReceived))
                {
                    MessageBox.Show("Vui lòng nhập số tiền khách đưa hợp lệ.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCashReceived.Focus();
                    return;
                }
                if (cashReceived < total)
                {
                    MessageBox.Show("Tiền khách đưa chưa đủ thanh toán.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCashReceived.Focus();
                    return;
                }
            }
            else
            {
                // Bank/card is processed outside this sample application. Do not
                // record an order until the cashier confirms the external payment.
                if (!ConfirmExternalPayment(paymentMethod, total)) return;
            }

            List<CartItemDto> receiptItems = _cart.Select(i => new CartItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList();
            string customerPhone = (txtCustomerPhone.Text ?? string.Empty).Trim();

            var orderRequest = new
            {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = customerPhone,
                PaymentMethod = paymentMethod,
                ExternalPaymentConfirmed = !cashMode,
                Items = _cart.Select(i => new
                {
                    i.ProductId,
                    i.Quantity,
                    i.UnitPrice
                }).ToList()
            };

            isCheckingOut = true;
            btnCheckout.Enabled = false;
            btnClearCart.Enabled = false;
            btnRemoveSelected.Enabled = false;
            if (cboPaymentMethod != null) cboPaymentMethod.Enabled = false;

            try
            {
                ApiClientService.SetAuthenticationToken();
                using (HttpResponseMessage response = await ApiClientService.Client.PostAsJsonAsync("orders/checkout", orderRequest))
                {
                    string responseJson = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        int orderId = 0;
                        decimal serverTotal = total;
                        try
                        {
                            using (JsonDocument document = JsonDocument.Parse(responseJson))
                            {
                                JsonElement root = document.RootElement;
                                JsonElement value;
                                if ((root.TryGetProperty("orderId", out value) || root.TryGetProperty("OrderId", out value)) && value.TryGetInt32(out int parsedId))
                                    orderId = parsedId;
                                if ((root.TryGetProperty("totalAmount", out value) || root.TryGetProperty("TotalAmount", out value)) && value.TryGetDecimal(out decimal parsedTotal))
                                    serverTotal = parsedTotal;
                            }
                        }
                        catch (JsonException) { /* Keep local total if a proxy returns no JSON. */ }

                        _cart.Clear();
                        UpdateCartDisplay();
                        txtCashReceived.Clear();
                        txtCustomerPhone.Clear();
                        lblCustomerName.Text = "Khách vãng lai";
                        lblCustomerName.ForeColor = Color.FromArgb(100, 116, 139);
                        resolvedCustomerName = string.Empty;
                        resolvedCustomerRank = string.Empty;
                        lastCustomerLookupPhone = string.Empty;
                        txtBarcode.Clear();
                        UpdatePaymentMode();

                        MessageBox.Show(
                            "Đã ghi nhận hóa đơn #" + orderId + "\nPhương thức: " + paymentMethod +
                            "\nTổng tiền: " + serverTotal.ToString("N0") + " đ",
                            "Thanh toán thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowReceiptPreview(orderId, receiptItems, serverTotal, customerPhone, paymentMethod, cashMode ? (decimal?)cashReceived : null);
                        if (txtBarcode.CanFocus) txtBarcode.Focus();
                    }
                    else
                    {
                        string message = "Thanh toán thất bại. HTTP " + (int)response.StatusCode + "\n" + responseJson;
                        if (!cashMode)
                        {
                            message += "\n\nLƯU Ý: Bạn đã xác nhận giao dịch ngoài hệ thống. Hãy đối chiếu ngân hàng/máy POS trước khi thu tiền lần nữa.";
                        }
                        MessageBox.Show(message, "Lỗi thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                string message = "Không kết nối được máy chủ khi thanh toán.\n" + ex.Message;
                if (!cashMode)
                    message += "\n\nNếu tiền đã được chuyển hoặc máy POS đã duyệt, hãy kiểm tra trạng thái trước khi thử lại để tránh thu tiền hai lần.";
                MessageBox.Show(message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isCheckingOut = false;
                if (!IsDisposed)
                {
                    btnCheckout.Enabled = true;
                    btnClearCart.Enabled = true;
                    btnRemoveSelected.Enabled = true;
                    if (cboPaymentMethod != null) cboPaymentMethod.Enabled = true;
                }
            }
        }

        private bool ConfirmExternalPayment(string paymentMethod, decimal total)
        {
            string instructions;
            if (string.Equals(paymentMethod, "Chuyển khoản", StringComparison.OrdinalIgnoreCase))
            {
                instructions = "PHƯƠNG THỨC: CHUYỂN KHOẢN\nSố tiền cần nhận: " + total.ToString("N0") + " đ\n\n" +
                    "Hãy kiểm tra ứng dụng ngân hàng/tài khoản cửa hàng và chỉ chọn Yes sau khi thấy tiền đã vào đủ.\n\n" +
                    "MiniMart chưa kết nối trực tiếp với ngân hàng nên không tự xác minh tiền chuyển đến.";
            }
            else
            {
                instructions = "PHƯƠNG THỨC: THẺ POS\nSố tiền cần thanh toán: " + total.ToString("N0") + " đ\n\n" +
                    "Thực hiện giao dịch trên máy POS/terminal. Chỉ chọn Yes khi máy POS báo giao dịch thành công.\n\n" +
                    "MiniMart chưa kết nối SDK của máy POS nên không tự trừ tiền trên thẻ.";
            }

            return MessageBox.Show(this, instructions + "\n\nBạn xác nhận giao dịch bên ngoài đã thành công?",
                "Xác nhận thanh toán ngoài hệ thống", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        private void ShowReceiptPreview(int orderId, List<CartItemDto> items, decimal total,
            string customerPhone, string paymentMethod, decimal? cashReceived)
        {
            try
            {
                using (PrintDocument document = new PrintDocument())
                {
                    document.DocumentName = "Hóa đơn MiniMart #" + orderId;
                    document.DefaultPageSettings.Margins = new Margins(35, 35, 35, 35);
                    document.PrintPage += (sender, e) =>
                    {
                        Graphics graphics = e.Graphics;
                        float y = 20;
                        using (Font titleFont = new Font("Segoe UI", 16F, FontStyle.Bold))
                        using (Font bodyFont = new Font("Segoe UI", 9.5F))
                        using (Font boldFont = new Font("Segoe UI", 9.5F, FontStyle.Bold))
                        using (Brush brush = new SolidBrush(Color.FromArgb(30, 41, 59)))
                        using (Pen linePen = new Pen(Color.FromArgb(148, 163, 184)))
                        {
                            graphics.DrawString("MINIMART", titleFont, brush, 20, y); y += 32;
                            graphics.DrawString("HÓA ĐƠN BÁN HÀNG", boldFont, brush, 20, y); y += 24;
                            graphics.DrawString("Mã hóa đơn: " + orderId, bodyFont, brush, 20, y); y += 20;
                            graphics.DrawString("Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), bodyFont, brush, 20, y); y += 20;
                            graphics.DrawString("Thu ngân: " + SessionManager.CurrentUsername, bodyFont, brush, 20, y); y += 20;
                            graphics.DrawString("Khách hàng: " + (string.IsNullOrWhiteSpace(customerPhone) ? "Khách vãng lai" : (string.IsNullOrWhiteSpace(resolvedCustomerName) ? customerPhone : resolvedCustomerName + (string.IsNullOrWhiteSpace(resolvedCustomerRank) ? string.Empty : " - Hạng " + resolvedCustomerRank))), bodyFont, brush, 20, y); y += 20;
                            graphics.DrawString("Phương thức: " + paymentMethod, bodyFont, brush, 20, y); y += 22;
                            graphics.DrawLine(linePen, 20, y, 520, y); y += 10;
                            foreach (CartItemDto item in items)
                            {
                                graphics.DrawString(item.ProductName, bodyFont, brush, 20, y);
                                graphics.DrawString(item.Quantity + " x " + item.UnitPrice.ToString("N0"), bodyFont, brush, 300, y);
                                graphics.DrawString(item.TotalPrice.ToString("N0") + " đ", bodyFont, brush, 430, y);
                                y += 22;
                            }
                            y += 5;
                            graphics.DrawLine(linePen, 20, y, 520, y); y += 12;
                            graphics.DrawString("TỔNG CỘNG", boldFont, brush, 20, y);
                            graphics.DrawString(total.ToString("N0") + " đ", titleFont, brush, 350, y); y += 34;
                            if (cashReceived.HasValue)
                            {
                                graphics.DrawString("Tiền khách đưa: " + cashReceived.Value.ToString("N0") + " đ", bodyFont, brush, 20, y); y += 20;
                                graphics.DrawString("Tiền thừa: " + (cashReceived.Value - total).ToString("N0") + " đ", bodyFont, brush, 20, y); y += 20;
                            }
                            graphics.DrawString("Cảm ơn quý khách và hẹn gặp lại!", bodyFont, brush, 20, y);
                        }
                        e.HasMorePages = false;
                    };

                    using (PrintPreviewDialog preview = new PrintPreviewDialog())
                    {
                        preview.Document = document;
                        preview.Width = 900;
                        preview.Height = 700;
                        preview.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã ghi nhận hóa đơn nhưng không mở được bản xem trước.\n" + ex.Message,
                    "Hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int StockQuantity { get; set; }

        public decimal TotalPrice => UnitPrice * Quantity;
    }

    public class ProductDto
    {
        public int ProductId { get; set; }

        public string Barcode { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
    }
}
