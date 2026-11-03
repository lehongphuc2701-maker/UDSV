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
            labelKhoa = new Label();
            cboKhoa = new ComboBox();
            labelKy = new Label();
            cboKy = new ComboBox();
            labelMon = new Label();
            cboMonHoc = new ComboBox();
            btnLuu = new Button();
            lblThongTin = new Label();
            dgvBangDiem = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBangDiem).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(300, 9);
            label1.Name = "label1";
            label1.Size = new Size(380, 41);
            label1.TabIndex = 0;
            label1.Text = "QUẢN LÝ KẾT QUẢ HỌC TẬP";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(labelKhoa);
            groupBox1.Controls.Add(cboKhoa);
            groupBox1.Controls.Add(labelKy);
            groupBox1.Controls.Add(cboKy);
            groupBox1.Controls.Add(labelMon);
            groupBox1.Controls.Add(cboMonHoc);
            groupBox1.Controls.Add(btnLuu);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.ForeColor = SystemColors.Highlight;
            groupBox1.Location = new Point(30, 60);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(940, 105);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Chọn lớp để nhập điểm";
            // 
            // labelKhoa
            // 
            labelKhoa.AutoSize = true;
            labelKhoa.Location = new Point(15, 45);
            labelKhoa.Name = "labelKhoa";
            labelKhoa.Size = new Size(44, 20);
            labelKhoa.Text = "Khóa";
            // 
            // cboKhoa
            // 
            cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhoa.Location = new Point(70, 41);
            cboKhoa.Name = "cboKhoa";
            cboKhoa.Size = new Size(160, 28);
            cboKhoa.TabIndex = 1;
            // 
            // labelKy
            // 
            labelKy.AutoSize = true;
            labelKy.Location = new Point(250, 45);
            labelKy.Name = "labelKy";
            labelKy.Size = new Size(28, 20);
            labelKy.Text = "Kỳ";
            // 
            // cboKy
            // 
            cboKy.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKy.Location = new Point(290, 41);
            cboKy.Name = "cboKy";
            cboKy.Size = new Size(110, 28);
            cboKy.TabIndex = 2;
            // 
            // labelMon
            // 
            labelMon.AutoSize = true;
            labelMon.Location = new Point(420, 45);
            labelMon.Name = "labelMon";
            labelMon.Size = new Size(70, 20);
            labelMon.Text = "Môn học";
            // 
            // cboMonHoc
            // 
            cboMonHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMonHoc.Location = new Point(500, 41);
            cboMonHoc.Name = "cboMonHoc";
            cboMonHoc.Size = new Size(300, 28);
            cboMonHoc.TabIndex = 3;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.Lime;
            btnLuu.ForeColor = Color.Black;
            btnLuu.Location = new Point(815, 68);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(110, 30);
            btnLuu.TabIndex = 4;
            btnLuu.Text = "Lưu điểm";
            btnLuu.UseVisualStyleBackColor = false;
            // 
            // lblThongTin
            // 
            lblThongTin.AutoSize = true;
            lblThongTin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblThongTin.ForeColor = Color.DarkGreen;
            lblThongTin.Location = new Point(30, 180);
            lblThongTin.Name = "lblThongTin";
            lblThongTin.Size = new Size(400, 25);
            lblThongTin.TabIndex = 2;
            lblThongTin.Text = "Chọn khóa → kỳ → môn học để nhập điểm";
            // 
            // dgvBangDiem
            // 
            dgvBangDiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBangDiem.Location = new Point(30, 215);
            dgvBangDiem.Name = "dgvBangDiem";
            dgvBangDiem.RowHeadersWidth = 51;
            dgvBangDiem.Size = new Size(940, 360);
            dgvBangDiem.TabIndex = 3;
            // 
            // FrmKetQua
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(dgvBangDiem);
            Controls.Add(lblThongTin);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Name = "FrmKetQua";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý kết quả học tập";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBangDiem).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Label labelKhoa, labelKy, labelMon;
        private ComboBox cboKhoa, cboKy, cboMonHoc;
        private Button btnLuu;
        private Label lblThongTin;
        private DataGridView dgvBangDiem;
    }
}
