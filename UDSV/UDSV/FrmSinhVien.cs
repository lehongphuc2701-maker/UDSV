using System;
using System.Data;
using System.Windows.Forms;
using UDSV.BLL;
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

        // Mã của dòng đang chọn; rỗng nghĩa là đang ở chế độ thêm mới
        private string maDangChon = "";

        public FrmSinhVien()
        {
            InitializeComponent();
            GanSuKien();
            CauHinhLuoi();
        }

        /// <summary>Designer của nhóm chưa gắn sự kiện, nên gắn ở đây để không phải sửa file Designer.</summary>
        private void GanSuKien()
        {
            Load += FrmSinhVien_Load;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
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

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            cboGioiTinh.SelectedIndex = 0;
            TaiDuLieu();
        }

        // ---------- Nạp dữ liệu ----------

        private void TaiDuLieu(string keyword = "")
        {
            try
            {
                DataTable dt = string.IsNullOrWhiteSpace(keyword)
                    ? bll.LayDanhSachSinhVien()
                    : bll.TimKiemSinhVien(keyword);

                dgvSinhVien.DataSource = dt;
                DatTieuDeCot();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi kết nối CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Đổi tên cột tiếng Anh của bảng thành tiêu đề tiếng Việt cho dễ đọc.</summary>
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
            if (dgvSinhVien.Columns.Contains("Lop")) dgvSinhVien.Columns["Lop"].HeaderText = "Lớp";
            if (dgvSinhVien.Columns.Contains("SDT")) dgvSinhVien.Columns["SDT"].HeaderText = "SĐT";
            if (dgvSinhVien.Columns.Contains("DiemTB"))
            {
                dgvSinhVien.Columns["DiemTB"].HeaderText = "Điểm TB";
                dgvSinhVien.Columns["DiemTB"].DefaultCellStyle.Format = "0.00";
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
                Lop = txtLop.Text,
                SDT = txtSDT.Text,
                DiemTB = DocDiemTuForm()
            };
        }

        /// <summary>Ô điểm để trống = chưa có điểm (null). Người dùng có thể gõ 3.45 hoặc 3,45.</summary>
        private decimal? DocDiemTuForm()
        {
            string chuoi = txtDiemTB.Text.Trim().Replace(',', '.');
            if (chuoi.Length == 0) return null;

            if (decimal.TryParse(chuoi, System.Globalization.NumberStyles.Number,
                                 System.Globalization.CultureInfo.InvariantCulture, out decimal diem))
            {
                return diem;
            }

            // Không đọc được số: trả về giá trị chắc chắn sai để BLL từ chối và báo lỗi rõ ràng
            return -1m;
        }

        // ---------- Sự kiện nút ----------

        private void BtnThem_Click(object sender, EventArgs e)
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

        private void BtnSua_Click(object sender, EventArgs e)
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

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show(this, "Hãy chọn một sinh viên trong bảng để xoá.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                this,
                $"Bạn có chắc muốn xoá sinh viên {maDangChon} - {txtHoTen.Text}?",
                "Xác nhận xoá",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (!bll.XoaSinhVien(maDangChon, out string error))
                {
                    MessageBox.Show(this, error, "Không xoá được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, "Đã xoá sinh viên.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            TaiDuLieu(txtTimKiem.Text);
        }

        private void TxtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                TaiDuLieu(txtTimKiem.Text);
            }
        }

        private void DgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];
            if (row.DataBoundItem is not DataRowView item) return;

            maDangChon = item["MaSV"].ToString() ?? "";
            txtMaSV.Text = maDangChon;
            txtHoTen.Text = item["HoTen"].ToString() ?? "";
            txtLop.Text = item["Lop"].ToString() ?? "";
            txtSDT.Text = item["SDT"].ToString() ?? "";
            txtDiemTB.Text = item["DiemTB"] == DBNull.Value
                ? ""
                : Convert.ToDecimal(item["DiemTB"]).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

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
            txtLop.Clear();
            txtSDT.Clear();
            txtDiemTB.Clear();
            txtTimKiem.Clear();
            txtMaSV.ReadOnly = false;
            cboGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Today;
            dgvSinhVien.ClearSelection();
            txtMaSV.Focus();
            TaiDuLieu();
        }

        private void BaoLoi(Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
