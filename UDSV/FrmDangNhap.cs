using System;
using System.Windows.Forms;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV
{
    /// <summary>
    /// Màn hình đăng nhập — cửa vào duy nhất của ứng dụng.
    /// Đúng sai do TaiKhoanBLL quyết định; form chỉ hiển thị kết quả.
    /// </summary>
    public partial class FrmDangNhap : Form
    {
        private readonly TaiKhoanBLL tkBLL = new TaiKhoanBLL();

        public FrmDangNhap()
        {
            InitializeComponent();
            GanSuKien();
        }

        private void GanSuKien()
        {
            Shown += FrmDangNhap_Shown;
            btnDangNhap.Click += BtnDangNhap_Click;
            btnChuyenDangKy.Click += BtnChuyenDangKy_Click;
            txtMatKhau.KeyDown += TxtMatKhau_KeyDown;
        }

        private void FrmDangNhap_Shown(object sender, EventArgs e)
        {
            txtTaiKhoan.Focus();
        }

        // ---------- Đăng nhập ----------

        private void BtnDangNhap_Click(object sender, EventArgs e)
        {
            DangNhap();
        }

        private void TxtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                DangNhap();
            }
        }

        private void DangNhap()
        {
            try
            {
                if (!tkBLL.DangNhap(txtTaiKhoan.Text, txtMatKhau.Text,
                                    out TaiKhoanDTO taiKhoan, out string error))
                {
                    MessageBox.Show(this, error, "Đăng nhập thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                    return;
                }

                MoTrangChu(taiKhoan);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi kết nối CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Mở trang chủ dạng hộp thoại; ẩn form đăng nhập cho tới khi quay lại.</summary>
        private void MoTrangChu(TaiKhoanDTO taiKhoan)
        {
            using (FrmTrangChu trangChu = new FrmTrangChu(taiKhoan))
            {
                this.Hide();
                trangChu.ShowDialog(this);
                this.Show();

                txtMatKhau.Clear();
                txtTaiKhoan.Focus();
            }
        }

        // ---------- Chuyển sang đăng ký ----------

        private void BtnChuyenDangKy_Click(object sender, EventArgs e)
        {
            using (FrmDangKy dangKy = new FrmDangKy())
            {
                if (dangKy.ShowDialog(this) == DialogResult.OK)
                {
                    // Đăng ký xong: điền sẵn tên tài khoản vừa tạo
                    txtTaiKhoan.Text = dangKy.TenTaiKhoanVuaTao;
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
        }
    }
}
