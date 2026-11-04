using System;
using System.Windows.Forms;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV
{
    /// <summary>Trang chủ sau khi đăng nhập: điều hướng tới các màn hình quản lý.</summary>
    public partial class FrmTrangChu : Form
    {
        private readonly TaiKhoanDTO? taiKhoanHienTai;

        /// <summary>Constructor rỗng để Visual Studio Designer mở được form.</summary>
        public FrmTrangChu()
        {
            InitializeComponent();
            GanSuKien();
        }

        public FrmTrangChu(TaiKhoanDTO? taiKhoan) : this()
        {
            taiKhoanHienTai = taiKhoan;
            if (taiKhoan?.LaSinhVien == true) ApCheDoSinhVien();
        }

        /// <summary>Thông tin của sinh viên đang đăng nhập (nạp một lần).</summary>
        private System.Data.DataRow? _sv;

        private void ApCheDoSinhVien()
        {
            // 4 khối dành cho sinh viên; bỏ Môn học và Kết quả.
            btnMonHoc.Visible = false;
            btnKetQua.Visible = false;

            btnSinhVien.Title = "1. Thông tin sinh viên";
            btnSinhVien.Description = "Hồ sơ cá nhân: mã SV, họ tên, ngày sinh, giới tính, khóa, SĐT.";
            btnSinhVien.IconText = "👨‍🎓";

            btnLichHoc.Title = "2. Lịch học";
            btnLichHoc.Description = "Thời khóa biểu tuần của lớp bạn theo từng học kỳ.";
            btnLichHoc.IconText = "🗓️";

            btnKhoa.Title = "3. CT Đào tạo Khóa";
            btnKhoa.Description = "Chương trình khung của khóa bạn, kèm điểm từng môn.";
            btnKhoa.IconText = "🏛️";

            btnTaiKhoan.Title = "4. Tài khoản / Đổi MK";
            btnTaiKhoan.Description = "Xem thông tin đăng nhập và đổi mật khẩu.";
            btnTaiKhoan.IconText = "🛡️";

            // xếp lại 2 × 2 cho khít chỗ
            btnSinhVien.Location = new Point(0, 64);
            btnLichHoc.Location = new Point(376, 64);
            btnKhoa.Location = new Point(0, 174);
            btnTaiKhoan.Location = new Point(376, 174);

            lblSectionTitle.Text = "📌 DANH MỤC PHÂN HỆ SINH VIÊN";
            lblSectionDesc.Text = "Xem hồ sơ, lịch học, chương trình đào tạo và đổi mật khẩu";
            lblRightSub.Text = "Năm học 2025 - 2026 • Phân hệ Sinh viên";
            lblVaiTro.Text = "Vai trò: Sinh viên";
        }

        /// <summary>Nạp hồ sơ sinh viên đang đăng nhập (mã khóa + tên khóa) để mở các màn khác.</summary>
        private void NapHoSoSinhVien()
        {
            if (_sv != null) return;
            string? maSV = taiKhoanHienTai?.MaSV;
            if (string.IsNullOrWhiteSpace(maSV)) return;

            var ds = new SinhVienBLL().LayTheoMa(maSV);
            if (ds.Rows.Count > 0) _sv = ds.Rows[0];
        }

        private void GanSuKien()
        {
            Load += FrmTrangChu_Load;
            btnSinhVien.Click += BtnSinhVien_Click;
            btnMonHoc.Click += BtnMonHoc_Click;
            btnLichHoc.Click += BtnLichHoc_Click;
            btnKhoa.Click += BtnKhoa_Click;
            btnKetQua.Click += BtnKetQua_Click;
            btnTaiKhoan.Click += BtnTaiKhoan_Click;
            btnDoiMKHeader.Click += BtnTaiKhoan_Click;
            btnDangXuat.Click += BtnDangXuat_Click;
        }

        private void FrmTrangChu_Load(object? sender, EventArgs e)
        {
            lblXinChao.Text = taiKhoanHienTai == null
                ? "Xin chào, Quý khách!"
                : $"Xin chào, {taiKhoanHienTai.HoTen}!";

            lblVaiTro.Text = taiKhoanHienTai == null
                ? "Khách vãng lai"
                : $"Vai trò: {taiKhoanHienTai.VaiTro}";

            try
            {
                var ci = new System.Globalization.CultureInfo("vi-VN");
                lblNgayThang.Text = "📅 " + char.ToUpper(DateTime.Now.ToString("dddd", ci)[0])
                    + DateTime.Now.ToString("dddd", ci).Substring(1)
                    + DateTime.Now.ToString(", 'ngày' dd/MM/yyyy", ci);
            }
            catch
            {
                lblNgayThang.Text = "📅 " + DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        private void BtnSinhVien_Click(object? sender, EventArgs e)
        {
            if (taiKhoanHienTai?.LaSinhVien == true)
            {
                using (var frm = new FrmThongTinSV(taiKhoanHienTai.MaSV ?? ""))
                    frm.ShowDialog(this);
                return;
            }

            using (FrmSinhVien frm = new FrmSinhVien())
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnMonHoc_Click(object? sender, EventArgs e)
        {
            using (FrmMonHoc frm = new FrmMonHoc())
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnLichHoc_Click(object? sender, EventArgs e)
        {
            string? khoa = null;
            if (taiKhoanHienTai?.LaSinhVien == true)
            {
                NapHoSoSinhVien();
                khoa = _sv?["MaKhoa"]?.ToString();
            }

            using (FrmLichHoc frm = new FrmLichHoc(khoa))
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnKhoa_Click(object? sender, EventArgs e)
        {
            if (taiKhoanHienTai?.LaSinhVien == true)
            {
                NapHoSoSinhVien();
                using (var frm = new FrmChuongTrinhSV(
                           taiKhoanHienTai.MaSV ?? "",
                           _sv?["MaKhoa"]?.ToString() ?? "",
                           _sv != null && _sv.Table.Columns.Contains("TenKhoa") ? _sv["TenKhoa"]?.ToString() ?? "" : ""))
                    frm.ShowDialog(this);
                return;
            }

            using (FrmKhoa frm = new FrmKhoa(taiKhoanHienTai?.Username))
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnKetQua_Click(object? sender, EventArgs e)
        {
            using (FrmKetQua frm = new FrmKetQua())
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnTaiKhoan_Click(object? sender, EventArgs e)
        {
            using (FrmThongTinTaiKhoan frm = new FrmThongTinTaiKhoan(taiKhoanHienTai))
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnDangXuat_Click(object? sender, EventArgs e)
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
