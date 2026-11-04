namespace UDSV
{
    partial class FrmThongTinTaiKhoan
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
            pnlHeader = new Controls.GradientPanel();
            lblSub = new Label();
            lblTitle = new Label();
            cardInfo = new Controls.CardPanel();
            txtVaiTro = new TextBox();
            labelVaiTro = new Label();
            txtHoTen = new TextBox();
            labelHoTen = new Label();
            txtUsername = new TextBox();
            labelUser = new Label();
            lblTitleInfo = new Label();
            cardPass = new Controls.CardPanel();
            txtNhapLaiMK = new TextBox();
            labelNL = new Label();
            txtMatKhauMoi = new TextBox();
            labelMKM = new Label();
            txtMatKhauCu = new TextBox();
            labelMKC = new Label();
            lblTitlePass = new Label();
            btnDoiMatKhau = new Controls.GradientButton();
            btnDong = new Button();
            pnlHeader.SuspendLayout();
            cardInfo.SuspendLayout();
            cardPass.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Angle = 135F;
            pnlHeader.ColorEnd = Color.FromArgb(118, 75, 162);
            pnlHeader.ColorStart = Color.FromArgb(102, 126, 234);
            pnlHeader.Controls.Add(lblSub);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(500, 68);
            pnlHeader.TabIndex = 0;
            // 
            // lblSub
            // 
            lblSub.AutoSize = true;
            lblSub.BackColor = Color.Transparent;
            lblSub.Font = new Font("Segoe UI", 9F);
            lblSub.ForeColor = Color.FromArgb(230, 240, 255);
            lblSub.Location = new Point(20, 39);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(318, 20);
            lblSub.TabIndex = 1;
            lblSub.Text = "Xem thông tin người dùng và đổi mật khẩu mới";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(18, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(286, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÔNG TIN TÀI KHOẢN";
            // 
            // cardInfo
            // 
            cardInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cardInfo.BackColor = Color.White;
            cardInfo.Controls.Add(txtVaiTro);
            cardInfo.Controls.Add(labelVaiTro);
            cardInfo.Controls.Add(txtHoTen);
            cardInfo.Controls.Add(labelHoTen);
            cardInfo.Controls.Add(txtUsername);
            cardInfo.Controls.Add(labelUser);
            cardInfo.Controls.Add(lblTitleInfo);
            cardInfo.Location = new Point(25, 80);
            cardInfo.Name = "cardInfo";
            cardInfo.Radius = 14;
            cardInfo.Size = new Size(450, 150);
            cardInfo.TabIndex = 1;
            // 
            // txtVaiTro
            // 
            txtVaiTro.BackColor = Color.FromArgb(248, 250, 252);
            txtVaiTro.BorderStyle = BorderStyle.FixedSingle;
            txtVaiTro.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtVaiTro.ForeColor = Color.FromArgb(16, 185, 129);
            txtVaiTro.Location = new Point(275, 105);
            txtVaiTro.Name = "txtVaiTro";
            txtVaiTro.ReadOnly = true;
            txtVaiTro.Size = new Size(145, 29);
            txtVaiTro.TabIndex = 6;
            // 
            // labelVaiTro
            // 
            labelVaiTro.AutoSize = true;
            labelVaiTro.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            labelVaiTro.ForeColor = Color.FromArgb(100, 110, 125);
            labelVaiTro.Location = new Point(275, 85);
            labelVaiTro.Name = "labelVaiTro";
            labelVaiTro.Size = new Size(59, 19);
            labelVaiTro.TabIndex = 5;
            labelVaiTro.Text = "Vai trò:";
            // 
            // txtHoTen
            // 
            txtHoTen.BackColor = Color.FromArgb(248, 250, 252);
            txtHoTen.BorderStyle = BorderStyle.FixedSingle;
            txtHoTen.Font = new Font("Segoe UI", 9.5F);
            txtHoTen.ForeColor = Color.FromArgb(45, 52, 54);
            txtHoTen.Location = new Point(25, 105);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.ReadOnly = true;
            txtHoTen.Size = new Size(230, 29);
            txtHoTen.TabIndex = 4;
            // 
            // labelHoTen
            // 
            labelHoTen.AutoSize = true;
            labelHoTen.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            labelHoTen.ForeColor = Color.FromArgb(100, 110, 125);
            labelHoTen.Location = new Point(25, 85);
            labelHoTen.Name = "labelHoTen";
            labelHoTen.Size = new Size(80, 19);
            labelHoTen.TabIndex = 3;
            labelHoTen.Text = "Họ và tên:";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(248, 250, 252);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtUsername.ForeColor = Color.FromArgb(45, 52, 54);
            txtUsername.Location = new Point(140, 43);
            txtUsername.Name = "txtUsername";
            txtUsername.ReadOnly = true;
            txtUsername.Size = new Size(280, 29);
            txtUsername.TabIndex = 2;
            // 
            // labelUser
            // 
            labelUser.AutoSize = true;
            labelUser.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            labelUser.ForeColor = Color.FromArgb(100, 110, 125);
            labelUser.Location = new Point(25, 47);
            labelUser.Name = "labelUser";
            labelUser.Size = new Size(109, 19);
            labelUser.TabIndex = 1;
            labelUser.Text = "Tên đăng nhập:";
            // 
            // lblTitleInfo
            // 
            lblTitleInfo.AutoSize = true;
            lblTitleInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitleInfo.ForeColor = Color.FromArgb(102, 126, 234);
            lblTitleInfo.Location = new Point(20, 12);
            lblTitleInfo.Name = "lblTitleInfo";
            lblTitleInfo.Size = new Size(183, 23);
            lblTitleInfo.TabIndex = 0;
            lblTitleInfo.Text = "HỒ SƠ NGƯỜI DÙNG";
            // 
            // cardPass
            // 
            cardPass.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cardPass.BackColor = Color.White;
            cardPass.Controls.Add(txtNhapLaiMK);
            cardPass.Controls.Add(labelNL);
            cardPass.Controls.Add(txtMatKhauMoi);
            cardPass.Controls.Add(labelMKM);
            cardPass.Controls.Add(txtMatKhauCu);
            cardPass.Controls.Add(labelMKC);
            cardPass.Controls.Add(lblTitlePass);
            cardPass.Location = new Point(25, 245);
            cardPass.Name = "cardPass";
            cardPass.Radius = 14;
            cardPass.Size = new Size(450, 215);
            cardPass.TabIndex = 2;
            // 
            // txtNhapLaiMK
            // 
            txtNhapLaiMK.BorderStyle = BorderStyle.FixedSingle;
            txtNhapLaiMK.Font = new Font("Segoe UI", 9.5F);
            txtNhapLaiMK.Location = new Point(25, 168);
            txtNhapLaiMK.Name = "txtNhapLaiMK";
            txtNhapLaiMK.PlaceholderText = "Nhập lại mật khẩu mới để xác nhận";
            txtNhapLaiMK.Size = new Size(395, 29);
            txtNhapLaiMK.TabIndex = 6;
            txtNhapLaiMK.UseSystemPasswordChar = true;
            // 
            // labelNL
            // 
            labelNL.AutoSize = true;
            labelNL.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            labelNL.ForeColor = Color.FromArgb(100, 110, 125);
            labelNL.Location = new Point(25, 148);
            labelNL.Name = "labelNL";
            labelNL.Size = new Size(177, 19);
            labelNL.TabIndex = 5;
            labelNL.Text = "Xác nhận mật khẩu mới (*)";
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.BorderStyle = BorderStyle.FixedSingle;
            txtMatKhauMoi.Font = new Font("Segoe UI", 9.5F);
            txtMatKhauMoi.Location = new Point(25, 113);
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.PlaceholderText = "Tối thiểu 6 ký tự";
            txtMatKhauMoi.Size = new Size(395, 29);
            txtMatKhauMoi.TabIndex = 4;
            txtMatKhauMoi.UseSystemPasswordChar = true;
            // 
            // labelMKM
            // 
            labelMKM.AutoSize = true;
            labelMKM.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            labelMKM.ForeColor = Color.FromArgb(100, 110, 125);
            labelMKM.Location = new Point(25, 93);
            labelMKM.Name = "labelMKM";
            labelMKM.Size = new Size(137, 19);
            labelMKM.TabIndex = 3;
            labelMKM.Text = "Mật khẩu mới (*):";
            // 
            // txtMatKhauCu
            // 
            txtMatKhauCu.BorderStyle = BorderStyle.FixedSingle;
            txtMatKhauCu.Font = new Font("Segoe UI", 9.5F);
            txtMatKhauCu.Location = new Point(25, 58);
            txtMatKhauCu.Name = "txtMatKhauCu";
            txtMatKhauCu.PlaceholderText = "Nhập mật khẩu đang sử dụng";
            txtMatKhauCu.Size = new Size(395, 29);
            txtMatKhauCu.TabIndex = 2;
            txtMatKhauCu.UseSystemPasswordChar = true;
            // 
            // labelMKC
            // 
            labelMKC.AutoSize = true;
            labelMKC.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            labelMKC.ForeColor = Color.FromArgb(100, 110, 125);
            labelMKC.Location = new Point(25, 38);
            labelMKC.Name = "labelMKC";
            labelMKC.Size = new Size(157, 19);
            labelMKC.TabIndex = 1;
            labelMKC.Text = "Mật khẩu hiện tại (*):";
            // 
            // lblTitlePass
            // 
            lblTitlePass.AutoSize = true;
            lblTitlePass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitlePass.ForeColor = Color.FromArgb(102, 126, 234);
            lblTitlePass.Location = new Point(20, 12);
            lblTitlePass.Name = "lblTitlePass";
            lblTitlePass.Size = new Size(137, 23);
            lblTitlePass.TabIndex = 0;
            lblTitlePass.Text = "ĐỔI MẬT KHẨU";
            // 
            // btnDoiMatKhau
            // 
            btnDoiMatKhau.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnDoiMatKhau.BackColor = Color.Transparent;
            btnDoiMatKhau.Cursor = Cursors.Hand;
            btnDoiMatKhau.FlatAppearance.BorderSize = 0;
            btnDoiMatKhau.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnDoiMatKhau.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnDoiMatKhau.FlatStyle = FlatStyle.Flat;
            btnDoiMatKhau.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDoiMatKhau.ForeColor = Color.White;
            btnDoiMatKhau.GradientEnd = Color.FromArgb(118, 75, 162);
            btnDoiMatKhau.GradientStart = Color.FromArgb(102, 126, 234);
            btnDoiMatKhau.Location = new Point(25, 475);
            btnDoiMatKhau.Name = "btnDoiMatKhau";
            btnDoiMatKhau.Radius = 12;
            btnDoiMatKhau.Size = new Size(450, 42);
            btnDoiMatKhau.TabIndex = 3;
            btnDoiMatKhau.Text = "🔒 CẬP NHẬT MẬT KHẨU";
            btnDoiMatKhau.UseVisualStyleBackColor = false;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnDong.BackColor = Color.Transparent;
            btnDong.Cursor = Cursors.Hand;
            btnDong.FlatAppearance.BorderSize = 0;
            btnDong.FlatAppearance.MouseDownBackColor = Color.FromArgb(240, 240, 240);
            btnDong.FlatAppearance.MouseOverBackColor = Color.FromArgb(248, 249, 250);
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.Font = new Font("Segoe UI", 9.5F);
            btnDong.ForeColor = Color.FromArgb(108, 117, 125);
            btnDong.Location = new Point(25, 525);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(450, 32);
            btnDong.TabIndex = 4;
            btnDong.Text = "Đóng cửa sổ";
            btnDong.UseVisualStyleBackColor = false;
            // 
            // FrmThongTinTaiKhoan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(500, 575);
            Controls.Add(btnDong);
            Controls.Add(btnDoiMatKhau);
            Controls.Add(cardPass);
            Controls.Add(cardInfo);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmThongTinTaiKhoan";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thông tin tài khoản & Đổi mật khẩu";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            cardInfo.ResumeLayout(false);
            cardInfo.PerformLayout();
            cardPass.ResumeLayout(false);
            cardPass.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Controls.GradientPanel pnlHeader;
        private Label lblTitle;
        private Label lblSub;
        private Controls.CardPanel cardInfo;
        private Label lblTitleInfo;
        private Label labelUser;
        private TextBox txtUsername;
        private Label labelHoTen;
        private TextBox txtHoTen;
        private Label labelVaiTro;
        private TextBox txtVaiTro;
        private Controls.CardPanel cardPass;
        private Label lblTitlePass;
        private Label labelMKC;
        private TextBox txtMatKhauCu;
        private Label labelMKM;
        private TextBox txtMatKhauMoi;
        private Label labelNL;
        private TextBox txtNhapLaiMK;
        private Controls.GradientButton btnDoiMatKhau;
        private Button btnDong;
    }
}

