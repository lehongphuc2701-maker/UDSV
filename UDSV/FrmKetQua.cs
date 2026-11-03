using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using UDSV.BLL;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV
{
    /// <summary>
    /// Nhập điểm theo lớp: chọn Khóa → Kỳ → Môn, lưới hiện mọi sinh viên của khóa với cột điểm.
    /// Sửa điểm ngay trên lưới rồi bấm "Lưu điểm". Ô trống = chưa có điểm.
    /// </summary>
    public partial class FrmKetQua : Form
    {
        private readonly KetQuaBLL kqBLL = new KetQuaBLL();
        private readonly KhoaBLL khoaBLL = new KhoaBLL();
        private readonly ChuongTrinhBLL ctBLL = new ChuongTrinhBLL();

        public FrmKetQua()
        {
            InitializeComponent();

            Load += FrmKetQua_Load;
            cboKhoa.SelectedIndexChanged += CboKhoa_Changed;
            cboKy.SelectedIndexChanged += CboKy_Changed;
            cboMonHoc.SelectedIndexChanged += CboMonHoc_Changed;
            btnLuu.Click += BtnLuu_Click;
        }

        private string MaKhoaChon => LayGiaTri(cboKhoa, "MaKhoa");
        private string MaMonChon => LayGiaTri(cboMonHoc, "MaMH");

        /// <summary>
        /// Học kỳ đang chọn. Phải phòng trường hợp ComboBox còn đang binding:
        /// lúc đó SelectedValue trả về DataRowView chứ không phải số → Convert.ToInt32 sẽ ném lỗi.
        /// </summary>
        private int KyChon
        {
            get
            {
                object giaTri = LayGiaTriRaw(cboKy, "HocKy");
                return giaTri != null && int.TryParse(giaTri.ToString(), out int ky) ? ky : 0;
            }
        }

        /// <summary>Đọc giá trị của ComboBox an toàn, kể cả khi đang binding dở.</summary>
        private static object LayGiaTriRaw(ComboBox cbo, string tenCot)
        {
            object giaTri = cbo.SelectedValue;

            if (giaTri is DataRowView dong)
                giaTri = dong.Row.Table.Columns.Contains(tenCot) ? dong[tenCot] : null;

            if (giaTri == null || giaTri == DBNull.Value) return null;
            return giaTri;
        }

        private static string LayGiaTri(ComboBox cbo, string tenCot)
            => LayGiaTriRaw(cbo, tenCot)?.ToString() ?? "";

        private void FrmKetQua_Load(object sender, EventArgs e)
        {
            try
            {
                cboKhoa.DataSource = khoaBLL.LayDanhSach();
                cboKhoa.DisplayMember = "MaKhoa";
                cboKhoa.ValueMember = "MaKhoa";

                XoaKyVaMon();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        // ---------- Chuỗi chọn: khóa → kỳ → môn ----------

        private void CboKhoa_Changed(object sender, EventArgs e)
        {
            try
            {
                XoaKyVaMon();

                if (MaKhoaChon.Length == 0) return;

                DataTable dsKy = ctBLL.LayHocKy(MaKhoaChon);
                if (dsKy.Rows.Count == 0)
                {
                    lblThongTin.Text = $"Khóa {MaKhoaChon} chưa có chương trình.";
                    lblThongTin.ForeColor = Color.OrangeRed;
                    return;
                }

                cboKy.SelectedIndexChanged -= CboKy_Changed;
                cboKy.DataSource = dsKy;
                cboKy.DisplayMember = "HocKy";
                cboKy.ValueMember = "HocKy";
                cboKy.SelectedIndexChanged += CboKy_Changed;
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void CboKy_Changed(object sender, EventArgs e)
        {
            try
            {
                cboMonHoc.DataSource = null;
                dgvBangDiem.DataSource = null;

                if (MaKhoaChon.Length == 0 || KyChon <= 0) return;

                DataTable dsMon = ctBLL.LayMonTheoKy(MaKhoaChon, KyChon);
                if (dsMon.Rows.Count == 0)
                {
                    lblThongTin.Text = $"Học kỳ {KyChon} của khóa {MaKhoaChon} không có môn nào.";
                    lblThongTin.ForeColor = Color.OrangeRed;
                    return;
                }

                cboMonHoc.SelectedIndexChanged -= CboMonHoc_Changed;
                cboMonHoc.DataSource = dsMon;
                cboMonHoc.DisplayMember = "TenMH";
                cboMonHoc.ValueMember = "MaMH";
                cboMonHoc.SelectedIndexChanged += CboMonHoc_Changed;
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void CboMonHoc_Changed(object sender, EventArgs e) => TaiBangDiem();

        // ---------- Lưới ----------

        private void TaiBangDiem()
        {
            if (MaKhoaChon.Length == 0 || MaMonChon.Length == 0) return;

            try
            {
                dgvBangDiem.DataSource = kqBLL.LayBangDiem(MaKhoaChon, MaMonChon);
                DatTieuDeCot();

                lblThongTin.Text = $"Khóa {MaKhoaChon} — Kỳ {KyChon} — {cboMonHoc.Text}: " +
                                   $"{dgvBangDiem.Rows.Count} sinh viên";
                lblThongTin.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void DatTieuDeCot()
        {
            if (dgvBangDiem.Columns.Count == 0) return;

            if (dgvBangDiem.Columns.Contains("MaSV"))
            {
                dgvBangDiem.Columns["MaSV"].HeaderText = "Mã sinh viên";
                dgvBangDiem.Columns["MaSV"].ReadOnly = true;
            }
            if (dgvBangDiem.Columns.Contains("HoTen"))
            {
                dgvBangDiem.Columns["HoTen"].HeaderText = "Tên sinh viên";
                dgvBangDiem.Columns["HoTen"].ReadOnly = true;
            }
            if (dgvBangDiem.Columns.Contains("Diem"))
            {
                dgvBangDiem.Columns["Diem"].HeaderText = "Điểm";
                dgvBangDiem.Columns["Diem"].DefaultCellStyle.Format = "0.00";
            }
        }

        // ---------- Lưu cả bảng ----------

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (MaMonChon.Length == 0)
            {
                MessageBox.Show(this, "Chọn khóa, kỳ và môn học trước khi lưu.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                List<DongDiem> ds = new List<DongDiem>();
                foreach (DataGridViewRow row in dgvBangDiem.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.DataBoundItem is not DataRowView item) continue;

                    string maSV = item["MaSV"].ToString() ?? "";
                    object giaTri = item["Diem"];

                    decimal? diem = null;
                    if (giaTri != DBNull.Value && giaTri != null
                        && decimal.TryParse(giaTri.ToString(), out decimal so))
                    {
                        diem = so;
                    }

                    ds.Add(new DongDiem(maSV, diem));
                }

                if (!kqBLL.LuuBangDiem(MaMonChon, ds, out string error))
                {
                    MessageBox.Show(this, error, "Không lưu được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, $"Đã lưu điểm cho {ds.Count} sinh viên.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiBangDiem();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        // ---------- Tiện ích ----------

        private void XoaKyVaMon()
        {
            cboKy.DataSource = null;
            cboMonHoc.DataSource = null;
            dgvBangDiem.DataSource = null;
            lblThongTin.Text = "Chọn khóa → kỳ → môn học để nhập điểm";
            lblThongTin.ForeColor = Color.DarkGreen;
        }

        private void BaoLoi(Exception ex)
        {
            AppLog.For<FrmKetQua>().LogError(ex, "Lỗi ở màn kết quả");
            MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
