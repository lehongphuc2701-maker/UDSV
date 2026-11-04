using System;
using System.Data;
using System.Windows.Forms;
using UDSV.BLL;
using UDSV.Controls;
using UDSV.DTO;

namespace UDSV
{
    /// <summary>
    /// Giao diện quản lý sinh viên.
    /// Chỉ gọi BLL — không có câu SQL nào trong file này, đúng nguyên tắc UI → BLL → DAL.
    /// </summary>
    public partial class FrmSinhVien : Form
    {
        private readonly SinhVienBLL bll = new SinhVienBLL();
        private readonly KhoaBLL khoaBLL = new KhoaBLL();

        /// <summary>Mục đầu tiên của bộ lọc — không lọc theo trạng thái nào cả.</summary>
        private const string TatCaTrangThai = "Tất cả";

        private static readonly string[] CacTrangThai =
        {
            TatCaTrangThai, "Chưa tốt nghiệp", "Đã thôi học", "Đã tốt nghiệp", "Đã xóa"
        };

        // Hai điều khiển thêm bằng mã (không sửa file Designer): lọc trạng thái + nút Phục hồi
        private Label lblTrangThai = null!;
        private ComboBox cboTrangThai = null!;
        private GradientButton btnPhucHoi = null!;

        // Mã của dòng đang chọn; rỗng nghĩa là đang ở chế độ thêm mới
        private string maDangChon = "";

        public FrmSinhVien()
        {
            InitializeComponent();
            TaoBoLocTrangThai();
            TaoNutPhucHoi();
            GanSuKien();
            CauHinhLuoi();
        }

        /// <summary>
        /// Bộ lọc trạng thái đặt ở dải tiêu đề của vùng danh sách, nằm giữa tiêu đề
        /// "DANH SÁCH SINH VIÊN" và ô tìm kiếm. Toạ độ lấy theo các control có sẵn
        /// (không viết số cứng) để không lệch khi form được phóng to theo tỉ lệ DPI.
        /// </summary>
        private void TaoBoLocTrangThai()
        {
            lblTrangThai = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(90, 100, 115),
                Name = "lblTrangThai",
                Text = "Trạng thái:"
            };

            cboTrangThai = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                FormattingEnabled = true,
                Name = "cboTrangThai"
            };
            cboTrangThai.Items.AddRange(CacTrangThai);

            lblTrangThai.Location = new Point(lblListTitle.Right + 26, lblSearch.Top);
            cboTrangThai.Location = new Point(lblTrangThai.Right + 8, txtTimKiem.Top);

            // Chừa 16px trước nhãn "Tìm kiếm:" để hai bộ lọc không chạm nhau
            int conLai = lblSearch.Left - 16 - cboTrangThai.Left;
            cboTrangThai.Width = Math.Max(160, Math.Min(260, conLai));

            cardData.Controls.Add(lblTrangThai);
            cardData.Controls.Add(cboTrangThai);
        }

        /// <summary>Nút Phục hồi đặt ngay bên trái nút Xóa trong thẻ nhập liệu.</summary>
        private void TaoNutPhucHoi()
        {
            btnPhucHoi = new GradientButton
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Name = "btnPhucHoi",
                Radius = 12,
                Size = new Size(145, 42),
                Text = "♻ Phục hồi",
                Location = new Point(btnXoa.Left - 160, btnXoa.Top)   // cách nút Xóa 15px
            };

            cardInput.Controls.Add(btnPhucHoi);
        }

        /// <summary>Designer của nhóm chưa gắn sự kiện, nên gắn ở đây để không phải sửa file Designer.</summary>
        private void GanSuKien()
        {
            Load += FrmSinhVien_Load;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnPhucHoi.Click += BtnPhucHoi_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            dgvSinhVien.CellClick += DgvSinhVien_CellClick;
            txtTimKiem.KeyDown += TxtTimKiem_KeyDown;
        }

        private void CauHinhLuoi()
        {
            dgvSinhVien.AutoGenerateColumns = true;
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AllowUserToDeleteRows = false;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FrmSinhVien_Load(object? sender, EventArgs e)
        {
            NapComboKhoa();
            cboGioiTinh.SelectedIndex = 0;

            // Đặt mục mặc định TRƯỚC khi gắn sự kiện, để lúc mở form chỉ tải dữ liệu một lần
            cboTrangThai.SelectedIndex = 0;
            cboTrangThai.SelectedIndexChanged += CboTrangThai_Changed;
            CapNhatNutPhucHoi();

            TaiDuLieu();
        }

        /// <summary>Nạp danh sách khóa học vào ComboBox để chọn</summary>
        private void NapComboKhoa()
        {
            try
            {
                DataTable dtKhoa = khoaBLL.LayDanhSach();
                if (!dtKhoa.Columns.Contains("HienThi"))
                {
                    dtKhoa.Columns.Add("HienThi", typeof(string), "MaKhoa + ' - ' + TenKhoa");
                }
                cboMaKhoa.DataSource = dtKhoa;
                cboMaKhoa.DisplayMember = "HienThi";
                cboMaKhoa.ValueMember = "MaKhoa";
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ---------- Nạp dữ liệu ----------

        /// <summary>Trạng thái đang lọc; null nghĩa là "Tất cả" (không lọc gì).</summary>
        private string? TrangThaiDangLoc
        {
            get
            {
                string chon = cboTrangThai.SelectedItem?.ToString() ?? "";
                return chon.Length == 0 || chon == TatCaTrangThai ? null : chon;
            }
        }

        private void TaiDuLieu(string keyword = "")
        {
            try
            {
                // Bộ lọc trạng thái chạy dưới CSDL (LayTheoTrangThai); ô tìm kiếm lọc tiếp
                // trên đúng bảng vừa tải, nên hai bộ lọc dùng chung được với nhau.
                DataTable dt = bll.LayTheoTrangThai(TrangThaiDangLoc);

                string tuKhoa = (keyword ?? "").Trim().Replace("'", "''");
                dt.DefaultView.RowFilter = string.IsNullOrEmpty(tuKhoa)
                    ? ""
                    : $"MaSV LIKE '%{tuKhoa}%' OR HoTen LIKE '%{tuKhoa}%'";

                dgvSinhVien.DataSource = dt;
                DatTieuDeCot();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi kết nối CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Đổi tên cột của bảng thành tiêu đề tiếng Việt cho dễ đọc.</summary>
        private void DatTieuDeCot()
        {
            if (dgvSinhVien.Columns.Count == 0) return;

            if (dgvSinhVien.Columns.Contains("MaSV")) dgvSinhVien.Columns["MaSV"].HeaderText = "Mã SV";
            if (dgvSinhVien.Columns.Contains("HoTen")) dgvSinhVien.Columns["HoTen"].HeaderText = "Họ tên";
            if (dgvSinhVien.Columns.Contains("NgaySinh"))
            {
                dgvSinhVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dgvSinhVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvSinhVien.Columns.Contains("GioiTinh")) dgvSinhVien.Columns["GioiTinh"].HeaderText = "Giới tính";
            if (dgvSinhVien.Columns.Contains("MaKhoa")) dgvSinhVien.Columns["MaKhoa"].HeaderText = "Mã khóa";
            if (dgvSinhVien.Columns.Contains("TenKhoa")) dgvSinhVien.Columns["TenKhoa"].HeaderText = "Tên khóa";
            if (dgvSinhVien.Columns.Contains("SDT")) dgvSinhVien.Columns["SDT"].HeaderText = "SĐT";
            if (dgvSinhVien.Columns.Contains("TrangThai"))
            {
                dgvSinhVien.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvSinhVien.Columns["TrangThai"].ReadOnly = true;
                dgvSinhVien.Columns["TrangThai"].FillWeight = 110;
            }
        }

        // ---------- Lấy dữ liệu từ form ----------

        private SinhVienDTO LayDuLieuTuForm()
        {
            return new SinhVienDTO
            {
                MaSV = txtMaSV.Text,
                HoTen = txtHoTen.Text,
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = cboGioiTinh.Text,
                MaKhoa = cboMaKhoa.SelectedValue?.ToString() ?? cboMaKhoa.Text.Trim(),
                SDT = txtSDT.Text
            };
        }

        // ---------- Sự kiện nút ----------

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!bll.ThemSinhVien(LayDuLieuTuForm(), out string error))
                {
                    MessageBox.Show(this, error, "Không thêm được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, "Thêm sinh viên thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show(this, "Hãy chọn một sinh viên trong bảng để sửa.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (!bll.SuaSinhVien(LayDuLieuTuForm(), out string error))
                {
                    MessageBox.Show(this, error, "Không sửa được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, "Cập nhật sinh viên thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        /// <summary>
        /// Xoá MỀM: chỉ đổi TrangThai sang 'Đã xóa', dòng dữ liệu và điểm vẫn còn nguyên.
        /// Nhờ vậy có thể phục hồi ở bộ lọc "Đã xóa".
        /// </summary>
        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show(this, "Hãy chọn một sinh viên trong bảng để xoá.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                this,
                $"Bạn có chắc muốn xoá sinh viên {maDangChon} - {txtHoTen.Text}?\n\n" +
                "Sinh viên sẽ chuyển sang trạng thái 'Đã xóa' (không mất dữ liệu) " +
                "và có thể phục hồi sau.",
                "Xác nhận xoá",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (!bll.XoaMem(maDangChon, out string error))
                {
                    MessageBox.Show(this, error, "Không xoá được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, "Đã chuyển sinh viên sang trạng thái 'Đã xóa'.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        /// <summary>Phục hồi sinh viên đã xoá mềm — chỉ dùng được khi đang xem "Đã xóa".</summary>
        private void BtnPhucHoi_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show(this, "Hãy chọn một sinh viên trong danh sách 'Đã xóa' để phục hồi.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                this,
                $"Phục hồi sinh viên {maDangChon} - {txtHoTen.Text}?\n\n" +
                "Sinh viên sẽ trở lại trạng thái 'Chưa tốt nghiệp'.",
                "Xác nhận phục hồi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (!bll.PhucHoi(maDangChon, out string error))
                {
                    MessageBox.Show(this, error, "Không phục hồi được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, "Đã phục hồi sinh viên.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void CboTrangThai_Changed(object? sender, EventArgs e)
        {
            CapNhatNutPhucHoi();

            // Đổi bộ lọc thì dòng đang chọn không còn ý nghĩa
            maDangChon = "";
            dgvSinhVien.ClearSelection();

            TaiDuLieu(txtTimKiem.Text);
        }

        /// <summary>Nút Phục hồi chỉ bật khi đang xem thùng rác; tắt thì đổi sang màu xám.</summary>
        private void CapNhatNutPhucHoi()
        {
            bool bat = TrangThaiDangLoc == "Đã xóa";

            btnPhucHoi.Enabled = bat;
            btnPhucHoi.Cursor = bat ? Cursors.Hand : Cursors.Default;
            btnPhucHoi.GradientStart = bat ? Color.FromArgb(26, 188, 156) : Color.FromArgb(189, 195, 199);
            btnPhucHoi.GradientEnd = bat ? Color.FromArgb(22, 160, 133) : Color.FromArgb(149, 165, 166);
            btnPhucHoi.Invalidate();
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            LamMoi();
        }

        private void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            TaiDuLieu(txtTimKiem.Text);
        }

        private void TxtTimKiem_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TaiDuLieu(txtTimKiem.Text);
            }
        }

        private void DgvSinhVien_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];
            if (row.DataBoundItem is not DataRowView item) return;

            maDangChon = item["MaSV"].ToString() ?? "";
            txtMaSV.Text = maDangChon;
            txtHoTen.Text = item["HoTen"].ToString() ?? "";
            cboMaKhoa.SelectedValue = item["MaKhoa"].ToString() ?? "";
            txtSDT.Text = item["SDT"].ToString() ?? "";

            string gioiTinh = item["GioiTinh"].ToString() ?? "";
            if (gioiTinh == "Nam" || gioiTinh == "Nữ")
                cboGioiTinh.SelectedItem = gioiTinh;

            if (item["NgaySinh"] != DBNull.Value)
            {
                if (DateTime.TryParse(item["NgaySinh"].ToString(), out DateTime ngay))
                    dtpNgaySinh.Value = ngay;
            }

            // Mã SV là khoá chính nên không cho sửa khi đang cập nhật
            txtMaSV.ReadOnly = true;
        }

        // ---------- Tiện ích ----------

        private void LamMoi()
        {
            maDangChon = "";
            txtMaSV.Clear();
            txtHoTen.Clear();
            if (cboMaKhoa.Items.Count > 0)
                cboMaKhoa.SelectedIndex = 0;
            else
                cboMaKhoa.SelectedIndex = -1;
            txtSDT.Clear();
            txtTimKiem.Clear();
            txtMaSV.ReadOnly = false;
            cboGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Today;
            dgvSinhVien.ClearSelection();
            txtMaSV.Focus();
            // Giữ nguyên bộ lọc trạng thái: đó là cách xem danh sách, không phải dữ liệu nhập
            TaiDuLieu();
        }

        private void BaoLoi(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
