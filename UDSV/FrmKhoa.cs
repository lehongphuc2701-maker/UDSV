using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using UDSV.BLL;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV
{
    /// <summary>
    /// Quản lý khóa và chương trình của khóa.
    /// Điểm nhấn của màn này: chương trình bị KHOÁ ngay khi khóa đã có sinh viên —
    /// nhãn trạng thái và hai nút thêm/xoá môn phản ánh đúng luật đó.
    /// </summary>
    public partial class FrmKhoa : Form
    {
        private readonly KhoaBLL khoaBLL = new KhoaBLL();
        private readonly ChuongTrinhBLL ctBLL = new ChuongTrinhBLL();
        private readonly MonHocBLL monHocBLL = new MonHocBLL();

        private string maDangChon = "";

        public FrmKhoa()
        {
            InitializeComponent();

            Load += FrmKhoa_Load;
            dgvKhoa.CellClick += DgvKhoa_CellClick;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            btnThemMon.Click += BtnThemMon_Click;
            btnXoaMon.Click += BtnXoaMon_Click;
        }

        private void FrmKhoa_Load(object sender, EventArgs e)
        {
            NapComboMon();
            LamMoi();
        }

        // ---------- Nạp dữ liệu ----------

        private void NapComboMon()
        {
            cboMonHoc.DataSource = monHocBLL.LayDanhSachMonHoc();
            cboMonHoc.DisplayMember = "TenMH";
            cboMonHoc.ValueMember = "MaMH";
        }

        private void TaiDuLieu()
        {
            try
            {
                dgvKhoa.DataSource = khoaBLL.LayDanhSach();
                DatTieuDeCotKhoa();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void DatTieuDeCotKhoa()
        {
            if (dgvKhoa.Columns.Count == 0) return;

            if (dgvKhoa.Columns.Contains("MaKhoa")) dgvKhoa.Columns["MaKhoa"].HeaderText = "Mã khóa";
            if (dgvKhoa.Columns.Contains("TenKhoa")) dgvKhoa.Columns["TenKhoa"].HeaderText = "Tên khóa";
            if (dgvKhoa.Columns.Contains("NamBatDau")) dgvKhoa.Columns["NamBatDau"].HeaderText = "Từ năm";
            if (dgvKhoa.Columns.Contains("NamKetThuc")) dgvKhoa.Columns["NamKetThuc"].HeaderText = "Đến năm";
            if (dgvKhoa.Columns.Contains("TongTCBatBuoc")) dgvKhoa.Columns["TongTCBatBuoc"].HeaderText = "TC bắt buộc";
            if (dgvKhoa.Columns.Contains("TongTCTuChon")) dgvKhoa.Columns["TongTCTuChon"].HeaderText = "TC tự chọn";
        }

        private void TaiChuongTrinh()
        {
            if (maDangChon.Length == 0)
            {
                dgvChuongTrinh.DataSource = null;
                lblTrangThai.Text = "—";
                lblTrangThai.ForeColor = Color.Gray;
                lblTongTC.Text = "Tổng TC: —";
                return;
            }

            try
            {
                dgvChuongTrinh.DataSource = ctBLL.LayTheoKhoa(maDangChon);
                if (dgvChuongTrinh.Columns.Count > 0)
                {
                    if (dgvChuongTrinh.Columns.Contains("MaKhoa")) dgvChuongTrinh.Columns["MaKhoa"].Visible = false;
                    if (dgvChuongTrinh.Columns.Contains("MaMH")) dgvChuongTrinh.Columns["MaMH"].HeaderText = "Mã MH";
                    if (dgvChuongTrinh.Columns.Contains("TenMH")) dgvChuongTrinh.Columns["TenMH"].HeaderText = "Tên môn học";
                    if (dgvChuongTrinh.Columns.Contains("SoTinChi")) dgvChuongTrinh.Columns["SoTinChi"].HeaderText = "TC";
                    if (dgvChuongTrinh.Columns.Contains("BatBuoc")) dgvChuongTrinh.Columns["BatBuoc"].HeaderText = "Bắt buộc";
                }

                CapNhatTrangThai();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        /// <summary>Hiện luật khoá ngay trên màn hình — đây là bằng chứng cho báo cáo.</summary>
        private void CapNhatTrangThai()
        {
            int soSv = khoaBLL.DemSinhVien(maDangChon);
            if (soSv > 0)
            {
                lblTrangThai.Text = $"ĐÃ KHOÁ — khóa đang có {soSv} sinh viên";
                lblTrangThai.ForeColor = Color.Red;
                btnThemMon.Enabled = false;
                btnXoaMon.Enabled = false;
            }
            else
            {
                lblTrangThai.Text = "Còn sửa được — khóa chưa có sinh viên";
                lblTrangThai.ForeColor = Color.Green;
                btnThemMon.Enabled = true;
                btnXoaMon.Enabled = true;
            }

            KhoaDTO? khoa = khoaBLL.LayTheoMa(maDangChon);
            int batBuoc = ctBLL.TongTinChi(maDangChon, true);
            int tuChon = ctBLL.TongTinChi(maDangChon, false);

            string khaiBao = khoa == null ? "—" : $"{khoa.TongTCBatBuoc} + {khoa.TongTCTuChon}";
            lblTongTC.Text = $"Tổng TC: {batBuoc} bắt buộc + {tuChon} tự chọn   (khai báo: {khaiBao})";
            lblTongTC.ForeColor = khoa != null && (khoa.TongTCBatBuoc != batBuoc)
                ? Color.OrangeRed
                : SystemColors.ControlText;
        }

        // ---------- Sự kiện ----------

        private void DgvKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvKhoa.Rows[e.RowIndex].DataBoundItem is not DataRowView item) return;

            maDangChon = item["MaKhoa"].ToString() ?? "";
            txtMaKhoa.Text = maDangChon;
            txtTenKhoa.Text = item["TenKhoa"].ToString() ?? "";
            numNamBatDau.Value = Convert.ToDecimal(item["NamBatDau"]);
            numNamKetThuc.Value = Convert.ToDecimal(item["NamKetThuc"]);
            numTCBatBuoc.Value = Convert.ToDecimal(item["TongTCBatBuoc"]);
            numTCTuChon.Value = Convert.ToDecimal(item["TongTCTuChon"]);
            txtMaKhoa.ReadOnly = true;

            TaiChuongTrinh();
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            KhoaDTO khoa = LayDuLieuTuForm();
            try
            {
                if (!khoaBLL.Them(khoa, out string error))
                {
                    MessageBox.Show(this, error, "Không thêm được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show(this, "Đã thêm khóa.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (maDangChon.Length == 0)
            {
                MessageBox.Show(this, "Hãy chọn một khóa trong bảng.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (!khoaBLL.Sua(LayDuLieuTuForm(), out string error))
                {
                    MessageBox.Show(this, error, "Không sửa được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show(this, "Đã cập nhật khóa.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDuLieu();
                TaiChuongTrinh();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (maDangChon.Length == 0)
            {
                MessageBox.Show(this, "Hãy chọn một khóa trong bảng.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(this, $"Xoá khóa '{maDangChon}'?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                if (!khoaBLL.Xoa(maDangChon, out string error))
                {
                    MessageBox.Show(this, error, "Không xoá được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show(this, "Đã xoá khóa.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void BtnThemMon_Click(object sender, EventArgs e)
        {
            if (maDangChon.Length == 0)
            {
                MessageBox.Show(this, "Hãy chọn một khóa.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maMH = cboMonHoc.SelectedValue?.ToString() ?? "";
            try
            {
                if (!ctBLL.ThemMonVaoChuongTrinh(new ChuongTrinhDTO(maDangChon, maMH, true), out string error))
                {
                    MessageBox.Show(this, error, "Không thêm được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TaiChuongTrinh();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void BtnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvChuongTrinh.CurrentRow?.DataBoundItem is not DataRowView item)
            {
                MessageBox.Show(this, "Hãy chọn một môn trong chương trình.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maMH = item["MaMH"].ToString() ?? "";
            try
            {
                if (!ctBLL.XoaMonKhoiChuongTrinh(maDangChon, maMH, out string error))
                {
                    MessageBox.Show(this, error, "Không xoá được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TaiChuongTrinh();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e) => LamMoi();

        // ---------- Tiện ích ----------

        private KhoaDTO LayDuLieuTuForm() => new KhoaDTO(
            txtMaKhoa.Text.Trim(),
            txtTenKhoa.Text.Trim(),
            (int)numNamBatDau.Value,
            (int)numNamKetThuc.Value,
            (int)numTCBatBuoc.Value,
            (int)numTCTuChon.Value);

        private void LamMoi()
        {
            maDangChon = "";
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            numNamBatDau.Value = 2024;
            numNamKetThuc.Value = 2028;
            numTCBatBuoc.Value = 27;
            numTCTuChon.Value = 6;
            txtMaKhoa.ReadOnly = false;
            dgvKhoa.ClearSelection();
            TaiDuLieu();
            TaiChuongTrinh();
            txtMaKhoa.Focus();
        }

        private void BaoLoi(Exception ex)
        {
            AppLog.For<FrmKhoa>().LogError(ex, "Lỗi ở màn khóa");
            MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
