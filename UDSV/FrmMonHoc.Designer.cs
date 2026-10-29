namespace UDSV
{
    partial class FrmMonHoc
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
            txtGiangVien = new TextBox();
            txtSoTinChi = new TextBox();
            txtTenMH = new TextBox();
            txtMaMH = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            dgvMonHoc = new DataGridView();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMonHoc).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(392, 9);
            label1.Name = "label1";
            label1.Size = new Size(248, 41);
            label1.TabIndex = 1;
            label1.Text = "Quản lí môn học";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtGiangVien);
            groupBox1.Controls.Add(txtSoTinChi);
            groupBox1.Controls.Add(txtTenMH);
            groupBox1.Controls.Add(txtMaMH);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.ForeColor = SystemColors.Highlight;
            groupBox1.Location = new Point(112, 94);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(683, 213);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông tin môn học";
            // 
            // txtGiangVien
            // 
            txtGiangVien.Location = new Point(397, 110);
            txtGiangVien.Name = "txtGiangVien";
            txtGiangVien.Size = new Size(125, 27);
            txtGiangVien.TabIndex = 7;
            // 
            // txtSoTinChi
            // 
            txtSoTinChi.Location = new Point(117, 103);
            txtSoTinChi.Name = "txtSoTinChi";
            txtSoTinChi.Size = new Size(125, 27);
            txtSoTinChi.TabIndex = 6;
            // 
            // txtTenMH
            // 
            txtTenMH.Location = new Point(397, 28);
            txtTenMH.Name = "txtTenMH";
            txtTenMH.Size = new Size(125, 27);
            txtTenMH.TabIndex = 5;
            // 
            // txtMaMH
            // 
            txtMaMH.Location = new Point(117, 28);
            txtMaMH.Name = "txtMaMH";
            txtMaMH.Size = new Size(125, 27);
            txtMaMH.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(297, 110);
            label5.Name = "label5";
            label5.Size = new Size(79, 20);
            label5.TabIndex = 3;
            label5.Text = "Giảng viên";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 110);
            label4.Name = "label4";
            label4.Size = new Size(70, 20);
            label4.TabIndex = 2;
            label4.Text = "Số tín chỉ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(297, 33);
            label3.Name = "label3";
            label3.Size = new Size(94, 20);
            label3.TabIndex = 1;
            label3.Text = "Tên môn học";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 32);
            label2.Name = "label2";
            label2.Size = new Size(92, 20);
            label2.TabIndex = 0;
            label2.Text = "Mã môn học";
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.Lime;
            btnThem.Location = new Point(93, 341);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 3;
            btnThem.Text = "Thêm ";
            btnThem.UseVisualStyleBackColor = false;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.Orange;
            btnSua.Location = new Point(304, 341);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 4;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.Red;
            btnXoa.Location = new Point(509, 341);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 5;
            btnXoa.Text = "Xóa ";
            btnXoa.UseVisualStyleBackColor = false;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.PaleTurquoise;
            btnLamMoi.Location = new Point(688, 341);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 29);
            btnLamMoi.TabIndex = 6;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            // 
            // dgvMonHoc
            // 
            dgvMonHoc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMonHoc.Location = new Point(99, 399);
            dgvMonHoc.Name = "dgvMonHoc";
            dgvMonHoc.RowHeadersWidth = 51;
            dgvMonHoc.Size = new Size(696, 188);
            dgvMonHoc.TabIndex = 7;
            // 
            // FrmMonHoc
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1131, 619);
            Controls.Add(dgvMonHoc);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Name = "FrmMonHoc";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmMonHoc";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMonHoc).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private TextBox txtSoTinChi;
        private TextBox txtTenMH;
        private TextBox txtMaMH;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox txtGiangVien;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private DataGridView dgvMonHoc;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}