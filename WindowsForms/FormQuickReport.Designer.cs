namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.reportMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.filterLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.cboPeriod = new System.Windows.Forms.ComboBox();
            this.btnRunReport = new System.Windows.Forms.Button();
            this.contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this.hourlyCard = new System.Windows.Forms.Panel();
            this.hourlyChartPanel = new System.Windows.Forms.Panel();
            this.lblHourlyTitle = new System.Windows.Forms.Label();
            this.categoryCard = new System.Windows.Forms.Panel();
            this.categoryChartPanel = new System.Windows.Forms.Panel();
            this.lblCategoryTitle = new System.Windows.Forms.Label();
            this.summaryCard = new System.Windows.Forms.Panel();
            this.summaryLayout = new System.Windows.Forms.TableLayoutPanel();
            this.revenueMetricCard = new System.Windows.Forms.Panel();
            this.lblRevenueTitle = new System.Windows.Forms.Label();
            this.lblRevenueValue = new System.Windows.Forms.Label();
            this.lblRevenueUnit = new System.Windows.Forms.Label();
            this.averageMetricCard = new System.Windows.Forms.Panel();
            this.lblAverageTitle = new System.Windows.Forms.Label();
            this.lblAverageOrderValue = new System.Windows.Forms.Label();
            this.lblAverageUnit = new System.Windows.Forms.Label();
            this.ordersMetricCard = new System.Windows.Forms.Panel();
            this.lblOrdersTitle = new System.Windows.Forms.Label();
            this.lblOrderCountValue = new System.Windows.Forms.Label();
            this.lblOrdersUnit = new System.Windows.Forms.Label();
            this.lblSummaryTitle = new System.Windows.Forms.Label();
            this.topProductsCard = new System.Windows.Forms.Panel();
            this.dgvTopProducts = new System.Windows.Forms.DataGridView();
            this.lblTopProductsTitle = new System.Windows.Forms.Label();
            this.lblReportStatus = new System.Windows.Forms.Label();
            this.reportMainLayout.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.filterLayout.SuspendLayout();
            this.contentLayout.SuspendLayout();
            this.hourlyCard.SuspendLayout();
            this.categoryCard.SuspendLayout();
            this.summaryCard.SuspendLayout();
            this.summaryLayout.SuspendLayout();
            this.revenueMetricCard.SuspendLayout();
            this.averageMetricCard.SuspendLayout();
            this.ordersMetricCard.SuspendLayout();
            this.topProductsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // reportMainLayout
            // 
            this.reportMainLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.reportMainLayout.ColumnCount = 1;
            this.reportMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.reportMainLayout.Controls.Add(this.headerPanel, 0, 0);
            this.reportMainLayout.Controls.Add(this.filterPanel, 0, 1);
            this.reportMainLayout.Controls.Add(this.contentLayout, 0, 2);
            this.reportMainLayout.Controls.Add(this.lblReportStatus, 0, 3);
            this.reportMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportMainLayout.Location = new System.Drawing.Point(0, 0);
            this.reportMainLayout.Margin = new System.Windows.Forms.Padding(0);
            this.reportMainLayout.Name = "reportMainLayout";
            this.reportMainLayout.Padding = new System.Windows.Forms.Padding(16, 8, 16, 6);
            this.reportMainLayout.RowCount = 4;
            this.reportMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.reportMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.reportMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.reportMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.reportMainLayout.Size = new System.Drawing.Size(1280, 760);
            this.reportMainLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.Transparent;
            this.headerPanel.Controls.Add(this.lblPageTitle);
            this.headerPanel.Controls.Add(this.lblPageSubtitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerPanel.Location = new System.Drawing.Point(16, 8);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1248, 54);
            this.headerPanel.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 0);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(410, 32);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "BÁO CÁO & THỐNG KÊ DOANH THU";
            // 
            // lblPageSubtitle
            // 
            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(2, 35);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(380, 15);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Theo dõi doanh thu theo thời gian, ngành hàng và sản phẩm bán chạy";
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.Color.White;
            this.filterPanel.Controls.Add(this.filterLayout);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterPanel.Location = new System.Drawing.Point(16, 66);
            this.filterPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.filterPanel.Size = new System.Drawing.Size(1248, 40);
            this.filterPanel.TabIndex = 1;
            // 
            // filterLayout
            // 
            this.filterLayout.ColumnCount = 6;
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.filterLayout.Controls.Add(this.lblFrom, 0, 0);
            this.filterLayout.Controls.Add(this.dtpFrom, 1, 0);
            this.filterLayout.Controls.Add(this.lblTo, 2, 0);
            this.filterLayout.Controls.Add(this.dtpTo, 3, 0);
            this.filterLayout.Controls.Add(this.cboPeriod, 4, 0);
            this.filterLayout.Controls.Add(this.btnRunReport, 5, 0);
            this.filterLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterLayout.Location = new System.Drawing.Point(8, 4);
            this.filterLayout.Margin = new System.Windows.Forms.Padding(0);
            this.filterLayout.Name = "filterLayout";
            this.filterLayout.RowCount = 1;
            this.filterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filterLayout.Size = new System.Drawing.Size(1232, 32);
            this.filterLayout.TabIndex = 0;
            // 
            // lblFrom
            // 
            this.lblFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFrom.Location = new System.Drawing.Point(3, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(24, 32);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "Từ:";
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(32, 4);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(2, 4, 8, 4);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(263, 23);
            this.dtpFrom.TabIndex = 1;
            // 
            // lblTo
            // 
            this.lblTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTo.Location = new System.Drawing.Point(306, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(26, 32);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "Đến:";
            this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpTo
            // 
            this.dtpTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(337, 4);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(2, 4, 8, 4);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(263, 23);
            this.dtpTo.TabIndex = 3;
            // 
            // cboPeriod
            // 
            this.cboPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPeriod.FormattingEnabled = true;
            this.cboPeriod.Items.AddRange(new object[] {
            "Hôm nay",
            "Tuần này",
            "Tháng này",
            "Tùy chỉnh"});
            this.cboPeriod.Location = new System.Drawing.Point(610, 4);
            this.cboPeriod.Margin = new System.Windows.Forms.Padding(2, 4, 8, 4);
            this.cboPeriod.Name = "cboPeriod";
            this.cboPeriod.Size = new System.Drawing.Size(494, 23);
            this.cboPeriod.TabIndex = 4;
            // 
            // btnRunReport
            // 
            this.btnRunReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnRunReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRunReport.FlatAppearance.BorderSize = 0;
            this.btnRunReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunReport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRunReport.ForeColor = System.Drawing.Color.White;
            this.btnRunReport.Location = new System.Drawing.Point(1114, 1);
            this.btnRunReport.Margin = new System.Windows.Forms.Padding(2, 1, 0, 1);
            this.btnRunReport.Name = "btnRunReport";
            this.btnRunReport.Size = new System.Drawing.Size(118, 30);
            this.btnRunReport.TabIndex = 5;
            this.btnRunReport.Text = "↻  Xem báo cáo";
            this.btnRunReport.UseVisualStyleBackColor = false;
            // 
            // contentLayout
            // 
            this.contentLayout.ColumnCount = 2;
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.contentLayout.Controls.Add(this.hourlyCard, 0, 0);
            this.contentLayout.Controls.Add(this.categoryCard, 1, 0);
            this.contentLayout.Controls.Add(this.summaryCard, 0, 1);
            this.contentLayout.Controls.Add(this.topProductsCard, 1, 1);
            this.contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentLayout.Location = new System.Drawing.Point(16, 114);
            this.contentLayout.Margin = new System.Windows.Forms.Padding(0);
            this.contentLayout.Name = "contentLayout";
            this.contentLayout.RowCount = 2;
            this.contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57F));
            this.contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.contentLayout.Size = new System.Drawing.Size(1248, 616);
            this.contentLayout.TabIndex = 2;
            // 
            // hourlyCard
            // 
            this.hourlyCard.BackColor = System.Drawing.Color.White;
            this.hourlyCard.Controls.Add(this.hourlyChartPanel);
            this.hourlyCard.Controls.Add(this.lblHourlyTitle);
            this.hourlyCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hourlyCard.Location = new System.Drawing.Point(0, 0);
            this.hourlyCard.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this.hourlyCard.Name = "hourlyCard";
            this.hourlyCard.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.hourlyCard.Size = new System.Drawing.Size(713, 343);
            this.hourlyCard.TabIndex = 0;
            // 
            // hourlyChartPanel
            // 
            this.hourlyChartPanel.BackColor = System.Drawing.Color.White;
            this.hourlyChartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hourlyChartPanel.Location = new System.Drawing.Point(10, 34);
            this.hourlyChartPanel.Name = "hourlyChartPanel";
            this.hourlyChartPanel.Size = new System.Drawing.Size(693, 301);
            this.hourlyChartPanel.TabIndex = 1;
            // 
            // lblHourlyTitle
            // 
            this.lblHourlyTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHourlyTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHourlyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblHourlyTitle.Location = new System.Drawing.Point(10, 8);
            this.lblHourlyTitle.Name = "lblHourlyTitle";
            this.lblHourlyTitle.Size = new System.Drawing.Size(693, 26);
            this.lblHourlyTitle.TabIndex = 0;
            this.lblHourlyTitle.Text = "DOANH THU THEO GIỜ";
            this.lblHourlyTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // categoryCard
            // 
            this.categoryCard.BackColor = System.Drawing.Color.White;
            this.categoryCard.Controls.Add(this.categoryChartPanel);
            this.categoryCard.Controls.Add(this.lblCategoryTitle);
            this.categoryCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryCard.Location = new System.Drawing.Point(723, 0);
            this.categoryCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.categoryCard.Name = "categoryCard";
            this.categoryCard.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.categoryCard.Size = new System.Drawing.Size(525, 343);
            this.categoryCard.TabIndex = 1;
            // 
            // categoryChartPanel
            // 
            this.categoryChartPanel.BackColor = System.Drawing.Color.White;
            this.categoryChartPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryChartPanel.Location = new System.Drawing.Point(10, 34);
            this.categoryChartPanel.Name = "categoryChartPanel";
            this.categoryChartPanel.Size = new System.Drawing.Size(505, 301);
            this.categoryChartPanel.TabIndex = 1;
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblCategoryTitle.Location = new System.Drawing.Point(10, 8);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(505, 26);
            this.lblCategoryTitle.TabIndex = 0;
            this.lblCategoryTitle.Text = "TỈ TRỌNG DOANH THU THEO NGÀNH HÀNG";
            this.lblCategoryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // summaryCard
            // 
            this.summaryCard.BackColor = System.Drawing.Color.Transparent;
            this.summaryCard.Controls.Add(this.summaryLayout);
            this.summaryCard.Controls.Add(this.lblSummaryTitle);
            this.summaryCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryCard.Location = new System.Drawing.Point(0, 351);
            this.summaryCard.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.summaryCard.Name = "summaryCard";
            this.summaryCard.Size = new System.Drawing.Size(713, 265);
            this.summaryCard.TabIndex = 2;
            // 
            // summaryLayout
            // 
            this.summaryLayout.ColumnCount = 3;
            this.summaryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.summaryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.summaryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.summaryLayout.Controls.Add(this.revenueMetricCard, 0, 0);
            this.summaryLayout.Controls.Add(this.averageMetricCard, 1, 0);
            this.summaryLayout.Controls.Add(this.ordersMetricCard, 2, 0);
            this.summaryLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryLayout.Location = new System.Drawing.Point(0, 28);
            this.summaryLayout.Margin = new System.Windows.Forms.Padding(0);
            this.summaryLayout.Name = "summaryLayout";
            this.summaryLayout.RowCount = 1;
            this.summaryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.summaryLayout.Size = new System.Drawing.Size(713, 237);
            this.summaryLayout.TabIndex = 1;
            // 
            // revenueMetricCard
            // 
            this.revenueMetricCard.BackColor = System.Drawing.Color.White;
            this.revenueMetricCard.Controls.Add(this.lblRevenueTitle);
            this.revenueMetricCard.Controls.Add(this.lblRevenueValue);
            this.revenueMetricCard.Controls.Add(this.lblRevenueUnit);
            this.revenueMetricCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.revenueMetricCard.Location = new System.Drawing.Point(0, 0);
            this.revenueMetricCard.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.revenueMetricCard.Name = "revenueMetricCard";
            this.revenueMetricCard.Size = new System.Drawing.Size(236, 237);
            this.revenueMetricCard.TabIndex = 0;
            // 
            // lblRevenueTitle
            // 
            this.lblRevenueTitle.AutoEllipsis = true;
            this.lblRevenueTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblRevenueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblRevenueTitle.Location = new System.Drawing.Point(8, 10);
            this.lblRevenueTitle.Name = "lblRevenueTitle";
            this.lblRevenueTitle.Size = new System.Drawing.Size(210, 30);
            this.lblRevenueTitle.TabIndex = 0;
            this.lblRevenueTitle.Text = "DOANH THU THÁNG NÀY";
            // 
            // lblRevenueValue
            // 
            this.lblRevenueValue.AutoEllipsis = true;
            this.lblRevenueValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblRevenueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblRevenueValue.Location = new System.Drawing.Point(8, 43);
            this.lblRevenueValue.Name = "lblRevenueValue";
            this.lblRevenueValue.Size = new System.Drawing.Size(220, 32);
            this.lblRevenueValue.TabIndex = 1;
            this.lblRevenueValue.Text = "—";
            // 
            // lblRevenueUnit
            // 
            this.lblRevenueUnit.AutoSize = true;
            this.lblRevenueUnit.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblRevenueUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblRevenueUnit.Location = new System.Drawing.Point(9, 78);
            this.lblRevenueUnit.Name = "lblRevenueUnit";
            this.lblRevenueUnit.Size = new System.Drawing.Size(30, 13);
            this.lblRevenueUnit.TabIndex = 2;
            this.lblRevenueUnit.Text = "VND";
            // 
            // averageMetricCard
            // 
            this.averageMetricCard.BackColor = System.Drawing.Color.White;
            this.averageMetricCard.Controls.Add(this.lblAverageTitle);
            this.averageMetricCard.Controls.Add(this.lblAverageOrderValue);
            this.averageMetricCard.Controls.Add(this.lblAverageUnit);
            this.averageMetricCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.averageMetricCard.Location = new System.Drawing.Point(242, 0);
            this.averageMetricCard.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.averageMetricCard.Name = "averageMetricCard";
            this.averageMetricCard.Size = new System.Drawing.Size(229, 237);
            this.averageMetricCard.TabIndex = 1;
            // 
            // lblAverageTitle
            // 
            this.lblAverageTitle.AutoEllipsis = true;
            this.lblAverageTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblAverageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAverageTitle.Location = new System.Drawing.Point(8, 10);
            this.lblAverageTitle.Name = "lblAverageTitle";
            this.lblAverageTitle.Size = new System.Drawing.Size(200, 30);
            this.lblAverageTitle.TabIndex = 0;
            this.lblAverageTitle.Text = "GIÁ TRỊ HÓA ĐƠN TRUNG BÌNH";
            // 
            // lblAverageOrderValue
            // 
            this.lblAverageOrderValue.AutoEllipsis = true;
            this.lblAverageOrderValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAverageOrderValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(148)))), ((int)(((byte)(136)))));
            this.lblAverageOrderValue.Location = new System.Drawing.Point(8, 43);
            this.lblAverageOrderValue.Name = "lblAverageOrderValue";
            this.lblAverageOrderValue.Size = new System.Drawing.Size(210, 32);
            this.lblAverageOrderValue.TabIndex = 1;
            this.lblAverageOrderValue.Text = "—";
            // 
            // lblAverageUnit
            // 
            this.lblAverageUnit.AutoSize = true;
            this.lblAverageUnit.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblAverageUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAverageUnit.Location = new System.Drawing.Point(9, 78);
            this.lblAverageUnit.Name = "lblAverageUnit";
            this.lblAverageUnit.Size = new System.Drawing.Size(84, 13);
            this.lblAverageUnit.TabIndex = 2;
            this.lblAverageUnit.Text = "VND / hóa đơn";
            // 
            // ordersMetricCard
            // 
            this.ordersMetricCard.BackColor = System.Drawing.Color.White;
            this.ordersMetricCard.Controls.Add(this.lblOrdersTitle);
            this.ordersMetricCard.Controls.Add(this.lblOrderCountValue);
            this.ordersMetricCard.Controls.Add(this.lblOrdersUnit);
            this.ordersMetricCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ordersMetricCard.Location = new System.Drawing.Point(477, 0);
            this.ordersMetricCard.Margin = new System.Windows.Forms.Padding(0);
            this.ordersMetricCard.Name = "ordersMetricCard";
            this.ordersMetricCard.Size = new System.Drawing.Size(236, 237);
            this.ordersMetricCard.TabIndex = 2;
            // 
            // lblOrdersTitle
            // 
            this.lblOrdersTitle.AutoEllipsis = true;
            this.lblOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblOrdersTitle.Location = new System.Drawing.Point(8, 10);
            this.lblOrdersTitle.Name = "lblOrdersTitle";
            this.lblOrdersTitle.Size = new System.Drawing.Size(180, 30);
            this.lblOrdersTitle.TabIndex = 0;
            this.lblOrdersTitle.Text = "TỔNG SỐ HÓA ĐƠN";
            // 
            // lblOrderCountValue
            // 
            this.lblOrderCountValue.AutoEllipsis = true;
            this.lblOrderCountValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblOrderCountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblOrderCountValue.Location = new System.Drawing.Point(8, 43);
            this.lblOrderCountValue.Name = "lblOrderCountValue";
            this.lblOrderCountValue.Size = new System.Drawing.Size(180, 32);
            this.lblOrderCountValue.TabIndex = 1;
            this.lblOrderCountValue.Text = "—";
            // 
            // lblOrdersUnit
            // 
            this.lblOrdersUnit.AutoSize = true;
            this.lblOrdersUnit.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblOrdersUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblOrdersUnit.Location = new System.Drawing.Point(9, 78);
            this.lblOrdersUnit.Name = "lblOrdersUnit";
            this.lblOrdersUnit.Size = new System.Drawing.Size(51, 13);
            this.lblOrdersUnit.TabIndex = 2;
            this.lblOrdersUnit.Text = "hóa đơn";
            // 
            // lblSummaryTitle
            // 
            this.lblSummaryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSummaryTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSummaryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSummaryTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSummaryTitle.Name = "lblSummaryTitle";
            this.lblSummaryTitle.Size = new System.Drawing.Size(713, 28);
            this.lblSummaryTitle.TabIndex = 0;
            this.lblSummaryTitle.Text = "TỔNG QUAN KINH DOANH";
            this.lblSummaryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // topProductsCard
            // 
            this.topProductsCard.BackColor = System.Drawing.Color.White;
            this.topProductsCard.Controls.Add(this.dgvTopProducts);
            this.topProductsCard.Controls.Add(this.lblTopProductsTitle);
            this.topProductsCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topProductsCard.Location = new System.Drawing.Point(723, 351);
            this.topProductsCard.Margin = new System.Windows.Forms.Padding(0);
            this.topProductsCard.Name = "topProductsCard";
            this.topProductsCard.Padding = new System.Windows.Forms.Padding(8);
            this.topProductsCard.Size = new System.Drawing.Size(525, 265);
            this.topProductsCard.TabIndex = 3;
            // 
            // dgvTopProducts
            // 
            this.dgvTopProducts.AllowUserToAddRows = false;
            this.dgvTopProducts.AllowUserToDeleteRows = false;
            this.dgvTopProducts.AllowUserToResizeRows = false;
            this.dgvTopProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopProducts.ColumnHeadersHeight = 32;
            this.dgvTopProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopProducts.EnableHeadersVisualStyles = false;
            this.dgvTopProducts.Location = new System.Drawing.Point(8, 38);
            this.dgvTopProducts.MultiSelect = false;
            this.dgvTopProducts.Name = "dgvTopProducts";
            this.dgvTopProducts.ReadOnly = true;
            this.dgvTopProducts.RowHeadersVisible = false;
            this.dgvTopProducts.RowTemplate.Height = 26;
            this.dgvTopProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopProducts.Size = new System.Drawing.Size(509, 219);
            this.dgvTopProducts.TabIndex = 1;
            // 
            // lblTopProductsTitle
            // 
            this.lblTopProductsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTopProductsTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTopProductsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTopProductsTitle.Location = new System.Drawing.Point(8, 8);
            this.lblTopProductsTitle.Name = "lblTopProductsTitle";
            this.lblTopProductsTitle.Size = new System.Drawing.Size(509, 30);
            this.lblTopProductsTitle.TabIndex = 0;
            this.lblTopProductsTitle.Text = "TOP SẢN PHẨM BÁN CHẠY NHẤT";
            this.lblTopProductsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblReportStatus
            // 
            this.lblReportStatus.AutoEllipsis = true;
            this.lblReportStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReportStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblReportStatus.Location = new System.Drawing.Point(19, 730);
            this.lblReportStatus.Name = "lblReportStatus";
            this.lblReportStatus.Size = new System.Drawing.Size(1242, 24);
            this.lblReportStatus.TabIndex = 3;
            this.lblReportStatus.Text = "Dữ liệu báo cáo được đồng bộ từ API MiniMart.";
            this.lblReportStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormQuickReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.reportMainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 680);
            this.Name = "FormQuickReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiniMart | Báo cáo & thống kê doanh thu";
            this.reportMainLayout.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.filterPanel.ResumeLayout(false);
            this.filterLayout.ResumeLayout(false);
            this.contentLayout.ResumeLayout(false);
            this.hourlyCard.ResumeLayout(false);
            this.categoryCard.ResumeLayout(false);
            this.summaryCard.ResumeLayout(false);
            this.summaryLayout.ResumeLayout(false);
            this.revenueMetricCard.ResumeLayout(false);
            this.revenueMetricCard.PerformLayout();
            this.averageMetricCard.ResumeLayout(false);
            this.averageMetricCard.PerformLayout();
            this.ordersMetricCard.ResumeLayout(false);
            this.ordersMetricCard.PerformLayout();
            this.topProductsCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel reportMainLayout;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.TableLayoutPanel filterLayout;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.ComboBox cboPeriod;
        private System.Windows.Forms.Button btnRunReport;
        private System.Windows.Forms.TableLayoutPanel contentLayout;
        private System.Windows.Forms.Panel hourlyCard;
        private System.Windows.Forms.Label lblHourlyTitle;
        private System.Windows.Forms.Panel hourlyChartPanel;
        private System.Windows.Forms.Panel categoryCard;
        private System.Windows.Forms.Label lblCategoryTitle;
        private System.Windows.Forms.Panel categoryChartPanel;
        private System.Windows.Forms.Panel summaryCard;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.TableLayoutPanel summaryLayout;
        private System.Windows.Forms.Panel revenueMetricCard;
        private System.Windows.Forms.Label lblRevenueTitle;
        private System.Windows.Forms.Label lblRevenueValue;
        private System.Windows.Forms.Label lblRevenueUnit;
        private System.Windows.Forms.Panel averageMetricCard;
        private System.Windows.Forms.Label lblAverageTitle;
        private System.Windows.Forms.Label lblAverageOrderValue;
        private System.Windows.Forms.Label lblAverageUnit;
        private System.Windows.Forms.Panel ordersMetricCard;
        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.Label lblOrderCountValue;
        private System.Windows.Forms.Label lblOrdersUnit;
        private System.Windows.Forms.Panel topProductsCard;
        private System.Windows.Forms.Label lblTopProductsTitle;
        private System.Windows.Forms.DataGridView dgvTopProducts;
        private System.Windows.Forms.Label lblReportStatus;
    }
}
