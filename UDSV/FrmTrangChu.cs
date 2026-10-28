using System;
using System.Windows.Forms;
using UDSV.DTO;

namespace UDSV
{
    /// <summary>Trang chủ sau khi đăng nhập: điều hướng tới ba màn hình quản lý.</summary>
    public partial class FrmTrangChu : Form
    {
        private readonly TaiKhoanDTO taiKhoanHienTai;

        /// <summary>Constructor rỗng để Visual Studio Designer mở được form.</summary>
        public FrmTrangChu()
        {
            InitializeComponent();
            GanSuKien();
        }

        public FrmTrangChu(TaiKhoanDTO taiKhoan) : this()
        {
            taiKhoanHienTai = taiKhoan;
        }

        private void GanSuKien()
        {
            Load += FrmTrangChu_Load;
            btnSinhVien.Click += BtnSinhVien_Click;
            btnMonHoc.Click += BtnMonHoc_Click;
            btnLichHoc.Click += BtnLichHoc_Click;
            btnDangXuat.Click += BtnDangXuat_Click;
        }

        private void FrmTrangChu_Load(object sender, EventArgs e)
        {
            lblXinChao.Text = taiKhoanHienTai == null
                ? "Xin chào!"
                : $"Xin chào, {taiKhoanHienTai.HoTen}!";
        }

        private void BtnSinhVien_Click(object sender, EventArgs e)
        {
            using (FrmSinhVien frm = new FrmSinhVien())
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnMonHoc_Click(object sender, EventArgs e)
        {
            using (FrmMonHoc frm = new FrmMonHoc())
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnLichHoc_Click(object sender, EventArgs e)
        {
            using (FrmLichHoc frm = new FrmLichHoc())
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                this, "Đăng xuất và quay về màn hình đăng nhập?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
