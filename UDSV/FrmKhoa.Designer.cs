namespace UDSV
{
    partial class FrmKhoa
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            pnlHeader = new UDSV.Controls.GradientPanel();
            lblSub = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            lblIcon = new System.Windows.Forms.Label();
            cardKhoa = new UDSV.Controls.CardPanel();
            btnLamMoi = new System.Windows.Forms.Button();
            btnXoa = new System.Windows.Forms.Button();
            btnSua = new System.Windows.Forms.Button();
            btnThem = new UDSV.Controls.GradientButton();
            numTCTuChon = new System.Windows.Forms.NumericUpDown();
            label7 = new System.Windows.Forms.Label();
            numTCBatBuoc = new System.Windows.Forms.NumericUpDown();
            label6 = new System.Windows.Forms.Label();
            numNamKetThuc = new System.Windows.Forms.NumericUpDown();
            label5 = new System.Windows.Forms.Label();
            numNamBatDau = new System.Windows.Forms.NumericUpDown();
            label4 = new System.Windows.Forms.Label();
            txtTenKhoa = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            txtMaKhoa = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            lblTitleKhoa = new System.Windows.Forms.Label();
            cardDsKhoa = new UDSV.Controls.CardPanel();
            lblDsKhoaSub = new System.Windows.Forms.Label();
            lblDsKhoaTitle = new System.Windows.Forms.Label();
            dgvKhoa = new System.Windows.Forms.DataGridView();
            cardChuongTrinh = new UDSV.Controls.CardPanel();
            lblTongTC = new System.Windows.Forms.Label();
            btnXoaMon = new System.Windows.Forms.Button();
            btnThemMon = new System.Windows.Forms.Button();
            cboMonHoc = new System.Windows.Forms.ComboBox();
            dgvChuongTrinh = new System.Windows.Forms.DataGridView();
            cboHocKy = new System.Windows.Forms.ComboBox();
            labelHocKy = new System.Windows.Forms.Label();
            lblTrangThai = new System.Windows.Forms.Label();
            lblCtTitle = new System.Windows.Forms.Label();
            pnlHeader.SuspendLayout();
            cardKhoa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTCTuChon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTCBatBuoc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNamKetThuc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNamBatDau).BeginInit();
            cardDsKhoa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).BeginInit();
            cardChuongTrinh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChuongTrinh).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Angle = 135F;
            pnlHeader.ColorEnd = System.Drawing.Color.FromArgb(147, 51, 234);
            pnlHeader.ColorStart = System.Drawing.Color.FromArgb(124, 58, 237);
            pnlHeader.Controls.Add(lblSub);
            pnlHeader.Controls.Add(label1);
            pnlHeader.Controls.Add(lblIcon);
            pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            pnlHeader.Location = new System.Drawing.Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new System.Drawing.Size(1320, 75);
            pnlHeader.TabIndex = 0;
            // 
            // lblSub
            // 
            lblSub.AutoSize = true;
            lblSub.BackColor = System.Drawing.Color.Transparent;
            lblSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblSub.ForeColor = System.Drawing.Color.FromArgb(237, 233, 254);
            lblSub.Location = new System.Drawing.Point(75, 43);
            lblSub.Name = "lblSub";
            lblSub.Size = new System.Drawing.Size(434, 20);
            lblSub.TabIndex = 2;
            lblSub.Text = "Quản trị danh mục khóa đào tạo, khung chương trình và học kỳ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(73, 11);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(414, 35);
            label1.TabIndex = 1;
            label1.Text = "QUẢN LÝ CHƯƠNG TRÌNH KHÓA";
            // 
            // lblIcon
            // 
            lblIcon.AutoSize = true;
            lblIcon.BackColor = System.Drawing.Color.Transparent;
            lblIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 24F);
            lblIcon.ForeColor = System.Drawing.Color.White;
            lblIcon.Location = new System.Drawing.Point(18, 10);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new System.Drawing.Size(61, 54);
            lblIcon.TabIndex = 0;
            lblIcon.Text = "🏛️";
            // 
            // cardKhoa
            // 
            cardKhoa.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cardKhoa.BackColor = System.Drawing.Color.White;
            cardKhoa.Controls.Add(btnLamMoi);
            cardKhoa.Controls.Add(btnXoa);
            cardKhoa.Controls.Add(btnSua);
            cardKhoa.Controls.Add(btnThem);
            cardKhoa.Controls.Add(numTCTuChon);
            cardKhoa.Controls.Add(label7);
            cardKhoa.Controls.Add(numTCBatBuoc);
            cardKhoa.Controls.Add(label6);
            cardKhoa.Controls.Add(numNamKetThuc);
            cardKhoa.Controls.Add(label5);
            cardKhoa.Controls.Add(numNamBatDau);
            cardKhoa.Controls.Add(label4);
            cardKhoa.Controls.Add(txtTenKhoa);
            cardKhoa.Controls.Add(label3);
            cardKhoa.Controls.Add(txtMaKhoa);
            cardKhoa.Controls.Add(label2);
            cardKhoa.Controls.Add(lblTitleKhoa);
            cardKhoa.Location = new System.Drawing.Point(20, 85);
            cardKhoa.Name = "cardKhoa";
            cardKhoa.Radius = 14;
            cardKhoa.Size = new System.Drawing.Size(1280, 152);
            cardKhoa.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            btnLamMoi.Location = new System.Drawing.Point(461, 98);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new System.Drawing.Size(125, 38);
            btnLamMoi.TabIndex = 16;
            btnLamMoi.Text = "🔄 Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
            btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnXoa.ForeColor = System.Drawing.Color.White;
            btnXoa.Location = new System.Drawing.Point(323, 98);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new System.Drawing.Size(125, 38);
            btnXoa.TabIndex = 15;
            btnXoa.Text = "🗑️ Xóa khóa";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSua.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnSua.ForeColor = System.Drawing.Color.White;
            btnSua.Location = new System.Drawing.Point(175, 98);
            btnSua.Name = "btnSua";
            btnSua.Size = new System.Drawing.Size(135, 38);
            btnSua.TabIndex = 14;
            btnSua.Text = "💾 Lưu thay đổi";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnThem
            // 
            btnThem.BackColor = System.Drawing.Color.Transparent;
            btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            btnThem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnThem.Font = new System.Drawing.Font("Segoe UI", 9.8F, System.Drawing.FontStyle.Bold);
            btnThem.ForeColor = System.Drawing.Color.White;
            btnThem.GradientEnd = System.Drawing.Color.FromArgb(16, 185, 129);
            btnThem.GradientStart = System.Drawing.Color.FromArgb(5, 150, 105);
            btnThem.Location = new System.Drawing.Point(20, 98);
            btnThem.Name = "btnThem";
            btnThem.Radius = 12;
            btnThem.Size = new System.Drawing.Size(142, 38);
            btnThem.TabIndex = 13;
            btnThem.Text = "＋ Thêm khóa";
            btnThem.UseVisualStyleBackColor = false;
            // 
            // numTCTuChon
            // 
            numTCTuChon.Font = new System.Drawing.Font("Segoe UI", 10F);
            numTCTuChon.Location = new System.Drawing.Point(1015, 54);
            numTCTuChon.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            numTCTuChon.Name = "numTCTuChon";
            numTCTuChon.Size = new System.Drawing.Size(90, 30);
            numTCTuChon.TabIndex = 12;
            numTCTuChon.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI", 8.8F, System.Drawing.FontStyle.Bold);
            label7.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            label7.Location = new System.Drawing.Point(1015, 31);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(85, 20);
            label7.TabIndex = 11;
            label7.Text = "TC tự chọn";
            // 
            // numTCBatBuoc
            // 
            numTCBatBuoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            numTCBatBuoc.Location = new System.Drawing.Point(905, 54);
            numTCBatBuoc.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            numTCBatBuoc.Name = "numTCBatBuoc";
            numTCBatBuoc.Size = new System.Drawing.Size(90, 30);
            numTCBatBuoc.TabIndex = 10;
            numTCBatBuoc.Value = new decimal(new int[] { 27, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI", 8.8F, System.Drawing.FontStyle.Bold);
            label6.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            label6.Location = new System.Drawing.Point(905, 31);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(94, 20);
            label6.TabIndex = 9;
            label6.Text = "TC bắt buộc";
            // 
            // numNamKetThuc
            // 
            numNamKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            numNamKetThuc.Location = new System.Drawing.Point(800, 54);
            numNamKetThuc.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            numNamKetThuc.Minimum = new decimal(new int[] { 2001, 0, 0, 0 });
            numNamKetThuc.Name = "numNamKetThuc";
            numNamKetThuc.Size = new System.Drawing.Size(85, 30);
            numNamKetThuc.TabIndex = 8;
            numNamKetThuc.Value = new decimal(new int[] { 2028, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 8.8F, System.Drawing.FontStyle.Bold);
            label5.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            label5.Location = new System.Drawing.Point(800, 31);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(71, 20);
            label5.TabIndex = 7;
            label5.Text = "Đến năm";
            // 
            // numNamBatDau
            // 
            numNamBatDau.Font = new System.Drawing.Font("Segoe UI", 10F);
            numNamBatDau.Location = new System.Drawing.Point(695, 54);
            numNamBatDau.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            numNamBatDau.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            numNamBatDau.Name = "numNamBatDau";
            numNamBatDau.Size = new System.Drawing.Size(85, 30);
            numNamBatDau.TabIndex = 6;
            numNamBatDau.Value = new decimal(new int[] { 2024, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 8.8F, System.Drawing.FontStyle.Bold);
            label4.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            label4.Location = new System.Drawing.Point(695, 31);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(61, 20);
            label4.TabIndex = 5;
            label4.Text = "Từ năm";
            // 
            // txtTenKhoa
            // 
            txtTenKhoa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtTenKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtTenKhoa.Location = new System.Drawing.Point(165, 54);
            txtTenKhoa.Name = "txtTenKhoa";
            txtTenKhoa.PlaceholderText = "Ví dụ: Khóa 2024 - 2028";
            txtTenKhoa.Size = new System.Drawing.Size(510, 30);
            txtTenKhoa.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 8.8F, System.Drawing.FontStyle.Bold);
            label3.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            label3.Location = new System.Drawing.Point(165, 31);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(82, 20);
            label3.TabIndex = 3;
            label3.Text = "Tên khóa *";
            // 
            // txtMaKhoa
            // 
            txtMaKhoa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtMaKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtMaKhoa.Location = new System.Drawing.Point(20, 54);
            txtMaKhoa.Name = "txtMaKhoa";
            txtMaKhoa.Size = new System.Drawing.Size(125, 30);
            txtMaKhoa.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 8.8F, System.Drawing.FontStyle.Bold);
            label2.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            label2.Location = new System.Drawing.Point(20, 31);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 20);
            label2.TabIndex = 1;
            label2.Text = "Mã khóa";
            // 
            // lblTitleKhoa
            // 
            lblTitleKhoa.AutoSize = true;
            lblTitleKhoa.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblTitleKhoa.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblTitleKhoa.Location = new System.Drawing.Point(18, 8);
            lblTitleKhoa.Name = "lblTitleKhoa";
            lblTitleKhoa.Size = new System.Drawing.Size(182, 25);
            lblTitleKhoa.TabIndex = 0;
            lblTitleKhoa.Text = "THÔNG TIN KHÓA";
            // 
            // cardDsKhoa
            // 
            cardDsKhoa.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            cardDsKhoa.BackColor = System.Drawing.Color.White;
            cardDsKhoa.Controls.Add(lblDsKhoaSub);
            cardDsKhoa.Controls.Add(lblDsKhoaTitle);
            cardDsKhoa.Controls.Add(dgvKhoa);
            cardDsKhoa.Location = new System.Drawing.Point(20, 250);
            cardDsKhoa.Name = "cardDsKhoa";
            cardDsKhoa.Radius = 14;
            cardDsKhoa.Size = new System.Drawing.Size(485, 480);
            cardDsKhoa.TabIndex = 2;
            // 
            // lblDsKhoaSub
            // 
            lblDsKhoaSub.AutoSize = true;
            lblDsKhoaSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblDsKhoaSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            lblDsKhoaSub.Location = new System.Drawing.Point(16, 36);
            lblDsKhoaSub.Name = "lblDsKhoaSub";
            lblDsKhoaSub.Size = new System.Drawing.Size(306, 19);
            lblDsKhoaSub.TabIndex = 2;
            lblDsKhoaSub.Text = "Nhấn chọn khóa để xem và sửa chương trình học";
            // 
            // lblDsKhoaTitle
            // 
            lblDsKhoaTitle.AutoSize = true;
            lblDsKhoaTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblDsKhoaTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblDsKhoaTitle.Location = new System.Drawing.Point(16, 12);
            lblDsKhoaTitle.Name = "lblDsKhoaTitle";
            lblDsKhoaTitle.Size = new System.Drawing.Size(232, 25);
            lblDsKhoaTitle.TabIndex = 1;
            lblDsKhoaTitle.Text = "DANH SÁCH KHÓA HỌC";
            // 
            // dgvKhoa
            // 
            dgvKhoa.AllowUserToAddRows = false;
            dgvKhoa.AllowUserToDeleteRows = false;
            dgvKhoa.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(250, 245, 255);
            dgvKhoa.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvKhoa.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvKhoa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhoa.BackgroundColor = System.Drawing.Color.White;
            dgvKhoa.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvKhoa.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvKhoa.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(124, 58, 237);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(124, 58, 237);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvKhoa.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvKhoa.ColumnHeadersHeight = 38;
            dgvKhoa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(45, 52, 54);
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(76, 29, 149);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvKhoa.DefaultCellStyle = dataGridViewCellStyle3;
            dgvKhoa.EnableHeadersVisualStyles = false;
            dgvKhoa.GridColor = System.Drawing.Color.FromArgb(238, 242, 246);
            dgvKhoa.Location = new System.Drawing.Point(16, 62);
            dgvKhoa.MultiSelect = false;
            dgvKhoa.Name = "dgvKhoa";
            dgvKhoa.ReadOnly = true;
            dgvKhoa.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dgvKhoa.RowHeadersVisible = false;
            dgvKhoa.RowHeadersWidth = 51;
            dgvKhoa.RowTemplate.Height = 34;
            dgvKhoa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvKhoa.Size = new System.Drawing.Size(453, 402);
            dgvKhoa.TabIndex = 0;
            // 
            // cardChuongTrinh
            // 
            cardChuongTrinh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cardChuongTrinh.BackColor = System.Drawing.Color.White;
            cardChuongTrinh.Controls.Add(lblTongTC);
            cardChuongTrinh.Controls.Add(btnXoaMon);
            cardChuongTrinh.Controls.Add(btnThemMon);
            cardChuongTrinh.Controls.Add(cboMonHoc);
            cardChuongTrinh.Controls.Add(dgvChuongTrinh);
            cardChuongTrinh.Controls.Add(cboHocKy);
            cardChuongTrinh.Controls.Add(labelHocKy);
            cardChuongTrinh.Controls.Add(lblTrangThai);
            cardChuongTrinh.Controls.Add(lblCtTitle);
            cardChuongTrinh.Location = new System.Drawing.Point(520, 250);
            cardChuongTrinh.Name = "cardChuongTrinh";
            cardChuongTrinh.Radius = 14;
            cardChuongTrinh.Size = new System.Drawing.Size(780, 480);
            cardChuongTrinh.TabIndex = 3;
            // 
            // lblTongTC
            // 
            lblTongTC.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblTongTC.AutoSize = true;
            lblTongTC.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblTongTC.ForeColor = System.Drawing.Color.FromArgb(124, 58, 237);
            lblTongTC.Location = new System.Drawing.Point(16, 451);
            lblTongTC.Name = "lblTongTC";
            lblTongTC.Size = new System.Drawing.Size(99, 21);
            lblTongTC.TabIndex = 8;
            lblTongTC.Text = "Tổng TC: —";
            // 
            // btnXoaMon
            // 
            btnXoaMon.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnXoaMon.BackColor = System.Drawing.Color.FromArgb(254, 226, 226);
            btnXoaMon.Cursor = System.Windows.Forms.Cursors.Hand;
            btnXoaMon.FlatAppearance.BorderSize = 0;
            btnXoaMon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnXoaMon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnXoaMon.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            btnXoaMon.Location = new System.Drawing.Point(655, 410);
            btnXoaMon.Name = "btnXoaMon";
            btnXoaMon.Size = new System.Drawing.Size(105, 34);
            btnXoaMon.TabIndex = 7;
            btnXoaMon.Text = "🗑️ Xoá môn";
            btnXoaMon.UseVisualStyleBackColor = false;
            // 
            // btnThemMon
            // 
            btnThemMon.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnThemMon.BackColor = System.Drawing.Color.FromArgb(220, 252, 231);
            btnThemMon.Cursor = System.Windows.Forms.Cursors.Hand;
            btnThemMon.FlatAppearance.BorderSize = 0;
            btnThemMon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnThemMon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnThemMon.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
            btnThemMon.Location = new System.Drawing.Point(520, 410);
            btnThemMon.Name = "btnThemMon";
            btnThemMon.Size = new System.Drawing.Size(125, 34);
            btnThemMon.TabIndex = 6;
            btnThemMon.Text = "＋ Thêm môn";
            btnThemMon.UseVisualStyleBackColor = false;
            // 
            // cboMonHoc
            // 
            cboMonHoc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            cboMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboMonHoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            cboMonHoc.FormattingEnabled = true;
            cboMonHoc.Location = new System.Drawing.Point(16, 412);
            cboMonHoc.Name = "cboMonHoc";
            cboMonHoc.Size = new System.Drawing.Size(490, 31);
            cboMonHoc.TabIndex = 5;
            // 
            // dgvChuongTrinh
            // 
            dgvChuongTrinh.AllowUserToAddRows = false;
            dgvChuongTrinh.AllowUserToDeleteRows = false;
            dgvChuongTrinh.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(250, 245, 255);
            dgvChuongTrinh.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvChuongTrinh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dgvChuongTrinh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvChuongTrinh.BackgroundColor = System.Drawing.Color.White;
            dgvChuongTrinh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvChuongTrinh.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dgvChuongTrinh.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(124, 58, 237);
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(124, 58, 237);
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvChuongTrinh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvChuongTrinh.ColumnHeadersHeight = 38;
            dgvChuongTrinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(45, 52, 54);
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(237, 233, 254);
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(76, 29, 149);
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvChuongTrinh.DefaultCellStyle = dataGridViewCellStyle6;
            dgvChuongTrinh.EnableHeadersVisualStyles = false;
            dgvChuongTrinh.GridColor = System.Drawing.Color.FromArgb(238, 242, 246);
            dgvChuongTrinh.Location = new System.Drawing.Point(16, 70);
            dgvChuongTrinh.MultiSelect = false;
            dgvChuongTrinh.Name = "dgvChuongTrinh";
            dgvChuongTrinh.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dgvChuongTrinh.RowHeadersVisible = false;
            dgvChuongTrinh.RowHeadersWidth = 51;
            dgvChuongTrinh.RowTemplate.Height = 34;
            dgvChuongTrinh.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvChuongTrinh.Size = new System.Drawing.Size(748, 332);
            dgvChuongTrinh.TabIndex = 4;
            // 
            // cboHocKy
            // 
            cboHocKy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            cboHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboHocKy.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            cboHocKy.FormattingEnabled = true;
            cboHocKy.Location = new System.Drawing.Point(660, 10);
            cboHocKy.Name = "cboHocKy";
            cboHocKy.Size = new System.Drawing.Size(105, 29);
            cboHocKy.TabIndex = 3;
            // 
            // labelHocKy
            // 
            labelHocKy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            labelHocKy.AutoSize = true;
            labelHocKy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            labelHocKy.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            labelHocKy.Location = new System.Drawing.Point(595, 14);
            labelHocKy.Name = "labelHocKy";
            labelHocKy.Size = new System.Drawing.Size(60, 20);
            labelHocKy.TabIndex = 2;
            labelHocKy.Text = "Học kỳ:";
            // 
            // lblTrangThai
            // 
            lblTrangThai.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblTrangThai.AutoEllipsis = true;
            lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129);
            lblTrangThai.Location = new System.Drawing.Point(16, 42);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new System.Drawing.Size(748, 22);
            lblTrangThai.TabIndex = 1;
            lblTrangThai.Text = "—";
            // 
            // lblCtTitle
            // 
            lblCtTitle.AutoSize = true;
            lblCtTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            lblCtTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            lblCtTitle.Location = new System.Drawing.Point(16, 13);
            lblCtTitle.Name = "lblCtTitle";
            lblCtTitle.Size = new System.Drawing.Size(193, 25);
            lblCtTitle.TabIndex = 0;
            lblCtTitle.Text = "CHƯƠNG TRÌNH HỌC";
            // 
            // FrmKhoa
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            ClientSize = new System.Drawing.Size(1320, 750);
            Controls.Add(cardChuongTrinh);
            Controls.Add(cardDsKhoa);
            Controls.Add(cardKhoa);
            Controls.Add(pnlHeader);
            MinimumSize = new System.Drawing.Size(1180, 680);
            Name = "FrmKhoa";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Quản lý chương trình khóa";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            cardKhoa.ResumeLayout(false);
            cardKhoa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTCTuChon).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTCBatBuoc).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNamKetThuc).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNamBatDau).EndInit();
            cardDsKhoa.ResumeLayout(false);
            cardDsKhoa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).EndInit();
            cardChuongTrinh.ResumeLayout(false);
            cardChuongTrinh.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChuongTrinh).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Controls.GradientPanel pnlHeader;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSub;
        private Controls.CardPanel cardKhoa;
        private System.Windows.Forms.Label lblTitleKhoa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaKhoa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenKhoa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numNamBatDau;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numNamKetThuc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numTCBatBuoc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numTCTuChon;
        private Controls.GradientButton btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private Controls.CardPanel cardDsKhoa;
        private System.Windows.Forms.Label lblDsKhoaTitle;
        private System.Windows.Forms.Label lblDsKhoaSub;
        private System.Windows.Forms.DataGridView dgvKhoa;
        private Controls.CardPanel cardChuongTrinh;
        private System.Windows.Forms.Label lblCtTitle;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label labelHocKy;
        private System.Windows.Forms.ComboBox cboHocKy;
        private System.Windows.Forms.DataGridView dgvChuongTrinh;
        private System.Windows.Forms.ComboBox cboMonHoc;
        private System.Windows.Forms.Button btnThemMon;
        private System.Windows.Forms.Button btnXoaMon;
        private System.Windows.Forms.Label lblTongTC;
    }
}