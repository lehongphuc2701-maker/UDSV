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
            btnLamMoi.Click += (s, e) => TaiBangDiem();

            // Chặn lỗi định dạng ngay khi rời ô, và bắt luôn mọi lỗi còn sót của lưới
            // để không hiện hộp thoại đỏ mặc định của WinForms.
            dgvBangDiem.CellValidating += DgvBangDiem_CellValidating;
            dgvBangDiem.DataError += DgvBangDiem_DataError;

            // Bật DoubleBuffered cuộn mượt mà
            typeof(Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(dgvBangDiem, true);
        }

        /// <summary>
        /// Kiểm tra ô Điểm ngay khi người dùng rời ô: phải là số từ 0 đến 4, hoặc để trống.
        /// Chặn tại đây thì lưới không bao giờ nhận giá trị sai, nên không sinh lỗi định dạng.
        /// </summary>
        private void DgvBangDiem_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvBangDiem.Columns[e.ColumnIndex].Name != "Diem") return;

            string nhap = Convert.ToString(e.FormattedValue)?.Trim() ?? "";
            if (nhap.Length == 0) return;              // để trống = chưa có điểm

            if (!ThuDocSo(nhap, out decimal diem))
            {
                BaoNhapSai(e, $"Bạn đã nhập sai định dạng cho điểm: \"{nhap}\".\n\n" +
                             "Điểm phải là số từ 0 đến 4 (ví dụ 3.2). Để trống nếu chưa có điểm.");
                return;
            }

            if (diem < 0m || diem > 4m)
            {
                BaoNhapSai(e, $"Điểm {diem:0.0#} nằm ngoài khoảng cho phép.\n\n" +
                             "Điểm chỉ nhận giá trị từ 0 đến 4.");
            }
        }

        /// <summary>Báo lỗi tại chỗ: giữ nguyên ô để sửa, hiện thông báo, không làm hỏng dữ liệu.</summary>
        private void BaoNhapSai(DataGridViewCellValidatingEventArgs e, string thongBao)
        {
            e.Cancel = true;
            dgvBangDiem.Rows[e.RowIndex].ErrorText = "Điểm không hợp lệ";
            ThongBao.Show(this, thongBao, "Điểm không hợp lệ",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>Đọc số theo cả kiểu Việt Nam (dấu phẩy) và kiểu Anh (dấu chấm).</summary>
        private static bool ThuDocSo(string chuoi, out decimal so)
        {
            if (decimal.TryParse(chuoi, System.Globalization.NumberStyles.Number,
                                 System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), out so))
                return true;

            return decimal.TryParse(chuoi, System.Globalization.NumberStyles.Number,
                                    System.Globalization.CultureInfo.InvariantCulture, out so);
        }

        /// <summary>
        /// Lưới báo lỗi: nuốt lỗi mặc định của WinForms và hiện thông báo tiếng Việt thay thế.
        /// </summary>
        private void DgvBangDiem_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;

            if (e.RowIndex >= 0) dgvBangDiem.Rows[e.RowIndex].ErrorText = "";

            ThongBao.Show(this,
                "Bạn đã nhập sai định dạng cho điểm.\n\n" +
                "Điểm phải là số từ 0 đến 4 (ví dụ 3.2). Để trống nếu chưa có điểm.",
                "Điểm không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                // Gán DataSource xong thì SelectedValue mới có giá trị. Phải tự nạp chuỗi
                // khóa → kỳ → môn, vì sự kiện SelectedIndexChanged không bắn trong lúc bind.
                CboKhoa_Changed(cboKhoa, EventArgs.Empty);
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

                // Handler bị gỡ trong lúc bind nên danh sách môn không tự nạp — gọi thẳng.
                CboKy_Changed(cboKy, EventArgs.Empty);
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
                cboMonHoc.DisplayMember = ThemCotHienThiMon(dsMon);
                cboMonHoc.ValueMember = "MaMH";
                cboMonHoc.SelectedIndexChanged += CboMonHoc_Changed;
                TaiBangDiem();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        /// <summary>
        /// Gắn thêm cột hiển thị cho ComboBox môn: môn không nằm trong chương trình của kỳ này
        /// (chỉ còn vì sinh viên đã có điểm) được ghi chú " (ngoài chương trình)".
        /// Không bỏ môn nào khỏi danh sách — điểm đã nhập phải luôn xem và sửa được.
        /// Trả về tên cột để dùng làm DisplayMember.
        /// </summary>
        private static string ThemCotHienThiMon(DataTable dsMon)
        {
            const string cotHienThi = "TenHienThi";
            bool coCotNgoai = dsMon.Columns.Contains("NgoaiChuongTrinh");

            if (!dsMon.Columns.Contains(cotHienThi))
                dsMon.Columns.Add(cotHienThi, typeof(string));

            foreach (DataRow dong in dsMon.Rows)
            {
                string ten = dong["TenMH"]?.ToString() ?? "";
                bool ngoai = coCotNgoai
                             && dong["NgoaiChuongTrinh"] != DBNull.Value
                             && Convert.ToBoolean(dong["NgoaiChuongTrinh"]);

                dong[cotHienThi] = ngoai ? ten + " (ngoài chương trình)" : ten;
            }

            return cotHienThi;
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

                lblThongTin.Text = $"📘 Khóa {MaKhoaChon}  •  Kỳ {KyChon}  •  {cboMonHoc.Text}: {dgvBangDiem.Rows.Count} sinh viên";
                lblThongTin.ForeColor = Color.FromArgb(5, 150, 105);
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void DatTieuDeCot()
        {
            if (dgvBangDiem.Columns.Count == 0) return;

            if (dgvBangDiem.Columns.Contains("MaSV"))
            {
                dgvBangDiem.Columns["MaSV"].HeaderText = "Mã sinh viên";
                dgvBangDiem.Columns["MaSV"].Width = 150;
                dgvBangDiem.Columns["MaSV"].ReadOnly = true;
                dgvBangDiem.Columns["MaSV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBangDiem.Columns["MaSV"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                dgvBangDiem.Columns["MaSV"].DefaultCellStyle.ForeColor = Color.FromArgb(79, 70, 229);
            }
            if (dgvBangDiem.Columns.Contains("HoTen"))
            {
                dgvBangDiem.Columns["HoTen"].HeaderText = "Tên sinh viên";
                dgvBangDiem.Columns["HoTen"].MinimumWidth = 220;
                dgvBangDiem.Columns["HoTen"].ReadOnly = true;
                dgvBangDiem.Columns["HoTen"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }
            if (dgvBangDiem.Columns.Contains("Diem"))
            {
                dgvBangDiem.Columns["Diem"].HeaderText = "Điểm";
                dgvBangDiem.Columns["Diem"].Width = 130;
                dgvBangDiem.Columns["Diem"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBangDiem.Columns["Diem"].DefaultCellStyle.Format = "0.00";
                dgvBangDiem.Columns["Diem"].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvBangDiem.Columns["Diem"].DefaultCellStyle.ForeColor = Color.FromArgb(225, 29, 72);
            }
        }

        // ---------- Lưu cả bảng ----------

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (MaMonChon.Length == 0)
            {
                ThongBao.Show(this, "Chọn khóa, kỳ và môn học trước khi lưu.", "Chưa chọn",
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
                    ThongBao.Show(this, error, "Không lưu được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ThongBao.Show(this, $"Đã lưu điểm cho {ds.Count} sinh viên.", "Thành công",
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
            ThongBao.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
