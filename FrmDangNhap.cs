using System;
using System.Windows.Forms;
using UDSV.BLL; // Gọi tầng BLL

namespace UDSV
{
    public partial class FrmDangNhap : Form
    {
        // Khởi tạo BLL xử lý tài khoản
        private readonly TaiKhoanBLL tkBLL = new TaiKhoanBLL();

        public FrmDangNhap()
        {
            InitializeComponent();

            // Gắn sự kiện cho các nút
            btnDangNhap.Click += BtnDangNhap_Click;
            btnChuyenDangKy.Click += BtnChuyenDangKy_Click;
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

            // 2. Gọi BLL kiểm tra Database thật
            if (tkBLL.KiemTraDangNhap(taiKhoan, matKhau))
            {
                // Đăng nhập thành công -> Ẩn form Đăng nhập
                this.Hide();

                // Mở form Trang chủ
                FrmTrangChu frmTrangChu = new FrmTrangChu();
                frmTrangChu.ShowDialog();

                // Khi form Trang chủ bị đóng, xóa trắng ô nhập và hiện lại Đăng nhập
                txtTaiKhoan.Clear();
                txtMatKhau.Clear();
                this.Show();
                txtTaiKhoan.Focus();
            }
            else
            {
                // Đăng nhập thất bại
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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