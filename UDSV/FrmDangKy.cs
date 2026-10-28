using System;
using System.Windows.Forms;
using UDSV.BLL;

namespace UDSV
{
    /// <summary>Màn hình đăng ký tài khoản mới.</summary>
    public partial class FrmDangKy : Form
    {
        private readonly TaiKhoanBLL tkBLL = new TaiKhoanBLL();

        /// <summary>Tên tài khoản vừa tạo — form đăng nhập dùng để điền sẵn.</summary>
        public string TenTaiKhoanVuaTao { get; private set; } = "";

        public FrmDangKy()
        {
            InitializeComponent();
            GanSuKien();
        }

        private void GanSuKien()
        {
            btnDangKy.Click += BtnDangKy_Click;
            btnQuayLai.Click += BtnQuayLai_Click;
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            try
            {
                if (!tkBLL.DangKy(txtDK_TaiKhoan.Text, txtDK_MatKhau.Text,
                                  txtDK_NhapLaiMK.Text, txtDK_HoTen.Text, out string error))
                {
                    MessageBox.Show(this, error, "Đăng ký thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TenTaiKhoanVuaTao = txtDK_TaiKhoan.Text.Trim();

                MessageBox.Show(this, "Đăng ký thành công! Hãy đăng nhập.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi kết nối CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnQuayLai_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
