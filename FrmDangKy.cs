using System;
using System.Windows.Forms;
using UDSV.BLL;
using UDSV.DTO; // Dùng để gọi TaiKhoanDTO

namespace UDSV
{
    public partial class FrmDangKy : Form
    {
        // Gọi tầng BLL xử lý đăng ký
        private readonly TaiKhoanBLL tkBLL = new TaiKhoanBLL();

        public FrmDangKy()
        {
            InitializeComponent();

            // Gắn sự kiện click cho 2 nút bấm theo đúng tên trong Designer
            btnDangKy.Click += BtnDangKy_Click;
            btnQuayLai.Click += BtnQuayLai_Click;
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