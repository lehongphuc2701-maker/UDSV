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

        /// <summary>Toàn bộ chương trình của khóa đang chọn (mọi học kỳ); lưới chỉ hiện phần đã lọc.</summary>
        private DataTable? _ctFull;

        // Màu gốc của các nút, để trả lại khi mở khoá
        private Color mauThemMon, mauXoaMon, mauXoa, mauSua;

        /// <summary>Nút "Khoá số tín chỉ" — tạo bằng code để khỏi sửa Designer.</summary>
        private Button btnKhoaTC;

        /// <summary>Tài khoản đang đăng nhập — ghi vào Khoa.NguoiKhoa khi bấm khoá.</summary>
        private readonly string nguoiDung;

        public FrmKhoa() : this(null) { }

        public FrmKhoa(string tenDangNhap)
        {
            nguoiDung = string.IsNullOrWhiteSpace(tenDangNhap) ? "admin" : tenDangNhap;
            InitializeComponent();

            Load += FrmKhoa_Load;
            dgvKhoa.CellClick += DgvKhoa_CellClick;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            btnThemMon.Click += BtnThemMon_Click;
            btnXoaMon.Click += BtnXoaMon_Click;

            // Lưới danh sách khóa: chỉ để chọn.
            dgvKhoa.ReadOnly = true;
            dgvKhoa.AllowUserToAddRows = false;
            dgvKhoa.AllowUserToDeleteRows = false;

            // Lưới chương trình: gõ được tự do như bảng tính.
            dgvChuongTrinh.ReadOnly = false;
            dgvChuongTrinh.AllowUserToAddRows = false;
            dgvChuongTrinh.AllowUserToDeleteRows = false;

            mauThemMon = btnThemMon.BackColor;
            mauXoaMon = btnXoaMon.BackColor;
            mauXoa = btnXoa.BackColor;
            mauSua = btnSua.BackColor;

            // Nhãn mới: nút này LƯU cấu hình khóa đang chọn (không tạo khóa mới).
            btnSua.Text = "Lưu thay đổi";
            btnThem.Text = "Thêm khóa";

            // Mã khóa là khoá chính: không bao giờ gõ tay.
            txtMaKhoa.ReadOnly = true;

            Text = "Quản lý chương trình khóa";

            // Nút chốt chương trình — đặt cùng hàng với các nút khác, phía bên phải
            btnKhoaTC = new Button
            {
                Text = "KHOÁ SỐ TÍN CHỈ",
                Location = new Point(760, 596),
                Size = new Size(330, 30),
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };
            Controls.Add(btnKhoaTC);
            btnKhoaTC.BringToFront();
            btnKhoaTC.Click += BtnKhoaTC_Click;
        }

        /// <summary>Bật/tắt cả khối ô nhập "Thông tin khóa" (mã, tên, năm, tín chỉ).</summary>
        private void BatTatNhap(bool bat)
        {
            txtMaKhoa.Enabled = bat;
            txtTenKhoa.Enabled = bat;
            numNamBatDau.Enabled = bat;
            numNamKetThuc.Enabled = bat;
            numTCBatBuoc.Enabled = bat;
            numTCTuChon.Enabled = bat;
        }

        /// <summary>Bật/tắt một nút và làm nó xám hẳn đi khi tắt (Enabled=false chỉ xám chữ).</summary>
        private static void BatTat(Button nut, bool bat, Color mauGoc)
        {
            nut.Enabled = bat;
            nut.BackColor = bat ? mauGoc : SystemColors.Control;
            nut.ForeColor = bat ? SystemColors.ControlText : SystemColors.GrayText;
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
                _ctFull = null;
                dgvChuongTrinh.DataSource = null;
                cboHocKy.DataSource = null;
                lblTrangThai.Text = "—";
                lblTrangThai.ForeColor = Color.Gray;
                lblTongTC.Text = "Tổng TC: —";
                return;
            }

            try
            {
                // Giữ TOÀN BỘ chương trình trong bộ nhớ; lưới chỉ hiện phần đã lọc theo học kỳ.
                // Nhờ vậy sửa ở một học kỳ không làm mất dòng của các học kỳ khác khi lưu.
                _ctFull = ctBLL.LayTheoKhoa(maDangChon);

                dgvChuongTrinh.DataSource = _ctFull.DefaultView;
                if (dgvChuongTrinh.Columns.Count > 0)
                {
                    if (dgvChuongTrinh.Columns.Contains("MaKhoa")) dgvChuongTrinh.Columns["MaKhoa"].Visible = false;
                    if (dgvChuongTrinh.Columns.Contains("MaMH")) dgvChuongTrinh.Columns["MaMH"].HeaderText = "Mã MH";
                    if (dgvChuongTrinh.Columns.Contains("TenMH")) dgvChuongTrinh.Columns["TenMH"].HeaderText = "Tên môn học";
                    if (dgvChuongTrinh.Columns.Contains("SoTinChi")) dgvChuongTrinh.Columns["SoTinChi"].HeaderText = "TC";
                    if (dgvChuongTrinh.Columns.Contains("BatBuoc")) dgvChuongTrinh.Columns["BatBuoc"].HeaderText = "Bắt buộc";
                    if (dgvChuongTrinh.Columns.Contains("HocKy")) dgvChuongTrinh.Columns["HocKy"].Visible = false;
                    if (dgvChuongTrinh.Columns.Contains("Thu")) dgvChuongTrinh.Columns["Thu"].HeaderText = "Thứ (2..8)";
                    if (dgvChuongTrinh.Columns.Contains("TietBD")) dgvChuongTrinh.Columns["TietBD"].HeaderText = "Tiết BĐ";
                    if (dgvChuongTrinh.Columns.Contains("TietKT")) dgvChuongTrinh.Columns["TietKT"].HeaderText = "Tiết KT";
                    if (dgvChuongTrinh.Columns.Contains("GiangVien")) dgvChuongTrinh.Columns["GiangVien"].HeaderText = "Giảng viên";
                }

                NapHocKy();
                ApDungLocHocKy();
                CapNhatTrangThai();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        /// <summary>
        /// Luôn cho chọn HK 1..10, kể cả khi khóa chưa có môn nào — để admin có thể đặt môn đầu tiên
        /// vào bất kỳ học kỳ nào (ví dụ K26: thêm môn vào HK7 trước, rồi nhảy về HK5 thêm tiếp).
        /// </summary>
        private void NapHocKy()
        {
            cboHocKy.SelectedIndexChanged -= CboHocKy_Changed;

            cboHocKy.DataSource = null;
            cboHocKy.Items.Clear();
            cboHocKy.DisplayMember = "HocKy";
            cboHocKy.ValueMember = "HocKy";

            DataTable dsKy = new DataTable();
            dsKy.Columns.Add("HocKy", typeof(int));
            for (int ky = 1; ky <= 10; ky++) dsKy.Rows.Add(ky);

            cboHocKy.DataSource = dsKy;
            cboHocKy.Enabled = maDangChon.Length > 0;

            // Chọn sẵn học kỳ nhỏ nhất đang có môn; chưa có môn nào thì HK1.
            int macDinh = 1;
            if (_ctFull != null)
            {
                int nhoNhat = int.MaxValue;
                foreach (DataRow r in _ctFull.Rows)
                {
                    int ky = _ctFull.Columns.Contains("HocKy") ? Convert.ToInt32(r["HocKy"]) : 1;
                    if (ky < nhoNhat) nhoNhat = ky;
                }
                if (nhoNhat != int.MaxValue) macDinh = nhoNhat;
            }
            cboHocKy.SelectedValue = macDinh;

            cboHocKy.SelectedIndexChanged += CboHocKy_Changed;
        }

        private void CboHocKy_Changed(object sender, EventArgs e) => ApDungLocHocKy();

        /// <summary>Học kỳ đang chọn trong combo (0 = chưa chọn).</summary>
        private int HocKyChon
        {
            get
            {
                object v = cboHocKy.SelectedValue;
                if (v is DataRowView dong) v = dong["HocKy"];
                return v != null && int.TryParse(v.ToString(), out int ky) ? ky : 0;
            }
        }

        private void ApDungLocHocKy()
        {
            if (_ctFull == null) return;

            int ky = HocKyChon;
            _ctFull.DefaultView.RowFilter = ky > 0 ? $"HocKy = {ky}" : "";
        }

        /// <summary>Hiện luật khoá ngay trên màn hình — đây là bằng chứng cho báo cáo.</summary>
        private void CapNhatTrangThai()
        {
            KhoaDTO? khoa = khoaBLL.LayTheoMa(maDangChon);
            bool daKhoa = khoa != null && khoa.DaKhoa;

            if (daKhoa)
            {
                lblTrangThai.Text = $"ĐÃ KHOÁ — chốt ngày {khoa!.NgayKhoa:dd/MM/yyyy} bởi {khoa.NguoiKhoa}";
                lblTrangThai.ForeColor = Color.Red;

                // Đã chốt: khoá cứng sáu ô nhập (kể cả Mã khóa). Lưới vẫn gõ được;
                // nếu tổng trên lưới lệch số đã chốt thì nút Sửa sẽ từ chối bằng hộp thoại.
                BatTatNhap(false);
                if (btnKhoaTC != null)
                {
                    btnKhoaTC.Enabled = false;
                    btnKhoaTC.BackColor = SystemColors.Control;
                    btnKhoaTC.ForeColor = SystemColors.GrayText;
                }
            }
            else
            {
                lblTrangThai.Text = maDangChon.Length == 0
                    ? "—"
                    : "Còn sửa được — chương trình chưa bị khoá";
                lblTrangThai.ForeColor = Color.Green;
                BatTatNhap(true);
                if (btnKhoaTC != null)
                {
                    btnKhoaTC.Enabled = true;
                    btnKhoaTC.BackColor = Color.OrangeRed;
                    btnKhoaTC.ForeColor = Color.White;
                }
            }

            BatTat(btnThemMon, true, mauThemMon);
            BatTat(btnXoaMon, true, mauXoaMon);
            BatTat(btnXoa, true, mauXoa);
            BatTat(btnSua, true, mauSua);

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
            try
            {
                // Mã khóa tự sinh (K26, K27…) — người dùng không gõ tay vì đây là khoá chính.
                if (!khoaBLL.TaoKhoaKeTiep(out string maMoi, out string error))
                {
                    MessageBox.Show(this, error, "Không thêm được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TaiDuLieu();
                ChonKhoa(maMoi);

                MessageBox.Show(this,
                    $"Đã thêm khóa {maMoi} (chương trình đang trống).\n" +
                    "Chọn khóa vừa tạo rồi thêm môn vào lưới bên phải.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        /// <summary>Chọn một dòng trong lưới khóa theo mã.</summary>
        private void ChonKhoa(string maKhoa)
        {
            foreach (DataGridViewRow row in dgvKhoa.Rows)
            {
                if (row.DataBoundItem is DataRowView item && item["MaKhoa"].ToString() == maKhoa)
                {
                    dgvKhoa.CurrentCell = row.Cells[0];
                    DgvKhoa_CellClick(this, new DataGridViewCellEventArgs(0, row.Index));
                    return;
                }
            }
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
                // Trước khi lưu: nếu khóa đã chốt, tổng tín chỉ TRÊN LƯỚI phải khớp số đã chốt.
                // (Lưới sửa tại chỗ nên đây là nơi duy nhất nhìn thấy thay đổi người dùng vừa gõ.)
                KhoaDTO? hienTai = khoaBLL.LayTheoMa(maDangChon);
                if (hienTai != null && hienTai.DaKhoa)
                {
                    int bbLuoi = TinhTongTuLuoi(true);
                    int tcLuoi = TinhTongTuLuoi(false);

                    if (bbLuoi != hienTai.TongTCBatBuoc || tcLuoi != hienTai.TongTCTuChon)
                    {
                        MessageBox.Show(this,
                            $"Không thể sửa vì số tín chỉ sau khi chỉnh sửa khác với số tín chỉ đã khoá.\n\n" +
                            $"Trên lưới hiện tại : {bbLuoi} bắt buộc + {tcLuoi} tự chọn\n" +
                            $"Đã khoá            : {hienTai.TongTCBatBuoc} bắt buộc + {hienTai.TongTCTuChon} tự chọn\n\n" +
                            "Muốn đổi số tín chỉ thì phải tạo khóa mới.",
                            "Không sửa được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (!khoaBLL.Sua(LayDuLieuTuForm(), out string error))
                {
                    MessageBox.Show(this, error, "Không sửa được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Lưu luôn chương trình: các ô sửa trực tiếp trên lưới chỉ có hiệu lực từ đây.
                bool luuCT = ctBLL.LuuChuongTrinh(maDangChon, DocLuoi(), out string loiCT);

                if (luuCT)
                    MessageBox.Show(this, "Đã cập nhật khóa và chương trình.", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(this, "Đã lưu khóa, nhưng chương trình chưa lưu được:\n\n" + loiCT,
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                TaiDuLieu();
                TaiChuongTrinh();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        /// <summary>Tổng tín chỉ của TOÀN BỘ chương trình (mọi học kỳ), không phụ thuộc bộ lọc.</summary>
        private int TinhTongTuLuoi(bool batBuoc)
        {
            int tong = 0;
            if (_ctFull == null) return 0;

            foreach (DataRow r in _ctFull.Rows)
            {
                if (Convert.ToBoolean(r["BatBuoc"]) != batBuoc) continue;
                tong += r["SoTinChi"] == DBNull.Value ? 0 : Convert.ToInt32(r["SoTinChi"]);
            }
            return tong;
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

        /// <summary>Chốt số tín chỉ của chương trình — vĩnh viễn, không mở lại.</summary>
        private void BtnKhoaTC_Click(object sender, EventArgs e)
        {
            if (maDangChon.Length == 0)
            {
                MessageBox.Show(this, "Hãy chọn một khóa.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                KhoaDTO? khoa = khoaBLL.LayTheoMa(maDangChon);
                if (khoa == null) return;

                if (khoa.DaKhoa)
                {
                    MessageBox.Show(this,
                        $"Chương trình khóa {maDangChon} đã được khoá ngày {khoa.NgayKhoa:dd/MM/yyyy} " +
                        $"bởi {khoa.NguoiKhoa}. Không mở khoá lại.",
                        "Đã khoá", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int batBuoc = ctBLL.TongTinChi(maDangChon, true);
                int tuChon = ctBLL.TongTinChi(maDangChon, false);

                if (MessageBox.Show(this,
                        $"Khoá số tín chỉ của khóa {maDangChon}?\n\n" +
                        $"Tổng sẽ chốt: {batBuoc} bắt buộc + {tuChon} tự chọn\n\n" +
                        "Sau khi khoá KHÔNG sửa được nữa (phải tạo khóa mới).",
                        "Xác nhận khoá", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }

                if (!ctBLL.KhoaChuongTrinh(maDangChon, nguoiDung, out string error))
                {
                    MessageBox.Show(this, error, "Không khoá được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                KhoaDTO? sauKhiKhoa = khoaBLL.LayTheoMa(maDangChon);
                MessageBox.Show(this,
                    $"ĐÃ KHOÁ chương trình khóa {maDangChon}: " +
                    $"{sauKhiKhoa?.TongTCBatBuoc} bắt buộc + {sauKhiKhoa?.TongTCTuChon} tự chọn.",
                    "Đã khoá", MessageBoxButtons.OK, MessageBoxIcon.Information);

                TaiDuLieu();
                TaiChuongTrinh();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        /// <summary>Đọc một ô số có thể rỗng (DBNull) thành int?.</summary>
        private static int? LaySo(DataRow r, string cot)
        {
            if (!r.Table.Columns.Contains(cot)) return null;
            object v = r[cot];
            if (v == null || v == DBNull.Value) return null;
            return int.TryParse(v.ToString(), out int so) ? so : (int?)null;
        }

        /// <summary>Đọc TOÀN BỘ chương trình (mọi học kỳ) thành danh sách dòng để lưu một lần.</summary>
        private List<ChuongTrinhDTO> DocLuoi()
        {
            List<ChuongTrinhDTO> ds = new List<ChuongTrinhDTO>();
            if (_ctFull == null) return ds;

            foreach (DataRow r in _ctFull.Rows)
            {
                ds.Add(new ChuongTrinhDTO(
                    maDangChon,
                    r["MaMH"].ToString() ?? "",
                    Convert.ToBoolean(r["BatBuoc"]),
                    _ctFull.Columns.Contains("HocKy") ? Convert.ToInt32(r["HocKy"]) : 1,
                    LaySo(r, "Thu"),
                    LaySo(r, "TietBD"),
                    LaySo(r, "TietKT"),
                    _ctFull.Columns.Contains("GiangVien") && r["GiangVien"] != DBNull.Value
                        ? r["GiangVien"].ToString()
                        : null));
            }
            return ds;
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
            int hocKy = HocKyChon > 0 ? HocKyChon : 1;

            try
            {
                List<ChuongTrinhDTO> ds = DocLuoi();
                ds.Add(new ChuongTrinhDTO(maDangChon, maMH, true, hocKy));

                if (!ctBLL.LuuChuongTrinh(maDangChon, ds, out string error))
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
                List<ChuongTrinhDTO> ds = DocLuoi().FindAll(d => d.MaMH != maMH);

                if (!ctBLL.LuuChuongTrinh(maDangChon, ds, out string error))
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
            txtMaKhoa.ReadOnly = true;
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
