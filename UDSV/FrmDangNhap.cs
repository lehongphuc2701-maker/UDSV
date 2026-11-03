using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UDSV.BLL;
using UDSV.DTO; // Gọi tầng BLL

namespace UDSV
{
    public partial class FrmDangNhap : Form
    {
        // Khởi tạo BLL xử lý tài khoản
        private readonly TaiKhoanBLL tkBLL = new TaiKhoanBLL();

        // --- Hỗ trợ kéo form không viền ---
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        public FrmDangNhap()
        {
            InitializeComponent();
            SetupUI();

            // Gắn sự kiện cho các nút
            btnDangNhap.Click += BtnDangNhap_Click;
            btnChuyenDangKy.Click += BtnChuyenDangKy_Click;
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
            lblClose.Click += (s, e) => Application.Exit();
            lblClose.MouseEnter += (s, e) => lblClose.ForeColor = Color.FromArgb(231, 76, 60);
            lblClose.MouseLeave += (s, e) => lblClose.ForeColor = Color.FromArgb(180, 180, 180);

            // Hiệu ứng hover cho link đăng ký
            btnChuyenDangKy.MouseEnter += (s, e) => btnChuyenDangKy.ForeColor = Color.FromArgb(118, 75, 162);
            btnChuyenDangKy.MouseLeave += (s, e) => btnChuyenDangKy.ForeColor = Color.FromArgb(102, 126, 234);
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

        // --- XỬ LÝ NÚT ĐĂNG NHẬP ---
        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            // 1. Kiểm tra rỗng
            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTaiKhoan.Focus();
                return;
            }

            // 2. Gọi BLL kiểm tra Database thật — BLL trả về DTO và thông báo lỗi cụ thể
            if (tkBLL.DangNhap(taiKhoan, matKhau, out TaiKhoanDTO tkDangNhap, out string loiDangNhap))
            {
                // Đăng nhập thành công -> Ẩn form Đăng nhập
                this.Hide();

                // Mở form Trang chủ, truyền tài khoản để trang chủ chào đúng tên người dùng
                FrmTrangChu frmTrangChu = new FrmTrangChu(tkDangNhap);
                frmTrangChu.ShowDialog();

                // Khi form Trang chủ bị đóng, xóa trắng ô nhập và hiện lại Đăng nhập
                txtTaiKhoan.Clear();
                txtMatKhau.Clear();
                this.Show();
                txtTaiKhoan.Focus();
            }
            else
            {
                // Đăng nhập thất bại: ưu tiên thông báo cụ thể của BLL
                string thongBao = string.IsNullOrWhiteSpace(loiDangNhap)
                    ? "Tài khoản hoặc mật khẩu không đúng!"
                    : loiDangNhap;
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- XỬ LÝ NÚT CHUYỂN SANG FORM ĐĂNG KÝ ---
        private void BtnChuyenDangKy_Click(object sender, EventArgs e)
        {
            // Ẩn form Đăng nhập hiện tại
            this.Hide();

            // Mở form Đăng ký
            FrmDangKy frmDangKy = new FrmDangKy();
            frmDangKy.ShowDialog();

            // Khi tắt form Đăng ký, hiện lại form Đăng nhập
            this.Show();
        }
    }
}
