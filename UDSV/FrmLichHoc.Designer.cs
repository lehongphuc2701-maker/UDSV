namespace UDSV
{
    partial class FrmLichHoc
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
            label2 = new Label();
            cboMonHoc = new ComboBox();
            label3 = new Label();
            cboSinhVien = new ComboBox();
            label4 = new Label();
            dtpNgayHoc = new DateTimePicker();
            label5 = new Label();
            cboCaHoc = new ComboBox();
            label6 = new Label();
            txtPhongHoc = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            dgvLichHoc = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLichHoc).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(400, 9);
            label1.Name = "label1";
            label1.Size = new Size(300, 41);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ LỊCH HỌC";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cboMonHoc);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(cboSinhVien);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(dtpNgayHoc);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(cboCaHoc);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtPhongHoc);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 163);
            groupBox1.ForeColor = SystemColors.Highlight;
            groupBox1.Location = new Point(112, 94);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(900, 140);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin lịch học";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 35);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 0;
            label2.Text = "Môn học";
            // 
            // cboMonHoc
            // 
            cboMonHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMonHoc.FormattingEnabled = true;
            cboMonHoc.Location = new Point(95, 30);
            cboMonHoc.Name = "cboMonHoc";
            cboMonHoc.Size = new Size(300, 28);
            cboMonHoc.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(430, 35);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 2;
            label3.Text = "Sinh viên";
            // 
            // cboSinhVien
            // 
            cboSinhVien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSinhVien.FormattingEnabled = true;
            cboSinhVien.Location = new Point(510, 30);
            cboSinhVien.Name = "cboSinhVien";
            cboSinhVien.Size = new Size(370, 28);
            cboSinhVien.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 90);
            label4.Name = "label4";
            label4.Size = new Size(75, 20);
            label4.TabIndex = 4;
            label4.Text = "Ngày học";
            // 
            // dtpNgayHoc
            // 
            dtpNgayHoc.Format = DateTimePickerFormat.Short;
            dtpNgayHoc.Location = new Point(95, 85);
            dtpNgayHoc.Name = "dtpNgayHoc";
            dtpNgayHoc.Size = new Size(300, 27);
            dtpNgayHoc.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(430, 90);
            label5.Name = "label5";
            label5.Size = new Size(58, 20);
            label5.TabIndex = 6;
            label5.Text = "Ca học";
            // 
            // cboCaHoc
            // 
            cboCaHoc.FormattingEnabled = true;
            cboCaHoc.Items.AddRange(new object[] { "Ca 1 (07:00 - 11:00)", "Ca 2 (13:00 - 17:00)", "Ca 3 (17:30 - 21:00)" });
            cboCaHoc.Location = new Point(510, 85);
            cboCaHoc.Name = "cboCaHoc";
            cboCaHoc.Size = new Size(215, 28);
            cboCaHoc.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(745, 90);
            label6.Name = "label6";
            label6.Size = new Size(45, 20);
            label6.TabIndex = 8;
            label6.Text = "Phòng";
            // 
            // txtPhongHoc
            // 
            txtPhongHoc.Location = new Point(795, 85);
            txtPhongHoc.Name = "txtPhongHoc";
            txtPhongHoc.Size = new Size(85, 27);
            txtPhongHoc.TabIndex = 9;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.Lime;
            btnThem.ForeColor = SystemColors.InactiveCaptionText;
            btnThem.Location = new Point(112, 250);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.Orange;
            btnSua.ForeColor = SystemColors.InfoText;
            btnSua.Location = new Point(240, 250);
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
            btnXoa.Location = new Point(368, 250);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 4;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.PaleTurquoise;
            btnLamMoi.ForeColor = SystemColors.InfoText;
            btnLamMoi.Location = new Point(496, 250);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 29);
            btnLamMoi.TabIndex = 5;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // dgvLichHoc
            // 
            dgvLichHoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLichHoc.Location = new Point(99, 300);
            dgvLichHoc.Name = "dgvLichHoc";
            dgvLichHoc.RowHeadersWidth = 51;
            dgvLichHoc.Size = new Size(933, 280);
            dgvLichHoc.TabIndex = 6;
            // 
            // FrmLichHoc
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1131, 619);
            Controls.Add(dgvLichHoc);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Name = "FrmLichHoc";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmLichHoc";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLichHoc).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Label label2;
        private ComboBox cboMonHoc;
        private Label label3;
        private ComboBox cboSinhVien;
        private Label label4;
        private DateTimePicker dtpNgayHoc;
        private Label label5;
        private ComboBox cboCaHoc;
        private Label label6;
        private TextBox txtPhongHoc;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private DataGridView dgvLichHoc;
    }
}
