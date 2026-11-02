using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UDSV.BLL;
using UDSV.DTO; // Dùng để gọi TaiKhoanDTO

namespace UDSV
{
    public partial class FrmDangKy : Form
    {
        // Gọi tầng BLL xử lý đăng ký
        private readonly TaiKhoanBLL tkBLL = new TaiKhoanBLL();

        // --- Hỗ trợ kéo form không viền ---
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        public FrmDangKy()
        {
            InitializeComponent();
            SetupUI();

            // Gắn sự kiện click cho 2 nút bấm
            btnDangKy.Click += BtnDangKy_Click;
            btnQuayLai.Click += BtnQuayLai_Click;
        }

        // --- Hiệu ứng đổ bóng cho form không viền ---
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000; // CS_DROPSHADOW
                return cp;
            }
        }

        // --- THIẾT LẬP GIAO DIỆN HIỆN ĐẠI ---
        private void SetupUI()
        {
            // Kéo form khi nhấn chuột vào panel trái hoặc vùng trống
            pnlLeft.MouseDown += DragForm_MouseDown;
            this.MouseDown += DragForm_MouseDown;

            // Nút đóng ✕
            lblClose.Click += (s, e) => this.Close();
            lblClose.MouseEnter += (s, e) => lblClose.ForeColor = Color.FromArgb(231, 76, 60);
            lblClose.MouseLeave += (s, e) => lblClose.ForeColor = Color.FromArgb(180, 180, 180);

            // Hiệu ứng hover cho link quay lại
            btnQuayLai.MouseEnter += (s, e) => btnQuayLai.ForeColor = Color.FromArgb(118, 75, 162);
            btnQuayLai.MouseLeave += (s, e) => btnQuayLai.ForeColor = Color.FromArgb(102, 126, 234);
        }

        // --- KÉO FORM ---
        private void DragForm_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0); // WM_NCLBUTTONDOWN, HT_CAPTION
            }
        }

        // --- XỬ LÝ NÚT ĐĂNG KÝ ---
        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ các ô nhập
            string taiKhoan = txtDK_TaiKhoan.Text.Trim();
            string matKhau = txtDK_MatKhau.Text.Trim();
            string xacNhan = txtDK_NhapLaiMK.Text.Trim();
            string hoTen = txtDK_HoTen.Text.Trim();

            // 2. Bắt lỗi người dùng để trống
            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tài khoản, Mật khẩu và Họ tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDK_TaiKhoan.Focus();
                return;
            }

            // 3. Bắt lỗi nhập sai mật khẩu xác nhận
            if (matKhau != xacNhan)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDK_NhapLaiMK.Focus();
                return;
            }

            // 4. Gom đủ 3 biến vào đối tượng DTO
            TaiKhoanDTO tk = new TaiKhoanDTO(taiKhoan, matKhau, hoTen);

            // 5. Đẩy xuống BLL để xử lý lưu vào CSDL
            if (tkBLL.DangKy(tk, out string error))
            {
                MessageBox.Show("Đăng ký tài khoản thành công! Vui lòng đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Thành công thì tự động đóng form đăng ký để quay lại form đăng nhập
                this.Close();
            }
            else
            {
                MessageBox.Show(error, "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- XỬ LÝ NÚT QUAY LẠI ---
        private void BtnQuayLai_Click(object sender, EventArgs e)
        {
            // Đóng form hiện tại, Form Đăng Nhập sẽ tự động hiện lên (do đã cấu hình Show() ở FrmDangNhap)
            this.Close();
        }
    }
}
