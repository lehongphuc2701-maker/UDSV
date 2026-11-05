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
    /// Điểm nhấn của màn này: sửa chương trình trên lưới là TỰ DO — thêm/xoá môn chỉ đổi
    /// bảng trong bộ nhớ, không ghi CSDL. Luật (trùng môn, tổng tín chỉ của khóa đã khoá)
    /// chỉ chạy khi bấm "Lưu thay đổi"; không bấm thì mọi sửa đổi bị bỏ.
    /// </summary>
    public partial class FrmKhoa : Form
    {
        private readonly KhoaBLL khoaBLL = new KhoaBLL();
        private readonly ChuongTrinhBLL ctBLL = new ChuongTrinhBLL();
        private readonly MonHocBLL monHocBLL = new MonHocBLL();

        private string maDangChon = "";

        /// <summary>Toàn bộ chương trình của khóa đang chọn (mọi học kỳ); lưới chỉ hiện phần đã lọc.</summary>
        private DataTable? _ctFull;

        /// <summary>Khóa đang chọn, đọc từ CSDL lúc nạp — để nhãn tổng so với số đã khai báo.</summary>
        private KhoaDTO? _khoaDangChon;

        /// <summary>Đang nạp lại lưới: bỏ qua CellValueChanged phát sinh từ chính việc gán DataSource.</summary>
        private bool dangNap;

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

            // Tổng tín chỉ trên nhãn phải chạy theo lưới, kể cả khi admin sửa cờ "Bắt buộc".
            dgvChuongTrinh.CurrentCellDirtyStateChanged += DgvChuongTrinh_CurrentCellDirtyStateChanged;
            dgvChuongTrinh.CellValueChanged += DgvChuongTrinh_CellValueChanged;

            // Lưới danh sách khóa: chỉ để chọn.
            dgvKhoa.ReadOnly = true;
            dgvKhoa.AllowUserToAddRows = false;
            dgvKhoa.AllowUserToDeleteRows = false;

            // Lưới chương trình: gõ được tự do như bảng tính.
            dgvChuongTrinh.ReadOnly = false;
            dgvChuongTrinh.AllowUserToAddRows = false;
            dgvChuongTrinh.AllowUserToDeleteRows = false;

            // Bật DoubleBuffered để cuộn và co giãn mượt mà không nhấp nháy
            typeof(Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(dgvKhoa, true);
            typeof(Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(dgvChuongTrinh, true);

            Resize += (s, e) => CanLaiNutKhoaTC();

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

            // Nút chốt chương trình — nằm trên hàng nút của thẻ "Thông tin khóa", ngay sau "Làm mới".
            // Nút tạo bằng code KHÔNG được form tự co giãn theo font như các nút trong Designer,
            // nên vị trí được căn theo nút "Làm mới" lúc chạy (xem CanLaiNutKhoaTC).
            btnKhoaTC = new Button
            {
                Text = "🔒 KHOÁ SỐ TÍN CHỈ",
                Size = new Size(210, 38),
                BackColor = Color.FromArgb(225, 29, 72),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
            btnKhoaTC.FlatAppearance.BorderSize = 0;
            cardKhoa.Controls.Add(btnKhoaTC);
            btnKhoaTC.BringToFront();
            btnKhoaTC.Click += BtnKhoaTC_Click;
        }

        /// <summary>
        /// Form co giãn theo font lúc chạy (AutoScaleMode.Font) nên toạ độ trong Designer bị nhân
        /// với một hệ số; nút tạo bằng code thì không. Vì vậy đặt nó cạnh "Làm mới" theo số đo THẬT,
        /// sau khi form đã co giãn xong — cùng hàng, cùng chiều cao, cách 20 px.
        /// </summary>
        private void CanLaiNutKhoaTC()
        {
            if (btnKhoaTC == null) return;

            btnKhoaTC.Size = new Size(210, btnLamMoi.Height);
            btnKhoaTC.Location = new Point(btnLamMoi.Right + 15, btnLamMoi.Top);
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
            CanLaiNutKhoaTC();
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

            dgvKhoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvKhoa.ScrollBars = ScrollBars.Both;

            if (dgvKhoa.Columns.Contains("MaKhoa"))
            {
                var col = dgvKhoa.Columns["MaKhoa"];
                col.HeaderText = "Mã khóa";
                col.Width = 75;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvKhoa.Columns.Contains("TenKhoa"))
            {
                var col = dgvKhoa.Columns["TenKhoa"];
                col.HeaderText = "Tên khóa";
                col.Width = 175;
            }
            if (dgvKhoa.Columns.Contains("NamBatDau"))
            {
                var col = dgvKhoa.Columns["NamBatDau"];
                col.HeaderText = "Từ năm";
                col.Width = 65;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvKhoa.Columns.Contains("NamKetThuc"))
            {
                var col = dgvKhoa.Columns["NamKetThuc"];
                col.HeaderText = "Đến năm";
                col.Width = 65;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvKhoa.Columns.Contains("TongTCBatBuoc"))
            {
                var col = dgvKhoa.Columns["TongTCBatBuoc"];
                col.HeaderText = "TC BB";
                col.ToolTipText = "Tổng tín chỉ bắt buộc";
                col.Width = 60;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvKhoa.Columns.Contains("TongTCTuChon"))
            {
                var col = dgvKhoa.Columns["TongTCTuChon"];
                col.HeaderText = "TC TC";
                col.ToolTipText = "Tổng tín chỉ tự chọn";
                col.Width = 60;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvKhoa.Columns.Contains("NgayKhoa"))
            {
                var col = dgvKhoa.Columns["NgayKhoa"];
                col.HeaderText = "Ngày khoá";
                col.Width = 90;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvKhoa.Columns.Contains("NguoiKhoa"))
            {
                var col = dgvKhoa.Columns["NguoiKhoa"];
                col.HeaderText = "Người khoá";
                col.Width = 85;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
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
                dangNap = true;

                // Giữ TOÀN BỘ chương trình trong bộ nhớ; lưới chỉ hiện phần đã lọc theo học kỳ.
                // Nhờ vậy sửa ở một học kỳ không làm mất dòng của các học kỳ khác khi lưu.
                _ctFull = ctBLL.LayTheoKhoa(maDangChon);

                dgvChuongTrinh.DataSource = _ctFull.DefaultView;
                if (dgvChuongTrinh.Columns.Count > 0)
                {
                    dgvChuongTrinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvChuongTrinh.ScrollBars = ScrollBars.Both;

                    if (dgvChuongTrinh.Columns.Contains("MaKhoa")) dgvChuongTrinh.Columns["MaKhoa"].Visible = false;
                    if (dgvChuongTrinh.Columns.Contains("HocKy")) dgvChuongTrinh.Columns["HocKy"].Visible = false;

                    if (dgvChuongTrinh.Columns.Contains("MaMH"))
                    {
                        var col = dgvChuongTrinh.Columns["MaMH"];
                        col.HeaderText = "Mã MH";
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        col.Width = 105;
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    if (dgvChuongTrinh.Columns.Contains("TenMH"))
                    {
                        var col = dgvChuongTrinh.Columns["TenMH"];
                        col.HeaderText = "Tên môn học";
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        col.FillWeight = 260;
                        col.MinimumWidth = 180;
                    }
                    if (dgvChuongTrinh.Columns.Contains("SoTinChi"))
                    {
                        var col = dgvChuongTrinh.Columns["SoTinChi"];
                        col.HeaderText = "TC";
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        col.Width = 55;
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    if (dgvChuongTrinh.Columns.Contains("BatBuoc"))
                    {
                        var col = dgvChuongTrinh.Columns["BatBuoc"];
                        col.HeaderText = "Bắt buộc";
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        col.Width = 85;
                    }
                    if (dgvChuongTrinh.Columns.Contains("Thu"))
                    {
                        var col = dgvChuongTrinh.Columns["Thu"];
                        col.HeaderText = "Thứ (2..8)";
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        col.Width = 85;
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    if (dgvChuongTrinh.Columns.Contains("TietBD"))
                    {
                        var col = dgvChuongTrinh.Columns["TietBD"];
                        col.HeaderText = "Tiết BĐ";
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        col.Width = 70;
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    if (dgvChuongTrinh.Columns.Contains("TietKT"))
                    {
                        var col = dgvChuongTrinh.Columns["TietKT"];
                        col.HeaderText = "Tiết KT";
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        col.Width = 70;
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    if (dgvChuongTrinh.Columns.Contains("GiangVien"))
                    {
                        var col = dgvChuongTrinh.Columns["GiangVien"];
                        col.HeaderText = "Giảng viên";
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        col.FillWeight = 160;
                        col.MinimumWidth = 140;
                    }
                    if (dgvChuongTrinh.Columns.Contains("PhongHoc"))
                    {
                        var col = dgvChuongTrinh.Columns["PhongHoc"];
                        col.HeaderText = "Phòng học";
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        col.Width = 100;
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }

                NapHocKy();
                ApDungLocHocKy();
                CapNhatTrangThai();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
            finally
            {
                dangNap = false;
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
                lblTrangThai.Text = $"🔒 ĐÃ KHOÁ — chốt ngày {khoa!.NgayKhoa:dd/MM/yyyy} bởi {khoa.NguoiKhoa}";
                lblTrangThai.ForeColor = Color.FromArgb(220, 38, 38);

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
                    : "✏️ Còn sửa được — chương trình chưa bị khoá";
                lblTrangThai.ForeColor = Color.FromArgb(22, 163, 74);
                BatTatNhap(true);
                if (btnKhoaTC != null)
                {
                    btnKhoaTC.Enabled = true;
                    btnKhoaTC.BackColor = Color.FromArgb(225, 29, 72);
                    btnKhoaTC.ForeColor = Color.White;
                }
            }

            BatTat(btnThemMon, true, mauThemMon);
            BatTat(btnXoaMon, true, mauXoaMon);
            BatTat(btnXoa, true, mauXoa);
            BatTat(btnSua, true, mauSua);

            // Giữ lại khóa vừa đọc để nhãn tổng so được với số đã khai báo mà không phải đọc lại CSDL.
            _khoaDangChon = khoa;
            CapNhatTongTinChi();
        }

        /// <summary>
        /// Nhãn tổng tín chỉ, cộng thẳng trên bảng trong bộ nhớ (_ctFull) nên nhảy ngay sau mỗi lần
        /// thêm / xoá môn hoặc đổi cờ "Bắt buộc" — không đọc CSDL. Phần "(khai báo: ...)" là số đã
        /// chốt trong bảng Khoa, để admin nhìn một phát là biết lưới đang khớp hay lệch.
        /// </summary>
        private void CapNhatTongTinChi()
        {
            if (maDangChon.Length == 0)
            {
                lblTongTC.Text = "Tổng TC hiện tại: —";
                lblTongTC.ForeColor = SystemColors.ControlText;
                return;
            }

            int batBuoc = TinhTongTuLuoi(true);
            int tuChon = TinhTongTuLuoi(false);

            string khaiBao = _khoaDangChon == null
                ? "—"
                : $"{_khoaDangChon.TongTCBatBuoc} + {_khoaDangChon.TongTCTuChon}";

            lblTongTC.Text = $"Tổng TC hiện tại: {batBuoc} bắt buộc + {tuChon} tự chọn   (khai báo: {khaiBao})";
            lblTongTC.ForeColor = _khoaDangChon != null
                                  && (_khoaDangChon.TongTCBatBuoc != batBuoc
                                      || _khoaDangChon.TongTCTuChon != tuChon)
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

        /// <summary>
        /// Cột "Bắt buộc" là checkbox: phải ghi giá trị ngay khi bấm thì CellValueChanged mới chạy
        /// và nhãn tổng tín chỉ mới nhảy theo, thay vì đợi rời ô.
        /// </summary>
        private void DgvChuongTrinh_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (dgvChuongTrinh.IsCurrentCellDirty && dgvChuongTrinh.CurrentCell is DataGridViewCheckBoxCell)
                dgvChuongTrinh.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        /// <summary>Sửa cờ "Bắt buộc" hay số tín chỉ trên lưới → tính lại tổng ngay.</summary>
        private void DgvChuongTrinh_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (dangNap || e.RowIndex < 0) return;
            CapNhatTongTinChi();
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Mã khóa tự sinh (K26, K27…) — người dùng không gõ tay vì đây là khoá chính.
                if (!khoaBLL.TaoKhoaKeTiep(out string maMoi, out string error))
                {
                    ThongBao.Show(this, error, "Không thêm được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TaiDuLieu();
                ChonKhoa(maMoi);

                ThongBao.Show(this,
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
                ThongBao.Show(this, "Hãy chọn một khóa trong bảng.", "Chưa chọn",
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
                        ThongBao.Show(this,
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
                    ThongBao.Show(this, error, "Không sửa được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Lưu luôn chương trình: các ô sửa trực tiếp trên lưới chỉ có hiệu lực từ đây.
                bool luuCT = ctBLL.LuuChuongTrinh(maDangChon, DocLuoi(), out string loiCT);

                if (luuCT)
                    ThongBao.Show(this, "Đã cập nhật khóa và chương trình.", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    ThongBao.Show(this, "Đã lưu khóa, nhưng chương trình chưa lưu được:\n\n" + loiCT,
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

        /// <summary>Dòng môn học trong combo "Thêm môn" — nguồn của TenMH và SoTinChi.</summary>
        private DataRow? DongMonTrongCombo(string maMH)
        {
            if (maMH.Length == 0 || cboMonHoc.DataSource is not DataTable dsMon) return null;

            foreach (DataRow r in dsMon.Rows)
            {
                if (string.Equals(r["MaMH"]?.ToString(), maMH, StringComparison.OrdinalIgnoreCase))
                    return r;
            }
            return null;
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (maDangChon.Length == 0)
            {
                ThongBao.Show(this, "Hãy chọn một khóa trong bảng.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ThongBao.Show(this, $"Xoá khóa '{maDangChon}'?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                if (!khoaBLL.Xoa(maDangChon, out string error))
                {
                    ThongBao.Show(this, error, "Không xoá được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ThongBao.Show(this, "Đã xoá khóa.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        /// <summary>Chốt số tín chỉ của chương trình — vĩnh viễn, không mở lại.</summary>
        private void BtnKhoaTC_Click(object sender, EventArgs e)
        {
            if (maDangChon.Length == 0)
            {
                ThongBao.Show(this, "Hãy chọn một khóa.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                KhoaDTO? khoa = khoaBLL.LayTheoMa(maDangChon);
                if (khoa == null) return;

                if (khoa.DaKhoa)
                {
                    ThongBao.Show(this,
                        $"Chương trình khóa {maDangChon} đã được khoá ngày {khoa.NgayKhoa:dd/MM/yyyy} " +
                        $"bởi {khoa.NguoiKhoa}. Không mở khoá lại.",
                        "Đã khoá", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int batBuoc = ctBLL.TongTinChi(maDangChon, true);
                int tuChon = ctBLL.TongTinChi(maDangChon, false);

                if (ThongBao.Show(this,
                        $"Khoá số tín chỉ của khóa {maDangChon}?\n\n" +
                        $"Tổng sẽ chốt: {batBuoc} bắt buộc + {tuChon} tự chọn\n\n" +
                        "Sau khi khoá KHÔNG sửa được nữa (phải tạo khóa mới).",
                        "Xác nhận khoá", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }

                if (!ctBLL.KhoaChuongTrinh(maDangChon, nguoiDung, out string error))
                {
                    ThongBao.Show(this, error, "Không khoá được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                KhoaDTO? sauKhiKhoa = khoaBLL.LayTheoMa(maDangChon);
                ThongBao.Show(this,
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
                        : null,
                    _ctFull.Columns.Contains("PhongHoc") && r["PhongHoc"] != DBNull.Value
                        ? r["PhongHoc"].ToString()
                        : null));
            }
            return ds;
        }

        /// <summary>
        /// "Thêm môn" chỉ thêm MỘT DÒNG vào bảng trong bộ nhớ — không gọi BLL, không ghi CSDL.
        /// Luật thật (trùng môn, tổng tín chỉ của khóa đã khoá) chỉ chạy khi bấm "Lưu thay đổi".
        /// Chặn trùng tại đây chỉ để lưới khỏi có hai dòng cùng một môn — kiểm tra cục bộ, không phải luật.
        /// </summary>
        private void BtnThemMon_Click(object sender, EventArgs e)
        {
            if (maDangChon.Length == 0 || _ctFull == null)
            {
                ThongBao.Show(this, "Hãy chọn một khóa.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maMH = cboMonHoc.SelectedValue?.ToString() ?? "";
            DataRow? mon = DongMonTrongCombo(maMH);
            if (mon == null)
            {
                ThongBao.Show(this, "Hãy chọn một môn học để thêm.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tenMH = mon["TenMH"]?.ToString() ?? "";

            foreach (DataRow r in _ctFull.Rows)
            {
                if (!string.Equals(r["MaMH"]?.ToString(), maMH, StringComparison.OrdinalIgnoreCase)) continue;

                ThongBao.Show(this, $"Môn '{tenMH}' đã có trong chương trình.", "Không thêm được",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dangNap = true;

                DataRow moi = _ctFull.NewRow();
                moi["MaKhoa"] = maDangChon;
                moi["MaMH"] = maMH;
                moi["TenMH"] = mon["TenMH"];
                moi["SoTinChi"] = mon["SoTinChi"];
                moi["BatBuoc"] = true;
                moi["HocKy"] = HocKyChon > 0 ? HocKyChon : 1;
                // Thứ / Tiết BĐ / Tiết KT / Giảng viên / Phòng học để trống — admin xếp lịch sau.
                _ctFull.Rows.Add(moi);
            }
            catch (Exception ex) { BaoLoi(ex); return; }
            finally { dangNap = false; }

            // Dòng mới mang đúng học kỳ đang lọc, nhưng vẫn lọc lại cho chắc rồi mới tính tổng.
            ApDungLocHocKy();
            dgvChuongTrinh.Refresh();
            CapNhatTongTinChi();
        }

        /// <summary>
        /// "Xoá môn" chỉ bỏ MỘT DÒNG khỏi bảng trong bộ nhớ — không gọi BLL, không ghi CSDL.
        /// Dòng lấy từ DataRowView của lưới, mà DataRowView giữ đúng DataRow của _ctFull,
        /// nên đang lọc học kỳ hay không cũng không xoá nhầm dòng khác.
        /// </summary>
        private void BtnXoaMon_Click(object sender, EventArgs e)
        {
            if (maDangChon.Length == 0 || _ctFull == null)
            {
                ThongBao.Show(this, "Hãy chọn một khóa.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvChuongTrinh.CurrentRow?.DataBoundItem is not DataRowView item)
            {
                ThongBao.Show(this, "Hãy chọn một môn trong chương trình.", "Chưa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow dong = item.Row;
            string tenMH = dong.Table.Columns.Contains("TenMH") && dong["TenMH"] != DBNull.Value
                ? dong["TenMH"].ToString() ?? ""
                : dong["MaMH"].ToString() ?? "";

            if (ThongBao.Show(this,
                    $"Xoá môn '{tenMH}' khỏi chương trình?\n" +
                    "(Chỉ xoá trên lưới — bấm \"Lưu thay đổi\" mới ghi vào CSDL.)",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                dangNap = true;
                _ctFull.Rows.Remove(dong);
            }
            catch (Exception ex) { BaoLoi(ex); return; }
            finally { dangNap = false; }

            ApDungLocHocKy();
            dgvChuongTrinh.Refresh();
            CapNhatTongTinChi();
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
            ThongBao.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}