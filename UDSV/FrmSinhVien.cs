using System;
using System.Data;
using System.Windows.Forms;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV
{
    public partial class FrmSinhVien : Form
    {
        // Khởi tạo đối tượng BLL để gọi các hàm xử lý
        private readonly SinhVienBLL svBLL = new SinhVienBLL();

        public FrmSinhVien()
        {
            InitializeComponent();

            // Gắn các sự kiện (Events) tự động
            this.Load += FrmSinhVien_Load;
            dgvSinhVien.CellClick += DgvSinhVien_CellClick;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;

            // Tìm kiếm dữ liệu
            btnTimKiem.Click += (s, e) => ThucHienTimKiem();
            txtTimKiem.TextChanged += (s, e) => ThucHienTimKiem();
        }

        // 1. Khi Form vừa mở lên, tự động tải dữ liệu
        private void FrmSinhVien_Load(object? sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = svBLL.LayDanhSachSinhVien();
            dgvSinhVien.DataSource = dt;
            DinhDangCot();
        }

        // Định dạng tiêu đề cột và hiển thị đẹp mắt
        private void DinhDangCot()
        {
            if (dgvSinhVien.Columns.Count == 0) return;

            if (dgvSinhVien.Columns.Contains("MaSV"))
                dgvSinhVien.Columns["MaSV"].HeaderText = "Mã Sinh Viên";

            if (dgvSinhVien.Columns.Contains("HoTen"))
                dgvSinhVien.Columns["HoTen"].HeaderText = "Họ và Tên";

            if (dgvSinhVien.Columns.Contains("NgaySinh"))
            {
                dgvSinhVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                dgvSinhVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvSinhVien.Columns.Contains("GioiTinh"))
                dgvSinhVien.Columns["GioiTinh"].HeaderText = "Giới Tính";

            if (dgvSinhVien.Columns.Contains("Lop"))
                dgvSinhVien.Columns["Lop"].HeaderText = "Lớp";

            if (dgvSinhVien.Columns.Contains("SDT"))
                dgvSinhVien.Columns["SDT"].HeaderText = "Số Điện Thoại";

            if (dgvSinhVien.Columns.Contains("DiemTB"))
            {
                dgvSinhVien.Columns["DiemTB"].HeaderText = "Điểm TB";
                dgvSinhVien.Columns["DiemTB"].DefaultCellStyle.Format = "N2";
            }
        }

        // 2. Tìm kiếm sinh viên theo từ khóa (Mã SV, Tên, Lớp, SĐT)
        private void ThucHienTimKiem()
        {
            if (dgvSinhVien.DataSource is DataTable dt)
            {
                string tuKhoa = txtTimKiem.Text.Trim().Replace("'", "''");
                if (string.IsNullOrEmpty(tuKhoa))
                {
                    dt.DefaultView.RowFilter = string.Empty;
                }
                else
                {
                    dt.DefaultView.RowFilter = string.Format(
                        "MaSV LIKE '%{0}%' OR HoTen LIKE '%{0}%' OR Lop LIKE '%{0}%' OR SDT LIKE '%{0}%'",
                        tuKhoa
                    );
                }
            }
        }

        // 3. Làm trống các ô nhập liệu
        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtLop.Clear();
            txtSDT.Clear();
            txtDiemTB.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = -1;

            txtMaSV.Focus();
        }

        // 4. Click chọn 1 dòng trên DataGridView để đưa dữ liệu lên ô nhập
        private void DgvSinhVien_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

                txtMaSV.Text = row.Cells["MaSV"].Value?.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["NgaySinh"].Value?.ToString(), out DateTime ns))
                    dtpNgaySinh.Value = ns;

                cboGioiTinh.Text = row.Cells["GioiTinh"].Value?.ToString();
                txtLop.Text = row.Cells["Lop"].Value?.ToString();
                txtSDT.Text = row.Cells["SDT"].Value?.ToString();
                txtDiemTB.Text = row.Cells["DiemTB"].Value?.ToString();
            }
        }

        // 5. Hàm hỗ trợ gom dữ liệu từ Form thành đối tượng DTO
        private SinhVienDTO LayDuLieuTuForm()
        {
            decimal? diemTB = null;

            if (decimal.TryParse(txtDiemTB.Text.Trim(), out decimal d))
                diemTB = d;

            return new SinhVienDTO(
                txtMaSV.Text.Trim(),
                txtHoTen.Text.Trim(),
                dtpNgaySinh.Value,
                cboGioiTinh.Text,
                txtLop.Text.Trim(),
                txtSDT.Text.Trim(),
                diemTB
            );
        }

        // 6. Thêm Sinh Viên
        private void BtnThem_Click(object? sender, EventArgs e)
        {
            SinhVienDTO sv = LayDuLieuTuForm();
            if (svBLL.ThemSinhVien(sv, out string error))
            {
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Cập nhật lại bảng
                BtnLamMoi_Click(sender, e); // Xóa trắng ô nhập
            }
            else
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 7. Sửa Sinh Viên
        private void BtnSua_Click(object? sender, EventArgs e)
        {
            SinhVienDTO sv = LayDuLieuTuForm();
            if (svBLL.SuaSinhVien(sv, out string error))
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            else
            {
                MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 8. Xóa Sinh Viên
        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (svBLL.XoaSinhVien(txtMaSV.Text.Trim(), out string error))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    BtnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
