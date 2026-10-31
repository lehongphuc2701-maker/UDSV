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

            // Gắn các sự kiện (Events) tự động thay vì phải nhấp đúp vào giao diện
            this.Load += FrmSinhVien_Load;
            dgvSinhVien.CellClick += DgvSinhVien_CellClick;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
        }

        // 1. Khi Form vừa mở lên, tự động tải dữ liệu
        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgvSinhVien.DataSource = svBLL.LayDanhSachSinhVien();
        }

        // 2. Làm trống các ô nhập liệu
        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtLop.Clear();
            txtSDT.Clear();
            txtDiemTB.Clear(); // Đã mở khóa ô Điểm TB
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = -1;

            txtMaSV.Focus();
        }

        // 3. Click chọn 1 dòng trên DataGridView để đưa dữ liệu lên TextBox
        private void DgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
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

                // Đã mở khóa: Lấy dữ liệu điểm từ DataGridView hiển thị lên TextBox
                txtDiemTB.Text = row.Cells["DiemTB"].Value?.ToString();
            }
        }

        // 4. Hàm hỗ trợ gom dữ liệu từ Form thành đối tượng DTO
        private SinhVienDTO LayDuLieuTuForm()
        {
            decimal? diemTB = null;

            // Đã mở khóa: Ép kiểu dữ liệu từ TextBox sang dạng số thập phân (decimal)
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

        // 5. Thêm Sinh Viên
        private void BtnThem_Click(object sender, EventArgs e)
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

        // 6. Sửa Sinh Viên
        private void BtnSua_Click(object sender, EventArgs e)
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

        // 7. Xóa Sinh Viên
        private void BtnXoa_Click(object sender, EventArgs e)
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