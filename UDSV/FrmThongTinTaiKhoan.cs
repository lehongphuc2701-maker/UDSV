using System;
using System.Windows.Forms;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV
{
    public partial class FrmThongTinTaiKhoan : Form
    {
        private readonly TaiKhoanDTO? taiKhoanHienTai;
        private readonly TaiKhoanBLL tkBLL = new TaiKhoanBLL();

        public FrmThongTinTaiKhoan() : this(null) { }

        public FrmThongTinTaiKhoan(TaiKhoanDTO? taiKhoan)
        {
            InitializeComponent();
            taiKhoanHienTai = taiKhoan;

            Load += FrmThongTinTaiKhoan_Load;
            btnDoiMatKhau.Click += BtnDoiMatKhau_Click;
            btnDong.Click += (s, e) => this.Close();
        }

        private void FrmThongTinTaiKhoan_Load(object? sender, EventArgs e)
        {
            if (taiKhoanHienTai != null)
            {
                txtUsername.Text = taiKhoanHienTai.Username;
                txtHoTen.Text = taiKhoanHienTai.HoTen;
                txtVaiTro.Text = string.IsNullOrWhiteSpace(taiKhoanHienTai.VaiTro)
                    ? "Quản trị viên (Admin)"
                    : taiKhoanHienTai.VaiTro;
            }
            else
            {
                txtUsername.Text = "Chưa xác định";
                txtHoTen.Text = "Người dùng";
                txtVaiTro.Text = "Khách";
            }
        }

        private void BtnDoiMatKhau_Click(object? sender, EventArgs e)
        {
            if (taiKhoanHienTai == null || string.IsNullOrWhiteSpace(taiKhoanHienTai.Username))
            {
                MessageBox.Show(this, "Không tìm thấy thông tin tài khoản đang đăng nhập!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string matKhauCu = txtMatKhauCu.Text.Trim();
            string matKhauMoi = txtMatKhauMoi.Text.Trim();
            string xacNhan = txtNhapLaiMK.Text.Trim();

            if (tkBLL.DoiMatKhau(taiKhoanHienTai.Username, matKhauCu, matKhauMoi, xacNhan, out string error))
            {
                // Cập nhật lại mật khẩu trong đối tượng bộ nhớ hiện tại
                taiKhoanHienTai.Password = matKhauMoi;

                MessageBox.Show(this,
                    "Đổi mật khẩu thành công! Vui lòng ghi nhớ mật khẩu mới cho lần đăng nhập sau.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Xóa trắng các ô nhập mật khẩu
                txtMatKhauCu.Clear();
                txtMatKhauMoi.Clear();
                txtNhapLaiMK.Clear();
                txtMatKhauCu.Focus();
            }
            else
            {
                MessageBox.Show(this, error, "Không thể đổi mật khẩu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
