namespace UDSV
{
    partial class FrmSinhVien
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pnlHeader = new Controls.GradientPanel();
            lblHeaderSub = new Label();
            lblHeaderTitle = new Label();
            cardInput = new Controls.CardPanel();
            btnLamMoi = new Controls.GradientButton();
            btnXoa = new Controls.GradientButton();
            btnSua = new Controls.GradientButton();
            btnThem = new Controls.GradientButton();
            cboMaKhoa = new ComboBox();
            label6 = new Label();
            txtSDT = new TextBox();
            label7 = new Label();
            cboGioiTinh = new ComboBox();
            label5 = new Label();
            dtpNgaySinh = new DateTimePicker();
            label4 = new Label();
            txtHoTen = new TextBox();
            label2 = new Label();
            txtMaSV = new TextBox();
            label3 = new Label();
            lblSectionInfo = new Label();
            cardData = new Controls.CardPanel();
            dgvSinhVien = new DataGridView();
            btnTimKiem = new Controls.GradientButton();
            txtTimKiem = new TextBox();
            lblSearch = new Label();
            lblListTitle = new Label();
            pnlHeader.SuspendLayout();
            cardInput.SuspendLayout();
            cardData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Angle = 135F;
            pnlHeader.ColorEnd = Color.FromArgb(118, 75, 162);
            pnlHeader.ColorStart = Color.FromArgb(102, 126, 234);
            pnlHeader.Controls.Add(lblHeaderSub);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1260, 75);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeaderSub
            // 
            lblHeaderSub.AutoSize = true;
            lblHeaderSub.BackColor = Color.Transparent;
            lblHeaderSub.Font = new Font("Segoe UI", 9.5F);
            lblHeaderSub.ForeColor = Color.FromArgb(230, 240, 255);
            lblHeaderSub.Location = new Point(28, 42);
            lblHeaderSub.Name = "lblHeaderSub";
            lblHeaderSub.Size = new Size(393, 21);
            lblHeaderSub.TabIndex = 1;
            lblHeaderSub.Text = "Hệ thống quản lý thông tin, hồ sơ học thuật của sinh viên";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.BackColor = Color.Transparent;
            lblHeaderTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(25, 8);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(318, 41);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "QUẢN LÝ SINH VIÊN";
            // 
            // cardInput
            // 
            cardInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cardInput.BackColor = Color.White;
            cardInput.Controls.Add(btnLamMoi);
            cardInput.Controls.Add(btnXoa);
            cardInput.Controls.Add(btnSua);
            cardInput.Controls.Add(btnThem);
            cardInput.Controls.Add(cboMaKhoa);
            cardInput.Controls.Add(label6);
            cardInput.Controls.Add(txtSDT);
            cardInput.Controls.Add(label7);
            cardInput.Controls.Add(cboGioiTinh);
            cardInput.Controls.Add(label5);
            cardInput.Controls.Add(dtpNgaySinh);
            cardInput.Controls.Add(label4);
            cardInput.Controls.Add(txtHoTen);
            cardInput.Controls.Add(label2);
            cardInput.Controls.Add(txtMaSV);
            cardInput.Controls.Add(label3);
            cardInput.Controls.Add(lblSectionInfo);
            cardInput.Location = new Point(25, 90);
            cardInput.Name = "cardInput";
            cardInput.Radius = 14;
            cardInput.Size = new Size(1210, 185);
            cardInput.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLamMoi.BackColor = Color.Transparent;
            btnLamMoi.Cursor = Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnLamMoi.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLamMoi.ForeColor = Color.White;
            btnLamMoi.GradientEnd = Color.FromArgb(73, 80, 87);
            btnLamMoi.GradientStart = Color.FromArgb(108, 117, 125);
            btnLamMoi.Location = new Point(1045, 112);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Radius = 12;
            btnLamMoi.Size = new Size(145, 42);
            btnLamMoi.TabIndex = 9;
            btnLamMoi.Text = "↻ Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnXoa.BackColor = Color.Transparent;
            btnXoa.Cursor = Cursors.Hand;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnXoa.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXoa.ForeColor = Color.White;
            btnXoa.GradientEnd = Color.FromArgb(192, 57, 43);
            btnXoa.GradientStart = Color.FromArgb(231, 76, 60);
            btnXoa.Location = new Point(885, 112);
            btnXoa.Name = "btnXoa";
            btnXoa.Radius = 12;
            btnXoa.Size = new Size(145, 42);
            btnXoa.TabIndex = 8;
            btnXoa.Text = "🗑 Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSua.BackColor = Color.Transparent;
            btnSua.Cursor = Cursors.Hand;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSua.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSua.ForeColor = Color.White;
            btnSua.GradientEnd = Color.FromArgb(230, 126, 34);
            btnSua.GradientStart = Color.FromArgb(243, 156, 18);
            btnSua.Location = new Point(1045, 52);
            btnSua.Name = "btnSua";
            btnSua.Radius = 12;
            btnSua.Size = new Size(145, 42);
            btnSua.TabIndex = 7;
            btnSua.Text = "✎ Sửa";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnThem
            // 
            btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThem.BackColor = Color.Transparent;
            btnThem.Cursor = Cursors.Hand;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnThem.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnThem.ForeColor = Color.White;
            btnThem.GradientEnd = Color.FromArgb(39, 174, 96);
            btnThem.GradientStart = Color.FromArgb(46, 204, 113);
            btnThem.Location = new Point(885, 52);
            btnThem.Name = "btnThem";
            btnThem.Radius = 12;
            btnThem.Size = new Size(145, 42);
            btnThem.TabIndex = 6;
            btnThem.Text = "＋ Thêm mới";
            btnThem.UseVisualStyleBackColor = false;
            // 
            // cboMaKhoa
            // 
            cboMaKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMaKhoa.Font = new Font("Segoe UI", 10F);
            cboMaKhoa.FormattingEnabled = true;
            cboMaKhoa.Location = new Point(25, 126);
            cboMaKhoa.Name = "cboMaKhoa";
            cboMaKhoa.Size = new Size(265, 31);
            cboMaKhoa.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(70, 80, 95);
            label6.Location = new Point(25, 103);
            label6.Name = "label6";
            label6.Size = new Size(74, 20);
            label6.TabIndex = 11;
            label6.Text = "Khóa học";
            // 
            // txtSDT
            // 
            txtSDT.BorderStyle = BorderStyle.FixedSingle;
            txtSDT.Font = new Font("Segoe UI", 10F);
            txtSDT.Location = new Point(310, 127);
            txtSDT.Name = "txtSDT";
            txtSDT.PlaceholderText = "Số điện thoại liên hệ";
            txtSDT.Size = new Size(250, 30);
            txtSDT.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(70, 80, 95);
            label7.Location = new Point(310, 103);
            label7.Name = "label7";
            label7.Size = new Size(100, 20);
            label7.TabIndex = 9;
            label7.Text = "Số điện thoại";
            // 
            // cboGioiTinh
            // 
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.Font = new Font("Segoe UI", 10F);
            cboGioiTinh.FormattingEnabled = true;
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGioiTinh.Location = new Point(745, 60);
            cboGioiTinh.Name = "cboGioiTinh";
            cboGioiTinh.Size = new Size(110, 31);
            cboGioiTinh.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(70, 80, 95);
            label5.Location = new Point(745, 37);
            label5.Name = "label5";
            label5.Size = new Size(69, 20);
            label5.TabIndex = 7;
            label5.Text = "Giới tính";
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.CalendarFont = new Font("Segoe UI", 10F);
            dtpNgaySinh.Font = new Font("Segoe UI", 10F);
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(580, 60);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(145, 30);
            dtpNgaySinh.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(70, 80, 95);
            label4.Location = new Point(580, 37);
            label4.Name = "label4";
            label4.Size = new Size(79, 20);
            label4.TabIndex = 5;
            label4.Text = "Ngày sinh";
            // 
            // txtHoTen
            // 
            txtHoTen.BorderStyle = BorderStyle.FixedSingle;
            txtHoTen.Font = new Font("Segoe UI", 10F);
            txtHoTen.Location = new Point(205, 60);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.PlaceholderText = "Nhập đầy đủ họ và tên";
            txtHoTen.Size = new Size(355, 30);
            txtHoTen.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(70, 80, 95);
            label2.Location = new Point(205, 37);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 3;
            label2.Text = "Họ và tên";
            // 
            // txtMaSV
            // 
            txtMaSV.BorderStyle = BorderStyle.FixedSingle;
            txtMaSV.Font = new Font("Segoe UI", 10F);
            txtMaSV.Location = new Point(25, 60);
            txtMaSV.Name = "txtMaSV";
            txtMaSV.PlaceholderText = "Ví dụ: SV001";
            txtMaSV.Size = new Size(160, 30);
            txtMaSV.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(70, 80, 95);
            label3.Location = new Point(25, 37);
            label3.Name = "label3";
            label3.Size = new Size(97, 20);
            label3.TabIndex = 1;
            label3.Text = "Mã sinh viên";
            // 
            // lblSectionInfo
            // 
            lblSectionInfo.AutoSize = true;
            lblSectionInfo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSectionInfo.ForeColor = Color.FromArgb(102, 126, 234);
            lblSectionInfo.Location = new Point(20, 12);
            lblSectionInfo.Name = "lblSectionInfo";
            lblSectionInfo.Size = new Size(227, 25);
            lblSectionInfo.TabIndex = 0;
            lblSectionInfo.Text = "THÔNG TIN SINH VIÊN";
            // 
            // cardData
            // 
            cardData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cardData.BackColor = Color.White;
            cardData.Controls.Add(dgvSinhVien);
            cardData.Controls.Add(btnTimKiem);
            cardData.Controls.Add(txtTimKiem);
            cardData.Controls.Add(lblSearch);
            cardData.Controls.Add(lblListTitle);
            cardData.Location = new Point(25, 290);
            cardData.Name = "cardData";
            cardData.Radius = 14;
            cardData.Size = new Size(1210, 425);
            cardData.TabIndex = 2;
            // 
            // dgvSinhVien
            // 
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AllowUserToDeleteRows = false;
            dgvSinhVien.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 250, 252);
            dgvSinhVien.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvSinhVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.BackgroundColor = Color.White;
            dgvSinhVien.BorderStyle = BorderStyle.None;
            dgvSinhVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSinhVien.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(102, 126, 234);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new Padding(8, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(102, 126, 234);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvSinhVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvSinhVien.ColumnHeadersHeight = 40;
            dgvSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(45, 52, 54);
            dataGridViewCellStyle3.Padding = new Padding(8, 0, 0, 0);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(224, 231, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(30, 27, 75);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvSinhVien.DefaultCellStyle = dataGridViewCellStyle3;
            dgvSinhVien.EnableHeadersVisualStyles = false;
            dgvSinhVien.GridColor = Color.FromArgb(238, 242, 246);
            dgvSinhVien.Location = new Point(20, 58);
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.Name = "dgvSinhVien";
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvSinhVien.RowHeadersVisible = false;
            dgvSinhVien.RowHeadersWidth = 51;
            dgvSinhVien.RowTemplate.Height = 36;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.Size = new Size(1170, 345);
            dgvSinhVien.TabIndex = 3;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTimKiem.BackColor = Color.Transparent;
            btnTimKiem.Cursor = Cursors.Hand;
            btnTimKiem.FlatAppearance.BorderSize = 0;
            btnTimKiem.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnTimKiem.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnTimKiem.FlatStyle = FlatStyle.Flat;
            btnTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnTimKiem.ForeColor = Color.White;
            btnTimKiem.GradientEnd = Color.FromArgb(118, 75, 162);
            btnTimKiem.GradientStart = Color.FromArgb(102, 126, 234);
            btnTimKiem.Location = new Point(1075, 12);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Radius = 10;
            btnTimKiem.Size = new Size(115, 36);
            btnTimKiem.TabIndex = 2;
            btnTimKiem.Text = "🔍 Tìm kiếm";
            btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtTimKiem.BorderStyle = BorderStyle.FixedSingle;
            txtTimKiem.Font = new Font("Segoe UI", 10F);
            txtTimKiem.Location = new Point(745, 15);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Nhập Mã SV hoặc Họ tên để tìm...";
            txtTimKiem.Size = new Size(315, 30);
            txtTimKiem.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(90, 100, 115);
            lblSearch.Location = new Point(660, 18);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(79, 21);
            lblSearch.TabIndex = 1;
            lblSearch.Text = "Tìm kiếm:";
            // 
            // lblListTitle
            // 
            lblListTitle.AutoSize = true;
            lblListTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblListTitle.ForeColor = Color.FromArgb(102, 126, 234);
            lblListTitle.Location = new Point(20, 16);
            lblListTitle.Name = "lblListTitle";
            lblListTitle.Size = new Size(228, 25);
            lblListTitle.TabIndex = 0;
            lblListTitle.Text = "DANH SÁCH SINH VIÊN";
            // 
            // FrmSinhVien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1260, 740);
            Controls.Add(cardData);
            Controls.Add(cardInput);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1100, 650);
            Name = "FrmSinhVien";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý sinh viên";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            cardInput.ResumeLayout(false);
            cardInput.PerformLayout();
            cardData.ResumeLayout(false);
            cardData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Controls.GradientPanel pnlHeader;
        private Label lblHeaderTitle;
        private Label lblHeaderSub;
        private Controls.CardPanel cardInput;
        private Label lblSectionInfo;
        private TextBox txtMaSV;
        private Label label3;
        private TextBox txtHoTen;
        private Label label2;
        private DateTimePicker dtpNgaySinh;
        private Label label4;
        private ComboBox cboGioiTinh;
        private Label label5;
        private ComboBox cboMaKhoa;
        private Label label6;
        private TextBox txtSDT;
        private Label label7;
        private Controls.GradientButton btnThem;
        private Controls.GradientButton btnSua;
        private Controls.GradientButton btnXoa;
        private Controls.GradientButton btnLamMoi;
        private Controls.CardPanel cardData;
        private Label lblListTitle;
        private Label lblSearch;
        private TextBox txtTimKiem;
        private Controls.GradientButton btnTimKiem;
        private DataGridView dgvSinhVien;
    }
}
