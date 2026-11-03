using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using UDSV.BLL;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV
{
    /// <summary>
    /// Nhập điểm cho từng sinh viên ở từng môn, và hiện tín chỉ tích luỹ ngay trên đầu màn hình.
    /// Một dòng kết quả = một cặp (sinh viên, môn); lưu lần hai là SỬA, không tạo dòng mới.
    /// </summary>
    public partial class FrmKetQua : Form
    {
        private readonly KetQuaBLL kqBLL = new KetQuaBLL();
        private readonly SinhVienBLL svBLL = new SinhVienBLL();
        private readonly MonHocBLL mhBLL = new MonHocBLL();
        private readonly KhoaBLL khoaBLL = new KhoaBLL();

        public FrmKetQua()
        {
            InitializeComponent();

            Load += FrmKetQua_Load;
            cboSinhVien.SelectedIndexChanged += (s, e) => TaiKetQua();
            chkChuaCoDiem.CheckedChanged += (s, e) => numDiem.Enabled = !chkChuaCoDiem.Checked;
            btnLuu.Click += BtnLuu_Click;
            btnXoa.Click += BtnXoa_Click;
        }

        private void FrmKetQua_Load(object sender, EventArgs e)
        {
            NapCombo();
            TaiKetQua();
        }

        private void NapCombo()
        {
            cboSinhVien.DataSource = svBLL.LayDanhSachSinhVien();
            cboSinhVien.DisplayMember = "MaSV";
            cboSinhVien.ValueMember = "MaSV";

            cboMonHoc.DataSource = mhBLL.LayDanhSachMonHoc();
            cboMonHoc.DisplayMember = "TenMH";
            cboMonHoc.ValueMember = "MaMH";
        }

        private string MaSVDangChon => cboSinhVien.SelectedValue?.ToString() ?? "";
        private string MaMHDangChon => cboMonHoc.SelectedValue?.ToString() ?? "";

        private void TaiKetQua()
        {
            string maSV = MaSVDangChon;
            if (maSV.Length == 0) return;

            try
            {
                dgvKetQua.DataSource = kqBLL.LayTheoSinhVien(maSV);
                DatTieuDeCot();
                CapNhatTinChi(maSV);
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void DatTieuDeCot()
        {
            if (dgvKetQua.Columns.Count == 0) return;

            if (dgvKetQua.Columns.Contains("MaMH")) dgvKetQua.Columns["MaMH"].HeaderText = "Mã MH";
            if (dgvKetQua.Columns.Contains("TenMH")) dgvKetQua.Columns["TenMH"].HeaderText = "Tên môn học";
            if (dgvKetQua.Columns.Contains("SoTinChi")) dgvKetQua.Columns["SoTinChi"].HeaderText = "TC";
            if (dgvKetQua.Columns.Contains("Diem"))
            {
                dgvKetQua.Columns["Diem"].HeaderText = "Điểm (hệ 4)";
                dgvKetQua.Columns["Diem"].DefaultCellStyle.Format = "0.00";
            }
            if (dgvKetQua.Columns.Contains("LanNhap"))
            {
                dgvKetQua.Columns["LanNhap"].HeaderText = "Lần nhập";
                dgvKetQua.Columns["LanNhap"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }

        /// <summary>Câu trả lời cho "còn thiếu bao nhiêu tín chỉ" — tính lại từ kết quả mỗi lần xem.</summary>
        private void CapNhatTinChi(string maSV)
        {
            int daCo = kqBLL.TinhTinChiTichLuy(maSV);

            string maKhoa = "";
            DataTable dsSv = svBLL.LayDanhSachSinhVien();
            DataRow[] tim = dsSv.Select($"MaSV = '{maSV}'");
            if (tim.Length > 0) maKhoa = tim[0]["MaKhoa"].ToString() ?? "";

            int mucTieu = khoaBLL.LayTheoMa(maKhoa)?.TongTCBatBuoc ?? 0;
            lblTongTC.Text = $"Tín chỉ tích luỹ: {daCo} / {mucTieu}   (khóa {maKhoa})";
        }

        // ---------- Sự kiện ----------

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            decimal? diem = chkChuaCoDiem.Checked ? (decimal?)null : numDiem.Value;

            try
            {
                if (!kqBLL.NhapDiem(MaSVDangChon, MaMHDangChon, diem, out string error))
                {
                    MessageBox.Show(this, error, "Không lưu được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TaiKetQua();
                MessageBox.Show(this, "Đã lưu điểm.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKetQua.CurrentRow?.DataBoundItem is not DataRowView item)
            {
                MessageBox.Show(this, "Hãy chọn một dòng trong bảng điểm.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maMH = item["MaMH"].ToString() ?? "";
            if (MessageBox.Show(this, $"Xoá kết quả môn '{maMH}' của sinh viên '{MaSVDangChon}'?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                if (!kqBLL.XoaKetQua(MaSVDangChon, maMH, out string error))
                {
                    MessageBox.Show(this, error, "Không xoá được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                TaiKetQua();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void BaoLoi(Exception ex)
        {
            AppLog.For<FrmKetQua>().LogError(ex, "Lỗi ở màn kết quả");
            MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
