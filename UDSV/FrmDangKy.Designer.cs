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
            pnlLeft = new Controls.GradientPanel();
            pictureBox1 = new PictureBox();
            lblBrand1 = new Label();
            lblBrand2 = new Label();
            lblBrandSub = new Label();
            lblClose = new Label();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblFieldTK = new Label();
            pnlDK_TaiKhoan = new Controls.TextBoxContainer();
            txtDK_TaiKhoan = new TextBox();
            lblFieldMK = new Label();
            pnlDK_MatKhau = new Controls.TextBoxContainer();
            txtDK_MatKhau = new TextBox();
            lblFieldNLMK = new Label();
            pnlDK_NhapLaiMK = new Controls.TextBoxContainer();
            txtDK_NhapLaiMK = new TextBox();
            lblFieldHT = new Label();
            pnlDK_HoTen = new Controls.TextBoxContainer();
            txtDK_HoTen = new TextBox();
            btnDangKy = new Controls.GradientButton();
            btnQuayLai = new Button();

            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlLeft.SuspendLayout();
            SuspendLayout();

            // ──────────────────────────────
            // pnlLeft - Panel gradient bên trái
            // ──────────────────────────────
            pnlLeft.Dock = DockStyle.Left;
            pnlLeft.Size = new Size(380, 600);
            pnlLeft.ColorStart = Color.FromArgb(102, 126, 234);
            pnlLeft.ColorEnd = Color.FromArgb(118, 75, 162);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.TabIndex = 100;

            // ──────────────────────────────
            // pictureBox1 - Hình minh họa
            // ──────────────────────────────
            pictureBox1.Image = Properties.Resources.images__1_;
            pictureBox1.Location = new Point(100, 100);
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
            lblBrand1.Location = new Point(62, 310);
            lblBrand1.Name = "lblBrand1";
            lblBrand1.Text = "HỆ THỐNG";

            // ──────────────────────────────
            // lblBrand2 - "QUẢN LÝ SINH VIÊN"
            // ──────────────────────────────
            lblBrand2.AutoSize = true;
            lblBrand2.Font = new Font("Segoe UI", 12F);
            lblBrand2.ForeColor = Color.FromArgb(220, 255, 255, 255);
            lblBrand2.BackColor = Color.Transparent;
            lblBrand2.Location = new Point(64, 363);
            lblBrand2.Name = "lblBrand2";
            lblBrand2.Text = "QUẢN LÝ SINH VIÊN";

            // ──────────────────────────────
            // lblBrandSub
            // ──────────────────────────────
            lblBrandSub.AutoSize = true;
            lblBrandSub.Font = new Font("Segoe UI", 9F);
            lblBrandSub.ForeColor = Color.FromArgb(180, 255, 255, 255);
            lblBrandSub.BackColor = Color.Transparent;
            lblBrandSub.Location = new Point(64, 400);
            lblBrandSub.Name = "lblBrandSub";
            lblBrandSub.Text = "Tạo tài khoản mới để bắt đầu";

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
            // lblTitle - "Đăng Ký Tài Khoản"
            // ──────────────────────────────
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(45, 52, 54);
            lblTitle.Location = new Point(450, 45);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "Đăng Ký Tài Khoản";

            // ──────────────────────────────
            // lblSubtitle
            // ──────────────────────────────
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(99, 110, 114);
            lblSubtitle.Location = new Point(455, 100);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "Điền thông tin bên dưới để tạo tài khoản mới.";

            // ──────────────────────────────
            // Field 1: Tên tài khoản
            // ──────────────────────────────
            lblFieldTK.AutoSize = true;
            lblFieldTK.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFieldTK.ForeColor = Color.FromArgb(99, 110, 114);
            lblFieldTK.Location = new Point(455, 148);
            lblFieldTK.Name = "lblFieldTK";
            lblFieldTK.Text = "Tên tài khoản";

            pnlDK_TaiKhoan.Location = new Point(452, 175);
            pnlDK_TaiKhoan.Size = new Size(410, 46);
            pnlDK_TaiKhoan.Name = "pnlDK_TaiKhoan";
            pnlDK_TaiKhoan.TabIndex = 10;

            txtDK_TaiKhoan.Name = "txtDK_TaiKhoan";
            txtDK_TaiKhoan.TabIndex = 0;

            // ──────────────────────────────
            // Field 2: Mật khẩu
            // ──────────────────────────────
            lblFieldMK.AutoSize = true;
            lblFieldMK.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFieldMK.ForeColor = Color.FromArgb(99, 110, 114);
            lblFieldMK.Location = new Point(455, 231);
            lblFieldMK.Name = "lblFieldMK";
            lblFieldMK.Text = "Mật khẩu";

            pnlDK_MatKhau.Location = new Point(452, 258);
            pnlDK_MatKhau.Size = new Size(410, 46);
            pnlDK_MatKhau.Name = "pnlDK_MatKhau";
            pnlDK_MatKhau.TabIndex = 11;

            txtDK_MatKhau.Name = "txtDK_MatKhau";
            txtDK_MatKhau.TabIndex = 1;
            txtDK_MatKhau.UseSystemPasswordChar = true;

            // ──────────────────────────────
            // Field 3: Nhập lại mật khẩu
            // ──────────────────────────────
            lblFieldNLMK.AutoSize = true;
            lblFieldNLMK.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFieldNLMK.ForeColor = Color.FromArgb(99, 110, 114);
            lblFieldNLMK.Location = new Point(455, 314);
            lblFieldNLMK.Name = "lblFieldNLMK";
            lblFieldNLMK.Text = "Nhập lại mật khẩu";

            pnlDK_NhapLaiMK.Location = new Point(452, 341);
            pnlDK_NhapLaiMK.Size = new Size(410, 46);
            pnlDK_NhapLaiMK.Name = "pnlDK_NhapLaiMK";
            pnlDK_NhapLaiMK.TabIndex = 12;

            txtDK_NhapLaiMK.Name = "txtDK_NhapLaiMK";
            txtDK_NhapLaiMK.TabIndex = 2;
            txtDK_NhapLaiMK.UseSystemPasswordChar = true;

            // ──────────────────────────────
            // Field 4: Họ và tên
            // ──────────────────────────────
            lblFieldHT.AutoSize = true;
            lblFieldHT.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFieldHT.ForeColor = Color.FromArgb(99, 110, 114);
            lblFieldHT.Location = new Point(455, 397);
            lblFieldHT.Name = "lblFieldHT";
            lblFieldHT.Text = "Họ và tên";

            pnlDK_HoTen.Location = new Point(452, 424);
            pnlDK_HoTen.Size = new Size(410, 46);
            pnlDK_HoTen.Name = "pnlDK_HoTen";
            pnlDK_HoTen.TabIndex = 13;

            txtDK_HoTen.Name = "txtDK_HoTen";
            txtDK_HoTen.TabIndex = 3;

            // ──────────────────────────────
            // btnDangKy - Nút đăng ký gradient
            // ──────────────────────────────
            btnDangKy.Location = new Point(452, 492);
            btnDangKy.Size = new Size(410, 48);
            btnDangKy.Text = "ĐĂNG KÝ";
            btnDangKy.TabIndex = 4;
            btnDangKy.Name = "btnDangKy";

            // ──────────────────────────────
            // btnQuayLai - Link quay lại đăng nhập
            // ──────────────────────────────
            btnQuayLai.FlatStyle = FlatStyle.Flat;
            btnQuayLai.FlatAppearance.BorderSize = 0;
            btnQuayLai.FlatAppearance.MouseOverBackColor = Color.White;
            btnQuayLai.FlatAppearance.MouseDownBackColor = Color.White;
            btnQuayLai.BackColor = Color.White;
            btnQuayLai.ForeColor = Color.FromArgb(102, 126, 234);
            btnQuayLai.Font = new Font("Segoe UI", 10F);
            btnQuayLai.Cursor = Cursors.Hand;
            btnQuayLai.Location = new Point(452, 553);
            btnQuayLai.Size = new Size(410, 35);
            btnQuayLai.Text = "Đã có tài khoản? Quay về đăng nhập";
            btnQuayLai.TabIndex = 5;
            btnQuayLai.Name = "btnQuayLai";
            btnQuayLai.UseVisualStyleBackColor = false;

            // ──────────────────────────────
            // Gắn TextBox vào Container
            // ──────────────────────────────
            pnlDK_TaiKhoan.AttachTextBox(txtDK_TaiKhoan);
            pnlDK_MatKhau.AttachTextBox(txtDK_MatKhau);
            pnlDK_NhapLaiMK.AttachTextBox(txtDK_NhapLaiMK);
            pnlDK_HoTen.AttachTextBox(txtDK_HoTen);

            // ──────────────────────────────
            // Thêm controls vào panel trái
            // ──────────────────────────────
            pnlLeft.Controls.Add(lblBrandSub);
            pnlLeft.Controls.Add(lblBrand2);
            pnlLeft.Controls.Add(lblBrand1);
            pnlLeft.Controls.Add(pictureBox1);

            // ──────────────────────────────
            // FrmDangKy
            // ──────────────────────────────
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(950, 600);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            AcceptButton = btnDangKy;
            Name = "FrmDangKy";
            Text = "Đăng ký tài khoản";

            Controls.Add(btnQuayLai);
            Controls.Add(btnDangKy);
            Controls.Add(pnlDK_HoTen);
            Controls.Add(lblFieldHT);
            Controls.Add(pnlDK_NhapLaiMK);
            Controls.Add(lblFieldNLMK);
            Controls.Add(pnlDK_MatKhau);
            Controls.Add(lblFieldMK);
            Controls.Add(pnlDK_TaiKhoan);
            Controls.Add(lblFieldTK);
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
        private Label lblFieldTK;
        private Controls.TextBoxContainer pnlDK_TaiKhoan;
        private TextBox txtDK_TaiKhoan;
        private Label lblFieldMK;
        private Controls.TextBoxContainer pnlDK_MatKhau;
        private TextBox txtDK_MatKhau;
        private Label lblFieldNLMK;
        private Controls.TextBoxContainer pnlDK_NhapLaiMK;
        private TextBox txtDK_NhapLaiMK;
        private Label lblFieldHT;
        private Controls.TextBoxContainer pnlDK_HoTen;
        private TextBox txtDK_HoTen;
        private Controls.GradientButton btnDangKy;
        private Button btnQuayLai;
    }
}
