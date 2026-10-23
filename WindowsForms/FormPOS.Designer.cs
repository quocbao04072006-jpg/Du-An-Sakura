
namespace MiniSupermarket.WinForms
{
    partial class FormPOS
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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblChangeTitle = new System.Windows.Forms.Label();
            this.txtCashReceived = new System.Windows.Forms.TextBox();
            this.lblCashTitle = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalTitle = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtCustomerPhone = new System.Windows.Forms.TextBox();
            this.lblCustPhoneTitle = new System.Windows.Forms.Label();

            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();

            // panelLeft
            this.panelLeft.Controls.Add(this.dgvCart);
            this.panelLeft.Controls.Add(this.txtBarcode);
            this.panelLeft.Controls.Add(this.lblBarcode);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(510, 600);
            this.panelLeft.TabIndex = 0;

            // dgvCart
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Location = new System.Drawing.Point(15, 57);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.RowTemplate.Height = 24;
            this.dgvCart.Size = new System.Drawing.Size(480, 528);
            this.dgvCart.TabIndex = 2;

            // txtBarcode
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBarcode.Location = new System.Drawing.Point(130, 16);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(365, 27);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.KeyDown +=
                new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);

            // lblBarcode
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font =
                new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBarcode.Location = new System.Drawing.Point(15, 19);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(108, 20);
            this.lblBarcode.TabIndex = 0;
            this.lblBarcode.Text = "Quét Barcode:";

            // panelRight
            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Controls.Add(this.btnClearCart);
            this.panelRight.Controls.Add(this.btnRemoveSelected);
            this.panelRight.Controls.Add(this.btnCheckout);
            this.panelRight.Controls.Add(this.lblChange);
            this.panelRight.Controls.Add(this.lblChangeTitle);
            this.panelRight.Controls.Add(this.txtCashReceived);
            this.panelRight.Controls.Add(this.lblCashTitle);
            this.panelRight.Controls.Add(this.lblTotalAmount);
            this.panelRight.Controls.Add(this.lblTotalTitle);
            this.panelRight.Controls.Add(this.lblCustomerName);
            this.panelRight.Controls.Add(this.txtCustomerPhone);
            this.panelRight.Controls.Add(this.lblCustPhoneTitle);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(510, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(278, 600);
            this.panelRight.TabIndex = 1;

            // btnClearCart
            this.btnClearCart.BackColor =
                System.Drawing.Color.FromArgb(190, 45, 45);
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearCart.ForeColor = System.Drawing.Color.White;
            this.btnClearCart.Location = new System.Drawing.Point(15, 535);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(248, 40);
            this.btnClearCart.TabIndex = 12;
            this.btnClearCart.Text = "HỦY TOÀN BỘ GIỎ HÀNG";
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Click +=
                new System.EventHandler(this.btnClearCart_Click);

            // btnRemoveSelected
            this.btnRemoveSelected.BackColor =
                System.Drawing.Color.FromArgb(230, 126, 34);
            this.btnRemoveSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveSelected.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRemoveSelected.ForeColor = System.Drawing.Color.White;
            this.btnRemoveSelected.Location = new System.Drawing.Point(15, 485);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(248, 40);
            this.btnRemoveSelected.TabIndex = 11;
            this.btnRemoveSelected.Text = "XÓA SẢN PHẨM ĐÃ CHỌN";
            this.btnRemoveSelected.UseVisualStyleBackColor = false;
            this.btnRemoveSelected.Click +=
                new System.EventHandler(this.btnRemoveSelected_Click);

            // btnCheckout
            this.btnCheckout.BackColor =
                System.Drawing.Color.FromArgb(15, 157, 88);
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font =
                new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(15, 425);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(248, 49);
            this.btnCheckout.TabIndex = 9;
            this.btnCheckout.Text = "THANH TOÁN (F9)";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click +=
                new System.EventHandler(this.btnCheckout_Click);

            // lblChange
            this.lblChange.Font =
                new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblChange.ForeColor = System.Drawing.Color.Blue;
            this.lblChange.Location = new System.Drawing.Point(15, 358);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(248, 32);
            this.lblChange.TabIndex = 8;
            this.lblChange.Text = "0 đ";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // lblChangeTitle
            this.lblChangeTitle.AutoSize = true;
            this.lblChangeTitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblChangeTitle.Location = new System.Drawing.Point(15, 333);
            this.lblChangeTitle.Name = "lblChangeTitle";
            this.lblChangeTitle.Size = new System.Drawing.Size(96, 20);
            this.lblChangeTitle.TabIndex = 7;
            this.lblChangeTitle.Text = "Tiền thừa trả:";

            // txtCashReceived
            this.txtCashReceived.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtCashReceived.Location = new System.Drawing.Point(15, 292);
            this.txtCashReceived.Name = "txtCashReceived";
            this.txtCashReceived.Size = new System.Drawing.Size(248, 29);
            this.txtCashReceived.TabIndex = 6;
            this.txtCashReceived.TextChanged +=
                new System.EventHandler(this.txtCashReceived_TextChanged);

            // lblCashTitle
            this.lblCashTitle.AutoSize = true;
            this.lblCashTitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblCashTitle.Location = new System.Drawing.Point(15, 268);
            this.lblCashTitle.Name = "lblCashTitle";
            this.lblCashTitle.Size = new System.Drawing.Size(112, 20);
            this.lblCashTitle.TabIndex = 5;
            this.lblCashTitle.Text = "Tiền khách đưa:";

            // lblTotalAmount
            this.lblTotalAmount.Font =
                new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor =
                System.Drawing.Color.FromArgb(219, 68, 55);
            this.lblTotalAmount.Location = new System.Drawing.Point(15, 203);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(248, 41);
            this.lblTotalAmount.TabIndex = 4;
            this.lblTotalAmount.Text = "0 đ";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // lblTotalTitle
            this.lblTotalTitle.AutoSize = true;
            this.lblTotalTitle.Font =
                new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalTitle.Location = new System.Drawing.Point(15, 175);
            this.lblTotalTitle.Name = "lblTotalTitle";
            this.lblTotalTitle.Size = new System.Drawing.Size(99, 21);
            this.lblTotalTitle.TabIndex = 3;
            this.lblTotalTitle.Text = "TỔNG TIỀN:";

            // lblCustomerName
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font =
                new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblCustomerName.Location = new System.Drawing.Point(15, 98);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(102, 19);
            this.lblCustomerName.TabIndex = 2;
            this.lblCustomerName.Text = "Khách vãng lai";

            // txtCustomerPhone
            this.txtCustomerPhone.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCustomerPhone.Location = new System.Drawing.Point(15, 57);
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.Size = new System.Drawing.Size(248, 27);
            this.txtCustomerPhone.TabIndex = 1;

            // lblCustPhoneTitle
            this.lblCustPhoneTitle.AutoSize = true;
            this.lblCustPhoneTitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblCustPhoneTitle.Location = new System.Drawing.Point(15, 24);
            this.lblCustPhoneTitle.Name = "lblCustPhoneTitle";
            this.lblCustPhoneTitle.Size = new System.Drawing.Size(120, 20);
            this.lblCustPhoneTitle.TabIndex = 0;
            this.lblCustPhoneTitle.Text = "SĐT Khách hàng:";

            // FormPOS
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 600);
            this.MinimumSize = new System.Drawing.Size(804, 639);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Name = "FormPOS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quầy thanh toán";

            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.TextBox txtCustomerPhone;
        private System.Windows.Forms.Label lblCustPhoneTitle;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblTotalTitle;
        private System.Windows.Forms.TextBox txtCashReceived;
        private System.Windows.Forms.Label lblCashTitle;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Label lblChangeTitle;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnClearCart;
    }
}
