namespace MiniSupermarket.WinForms
{
    partial class FormCustomerTransactionHistory
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblCustomerPointsValue = new System.Windows.Forms.Label();
            this.lblCustomerRankValue = new System.Windows.Forms.Label();
            this.lblCustomerPhoneValue = new System.Windows.Forms.Label();
            this.lblCustomerIdValue = new System.Windows.Forms.Label();
            this.lblCustomerNameValue = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderSubTitle = new System.Windows.Forms.Label();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.cardAverage = new System.Windows.Forms.Panel();
            this.lblAverageValue = new System.Windows.Forms.Label();
            this.lblAverageTitle = new System.Windows.Forms.Label();
            this.cardTotal = new System.Windows.Forms.Panel();
            this.lblTotalSpentValue = new System.Windows.Forms.Label();
            this.lblTotalSpentTitle = new System.Windows.Forms.Label();
            this.cardCount = new System.Windows.Forms.Panel();
            this.lblTransactionCountValue = new System.Windows.Forms.Label();
            this.lblTransactionCountTitle = new System.Windows.Forms.Label();
            this.panelHistory = new System.Windows.Forms.Panel();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblSelectedTransaction = new System.Windows.Forms.Label();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.dgvTransactionDetails = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();

            this.panelHeader.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.cardAverage.SuspendLayout();
            this.cardTotal.SuspendLayout();
            this.cardCount.SuspendLayout();
            this.panelHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.panelDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactionDetails)).BeginInit();
            this.SuspendLayout();

            // panelHeader
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Controls.Add(this.btnRefresh);
            this.panelHeader.Controls.Add(this.lblCustomerPointsValue);
            this.panelHeader.Controls.Add(this.lblCustomerRankValue);
            this.panelHeader.Controls.Add(this.lblCustomerPhoneValue);
            this.panelHeader.Controls.Add(this.lblCustomerIdValue);
            this.panelHeader.Controls.Add(this.lblCustomerNameValue);
            this.panelHeader.Controls.Add(this.lblHeaderTitle);
            this.panelHeader.Controls.Add(this.lblHeaderSubTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1100, 115);
            this.panelHeader.TabIndex = 0;

            // lblHeaderTitle
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font =
                new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor =
                System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblHeaderTitle.Location = new System.Drawing.Point(20, 14);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(330, 31);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "LỊCH SỬ GIAO DỊCH KHÁCH HÀNG";

            // lblHeaderSubTitle
            this.lblHeaderSubTitle.AutoSize = true;
            this.lblHeaderSubTitle.Font =
                new System.Drawing.Font("Segoe UI", 9F);
            this.lblHeaderSubTitle.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHeaderSubTitle.Location = new System.Drawing.Point(22, 48);
            this.lblHeaderSubTitle.Name = "lblHeaderSubTitle";
            this.lblHeaderSubTitle.Size = new System.Drawing.Size(300, 15);
            this.lblHeaderSubTitle.TabIndex = 1;
            this.lblHeaderSubTitle.Text = "Theo dõi các hóa đơn và chi tiết mua hàng của khách hàng";

            // lblCustomerNameValue
            this.lblCustomerNameValue.AutoSize = true;
            this.lblCustomerNameValue.Font =
                new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCustomerNameValue.ForeColor =
                System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblCustomerNameValue.Location = new System.Drawing.Point(22, 79);
            this.lblCustomerNameValue.Name = "lblCustomerNameValue";
            this.lblCustomerNameValue.Size = new System.Drawing.Size(90, 19);
            this.lblCustomerNameValue.TabIndex = 2;
            this.lblCustomerNameValue.Text = "Khách hàng";

            // lblCustomerIdValue
            this.lblCustomerIdValue.AutoSize = true;
            this.lblCustomerIdValue.Font =
                new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomerIdValue.ForeColor =
                System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCustomerIdValue.Location = new System.Drawing.Point(180, 81);
            this.lblCustomerIdValue.Name = "lblCustomerIdValue";
            this.lblCustomerIdValue.Size = new System.Drawing.Size(45, 15);
            this.lblCustomerIdValue.TabIndex = 3;
            this.lblCustomerIdValue.Text = "Mã KH: -";

            // lblCustomerPhoneValue
            this.lblCustomerPhoneValue.AutoSize = true;
            this.lblCustomerPhoneValue.Font =
                new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomerPhoneValue.ForeColor =
                System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCustomerPhoneValue.Location = new System.Drawing.Point(270, 81);
            this.lblCustomerPhoneValue.Name = "lblCustomerPhoneValue";
            this.lblCustomerPhoneValue.Size = new System.Drawing.Size(53, 15);
            this.lblCustomerPhoneValue.TabIndex = 4;
            this.lblCustomerPhoneValue.Text = "SĐT: -";

            // lblCustomerRankValue
            this.lblCustomerRankValue.AutoSize = true;
            this.lblCustomerRankValue.Font =
                new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomerRankValue.ForeColor =
                System.Drawing.Color.FromArgb(124, 58, 237);
            this.lblCustomerRankValue.Location = new System.Drawing.Point(420, 81);
            this.lblCustomerRankValue.Name = "lblCustomerRankValue";
            this.lblCustomerRankValue.Size = new System.Drawing.Size(57, 15);
            this.lblCustomerRankValue.TabIndex = 5;
            this.lblCustomerRankValue.Text = "Hạng: -";

            // lblCustomerPointsValue
            this.lblCustomerPointsValue.AutoSize = true;
            this.lblCustomerPointsValue.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomerPointsValue.ForeColor =
                System.Drawing.Color.FromArgb(5, 150, 105);
            this.lblCustomerPointsValue.Location = new System.Drawing.Point(510, 81);
            this.lblCustomerPointsValue.Name = "lblCustomerPointsValue";
            this.lblCustomerPointsValue.Size = new System.Drawing.Size(65, 15);
            this.lblCustomerPointsValue.TabIndex = 6;
            this.lblCustomerPointsValue.Text = "0 điểm";

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(875, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(95, 34);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = true;

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(978, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 34);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;

            // panelSummary
            this.panelSummary.BackColor =
                System.Drawing.Color.FromArgb(241, 245, 249);
            this.panelSummary.Controls.Add(this.cardAverage);
            this.panelSummary.Controls.Add(this.cardTotal);
            this.panelSummary.Controls.Add(this.cardCount);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummary.Location = new System.Drawing.Point(0, 115);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(1100, 90);
            this.panelSummary.TabIndex = 1;

            // cardCount
            this.cardCount.BackColor = System.Drawing.Color.White;
            this.cardCount.Controls.Add(this.lblTransactionCountValue);
            this.cardCount.Controls.Add(this.lblTransactionCountTitle);
            this.cardCount.Location = new System.Drawing.Point(20, 12);
            this.cardCount.Name = "cardCount";
            this.cardCount.Size = new System.Drawing.Size(330, 65);
            this.cardCount.TabIndex = 0;

            // lblTransactionCountTitle
            this.lblTransactionCountTitle.AutoSize = true;
            this.lblTransactionCountTitle.Font =
                new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTransactionCountTitle.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTransactionCountTitle.Location = new System.Drawing.Point(14, 9);
            this.lblTransactionCountTitle.Name = "lblTransactionCountTitle";
            this.lblTransactionCountTitle.Size = new System.Drawing.Size(112, 15);
            this.lblTransactionCountTitle.TabIndex = 0;
            this.lblTransactionCountTitle.Text = "SỐ LẦN GIAO DỊCH";

            // lblTransactionCountValue
            this.lblTransactionCountValue.AutoSize = true;
            this.lblTransactionCountValue.Font =
                new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblTransactionCountValue.ForeColor =
                System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblTransactionCountValue.Location = new System.Drawing.Point(14, 28);
            this.lblTransactionCountValue.Name = "lblTransactionCountValue";
            this.lblTransactionCountValue.Size = new System.Drawing.Size(29, 36);
            this.lblTransactionCountValue.TabIndex = 1;
            this.lblTransactionCountValue.Text = "0";

            // cardTotal
            this.cardTotal.BackColor = System.Drawing.Color.White;
            this.cardTotal.Controls.Add(this.lblTotalSpentValue);
            this.cardTotal.Controls.Add(this.lblTotalSpentTitle);
            this.cardTotal.Location = new System.Drawing.Point(365, 12);
            this.cardTotal.Name = "cardTotal";
            this.cardTotal.Size = new System.Drawing.Size(330, 65);
            this.cardTotal.TabIndex = 1;

            // lblTotalSpentTitle
            this.lblTotalSpentTitle.AutoSize = true;
            this.lblTotalSpentTitle.Font =
                new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTotalSpentTitle.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTotalSpentTitle.Location = new System.Drawing.Point(14, 9);
            this.lblTotalSpentTitle.Name = "lblTotalSpentTitle";
            this.lblTotalSpentTitle.Size = new System.Drawing.Size(105, 15);
            this.lblTotalSpentTitle.TabIndex = 0;
            this.lblTotalSpentTitle.Text = "TỔNG CHI TIÊU";

            // lblTotalSpentValue
            this.lblTotalSpentValue.AutoSize = true;
            this.lblTotalSpentValue.Font =
                new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblTotalSpentValue.ForeColor =
                System.Drawing.Color.FromArgb(217, 119, 6);
            this.lblTotalSpentValue.Location = new System.Drawing.Point(14, 28);
            this.lblTotalSpentValue.Name = "lblTotalSpentValue";
            this.lblTotalSpentValue.Size = new System.Drawing.Size(55, 36);
            this.lblTotalSpentValue.TabIndex = 1;
            this.lblTotalSpentValue.Text = "0 đ";

            // cardAverage
            this.cardAverage.BackColor = System.Drawing.Color.White;
            this.cardAverage.Controls.Add(this.lblAverageValue);
            this.cardAverage.Controls.Add(this.lblAverageTitle);
            this.cardAverage.Location = new System.Drawing.Point(710, 12);
            this.cardAverage.Name = "cardAverage";
            this.cardAverage.Size = new System.Drawing.Size(330, 65);
            this.cardAverage.TabIndex = 2;

            // lblAverageTitle
            this.lblAverageTitle.AutoSize = true;
            this.lblAverageTitle.Font =
                new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAverageTitle.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblAverageTitle.Location = new System.Drawing.Point(14, 9);
            this.lblAverageTitle.Name = "lblAverageTitle";
            this.lblAverageTitle.Size = new System.Drawing.Size(125, 15);
            this.lblAverageTitle.TabIndex = 0;
            this.lblAverageTitle.Text = "GIÁ TRỊ ĐƠN TRUNG BÌNH";

            // lblAverageValue
            this.lblAverageValue.AutoSize = true;
            this.lblAverageValue.Font =
                new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblAverageValue.ForeColor =
                System.Drawing.Color.FromArgb(124, 58, 237);
            this.lblAverageValue.Location = new System.Drawing.Point(14, 28);
            this.lblAverageValue.Name = "lblAverageValue";
            this.lblAverageValue.Size = new System.Drawing.Size(55, 36);
            this.lblAverageValue.TabIndex = 1;
            this.lblAverageValue.Text = "0 đ";

            // panelHistory
            this.panelHistory.BackColor = System.Drawing.Color.White;
            this.panelHistory.Controls.Add(this.dgvTransactions);
            this.panelHistory.Controls.Add(this.lblHistoryTitle);
            this.panelHistory.Location = new System.Drawing.Point(20, 220);
            this.panelHistory.Name = "panelHistory";
            this.panelHistory.Size = new System.Drawing.Size(670, 390);
            this.panelHistory.TabIndex = 2;

            // lblHistoryTitle
            this.lblHistoryTitle.AutoSize = true;
            this.lblHistoryTitle.Font =
                new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistoryTitle.ForeColor =
                System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblHistoryTitle.Location = new System.Drawing.Point(14, 12);
            this.lblHistoryTitle.Name = "lblHistoryTitle";
            this.lblHistoryTitle.Size = new System.Drawing.Size(170, 20);
            this.lblHistoryTitle.TabIndex = 0;
            this.lblHistoryTitle.Text = "DANH SÁCH GIAO DỊCH";

            // dgvTransactions
            this.dgvTransactions.Anchor =
                ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom) |
                System.Windows.Forms.AnchorStyles.Left) |
                System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTransactions.Location = new System.Drawing.Point(14, 42);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.Size = new System.Drawing.Size(642, 333);
            this.dgvTransactions.TabIndex = 1;

            // panelDetails
            this.panelDetails.BackColor = System.Drawing.Color.White;
            this.panelDetails.Controls.Add(this.dgvTransactionDetails);
            this.panelDetails.Controls.Add(this.lblSelectedTransaction);
            this.panelDetails.Controls.Add(this.lblDetailsTitle);
            this.panelDetails.Location = new System.Drawing.Point(710, 220);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(370, 390);
            this.panelDetails.TabIndex = 3;

            // lblDetailsTitle
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font =
                new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.ForeColor =
                System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblDetailsTitle.Location = new System.Drawing.Point(14, 12);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(183, 20);
            this.lblDetailsTitle.TabIndex = 0;
            this.lblDetailsTitle.Text = "CHI TIẾT GIAO DỊCH";

            // lblSelectedTransaction
            this.lblSelectedTransaction.AutoEllipsis = true;
            this.lblSelectedTransaction.Font =
                new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSelectedTransaction.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSelectedTransaction.Location = new System.Drawing.Point(15, 38);
            this.lblSelectedTransaction.Name = "lblSelectedTransaction";
            this.lblSelectedTransaction.Size = new System.Drawing.Size(340, 35);
            this.lblSelectedTransaction.TabIndex = 1;
            this.lblSelectedTransaction.Text = "Chưa chọn giao dịch";

            // dgvTransactionDetails
            this.dgvTransactionDetails.Anchor =
                ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom) |
                System.Windows.Forms.AnchorStyles.Left) |
                System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTransactionDetails.Location = new System.Drawing.Point(14, 78);
            this.dgvTransactionDetails.Name = "dgvTransactionDetails";
            this.dgvTransactionDetails.Size = new System.Drawing.Size(342, 297);
            this.dgvTransactionDetails.TabIndex = 2;

            // lblStatus
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Font =
                new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatus.ForeColor =
                System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblStatus.Location = new System.Drawing.Point(20, 622);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1060, 18);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Sẵn sàng.";

            // FormCustomerTransactionHistory
            this.AutoScaleDimensions =
                new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panelDetails);
            this.Controls.Add(this.panelHistory);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelHeader);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "FormCustomerTransactionHistory";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lịch sử giao dịch khách hàng";

            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelSummary.ResumeLayout(false);
            this.cardAverage.ResumeLayout(false);
            this.cardAverage.PerformLayout();
            this.cardTotal.ResumeLayout(false);
            this.cardTotal.PerformLayout();
            this.cardCount.ResumeLayout(false);
            this.cardCount.PerformLayout();
            this.panelHistory.ResumeLayout(false);
            this.panelHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactionDetails)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblCustomerPointsValue;
        private System.Windows.Forms.Label lblCustomerRankValue;
        private System.Windows.Forms.Label lblCustomerPhoneValue;
        private System.Windows.Forms.Label lblCustomerIdValue;
        private System.Windows.Forms.Label lblCustomerNameValue;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubTitle;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Panel cardAverage;
        private System.Windows.Forms.Label lblAverageValue;
        private System.Windows.Forms.Label lblAverageTitle;
        private System.Windows.Forms.Panel cardTotal;
        private System.Windows.Forms.Label lblTotalSpentValue;
        private System.Windows.Forms.Label lblTotalSpentTitle;
        private System.Windows.Forms.Panel cardCount;
        private System.Windows.Forms.Label lblTransactionCountValue;
        private System.Windows.Forms.Label lblTransactionCountTitle;
        private System.Windows.Forms.Panel panelHistory;
        private System.Windows.Forms.Label lblHistoryTitle;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblSelectedTransaction;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.DataGridView dgvTransactionDetails;
        private System.Windows.Forms.Label lblStatus;
    }
}
