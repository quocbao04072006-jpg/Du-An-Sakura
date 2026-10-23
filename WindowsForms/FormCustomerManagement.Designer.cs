namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelKpi;
        private System.Windows.Forms.Panel panelContent;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnLoad;

        private System.Windows.Forms.Panel cardTotal;
        private System.Windows.Forms.Panel cardNew;
        private System.Windows.Forms.Panel cardPoints;
        private System.Windows.Forms.Panel cardVip;

        private System.Windows.Forms.Label lblTotalCustomers;
        private System.Windows.Forms.Label lblTotalCustomersValue;

        private System.Windows.Forms.Label lblNewCustomers;
        private System.Windows.Forms.Label lblNewCustomersValue;

        private System.Windows.Forms.Label lblTotalPoints;
        private System.Windows.Forms.Label lblTotalPointsValue;

        private System.Windows.Forms.Label lblVipCustomers;
        private System.Windows.Forms.Label lblVipCustomersValue;

        private System.Windows.Forms.GroupBox grpCustomerDetail;
        private System.Windows.Forms.GroupBox grpCustomerList;

        private System.Windows.Forms.Label lblCustomerId;
        private System.Windows.Forms.TextBox txtCustomerId;

        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.TextBox txtCustomerName;

        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.TextBox txtPhoneNumber;

        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;

        private System.Windows.Forms.Label lblMembershipRank;
        private System.Windows.Forms.ComboBox cboMembershipRank;

        private System.Windows.Forms.Label lblRewardPoints;
        private System.Windows.Forms.TextBox txtRewardPoints;

        private System.Windows.Forms.Label lblCreatedAt;
        private System.Windows.Forms.DateTimePicker dtpCreatedAt;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;

        private System.Windows.Forms.DataGridView dgvCustomers;

        private System.Windows.Forms.Label customerStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing &&
                (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.panelKpi = new System.Windows.Forms.Panel();
            this.cardTotal = new System.Windows.Forms.Panel();
            this.lblTotalCustomers = new System.Windows.Forms.Label();
            this.lblTotalCustomersValue = new System.Windows.Forms.Label();
            this.cardNew = new System.Windows.Forms.Panel();
            this.lblNewCustomers = new System.Windows.Forms.Label();
            this.lblNewCustomersValue = new System.Windows.Forms.Label();
            this.cardPoints = new System.Windows.Forms.Panel();
            this.lblTotalPoints = new System.Windows.Forms.Label();
            this.lblTotalPointsValue = new System.Windows.Forms.Label();
            this.cardVip = new System.Windows.Forms.Panel();
            this.lblVipCustomers = new System.Windows.Forms.Label();
            this.lblVipCustomersValue = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.grpCustomerDetail = new System.Windows.Forms.GroupBox();
            this.lblCustomerId = new System.Windows.Forms.Label();
            this.txtCustomerId = new System.Windows.Forms.TextBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblMembershipRank = new System.Windows.Forms.Label();
            this.cboMembershipRank = new System.Windows.Forms.ComboBox();
            this.lblRewardPoints = new System.Windows.Forms.Label();
            this.txtRewardPoints = new System.Windows.Forms.TextBox();
            this.lblCreatedAt = new System.Windows.Forms.Label();
            this.dtpCreatedAt = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.grpCustomerList = new System.Windows.Forms.GroupBox();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.customerStatus = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelKpi.SuspendLayout();
            this.cardTotal.SuspendLayout();
            this.cardNew.SuspendLayout();
            this.cardPoints.SuspendLayout();
            this.cardVip.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.grpCustomerDetail.SuspendLayout();
            this.grpCustomerList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblSubTitle);
            this.panelTop.Controls.Add(this.lblSearch);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.btnLoad);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(19, 15, 19, 10);
            this.panelTop.Size = new System.Drawing.Size(945, 110);
            this.panelTop.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(19, 10);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(307, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "KHÁCH HÀNG TÍCH ĐIỂM";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubTitle.Location = new System.Drawing.Point(21, 38);
            this.lblSubTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(354, 15);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Quản lý hồ sơ thành viên, điểm thưởng và phân hạng khách hàng";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSearch.Location = new System.Drawing.Point(21, 74);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(111, 15);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "TÌM KHÁCH HÀNG";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(136, 69);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(325, 25);
            this.txtSearch.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(469, 68);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(79, 28);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Tìm kiếm";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(555, 68);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(79, 28);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Làm mới";
            // 
            // panelKpi
            // 
            this.panelKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.panelKpi.Controls.Add(this.cardTotal);
            this.panelKpi.Controls.Add(this.cardNew);
            this.panelKpi.Controls.Add(this.cardPoints);
            this.panelKpi.Controls.Add(this.cardVip);
            this.panelKpi.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelKpi.Location = new System.Drawing.Point(0, 110);
            this.panelKpi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelKpi.Name = "panelKpi";
            this.panelKpi.Padding = new System.Windows.Forms.Padding(19, 10, 19, 10);
            this.panelKpi.Size = new System.Drawing.Size(945, 77);
            this.panelKpi.TabIndex = 1;
            // 
            // cardTotal
            // 
            this.cardTotal.BackColor = System.Drawing.Color.White;
            this.cardTotal.Controls.Add(this.lblTotalCustomers);
            this.cardTotal.Controls.Add(this.lblTotalCustomersValue);
            this.cardTotal.Location = new System.Drawing.Point(19, 10);
            this.cardTotal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cardTotal.Name = "cardTotal";
            this.cardTotal.Size = new System.Drawing.Size(172, 57);
            this.cardTotal.TabIndex = 0;
            // 
            // lblTotalCustomers
            // 
            this.lblTotalCustomers.AutoSize = true;
            this.lblTotalCustomers.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTotalCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTotalCustomers.Location = new System.Drawing.Point(11, 8);
            this.lblTotalCustomers.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalCustomers.Name = "lblTotalCustomers";
            this.lblTotalCustomers.Size = new System.Drawing.Size(121, 15);
            this.lblTotalCustomers.TabIndex = 0;
            this.lblTotalCustomers.Text = "TỔNG KHÁCH HÀNG";
            // 
            // lblTotalCustomersValue
            // 
            this.lblTotalCustomersValue.AutoSize = true;
            this.lblTotalCustomersValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalCustomersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTotalCustomersValue.Location = new System.Drawing.Point(11, 24);
            this.lblTotalCustomersValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalCustomersValue.Name = "lblTotalCustomersValue";
            this.lblTotalCustomersValue.Size = new System.Drawing.Size(33, 37);
            this.lblTotalCustomersValue.TabIndex = 1;
            this.lblTotalCustomersValue.Text = "0";
            // 
            // cardNew
            // 
            this.cardNew.BackColor = System.Drawing.Color.White;
            this.cardNew.Controls.Add(this.lblNewCustomers);
            this.cardNew.Controls.Add(this.lblNewCustomersValue);
            this.cardNew.Location = new System.Drawing.Point(202, 10);
            this.cardNew.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cardNew.Name = "cardNew";
            this.cardNew.Size = new System.Drawing.Size(172, 57);
            this.cardNew.TabIndex = 1;
            // 
            // lblNewCustomers
            // 
            this.lblNewCustomers.AutoSize = true;
            this.lblNewCustomers.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblNewCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblNewCustomers.Location = new System.Drawing.Point(11, 8);
            this.lblNewCustomers.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNewCustomers.Name = "lblNewCustomers";
            this.lblNewCustomers.Size = new System.Drawing.Size(144, 15);
            this.lblNewCustomers.TabIndex = 0;
            this.lblNewCustomers.Text = "KHÁCH MỚI THÁNG NÀY";
            // 
            // lblNewCustomersValue
            // 
            this.lblNewCustomersValue.AutoSize = true;
            this.lblNewCustomersValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblNewCustomersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(148)))), ((int)(((byte)(136)))));
            this.lblNewCustomersValue.Location = new System.Drawing.Point(11, 24);
            this.lblNewCustomersValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNewCustomersValue.Name = "lblNewCustomersValue";
            this.lblNewCustomersValue.Size = new System.Drawing.Size(33, 37);
            this.lblNewCustomersValue.TabIndex = 1;
            this.lblNewCustomersValue.Text = "0";
            // 
            // cardPoints
            // 
            this.cardPoints.BackColor = System.Drawing.Color.White;
            this.cardPoints.Controls.Add(this.lblTotalPoints);
            this.cardPoints.Controls.Add(this.lblTotalPointsValue);
            this.cardPoints.Location = new System.Drawing.Point(386, 10);
            this.cardPoints.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cardPoints.Name = "cardPoints";
            this.cardPoints.Size = new System.Drawing.Size(172, 57);
            this.cardPoints.TabIndex = 2;
            // 
            // lblTotalPoints
            // 
            this.lblTotalPoints.AutoSize = true;
            this.lblTotalPoints.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTotalPoints.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblTotalPoints.Location = new System.Drawing.Point(11, 8);
            this.lblTotalPoints.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalPoints.Name = "lblTotalPoints";
            this.lblTotalPoints.Size = new System.Drawing.Size(125, 15);
            this.lblTotalPoints.TabIndex = 0;
            this.lblTotalPoints.Text = "TỔNG ĐIỂM TÍCH LŨY";
            // 
            // lblTotalPointsValue
            // 
            this.lblTotalPointsValue.AutoSize = true;
            this.lblTotalPointsValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalPointsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblTotalPointsValue.Location = new System.Drawing.Point(11, 24);
            this.lblTotalPointsValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalPointsValue.Name = "lblTotalPointsValue";
            this.lblTotalPointsValue.Size = new System.Drawing.Size(33, 37);
            this.lblTotalPointsValue.TabIndex = 1;
            this.lblTotalPointsValue.Text = "0";
            // 
            // cardVip
            // 
            this.cardVip.BackColor = System.Drawing.Color.White;
            this.cardVip.Controls.Add(this.lblVipCustomers);
            this.cardVip.Controls.Add(this.lblVipCustomersValue);
            this.cardVip.Location = new System.Drawing.Point(570, 10);
            this.cardVip.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cardVip.Name = "cardVip";
            this.cardVip.Size = new System.Drawing.Size(172, 57);
            this.cardVip.TabIndex = 3;
            // 
            // lblVipCustomers
            // 
            this.lblVipCustomers.AutoSize = true;
            this.lblVipCustomers.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblVipCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblVipCustomers.Location = new System.Drawing.Point(11, 8);
            this.lblVipCustomers.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVipCustomers.Name = "lblVipCustomers";
            this.lblVipCustomers.Size = new System.Drawing.Size(105, 15);
            this.lblVipCustomers.TabIndex = 0;
            this.lblVipCustomers.Text = "KHÁCH HÀNG VIP";
            // 
            // lblVipCustomersValue
            // 
            this.lblVipCustomersValue.AutoSize = true;
            this.lblVipCustomersValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblVipCustomersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.lblVipCustomersValue.Location = new System.Drawing.Point(11, 24);
            this.lblVipCustomersValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVipCustomersValue.Name = "lblVipCustomersValue";
            this.lblVipCustomersValue.Size = new System.Drawing.Size(33, 37);
            this.lblVipCustomersValue.TabIndex = 1;
            this.lblVipCustomersValue.Text = "0";
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.panelContent.Controls.Add(this.grpCustomerDetail);
            this.panelContent.Controls.Add(this.grpCustomerList);
            this.panelContent.Controls.Add(this.customerStatus);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 187);
            this.panelContent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(19, 15, 19, 8);
            this.panelContent.Size = new System.Drawing.Size(945, 398);
            this.panelContent.TabIndex = 0;
            // 
            // grpCustomerDetail
            // 
            this.grpCustomerDetail.BackColor = System.Drawing.Color.White;
            this.grpCustomerDetail.Controls.Add(this.lblCustomerId);
            this.grpCustomerDetail.Controls.Add(this.txtCustomerId);
            this.grpCustomerDetail.Controls.Add(this.lblCustomerName);
            this.grpCustomerDetail.Controls.Add(this.txtCustomerName);
            this.grpCustomerDetail.Controls.Add(this.lblPhoneNumber);
            this.grpCustomerDetail.Controls.Add(this.txtPhoneNumber);
            this.grpCustomerDetail.Controls.Add(this.lblAddress);
            this.grpCustomerDetail.Controls.Add(this.txtAddress);
            this.grpCustomerDetail.Controls.Add(this.lblMembershipRank);
            this.grpCustomerDetail.Controls.Add(this.cboMembershipRank);
            this.grpCustomerDetail.Controls.Add(this.lblRewardPoints);
            this.grpCustomerDetail.Controls.Add(this.txtRewardPoints);
            this.grpCustomerDetail.Controls.Add(this.lblCreatedAt);
            this.grpCustomerDetail.Controls.Add(this.dtpCreatedAt);
            this.grpCustomerDetail.Controls.Add(this.btnAdd);
            this.grpCustomerDetail.Controls.Add(this.btnUpdate);
            this.grpCustomerDetail.Controls.Add(this.btnDelete);
            this.grpCustomerDetail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpCustomerDetail.Location = new System.Drawing.Point(19, 15);
            this.grpCustomerDetail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpCustomerDetail.Name = "grpCustomerDetail";
            this.grpCustomerDetail.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpCustomerDetail.Size = new System.Drawing.Size(248, 362);
            this.grpCustomerDetail.TabIndex = 0;
            this.grpCustomerDetail.TabStop = false;
            this.grpCustomerDetail.Text = "CHI TIẾT KHÁCH HÀNG";
            // 
            // lblCustomerId
            // 
            this.lblCustomerId.AutoSize = true;
            this.lblCustomerId.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomerId.Location = new System.Drawing.Point(11, 28);
            this.lblCustomerId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomerId.Name = "lblCustomerId";
            this.lblCustomerId.Size = new System.Drawing.Size(90, 15);
            this.lblCustomerId.TabIndex = 0;
            this.lblCustomerId.Text = "Mã khách hàng";
            // 
            // txtCustomerId
            // 
            this.txtCustomerId.Location = new System.Drawing.Point(11, 46);
            this.txtCustomerId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCustomerId.Name = "txtCustomerId";
            this.txtCustomerId.Size = new System.Drawing.Size(222, 25);
            this.txtCustomerId.TabIndex = 1;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.Location = new System.Drawing.Point(11, 74);
            this.lblCustomerName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(61, 15);
            this.lblCustomerName.TabIndex = 2;
            this.lblCustomerName.Text = "Họ và tên";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(11, 91);
            this.txtCustomerName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(222, 25);
            this.txtCustomerName.TabIndex = 3;
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhoneNumber.Location = new System.Drawing.Point(11, 119);
            this.lblPhoneNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(80, 15);
            this.lblPhoneNumber.TabIndex = 4;
            this.lblPhoneNumber.Text = "Số điện thoại";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(11, 136);
            this.txtPhoneNumber.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(222, 25);
            this.txtPhoneNumber.TabIndex = 5;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAddress.Location = new System.Drawing.Point(11, 165);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(44, 15);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "Địa chỉ";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(11, 182);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(222, 25);
            this.txtAddress.TabIndex = 7;
            // 
            // lblMembershipRank
            // 
            this.lblMembershipRank.AutoSize = true;
            this.lblMembershipRank.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMembershipRank.Location = new System.Drawing.Point(11, 210);
            this.lblMembershipRank.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMembershipRank.Name = "lblMembershipRank";
            this.lblMembershipRank.Size = new System.Drawing.Size(102, 15);
            this.lblMembershipRank.TabIndex = 8;
            this.lblMembershipRank.Text = "Hạng khách hàng";
            // 
            // cboMembershipRank
            // 
            this.cboMembershipRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMembershipRank.FormattingEnabled = true;
            this.cboMembershipRank.Location = new System.Drawing.Point(11, 228);
            this.cboMembershipRank.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboMembershipRank.Name = "cboMembershipRank";
            this.cboMembershipRank.Size = new System.Drawing.Size(106, 25);
            this.cboMembershipRank.TabIndex = 9;
            // 
            // lblRewardPoints
            // 
            this.lblRewardPoints.AutoSize = true;
            this.lblRewardPoints.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRewardPoints.Location = new System.Drawing.Point(128, 210);
            this.lblRewardPoints.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRewardPoints.Name = "lblRewardPoints";
            this.lblRewardPoints.Size = new System.Drawing.Size(80, 15);
            this.lblRewardPoints.TabIndex = 10;
            this.lblRewardPoints.Text = "Điểm tích lũy";
            // 
            // txtRewardPoints
            // 
            this.txtRewardPoints.Location = new System.Drawing.Point(128, 228);
            this.txtRewardPoints.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRewardPoints.Name = "txtRewardPoints";
            this.txtRewardPoints.Size = new System.Drawing.Size(106, 25);
            this.txtRewardPoints.TabIndex = 11;
            // 
            // lblCreatedAt
            // 
            this.lblCreatedAt.AutoSize = true;
            this.lblCreatedAt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCreatedAt.Location = new System.Drawing.Point(11, 256);
            this.lblCreatedAt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCreatedAt.Name = "lblCreatedAt";
            this.lblCreatedAt.Size = new System.Drawing.Size(86, 15);
            this.lblCreatedAt.TabIndex = 12;
            this.lblCreatedAt.Text = "Ngày tham gia";
            // 
            // dtpCreatedAt
            // 
            this.dtpCreatedAt.CustomFormat = "dd/MM/yyyy";
            this.dtpCreatedAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreatedAt.Location = new System.Drawing.Point(11, 273);
            this.dtpCreatedAt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpCreatedAt.Name = "dtpCreatedAt";
            this.dtpCreatedAt.ShowCheckBox = true;
            this.dtpCreatedAt.Size = new System.Drawing.Size(222, 25);
            this.dtpCreatedAt.TabIndex = 13;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(11, 310);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(66, 29);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "+ Thêm";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(81, 310);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 29);
            this.btnUpdate.TabIndex = 15;
            this.btnUpdate.Text = "Cập nhật";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(160, 310);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(73, 29);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Xóa";
            // 
            // grpCustomerList
            // 
            this.grpCustomerList.BackColor = System.Drawing.Color.White;
            this.grpCustomerList.Controls.Add(this.dgvCustomers);
            this.grpCustomerList.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpCustomerList.Location = new System.Drawing.Point(281, 15);
            this.grpCustomerList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpCustomerList.Name = "grpCustomerList";
            this.grpCustomerList.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpCustomerList.Size = new System.Drawing.Size(626, 362);
            this.grpCustomerList.TabIndex = 1;
            this.grpCustomerList.TabStop = false;
            this.grpCustomerList.Text = "DANH SÁCH THÀNH VIÊN";
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCustomers.Location = new System.Drawing.Point(11, 24);
            this.dgvCustomers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.Size = new System.Drawing.Size(604, 321);
            this.dgvCustomers.TabIndex = 0;
            // 
            // customerStatus
            // 
            this.customerStatus.AutoSize = true;
            this.customerStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.customerStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.customerStatus.Location = new System.Drawing.Point(21, 390);
            this.customerStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.customerStatus.Name = "customerStatus";
            this.customerStatus.Size = new System.Drawing.Size(57, 15);
            this.customerStatus.TabIndex = 2;
            this.customerStatus.Text = "Sẵn sàng.";
            // 
            // FormCustomerManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(945, 585);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelKpi);
            this.Controls.Add(this.panelTop);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(829, 535);
            this.Name = "FormCustomerManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiniMart | Quản lý khách hàng";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelKpi.ResumeLayout(false);
            this.cardTotal.ResumeLayout(false);
            this.cardTotal.PerformLayout();
            this.cardNew.ResumeLayout(false);
            this.cardNew.PerformLayout();
            this.cardPoints.ResumeLayout(false);
            this.cardPoints.PerformLayout();
            this.cardVip.ResumeLayout(false);
            this.cardVip.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.grpCustomerDetail.ResumeLayout(false);
            this.grpCustomerDetail.PerformLayout();
            this.grpCustomerList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}