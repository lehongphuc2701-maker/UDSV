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
            label1 = new Label();
            groupBox1 = new GroupBox();
            txtSDT = new TextBox();
            txtLop = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            dtpNgaySinh = new DateTimePicker();
            cboGioiTinh = new ComboBox();
            label4 = new Label();
            txtMaSV = new TextBox();
            label3 = new Label();
            txtHoTen = new TextBox();
            label2 = new Label();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnTimKiem = new Button();
            txtTimKiem = new TextBox();
            dgvSinhVien = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.Location = new Point(373, 9);
            label1.Name = "label1";
            label1.Size = new Size(485, 41);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ THÔNG TIN SINH VIÊN";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtSDT);
            groupBox1.Controls.Add(txtLop);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(dtpNgaySinh);
            groupBox1.Controls.Add(cboGioiTinh);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtMaSV);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtHoTen);
            groupBox1.Controls.Add(label2);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 163);
            groupBox1.ForeColor = SystemColors.HotTrack;
            groupBox1.Location = new Point(136, 73);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1102, 195);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin sinh viên";
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(654, 98);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(125, 27);
            txtSDT.TabIndex = 12;
            // 
            // txtLop
            // 
            txtLop.Location = new Point(342, 101);
            txtLop.Name = "txtLop";
            txtLop.Size = new Size(125, 27);
            txtLop.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(541, 101);
            label7.Name = "label7";
            label7.Size = new Size(37, 20);
            label7.TabIndex = 10;
            label7.Text = "SĐT";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(267, 101);
            label6.Name = "label6";
            label6.Size = new Size(35, 20);
            label6.TabIndex = 9;
            label6.Text = "Lớp";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 101);
            label5.Name = "label5";
            label5.Size = new Size(69, 20);
            label5.TabIndex = 8;
            label5.Text = "Giới tính";
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(654, 29);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(250, 27);
            dtpNgaySinh.TabIndex = 7;
            // 
            // cboGioiTinh
            // 
            cboGioiTinh.FormattingEnabled = true;
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGioiTinh.Location = new Point(84, 93);
            cboGioiTinh.Name = "cboGioiTinh";
            cboGioiTinh.Size = new Size(151, 28);
            cboGioiTinh.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(541, 32);
            label4.Name = "label4";
            label4.Size = new Size(79, 20);
            label4.TabIndex = 4;
            label4.Text = "Ngày sinh";
            // 
            // txtMaSV
            // 
            txtMaSV.Location = new Point(342, 26);
            txtMaSV.Name = "txtMaSV";
            txtMaSV.Size = new Size(125, 27);
            txtMaSV.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(267, 32);
            label3.Name = "label3";
            label3.Size = new Size(53, 20);
            label3.TabIndex = 2;
            label3.Text = "Mã SV";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(84, 29);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(125, 27);
            txtHoTen.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 32);
            label2.Name = "label2";
            label2.Size = new Size(56, 20);
            label2.TabIndex = 0;
            label2.Text = "Họ tên";
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.Lime;
            btnThem.ForeColor = SystemColors.InactiveCaptionText;
            btnThem.Location = new Point(136, 299);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.Gold;
            btnSua.ForeColor = SystemColors.InfoText;
            btnSua.Location = new Point(320, 299);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 3;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.Red;
            btnXoa.ForeColor = SystemColors.InfoText;
            btnXoa.Location = new Point(546, 299);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 4;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.DimGray;
            btnLamMoi.ForeColor = SystemColors.InfoText;
            btnLamMoi.Location = new Point(764, 299);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 29);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // btnTimKiem
            // 
            btnTimKiem.BackColor = SystemColors.ActiveCaption;
            btnTimKiem.ForeColor = SystemColors.InfoText;
            btnTimKiem.Location = new Point(235, 347);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(94, 29);
            btnTimKiem.TabIndex = 6;
            btnTimKiem.Text = "Tìm";
            btnTimKiem.UseVisualStyleBackColor = false;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(355, 349);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(310, 27);
            txtTimKiem.TabIndex = 7;
            // 
            // dgvSinhVien
            // 
            dgvSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSinhVien.Location = new Point(142, 400);
            dgvSinhVien.Name = "dgvSinhVien";
            dgvSinhVien.RowHeadersWidth = 51;
            dgvSinhVien.Size = new Size(1020, 188);
            dgvSinhVien.TabIndex = 8;
            // 
            // FrmSinhVien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1284, 641);
            Controls.Add(dgvSinhVien);
            Controls.Add(txtTimKiem);
            Controls.Add(btnTimKiem);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            ForeColor = SystemColors.MenuHighlight;
            Name = "FrmSinhVien";
            Text = "FrmSinhVien";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Label label4;
        private TextBox txtMaSV;
        private Label label3;
        private TextBox txtHoTen;
        private Label label2;
        private Label label5;
        private DateTimePicker dtpNgaySinh;
        private ComboBox cboGioiTinh;
        private TextBox txtSDT;
        private TextBox txtLop;
        private Label label7;
        private Label label6;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnTimKiem;
        private TextBox txtTimKiem;
        private DataGridView dgvSinhVien;
    }
}