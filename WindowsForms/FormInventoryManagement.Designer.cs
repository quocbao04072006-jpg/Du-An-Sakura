namespace MiniSupermarket.WinForms
{
    partial class FormInventoryManagement
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label pageTitle;
        private System.Windows.Forms.Label pageSubtitle;
        private System.Windows.Forms.TableLayoutPanel kpiLayout;
        private System.Windows.Forms.Panel totalCard;
        private System.Windows.Forms.Panel totalStockCard;
        private System.Windows.Forms.Panel lowCard;
        private System.Windows.Forms.Panel outCard;
        private System.Windows.Forms.Panel totalAccent;
        private System.Windows.Forms.Panel totalStockAccent;
        private System.Windows.Forms.Panel lowAccent;
        private System.Windows.Forms.Panel outAccent;
        private System.Windows.Forms.Label totalCaption;
        private System.Windows.Forms.Label totalStockCaption;
        private System.Windows.Forms.Label lowCaption;
        private System.Windows.Forms.Label outCaption;
        private System.Windows.Forms.Label totalValue;
        private System.Windows.Forms.Label totalStockValue;
        private System.Windows.Forms.Label lowValue;
        private System.Windows.Forms.Label outValue;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.TableLayoutPanel filterLayout;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ComboBox stockFilter;
        private System.Windows.Forms.ComboBox categoryFilter;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.TableLayoutPanel contentLayout;
        private System.Windows.Forms.Panel listCard;
        private System.Windows.Forms.TableLayoutPanel listLayout;
        private System.Windows.Forms.Label listTitle;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.Panel detailCard;
        private System.Windows.Forms.TableLayoutPanel detailLayout;
        private System.Windows.Forms.Label detailTitle;
        private System.Windows.Forms.Label selectedName;
        private System.Windows.Forms.Label selectedCategory;
        private System.Windows.Forms.Label stockCaption;
        private System.Windows.Forms.Label selectedStock;
        private System.Windows.Forms.Button addStockButton;
        private System.Windows.Forms.Button removeStockButton;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.ContextMenuStrip rowMenu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.pageTitle = new System.Windows.Forms.Label();
            this.pageSubtitle = new System.Windows.Forms.Label();
            this.kpiLayout = new System.Windows.Forms.TableLayoutPanel();
            this.totalCard = new System.Windows.Forms.Panel();
            this.totalAccent = new System.Windows.Forms.Panel();
            this.totalCaption = new System.Windows.Forms.Label();
            this.totalValue = new System.Windows.Forms.Label();
            this.totalStockCard = new System.Windows.Forms.Panel();
            this.totalStockAccent = new System.Windows.Forms.Panel();
            this.totalStockCaption = new System.Windows.Forms.Label();
            this.totalStockValue = new System.Windows.Forms.Label();
            this.lowCard = new System.Windows.Forms.Panel();
            this.lowAccent = new System.Windows.Forms.Panel();
            this.lowCaption = new System.Windows.Forms.Label();
            this.lowValue = new System.Windows.Forms.Label();
            this.outCard = new System.Windows.Forms.Panel();
            this.outAccent = new System.Windows.Forms.Panel();
            this.outCaption = new System.Windows.Forms.Label();
            this.outValue = new System.Windows.Forms.Label();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.filterLayout = new System.Windows.Forms.TableLayoutPanel();
            this.searchLabel = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.stockFilter = new System.Windows.Forms.ComboBox();
            this.categoryFilter = new System.Windows.Forms.ComboBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this.listCard = new System.Windows.Forms.Panel();
            this.listLayout = new System.Windows.Forms.TableLayoutPanel();
            this.listTitle = new System.Windows.Forms.Label();
            this.grid = new System.Windows.Forms.DataGridView();
            this.detailCard = new System.Windows.Forms.Panel();
            this.detailLayout = new System.Windows.Forms.TableLayoutPanel();
            this.detailTitle = new System.Windows.Forms.Label();
            this.selectedName = new System.Windows.Forms.Label();
            this.selectedCategory = new System.Windows.Forms.Label();
            this.stockCaption = new System.Windows.Forms.Label();
            this.selectedStock = new System.Windows.Forms.Label();
            this.addStockButton = new System.Windows.Forms.Button();
            this.removeStockButton = new System.Windows.Forms.Button();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.status = new System.Windows.Forms.Label();
            this.rowMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainLayout.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.kpiLayout.SuspendLayout();
            this.totalCard.SuspendLayout();
            this.totalStockCard.SuspendLayout();
            this.lowCard.SuspendLayout();
            this.outCard.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.filterLayout.SuspendLayout();
            this.contentLayout.SuspendLayout();
            this.listCard.SuspendLayout();
            this.listLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.detailCard.SuspendLayout();
            this.detailLayout.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.headerPanel, 0, 0);
            this.mainLayout.Controls.Add(this.kpiLayout, 0, 1);
            this.mainLayout.Controls.Add(this.filterPanel, 0, 2);
            this.mainLayout.Controls.Add(this.contentLayout, 0, 3);
            this.mainLayout.Controls.Add(this.statusPanel, 0, 4);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this.mainLayout.RowCount = 5;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.mainLayout.Size = new System.Drawing.Size(1280, 760);
            this.mainLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.Transparent;
            this.headerPanel.Controls.Add(this.pageTitle);
            this.headerPanel.Controls.Add(this.pageSubtitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerPanel.Location = new System.Drawing.Point(16, 12);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1248, 62);
            this.headerPanel.TabIndex = 0;
            // 
            // pageTitle
            // 
            this.pageTitle.AutoSize = true;
            this.pageTitle.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pageTitle.Location = new System.Drawing.Point(0, 0);
            this.pageTitle.Name = "pageTitle";
            this.pageTitle.Size = new System.Drawing.Size(326, 36);
            this.pageTitle.TabIndex = 0;
            this.pageTitle.Text = "QUẢN LÝ KHO & TỒN KHO";
            // 
            // pageSubtitle
            // 
            this.pageSubtitle.AutoSize = true;
            this.pageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.pageSubtitle.Location = new System.Drawing.Point(2, 39);
            this.pageSubtitle.Name = "pageSubtitle";
            this.pageSubtitle.Size = new System.Drawing.Size(411, 15);
            this.pageSubtitle.TabIndex = 1;
            this.pageSubtitle.Text = "Theo dõi số lượng thực tế và xử lý nhập / xuất điều chỉnh từ dữ liệu hệ thống";
            // 
            // kpiLayout
            // 
            this.kpiLayout.ColumnCount = 4;
            this.kpiLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.kpiLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.kpiLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.kpiLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.kpiLayout.Controls.Add(this.totalCard, 0, 0);
            this.kpiLayout.Controls.Add(this.totalStockCard, 1, 0);
            this.kpiLayout.Controls.Add(this.lowCard, 2, 0);
            this.kpiLayout.Controls.Add(this.outCard, 3, 0);
            this.kpiLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kpiLayout.Location = new System.Drawing.Point(16, 80);
            this.kpiLayout.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.kpiLayout.Name = "kpiLayout";
            this.kpiLayout.RowCount = 1;
            this.kpiLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.kpiLayout.Size = new System.Drawing.Size(1248, 88);
            this.kpiLayout.TabIndex = 1;
            // 
            // totalCard
            // 
            this.totalCard.BackColor = System.Drawing.Color.White;
            this.totalCard.Controls.Add(this.totalAccent);
            this.totalCard.Controls.Add(this.totalCaption);
            this.totalCard.Controls.Add(this.totalValue);
            this.totalCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalCard.Location = new System.Drawing.Point(0, 0);
            this.totalCard.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.totalCard.Name = "totalCard";
            this.totalCard.Size = new System.Drawing.Size(300, 88);
            this.totalCard.TabIndex = 0;
            // 
            // totalAccent
            // 
            this.totalAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.totalAccent.Dock = System.Windows.Forms.DockStyle.Left;
            this.totalAccent.Location = new System.Drawing.Point(0, 0);
            this.totalAccent.Name = "totalAccent";
            this.totalAccent.Size = new System.Drawing.Size(4, 88);
            this.totalAccent.TabIndex = 0;
            // 
            // totalCaption
            // 
            this.totalCaption.AutoEllipsis = true;
            this.totalCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.totalCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.totalCaption.Location = new System.Drawing.Point(18, 10);
            this.totalCaption.Name = "totalCaption";
            this.totalCaption.Size = new System.Drawing.Size(260, 22);
            this.totalCaption.TabIndex = 1;
            this.totalCaption.Text = "TỔNG MẶT HÀNG";
            this.totalCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalValue
            // 
            this.totalValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.totalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.totalValue.Location = new System.Drawing.Point(18, 32);
            this.totalValue.Name = "totalValue";
            this.totalValue.Size = new System.Drawing.Size(260, 38);
            this.totalValue.TabIndex = 2;
            this.totalValue.Text = "—";
            this.totalValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalStockCard
            // 
            this.totalStockCard.BackColor = System.Drawing.Color.White;
            this.totalStockCard.Controls.Add(this.totalStockAccent);
            this.totalStockCard.Controls.Add(this.totalStockCaption);
            this.totalStockCard.Controls.Add(this.totalStockValue);
            this.totalStockCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalStockCard.Location = new System.Drawing.Point(312, 0);
            this.totalStockCard.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.totalStockCard.Name = "totalStockCard";
            this.totalStockCard.Size = new System.Drawing.Size(300, 88);
            this.totalStockCard.TabIndex = 1;
            // 
            // totalStockAccent
            // 
            this.totalStockAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(148)))), ((int)(((byte)(136)))));
            this.totalStockAccent.Dock = System.Windows.Forms.DockStyle.Left;
            this.totalStockAccent.Location = new System.Drawing.Point(0, 0);
            this.totalStockAccent.Name = "totalStockAccent";
            this.totalStockAccent.Size = new System.Drawing.Size(4, 88);
            this.totalStockAccent.TabIndex = 0;
            // 
            // totalStockCaption
            // 
            this.totalStockCaption.AutoEllipsis = true;
            this.totalStockCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.totalStockCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.totalStockCaption.Location = new System.Drawing.Point(18, 10);
            this.totalStockCaption.Name = "totalStockCaption";
            this.totalStockCaption.Size = new System.Drawing.Size(260, 22);
            this.totalStockCaption.TabIndex = 1;
            this.totalStockCaption.Text = "TỔNG SỐ LƯỢNG HÀNG TRONG KHO";
            this.totalStockCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalStockValue
            // 
            this.totalStockValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.totalStockValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(148)))), ((int)(((byte)(136)))));
            this.totalStockValue.Location = new System.Drawing.Point(18, 32);
            this.totalStockValue.Name = "totalStockValue";
            this.totalStockValue.Size = new System.Drawing.Size(260, 38);
            this.totalStockValue.TabIndex = 2;
            this.totalStockValue.Text = "—";
            this.totalStockValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lowCard
            // 
            this.lowCard.BackColor = System.Drawing.Color.White;
            this.lowCard.Controls.Add(this.lowAccent);
            this.lowCard.Controls.Add(this.lowCaption);
            this.lowCard.Controls.Add(this.lowValue);
            this.lowCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lowCard.Location = new System.Drawing.Point(624, 0);
            this.lowCard.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lowCard.Name = "lowCard";
            this.lowCard.Size = new System.Drawing.Size(300, 88);
            this.lowCard.TabIndex = 2;
            // 
            // lowAccent
            // 
            this.lowAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lowAccent.Dock = System.Windows.Forms.DockStyle.Left;
            this.lowAccent.Location = new System.Drawing.Point(0, 0);
            this.lowAccent.Name = "lowAccent";
            this.lowAccent.Size = new System.Drawing.Size(4, 88);
            this.lowAccent.TabIndex = 0;
            // 
            // lowCaption
            // 
            this.lowCaption.AutoEllipsis = true;
            this.lowCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lowCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lowCaption.Location = new System.Drawing.Point(18, 10);
            this.lowCaption.Name = "lowCaption";
            this.lowCaption.Size = new System.Drawing.Size(260, 22);
            this.lowCaption.TabIndex = 1;
            this.lowCaption.Text = "SẮP HẾT (≤ 10)";
            this.lowCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lowValue
            // 
            this.lowValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lowValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lowValue.Location = new System.Drawing.Point(18, 32);
            this.lowValue.Name = "lowValue";
            this.lowValue.Size = new System.Drawing.Size(260, 38);
            this.lowValue.TabIndex = 2;
            this.lowValue.Text = "—";
            this.lowValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // outCard
            // 
            this.outCard.BackColor = System.Drawing.Color.White;
            this.outCard.Controls.Add(this.outAccent);
            this.outCard.Controls.Add(this.outCaption);
            this.outCard.Controls.Add(this.outValue);
            this.outCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outCard.Location = new System.Drawing.Point(936, 0);
            this.outCard.Margin = new System.Windows.Forms.Padding(0);
            this.outCard.Name = "outCard";
            this.outCard.Size = new System.Drawing.Size(312, 88);
            this.outCard.TabIndex = 3;
            // 
            // outAccent
            // 
            this.outAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.outAccent.Dock = System.Windows.Forms.DockStyle.Left;
            this.outAccent.Location = new System.Drawing.Point(0, 0);
            this.outAccent.Name = "outAccent";
            this.outAccent.Size = new System.Drawing.Size(4, 88);
            this.outAccent.TabIndex = 0;
            // 
            // outCaption
            // 
            this.outCaption.AutoEllipsis = true;
            this.outCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.outCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.outCaption.Location = new System.Drawing.Point(18, 10);
            this.outCaption.Name = "outCaption";
            this.outCaption.Size = new System.Drawing.Size(260, 22);
            this.outCaption.TabIndex = 1;
            this.outCaption.Text = "HẾT HÀNG";
            this.outCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // outValue
            // 
            this.outValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.outValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.outValue.Location = new System.Drawing.Point(18, 32);
            this.outValue.Name = "outValue";
            this.outValue.Size = new System.Drawing.Size(260, 38);
            this.outValue.TabIndex = 2;
            this.outValue.Text = "—";
            this.outValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.Color.White;
            this.filterPanel.Controls.Add(this.filterLayout);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterPanel.Location = new System.Drawing.Point(16, 176);
            this.filterPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.filterPanel.Size = new System.Drawing.Size(1248, 52);
            this.filterPanel.TabIndex = 2;
            // 
            // filterLayout
            // 
            this.filterLayout.ColumnCount = 5;
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.filterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.filterLayout.Controls.Add(this.searchLabel, 0, 0);
            this.filterLayout.Controls.Add(this.searchBox, 1, 0);
            this.filterLayout.Controls.Add(this.stockFilter, 2, 0);
            this.filterLayout.Controls.Add(this.categoryFilter, 3, 0);
            this.filterLayout.Controls.Add(this.refreshButton, 4, 0);
            this.filterLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterLayout.Location = new System.Drawing.Point(8, 5);
            this.filterLayout.Margin = new System.Windows.Forms.Padding(0);
            this.filterLayout.Name = "filterLayout";
            this.filterLayout.RowCount = 1;
            this.filterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filterLayout.Size = new System.Drawing.Size(1232, 42);
            this.filterLayout.TabIndex = 0;
            // 
            // searchLabel
            // 
            this.searchLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.searchLabel.Location = new System.Drawing.Point(2, 0);
            this.searchLabel.Margin = new System.Windows.Forms.Padding(2, 0, 3, 0);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(73, 42);
            this.searchLabel.TabIndex = 0;
            this.searchLabel.Text = "Tìm nhanh";
            this.searchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // searchBox
            // 
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchBox.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.searchBox.Location = new System.Drawing.Point(80, 5);
            this.searchBox.Margin = new System.Windows.Forms.Padding(2, 5, 8, 5);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(404, 24);
            this.searchBox.TabIndex = 1;
            // 
            // stockFilter
            // 
            this.stockFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stockFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stockFilter.FormattingEnabled = true;
            this.stockFilter.Items.AddRange(new object[] {
            "Tất cả tồn kho",
            "Sắp hết hàng",
            "Hết hàng",
            "Còn hàng nhiều"});
            this.stockFilter.Location = new System.Drawing.Point(494, 4);
            this.stockFilter.Margin = new System.Windows.Forms.Padding(2, 4, 8, 4);
            this.stockFilter.Name = "stockFilter";
            this.stockFilter.Size = new System.Drawing.Size(248, 23);
            this.stockFilter.TabIndex = 2;
            this.stockFilter.SelectedIndexChanged += new System.EventHandler(this.stockFilter_SelectedIndexChanged);
            // 
            // categoryFilter
            // 
            this.categoryFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryFilter.FormattingEnabled = true;
            this.categoryFilter.Items.AddRange(new object[] {
            "Tất cả ngành hàng"});
            this.categoryFilter.Location = new System.Drawing.Point(752, 4);
            this.categoryFilter.Margin = new System.Windows.Forms.Padding(2, 4, 8, 4);
            this.categoryFilter.Name = "categoryFilter";
            this.categoryFilter.Size = new System.Drawing.Size(352, 23);
            this.categoryFilter.TabIndex = 3;
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(148)))), ((int)(((byte)(136)))));
            this.refreshButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.refreshButton.FlatAppearance.BorderSize = 0;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.Location = new System.Drawing.Point(1114, 2);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(118, 38);
            this.refreshButton.TabIndex = 4;
            this.refreshButton.Text = "↻  Làm mới";
            this.refreshButton.UseVisualStyleBackColor = false;
            // 
            // contentLayout
            // 
            this.contentLayout.ColumnCount = 2;
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.contentLayout.Controls.Add(this.listCard, 0, 0);
            this.contentLayout.Controls.Add(this.detailCard, 1, 0);
            this.contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentLayout.Location = new System.Drawing.Point(16, 236);
            this.contentLayout.Margin = new System.Windows.Forms.Padding(0);
            this.contentLayout.Name = "contentLayout";
            this.contentLayout.RowCount = 1;
            this.contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentLayout.Size = new System.Drawing.Size(1248, 488);
            this.contentLayout.TabIndex = 3;
            // 
            // listCard
            // 
            this.listCard.BackColor = System.Drawing.Color.White;
            this.listCard.Controls.Add(this.listLayout);
            this.listCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCard.Location = new System.Drawing.Point(0, 0);
            this.listCard.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.listCard.Name = "listCard";
            this.listCard.Padding = new System.Windows.Forms.Padding(10);
            this.listCard.Size = new System.Drawing.Size(861, 488);
            this.listCard.TabIndex = 0;
            // 
            // listLayout
            // 
            this.listLayout.ColumnCount = 1;
            this.listLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.listLayout.Controls.Add(this.listTitle, 0, 0);
            this.listLayout.Controls.Add(this.grid, 0, 1);
            this.listLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listLayout.Location = new System.Drawing.Point(10, 10);
            this.listLayout.Margin = new System.Windows.Forms.Padding(0);
            this.listLayout.Name = "listLayout";
            this.listLayout.RowCount = 2;
            this.listLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.listLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.listLayout.Size = new System.Drawing.Size(841, 468);
            this.listLayout.TabIndex = 0;
            // 
            // listTitle
            // 
            this.listTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.listTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.listTitle.Location = new System.Drawing.Point(3, 0);
            this.listTitle.Name = "listTitle";
            this.listTitle.Size = new System.Drawing.Size(835, 36);
            this.listTitle.TabIndex = 0;
            this.listTitle.Text = "BẢNG TỒN KHO THỰC TẾ";
            this.listTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grid.ColumnHeadersHeight = 38;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(251)))), ((int)(((byte)(241)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.Location = new System.Drawing.Point(3, 39);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 32;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(835, 426);
            this.grid.StandardTab = true;
            this.grid.TabIndex = 1;
            // 
            // detailCard
            // 
            this.detailCard.BackColor = System.Drawing.Color.White;
            this.detailCard.Controls.Add(this.detailLayout);
            this.detailCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailCard.Location = new System.Drawing.Point(873, 0);
            this.detailCard.Margin = new System.Windows.Forms.Padding(0);
            this.detailCard.Name = "detailCard";
            this.detailCard.Padding = new System.Windows.Forms.Padding(14, 10, 14, 12);
            this.detailCard.Size = new System.Drawing.Size(375, 488);
            this.detailCard.TabIndex = 1;
            // 
            // detailLayout
            // 
            this.detailLayout.ColumnCount = 1;
            this.detailLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.detailLayout.Controls.Add(this.detailTitle, 0, 0);
            this.detailLayout.Controls.Add(this.selectedName, 0, 1);
            this.detailLayout.Controls.Add(this.selectedCategory, 0, 2);
            this.detailLayout.Controls.Add(this.stockCaption, 0, 3);
            this.detailLayout.Controls.Add(this.selectedStock, 0, 4);
            this.detailLayout.Controls.Add(this.addStockButton, 0, 5);
            this.detailLayout.Controls.Add(this.removeStockButton, 0, 6);
            this.detailLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailLayout.Location = new System.Drawing.Point(14, 10);
            this.detailLayout.Margin = new System.Windows.Forms.Padding(0);
            this.detailLayout.Name = "detailLayout";
            this.detailLayout.RowCount = 8;
            this.detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.detailLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.detailLayout.Size = new System.Drawing.Size(347, 466);
            this.detailLayout.TabIndex = 0;
            // 
            // detailTitle
            // 
            this.detailTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.detailTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.detailTitle.Location = new System.Drawing.Point(3, 0);
            this.detailTitle.Name = "detailTitle";
            this.detailTitle.Size = new System.Drawing.Size(341, 34);
            this.detailTitle.TabIndex = 0;
            this.detailTitle.Text = "CHI TIẾT TỒN KHO";
            this.detailTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // selectedName
            // 
            this.selectedName.AutoEllipsis = true;
            this.selectedName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.selectedName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.selectedName.Location = new System.Drawing.Point(3, 34);
            this.selectedName.Name = "selectedName";
            this.selectedName.Size = new System.Drawing.Size(341, 64);
            this.selectedName.TabIndex = 1;
            this.selectedName.Text = "Chọn một sản phẩm";
            this.selectedName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // selectedCategory
            // 
            this.selectedCategory.AutoEllipsis = true;
            this.selectedCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedCategory.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.selectedCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.selectedCategory.Location = new System.Drawing.Point(3, 98);
            this.selectedCategory.Name = "selectedCategory";
            this.selectedCategory.Size = new System.Drawing.Size(341, 52);
            this.selectedCategory.TabIndex = 2;
            this.selectedCategory.Text = "Ngành hàng: —";
            this.selectedCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stockCaption
            // 
            this.stockCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stockCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.stockCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.stockCaption.Location = new System.Drawing.Point(3, 150);
            this.stockCaption.Name = "stockCaption";
            this.stockCaption.Size = new System.Drawing.Size(341, 24);
            this.stockCaption.TabIndex = 3;
            this.stockCaption.Text = "SỐ LƯỢNG TỒN";
            this.stockCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // selectedStock
            // 
            this.selectedStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedStock.Font = new System.Drawing.Font("Segoe UI", 23F, System.Drawing.FontStyle.Bold);
            this.selectedStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(148)))), ((int)(((byte)(136)))));
            this.selectedStock.Location = new System.Drawing.Point(3, 174);
            this.selectedStock.Name = "selectedStock";
            this.selectedStock.Size = new System.Drawing.Size(341, 52);
            this.selectedStock.TabIndex = 4;
            this.selectedStock.Text = "—";
            this.selectedStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // addStockButton
            // 
            this.addStockButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(148)))), ((int)(((byte)(136)))));
            this.addStockButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addStockButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addStockButton.FlatAppearance.BorderSize = 0;
            this.addStockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addStockButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.addStockButton.ForeColor = System.Drawing.Color.White;
            this.addStockButton.Location = new System.Drawing.Point(0, 229);
            this.addStockButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 5);
            this.addStockButton.Name = "addStockButton";
            this.addStockButton.Size = new System.Drawing.Size(347, 34);
            this.addStockButton.TabIndex = 5;
            this.addStockButton.Text = "+  Nhập bổ sung";
            this.addStockButton.UseVisualStyleBackColor = false;
            // 
            // removeStockButton
            // 
            this.removeStockButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.removeStockButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removeStockButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeStockButton.FlatAppearance.BorderSize = 0;
            this.removeStockButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeStockButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.removeStockButton.ForeColor = System.Drawing.Color.White;
            this.removeStockButton.Location = new System.Drawing.Point(0, 268);
            this.removeStockButton.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.removeStockButton.Name = "removeStockButton";
            this.removeStockButton.Size = new System.Drawing.Size(347, 38);
            this.removeStockButton.TabIndex = 6;
            this.removeStockButton.Text = "−  Xuất / giảm tồn";
            this.removeStockButton.UseVisualStyleBackColor = false;
            // 
            // statusPanel
            // 
            this.statusPanel.BackColor = System.Drawing.Color.Transparent;
            this.statusPanel.Controls.Add(this.status);
            this.statusPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusPanel.Location = new System.Drawing.Point(16, 724);
            this.statusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(1248, 28);
            this.statusPanel.TabIndex = 4;
            // 
            // status
            // 
            this.status.AutoEllipsis = true;
            this.status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.status.Location = new System.Drawing.Point(0, 0);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(1248, 28);
            this.status.TabIndex = 0;
            this.status.Text = "Dữ liệu được đồng bộ từ API MiniMart.";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rowMenu
            // 
            this.rowMenu.Name = "rowMenu";
            this.rowMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // FormInventoryManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.mainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "FormInventoryManagement";
            this.Text = "MiniMart | Quản lý kho & tồn kho";
            this.mainLayout.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.kpiLayout.ResumeLayout(false);
            this.totalCard.ResumeLayout(false);
            this.totalStockCard.ResumeLayout(false);
            this.lowCard.ResumeLayout(false);
            this.outCard.ResumeLayout(false);
            this.filterPanel.ResumeLayout(false);
            this.filterLayout.ResumeLayout(false);
            this.filterLayout.PerformLayout();
            this.contentLayout.ResumeLayout(false);
            this.listCard.ResumeLayout(false);
            this.listLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.detailCard.ResumeLayout(false);
            this.detailLayout.ResumeLayout(false);
            this.statusPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
