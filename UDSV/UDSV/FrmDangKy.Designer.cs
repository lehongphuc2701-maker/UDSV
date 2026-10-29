namespace UDSV
{
    partial class FrmDangKy
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
            pictureBox1 = new PictureBox();
            label2 = new Label();
            txtDK_TaiKhoan = new TextBox();
            txtDK_MatKhau = new TextBox();
            label3 = new Label();
            label4 = new Label();
            txtDK_NhapLaiMK = new TextBox();
            txtDK_HoTen = new TextBox();
            label5 = new Label();
            groupBox1 = new GroupBox();
            btnDangKy = new Button();
            btnQuayLai = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(277, 9);
            label1.Name = "label1";
            label1.Size = new Size(267, 41);
            label1.TabIndex = 1;
            label1.Text = "Đăng kí tài khoản";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.images__1_;
            pictureBox1.Location = new Point(26, 119);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(229, 202);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 34);
            label2.Name = "label2";
            label2.Size = new Size(103, 20);
            label2.TabIndex = 9;
            label2.Text = "Tên tài khoản";
            // 
            // txtDK_TaiKhoan
            // 
            txtDK_TaiKhoan.Location = new Point(142, 31);
            txtDK_TaiKhoan.Name = "txtDK_TaiKhoan";
            txtDK_TaiKhoan.Size = new Size(125, 27);
            txtDK_TaiKhoan.TabIndex = 10;
            // 
            // txtDK_MatKhau
            // 
            txtDK_MatKhau.Location = new Point(142, 77);
            txtDK_MatKhau.Name = "txtDK_MatKhau";
            txtDK_MatKhau.Size = new Size(125, 27);
            txtDK_MatKhau.TabIndex = 11;
            txtDK_MatKhau.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 77);
            label3.Name = "label3";
            label3.Size = new Size(75, 20);
            label3.TabIndex = 13;
            label3.Text = "Mật khẩu";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 160);
            label4.Name = "label4";
            label4.Size = new Size(93, 20);
            label4.TabIndex = 14;
            label4.Text = "Nhập lại mk";
            // 
            // txtDK_NhapLaiMK
            // 
            txtDK_NhapLaiMK.Location = new Point(142, 160);
            txtDK_NhapLaiMK.Name = "txtDK_NhapLaiMK";
            txtDK_NhapLaiMK.Size = new Size(125, 27);
            txtDK_NhapLaiMK.TabIndex = 15;
            txtDK_NhapLaiMK.UseSystemPasswordChar = true;
            // 
            // txtDK_HoTen
            // 
            txtDK_HoTen.Location = new Point(142, 220);
            txtDK_HoTen.Name = "txtDK_HoTen";
            txtDK_HoTen.Size = new Size(125, 27);
            txtDK_HoTen.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(26, 223);
            label5.Name = "label5";
            label5.Size = new Size(96, 20);
            label5.TabIndex = 17;
            label5.Text = "Nhập họ tên";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ScrollBar;
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtDK_HoTen);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtDK_TaiKhoan);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtDK_NhapLaiMK);
            groupBox1.Controls.Add(txtDK_MatKhau);
            groupBox1.Controls.Add(label4);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 163);
            groupBox1.ForeColor = SystemColors.Highlight;
            groupBox1.Location = new Point(355, 119);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(344, 263);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Biểu mẫu đăng kí";
            // 
            // btnDangKy
            // 
            btnDangKy.BackColor = SystemColors.MenuHighlight;
            btnDangKy.Location = new Point(485, 421);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(94, 29);
            btnDangKy.TabIndex = 19;
            btnDangKy.Text = "Đăng kí";
            btnDangKy.UseVisualStyleBackColor = false;
            // 
            // btnQuayLai
            // 
            btnQuayLai.BackColor = Color.Red;
            btnQuayLai.Location = new Point(200, 421);
            btnQuayLai.Name = "btnQuayLai";
            btnQuayLai.Size = new Size(154, 29);
            btnQuayLai.TabIndex = 20;
            btnQuayLai.Text = "Quay về đăng nhập";
            btnQuayLai.UseVisualStyleBackColor = false;
            // 
            // FrmDangKy
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(845, 513);
            Controls.Add(btnQuayLai);
            Controls.Add(btnDangKy);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Name = "FrmDangKy";
            Text = "FrmDangKy";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private TextBox txtDK_TaiKhoan;
        private TextBox txtDK_MatKhau;
        private Label label3;
        private Label label4;
        private TextBox txtDK_NhapLaiMK;
        private TextBox txtDK_HoTen;
        private Label label5;
        private GroupBox groupBox1;
        private Button btnDangKy;
        private Button btnQuayLai;
    }
}