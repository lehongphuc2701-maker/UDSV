namespace UDSV
{
    partial class FrmDangNhap
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
            pnlLeft = new Controls.GradientPanel();
            pictureBox1 = new PictureBox();
            lblBrand1 = new Label();
            lblBrand2 = new Label();
            lblBrandSub = new Label();
            lblClose = new Label();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblFieldUser = new Label();
            pnlTaiKhoan = new Controls.TextBoxContainer();
            txtTaiKhoan = new TextBox();
            lblFieldPass = new Label();
            pnlMatKhau = new Controls.TextBoxContainer();
            txtMatKhau = new TextBox();
            btnDangNhap = new Controls.GradientButton();
            btnChuyenDangKy = new Button();

            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlLeft.SuspendLayout();
            SuspendLayout();

            // ──────────────────────────────
            // pnlLeft - Panel gradient bên trái
            // ──────────────────────────────
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Size = new Size(380, 550);
            pnlLeft.ColorStart = Color.FromArgb(102, 126, 234);
            pnlLeft.ColorEnd = Color.FromArgb(118, 75, 162);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.TabIndex = 100;

            // ──────────────────────────────
            // pictureBox1 - Hình minh họa
            // ──────────────────────────────
            pictureBox1.Image = Properties.Resources.images__1_;
            pictureBox1.Location = new Point(100, 80);
            pictureBox1.Size = new Size(180, 180);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;

            // ──────────────────────────────
            // lblBrand1 - "HỆ THỐNG"
            // ──────────────────────────────
            lblBrand1.AutoSize = true;
            lblBrand1.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblBrand1.ForeColor = Color.White;
            lblBrand1.BackColor = Color.Transparent;
            lblBrand1.Location = new Point(62, 285);
            lblBrand1.Name = "lblBrand1";
            lblBrand1.Text = "HỆ THỐNG";

            // ──────────────────────────────
            // lblBrand2 - "QUẢN LÝ SINH VIÊN"
            // ──────────────────────────────
            lblBrand2.AutoSize = true;
            lblBrand2.Font = new Font("Segoe UI", 12F);
            lblBrand2.ForeColor = Color.FromArgb(220, 255, 255, 255);
            lblBrand2.BackColor = Color.Transparent;
            lblBrand2.Location = new Point(64, 338);
            lblBrand2.Name = "lblBrand2";
            lblBrand2.Text = "QUẢN LÝ SINH VIÊN";

            // ──────────────────────────────
            // lblBrandSub - Mô tả phụ
            // ──────────────────────────────
            lblBrandSub.AutoSize = true;
            lblBrandSub.Font = new Font("Segoe UI", 9F);
            lblBrandSub.ForeColor = Color.FromArgb(180, 255, 255, 255);
            lblBrandSub.BackColor = Color.Transparent;
            lblBrandSub.Location = new Point(64, 375);
            lblBrandSub.Name = "lblBrandSub";
            lblBrandSub.Text = "Đăng nhập để tiếp tục sử dụng";

            // ──────────────────────────────
            // lblClose - Nút đóng ✕
            // ──────────────────────────────
            lblClose.AutoSize = false;
            lblClose.Font = new Font("Segoe UI", 14F);
            lblClose.ForeColor = Color.FromArgb(180, 180, 180);
            lblClose.Location = new Point(910, 8);
            lblClose.Size = new Size(32, 32);
            lblClose.Text = "✕";
            lblClose.TextAlign = ContentAlignment.MiddleCenter;
            lblClose.Cursor = Cursors.Hand;
            lblClose.Name = "lblClose";

            // ──────────────────────────────
            // lblTitle - "Đăng Nhập"
            // ──────────────────────────────
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(45, 52, 54);
            lblTitle.Location = new Point(455, 85);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "Đăng Nhập";

            // ──────────────────────────────
            // lblSubtitle
            // ──────────────────────────────
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(99, 110, 114);
            lblSubtitle.Location = new Point(460, 145);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "Chào mừng bạn trở lại! Vui lòng đăng nhập.";

            // ──────────────────────────────
            // lblFieldUser - Nhãn "Tên đăng nhập"
            // ──────────────────────────────
            lblFieldUser.AutoSize = true;
            lblFieldUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFieldUser.ForeColor = Color.FromArgb(99, 110, 114);
            lblFieldUser.Location = new Point(460, 200);
            lblFieldUser.Name = "lblFieldUser";
            lblFieldUser.Text = "Tên đăng nhập";

            // ──────────────────────────────
            // pnlTaiKhoan - Container cho ô nhập tài khoản
            // ──────────────────────────────
            pnlTaiKhoan.Location = new Point(457, 228);
            pnlTaiKhoan.Size = new Size(410, 48);
            pnlTaiKhoan.Name = "pnlTaiKhoan";
            pnlTaiKhoan.TabIndex = 10;

            // ──────────────────────────────
            // txtTaiKhoan
            // ──────────────────────────────
            txtTaiKhoan.Name = "txtTaiKhoan";
            txtTaiKhoan.TabIndex = 0;

            // ──────────────────────────────
            // lblFieldPass - Nhãn "Mật khẩu"
            // ──────────────────────────────
            lblFieldPass.AutoSize = true;
            lblFieldPass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFieldPass.ForeColor = Color.FromArgb(99, 110, 114);
            lblFieldPass.Location = new Point(460, 293);
            lblFieldPass.Name = "lblFieldPass";
            lblFieldPass.Text = "Mật khẩu";

            // ──────────────────────────────
            // pnlMatKhau - Container cho ô nhập mật khẩu
            // ──────────────────────────────
            pnlMatKhau.Location = new Point(457, 321);
            pnlMatKhau.Size = new Size(410, 48);
            pnlMatKhau.Name = "pnlMatKhau";
            pnlMatKhau.TabIndex = 11;

            // ──────────────────────────────
            // txtMatKhau
            // ──────────────────────────────
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.TabIndex = 1;
            txtMatKhau.UseSystemPasswordChar = true;

            // ──────────────────────────────
            // btnDangNhap - Nút đăng nhập gradient
            // ──────────────────────────────
            btnDangNhap.Location = new Point(457, 400);
            btnDangNhap.Size = new Size(410, 48);
            btnDangNhap.Text = "ĐĂNG NHẬP";
            btnDangNhap.TabIndex = 2;
            btnDangNhap.Name = "btnDangNhap";

            // ──────────────────────────────
            // btnChuyenDangKy - Link chuyển đăng ký
            // ──────────────────────────────
            btnChuyenDangKy.FlatStyle = FlatStyle.Flat;
            btnChuyenDangKy.FlatAppearance.BorderSize = 0;
            btnChuyenDangKy.FlatAppearance.MouseOverBackColor = Color.White;
            btnChuyenDangKy.FlatAppearance.MouseDownBackColor = Color.White;
            btnChuyenDangKy.BackColor = Color.White;
            btnChuyenDangKy.ForeColor = Color.FromArgb(102, 126, 234);
            btnChuyenDangKy.Font = new Font("Segoe UI", 10F);
            btnChuyenDangKy.Cursor = Cursors.Hand;
            btnChuyenDangKy.Location = new Point(457, 462);
            btnChuyenDangKy.Size = new Size(410, 35);
            btnChuyenDangKy.Text = "Chưa có tài khoản? Đăng ký ngay";
            btnChuyenDangKy.TabIndex = 3;
            btnChuyenDangKy.Name = "btnChuyenDangKy";
            btnChuyenDangKy.UseVisualStyleBackColor = false;

            // ──────────────────────────────
            // Gắn TextBox vào Container
            // ──────────────────────────────
            pnlTaiKhoan.AttachTextBox(txtTaiKhoan);
            pnlMatKhau.AttachTextBox(txtMatKhau);

            // ──────────────────────────────
            // Thêm controls vào panel trái
            // ──────────────────────────────
            pnlLeft.Controls.Add(lblBrandSub);
            pnlLeft.Controls.Add(lblBrand2);
            pnlLeft.Controls.Add(lblBrand1);
            pnlLeft.Controls.Add(pictureBox1);

            // ──────────────────────────────
            // FrmDangNhap
            // ──────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(950, 550);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            AcceptButton = btnDangNhap;
            Name = "FrmDangNhap";
            Text = "Đăng nhập sinh viên";

            Controls.Add(btnChuyenDangKy);
            Controls.Add(btnDangNhap);
            Controls.Add(pnlMatKhau);
            Controls.Add(lblFieldPass);
            Controls.Add(pnlTaiKhoan);
            Controls.Add(lblFieldUser);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            Controls.Add(lblClose);
            Controls.Add(pnlLeft);

            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlLeft.ResumeLayout(false);
            pnlLeft.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.GradientPanel pnlLeft;
        private PictureBox pictureBox1;
        private Label lblBrand1;
        private Label lblBrand2;
        private Label lblBrandSub;
        private Label lblClose;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblFieldUser;
        private Controls.TextBoxContainer pnlTaiKhoan;
        private TextBox txtTaiKhoan;
        private Label lblFieldPass;
        private Controls.TextBoxContainer pnlMatKhau;
        private TextBox txtMatKhau;
        private Controls.GradientButton btnDangNhap;
        private Button btnChuyenDangKy;
    }
}
