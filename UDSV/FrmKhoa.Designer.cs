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
            label1 = new Label();
            groupBox1 = new GroupBox();
            label2 = new Label();
            txtMaKhoa = new TextBox();
            label3 = new Label();
            txtTenKhoa = new TextBox();
            label4 = new Label();
            numNamBatDau = new NumericUpDown();
            label5 = new Label();
            numNamKetThuc = new NumericUpDown();
            label6 = new Label();
            numTCBatBuoc = new NumericUpDown();
            label7 = new Label();
            numTCTuChon = new NumericUpDown();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            dgvKhoa = new DataGridView();
            groupBox2 = new GroupBox();
            lblTrangThai = new Label();
            labelHocKy = new Label();
            cboHocKy = new ComboBox();
            dgvChuongTrinh = new DataGridView();
            cboMonHoc = new ComboBox();
            btnThemMon = new Button();
            btnXoaMon = new Button();
            lblTongTC = new Label();
            ((System.ComponentModel.ISupportInitialize)numNamBatDau).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numNamKetThuc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTCBatBuoc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTCTuChon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvChuongTrinh).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(390, 9);
            label1.Name = "label1";
            label1.Size = new Size(380, 41);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ CHƯƠNG TRÌNH KHÓA";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtMaKhoa);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtTenKhoa);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(numNamBatDau);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(numNamKetThuc);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(numTCBatBuoc);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(numTCTuChon);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.ForeColor = SystemColors.Highlight;
            groupBox1.Location = new Point(30, 60);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1060, 100);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin khóa";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 40);
            label2.Name = "label2";
            label2.Size = new Size(66, 20);
            label2.Text = "Mã khóa";
            // 
            // txtMaKhoa
            // 
            txtMaKhoa.Location = new Point(100, 35);
            txtMaKhoa.Name = "txtMaKhoa";
            txtMaKhoa.Size = new Size(100, 27);
            txtMaKhoa.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(220, 40);
            label3.Name = "label3";
            label3.Size = new Size(45, 20);
            label3.Text = "Tên khóa";
            // 
            // txtTenKhoa
            // 
            txtTenKhoa.Location = new Point(300, 35);
            txtTenKhoa.Name = "txtTenKhoa";
            txtTenKhoa.Size = new Size(240, 27);
            txtTenKhoa.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(560, 40);
            label4.Name = "label4";
            label4.Size = new Size(40, 20);
            label4.Text = "Từ năm";
            // 
            // numNamBatDau
            // 
            numNamBatDau.Location = new Point(610, 35);
            numNamBatDau.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            numNamBatDau.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            numNamBatDau.Name = "numNamBatDau";
            numNamBatDau.Size = new Size(70, 27);
            numNamBatDau.TabIndex = 3;
            numNamBatDau.Value = new decimal(new int[] { 2024, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(695, 40);
            label5.Name = "label5";
            label5.Size = new Size(52, 20);
            label5.Text = "Đến năm";
            // 
            // numNamKetThuc
            // 
            numNamKetThuc.Location = new Point(760, 35);
            numNamKetThuc.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            numNamKetThuc.Minimum = new decimal(new int[] { 2001, 0, 0, 0 });
            numNamKetThuc.Name = "numNamKetThuc";
            numNamKetThuc.Size = new Size(70, 27);
            numNamKetThuc.TabIndex = 4;
            numNamKetThuc.Value = new decimal(new int[] { 2028, 0, 0, 0 });
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(845, 18);
            label6.Name = "label6";
            label6.Size = new Size(52, 20);
            label6.Text = "TC bắt buộc";
            // 
            // numTCBatBuoc
            // 
            numTCBatBuoc.Location = new Point(845, 42);
            numTCBatBuoc.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            numTCBatBuoc.Name = "numTCBatBuoc";
            numTCBatBuoc.Size = new Size(70, 27);
            numTCBatBuoc.TabIndex = 5;
            numTCBatBuoc.Value = new decimal(new int[] { 27, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(930, 18);
            label7.Name = "label7";
            label7.Size = new Size(52, 20);
            label7.Text = "TC tự chọn";
            // 
            // numTCTuChon
            // 
            numTCTuChon.Location = new Point(930, 42);
            numTCTuChon.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            numTCTuChon.Name = "numTCTuChon";
            numTCTuChon.Size = new Size(70, 27);
            numTCTuChon.TabIndex = 6;
            numTCTuChon.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.Lime;
            btnThem.ForeColor = Color.Black;
            btnThem.Location = new Point(30, 596);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(100, 30);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm khóa";
            btnThem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.Orange;
            btnSua.Location = new Point(145, 596);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(100, 30);
            btnSua.TabIndex = 3;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.Red;
            btnXoa.Location = new Point(260, 596);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 30);
            btnXoa.TabIndex = 4;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.PaleTurquoise;
            btnLamMoi.Location = new Point(375, 596);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(100, 30);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // dgvKhoa
            // 
            dgvKhoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhoa.Location = new Point(30, 230);
            dgvKhoa.Name = "dgvKhoa";
            dgvKhoa.RowHeadersWidth = 51;
            dgvKhoa.Size = new Size(470, 330);
            dgvKhoa.TabIndex = 6;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblTrangThai);
            groupBox2.Controls.Add(labelHocKy);
            groupBox2.Controls.Add(cboHocKy);
            groupBox2.Controls.Add(dgvChuongTrinh);
            groupBox2.Controls.Add(cboMonHoc);
            groupBox2.Controls.Add(btnThemMon);
            groupBox2.Controls.Add(btnXoaMon);
            groupBox2.Controls.Add(lblTongTC);
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox2.ForeColor = SystemColors.Highlight;
            groupBox2.Location = new Point(520, 230);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(570, 330);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Chương trình của khóa đang chọn";
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTrangThai.Location = new Point(15, 32);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(120, 23);
            lblTrangThai.Text = "—";
            // 
            // dgvChuongTrinh
            // 
            dgvChuongTrinh.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChuongTrinh.Location = new Point(15, 65);
            dgvChuongTrinh.Name = "dgvChuongTrinh";
            dgvChuongTrinh.RowHeadersWidth = 51;
            dgvChuongTrinh.Size = new Size(540, 180);
            dgvChuongTrinh.TabIndex = 8;
            // 
            // cboMonHoc
            // 
            cboMonHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMonHoc.Location = new Point(15, 258);
            cboMonHoc.Name = "cboMonHoc";
            cboMonHoc.Size = new Size(300, 28);
            cboMonHoc.TabIndex = 9;
            // 
            // btnThemMon
            // 
            btnThemMon.BackColor = Color.LightGreen;
            btnThemMon.Location = new Point(325, 256);
            btnThemMon.Name = "btnThemMon";
            btnThemMon.Size = new Size(110, 30);
            btnThemMon.TabIndex = 10;
            btnThemMon.Text = "Thêm môn";
            btnThemMon.UseVisualStyleBackColor = false;
            // 
            // btnXoaMon
            // 
            btnXoaMon.BackColor = Color.MistyRose;
            btnXoaMon.Location = new Point(445, 256);
            btnXoaMon.Name = "btnXoaMon";
            btnXoaMon.Size = new Size(110, 30);
            btnXoaMon.TabIndex = 11;
            btnXoaMon.Text = "Xoá môn";
            btnXoaMon.UseVisualStyleBackColor = false;
            // 
            // labelHocKy
            // 
            labelHocKy.AutoSize = true;
            labelHocKy.Location = new Point(330, 32);
            labelHocKy.Name = "labelHocKy";
            labelHocKy.Size = new Size(60, 20);
            labelHocKy.Text = "Học kỳ";
            // 
            // cboHocKy
            // 
            cboHocKy.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHocKy.Location = new Point(400, 28);
            cboHocKy.Name = "cboHocKy";
            cboHocKy.Size = new Size(110, 28);
            cboHocKy.TabIndex = 12;
            // 
            // lblTongTC
            // 
            lblTongTC.AutoSize = true;
            lblTongTC.Location = new Point(15, 295);
            lblTongTC.Name = "lblTongTC";
            lblTongTC.Size = new Size(200, 20);
            lblTongTC.Text = "Tổng TC: —";
            // 
            // FrmKhoa
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1131, 650);
            Controls.Add(groupBox2);
            Controls.Add(dgvKhoa);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Name = "FrmKhoa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý chương trình khóa";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numNamBatDau).EndInit();
            ((System.ComponentModel.ISupportInitialize)numNamKetThuc).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTCBatBuoc).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTCTuChon).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvChuongTrinh).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Label label2, label3, label4, label5, label6, label7;
        private TextBox txtMaKhoa, txtTenKhoa;
        private NumericUpDown numNamBatDau, numNamKetThuc, numTCBatBuoc, numTCTuChon;
        private Button btnThem, btnSua, btnXoa, btnLamMoi;
        private DataGridView dgvKhoa;
        private GroupBox groupBox2;
        private Label lblTrangThai, lblTongTC, labelHocKy;
        private DataGridView dgvChuongTrinh;
        private ComboBox cboMonHoc, cboHocKy;
        private Button btnThemMon, btnXoaMon;
    }
}
