namespace UDSV
{
    partial class FrmKetQua
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
            cboSinhVien = new ComboBox();
            label3 = new Label();
            cboMonHoc = new ComboBox();
            label4 = new Label();
            numDiem = new NumericUpDown();
            chkChuaCoDiem = new CheckBox();
            btnLuu = new Button();
            btnXoa = new Button();
            lblTongTC = new Label();
            dgvKetQua = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)numDiem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(310, 9);
            label1.Name = "label1";
            label1.Size = new Size(380, 41);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ KẾT QUẢ HỌC TẬP";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cboSinhVien);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(cboMonHoc);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(numDiem);
            groupBox1.Controls.Add(chkChuaCoDiem);
            groupBox1.Controls.Add(btnLuu);
            groupBox1.Controls.Add(btnXoa);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.ForeColor = SystemColors.Highlight;
            groupBox1.Location = new Point(30, 60);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(940, 105);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Nhập / sửa điểm";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 45);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.Text = "Sinh viên";
            // 
            // cboSinhVien
            // 
            cboSinhVien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSinhVien.Location = new Point(95, 41);
            cboSinhVien.Name = "cboSinhVien";
            cboSinhVien.Size = new Size(260, 28);
            cboSinhVien.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(375, 45);
            label3.Name = "label3";
            label3.Size = new Size(70, 20);
            label3.Text = "Môn học";
            // 
            // cboMonHoc
            // 
            cboMonHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMonHoc.Location = new Point(450, 41);
            cboMonHoc.Name = "cboMonHoc";
            cboMonHoc.Size = new Size(250, 28);
            cboMonHoc.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(715, 45);
            label4.Name = "label4";
            label4.Size = new Size(45, 20);
            label4.Text = "Điểm";
            // 
            // numDiem
            // 
            numDiem.DecimalPlaces = 2;
            numDiem.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numDiem.Location = new Point(765, 41);
            numDiem.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            numDiem.Name = "numDiem";
            numDiem.Size = new Size(70, 27);
            numDiem.TabIndex = 3;
            numDiem.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // chkChuaCoDiem
            // 
            chkChuaCoDiem.AutoSize = true;
            chkChuaCoDiem.Location = new Point(15, 75);
            chkChuaCoDiem.Name = "chkChuaCoDiem";
            chkChuaCoDiem.Size = new Size(240, 24);
            chkChuaCoDiem.TabIndex = 4;
            chkChuaCoDiem.Text = "Chưa có điểm (đang học — để trống)";
            chkChuaCoDiem.UseVisualStyleBackColor = true;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.Lime;
            btnLuu.ForeColor = Color.Black;
            btnLuu.Location = new Point(600, 68);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(120, 30);
            btnLuu.TabIndex = 5;
            btnLuu.Text = "Lưu điểm";
            btnLuu.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.Red;
            btnXoa.Location = new Point(740, 68);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(120, 30);
            btnXoa.TabIndex = 6;
            btnXoa.Text = "Xoá điểm";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // lblTongTC
            // 
            lblTongTC.AutoSize = true;
            lblTongTC.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTongTC.ForeColor = Color.DarkGreen;
            lblTongTC.Location = new Point(30, 180);
            lblTongTC.Name = "lblTongTC";
            lblTongTC.Size = new Size(400, 25);
            lblTongTC.TabIndex = 2;
            lblTongTC.Text = "Tín chỉ tích luỹ: —";
            // 
            // dgvKetQua
            // 
            dgvKetQua.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKetQua.Location = new Point(30, 215);
            dgvKetQua.Name = "dgvKetQua";
            dgvKetQua.RowHeadersWidth = 51;
            dgvKetQua.Size = new Size(940, 360);
            dgvKetQua.TabIndex = 3;
            // 
            // FrmKetQua
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(dgvKetQua);
            Controls.Add(lblTongTC);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Name = "FrmKetQua";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý kết quả học tập";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDiem).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvKetQua).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Label label2, label3, label4;
        private ComboBox cboSinhVien, cboMonHoc;
        private NumericUpDown numDiem;
        private CheckBox chkChuaCoDiem;
        private Button btnLuu, btnXoa;
        private Label lblTongTC;
        private DataGridView dgvKetQua;
    }
}
