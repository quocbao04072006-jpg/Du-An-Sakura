using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
    {
        private System.ComponentModel.IContainer components = null;

        private GroupBox gbTimKiem;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;

        private GroupBox gbDanhSach;
        private DataGridView dgvCategories;

        private GroupBox gbThongTin;
        private Label lblId;
        private TextBox txtId;
        private Label lblCategoryName;
        private TextBox txtCategoryName;
        private Label lblDescription;
        private TextBox txtDescription;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

        private Panel panelPagination;
        private Button btnFirst;
        private Button btnPrevious;
        private Label lblPage;
        private Button btnNext;
        private Button btnLast;
        private Label lblPageInfo;
        private Label lblPageSize;
        private ComboBox cboPageSize;

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            gbTimKiem = new GroupBox();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnLoad = new Button();

            gbDanhSach = new GroupBox();
            dgvCategories = new DataGridView();

            gbThongTin = new GroupBox();
            lblId = new Label();
            txtId = new TextBox();
            lblCategoryName = new Label();
            txtCategoryName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();

            panelPagination = new Panel();
            btnFirst = new Button();
            btnPrevious = new Button();
            lblPage = new Label();
            btnNext = new Button();
            btnLast = new Button();
            lblPageInfo = new Label();
            lblPageSize = new Label();
            cboPageSize = new ComboBox();

            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();

            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            gbTimKiem.SuspendLayout();
            gbDanhSach.SuspendLayout();
            gbThongTin.SuspendLayout();
            panelPagination.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();

            // FORM
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 251);
            ClientSize = new Size(1200, 720);
            MinimumSize = new Size(1100, 680);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MiniMart | Quản lý danh mục nhóm hàng";
            Load += FormCategoryManagement_Load;

            // SEARCH
            gbTimKiem.Text = "Tìm kiếm danh mục";
            gbTimKiem.Location = new Point(190, 12);
            gbTimKiem.Size = new Size(535, 65);

            txtKeyword.Location = new Point(12, 24);
            txtKeyword.Size = new Size(335, 27);
            txtKeyword.KeyDown += txtKeyword_KeyDown;

            btnSearch.Text = "Tìm kiếm";
            btnSearch.Location = new Point(355, 22);
            btnSearch.Size = new Size(82, 30);
            btnSearch.BackColor = Color.FromArgb(37, 99, 235);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += btnSearch_Click;

            btnLoad.Text = "Tải lại";
            btnLoad.Location = new Point(445, 22);
            btnLoad.Size = new Size(78, 30);
            btnLoad.BackColor = Color.FromArgb(71, 85, 105);
            btnLoad.ForeColor = Color.White;
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.FlatAppearance.BorderSize = 0;
            btnLoad.Click += btnLoad_Click;

            gbTimKiem.Controls.Add(txtKeyword);
            gbTimKiem.Controls.Add(btnSearch);
            gbTimKiem.Controls.Add(btnLoad);

            // LIST
            gbDanhSach.Text = "Danh sách Nhóm hàng";
            gbDanhSach.Location = new Point(190, 88);
            gbDanhSach.Size = new Size(535, 590);

            dgvCategories.Location = new Point(8, 25);
            dgvCategories.Size = new Size(519, 480);
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.AllowUserToResizeRows = false;
            dgvCategories.ReadOnly = true;
            dgvCategories.MultiSelect = false;
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.BackgroundColor = Color.White;
            dgvCategories.BorderStyle = BorderStyle.FixedSingle;
            dgvCategories.EnableHeadersVisualStyles = false;
            dgvCategories.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(30, 41, 59);
            dgvCategories.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCategories.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvCategories.RowTemplate.Height = 32;
            dgvCategories.CellClick += dgvCategories_CellClick;

            gbDanhSach.Controls.Add(dgvCategories);

            // PAGINATION - ĐẶT NGAY BÊN DƯỚI BẢNG
            panelPagination.Location = new Point(8, 512);
            panelPagination.Size = new Size(519, 62);
            panelPagination.BackColor = Color.FromArgb(248, 250, 252);
            panelPagination.BorderStyle = BorderStyle.FixedSingle;

            btnFirst.Text = "Trang đầu";
            btnFirst.Location = new Point(8, 15);
            btnFirst.Size = new Size(72, 30);
            btnFirst.Click += btnFirst_Click;

            btnPrevious.Text = "<";
            btnPrevious.Location = new Point(85, 15);
            btnPrevious.Size = new Size(35, 30);
            btnPrevious.Click += btnPrevious_Click;

            lblPage.Text = "Trang 1 / 1";
            lblPage.Location = new Point(125, 20);
            lblPage.Size = new Size(78, 20);
            lblPage.TextAlign = ContentAlignment.MiddleCenter;
            lblPage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnNext.Text = ">";
            btnNext.Location = new Point(208, 15);
            btnNext.Size = new Size(35, 30);
            btnNext.Click += btnNext_Click;

            btnLast.Text = "Trang cuối";
            btnLast.Location = new Point(248, 15);
            btnLast.Size = new Size(72, 30);
            btnLast.Click += btnLast_Click;

            lblPageInfo.Text = "0 - 0 / 0";
            lblPageInfo.Location = new Point(326, 20);
            lblPageInfo.Size = new Size(125, 20);

            panelPagination.Controls.Add(btnFirst);
            panelPagination.Controls.Add(btnPrevious);
            panelPagination.Controls.Add(lblPage);
            panelPagination.Controls.Add(btnNext);
            panelPagination.Controls.Add(btnLast);
            panelPagination.Controls.Add(lblPageInfo);

            gbDanhSach.Controls.Add(panelPagination);

            // PAGE SIZE - ĐẶT NGOÀI GROUP LIST, LUÔN NHÌN THẤY
            lblPageSize.Text = "Số dòng:";
            lblPageSize.Location = new Point(735, 103);
            lblPageSize.Size = new Size(60, 23);

            cboPageSize.Location = new Point(795, 100);
            cboPageSize.Size = new Size(75, 27);
            cboPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPageSize.SelectedIndexChanged +=
                cboPageSize_SelectedIndexChanged;

            // DETAIL
            gbThongTin.Text = "Thông tin Nhóm hàng";
            gbThongTin.Location = new Point(745, 135);
            gbThongTin.Size = new Size(420, 543);

            lblId.Text = "Mã ID";
            lblId.Location = new Point(18, 32);
            lblId.AutoSize = true;

            txtId.Location = new Point(18, 52);
            txtId.Size = new Size(370, 27);
            txtId.ReadOnly = true;

            lblCategoryName.Text = "Tên Nhóm hàng";
            lblCategoryName.Location = new Point(18, 92);
            lblCategoryName.AutoSize = true;

            txtCategoryName.Location = new Point(18, 112);
            txtCategoryName.Size = new Size(370, 27);

            lblDescription.Text = "Mô tả";
            lblDescription.Location = new Point(18, 152);
            lblDescription.AutoSize = true;

            txtDescription.Location = new Point(18, 172);
            txtDescription.Size = new Size(370, 275);
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;

            btnAdd.Text = "Thêm mới";
            btnAdd.Location = new Point(18, 475);
            btnAdd.Size = new Size(105, 36);
            btnAdd.BackColor = Color.FromArgb(13, 148, 136);
            btnAdd.ForeColor = Color.White;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Text = "Cập nhật";
            btnUpdate.Location = new Point(133, 475);
            btnUpdate.Size = new Size(105, 36);
            btnUpdate.BackColor = Color.FromArgb(37, 99, 235);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Text = "Xóa";
            btnDelete.Location = new Point(248, 475);
            btnDelete.Size = new Size(105, 36);
            btnDelete.BackColor = Color.FromArgb(220, 38, 38);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += btnDelete_Click;

            gbThongTin.Controls.Add(lblId);
            gbThongTin.Controls.Add(txtId);
            gbThongTin.Controls.Add(lblCategoryName);
            gbThongTin.Controls.Add(txtCategoryName);
            gbThongTin.Controls.Add(lblDescription);
            gbThongTin.Controls.Add(txtDescription);
            gbThongTin.Controls.Add(btnAdd);
            gbThongTin.Controls.Add(btnUpdate);
            gbThongTin.Controls.Add(btnDelete);

            // STATUS
            statusStrip1.Dock = DockStyle.Bottom;
            statusStrip1.Items.Add(lblStatus);
            lblStatus.Text = "Sẵn sàng";

            Controls.Add(gbTimKiem);
            Controls.Add(gbDanhSach);
            Controls.Add(gbThongTin);
            Controls.Add(lblPageSize);
            Controls.Add(cboPageSize);
            Controls.Add(statusStrip1);

            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            gbTimKiem.ResumeLayout(false);
            gbTimKiem.PerformLayout();
            gbDanhSach.ResumeLayout(false);
            gbThongTin.ResumeLayout(false);
            gbThongTin.PerformLayout();
            panelPagination.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
