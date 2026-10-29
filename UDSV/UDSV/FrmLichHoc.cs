using System;
using System.Data;
using System.Windows.Forms;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV
{
    /// <summary>
    /// Quản lý lịch học — màn hình còn thiếu trong bản giao diện của nhóm.
    /// Mỗi buổi học nối một môn với một sinh viên, nên hai ô chọn lấy dữ liệu
    /// từ MonHocBLL và SinhVienBLL chứ không gõ tay mã.
    /// </summary>
    public partial class FrmLichHoc : Form
    {
        private readonly LichHocBLL lichHocBLL = new LichHocBLL();
        private readonly SinhVienBLL sinhVienBLL = new SinhVienBLL();
        private readonly MonHocBLL monHocBLL = new MonHocBLL();

        private int maLichDangChon = 0;

        public FrmLichHoc()
        {
            InitializeComponent();
            GanSuKien();
            CauHinhLuoi();
        }

        private void GanSuKien()
        {
            Load += FrmLichHoc_Load;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            dgvLichHoc.CellClick += DgvLichHoc_CellClick;
        }

        private void CauHinhLuoi()
        {
            dgvLichHoc.AutoGenerateColumns = true;
            dgvLichHoc.ReadOnly = true;
            dgvLichHoc.AllowUserToAddRows = false;
            dgvLichHoc.AllowUserToDeleteRows = false;
            dgvLichHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLichHoc.MultiSelect = false;
            dgvLichHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FrmLichHoc_Load(object sender, EventArgs e)
        {
            try
            {
                NapCombo();
                dtpNgayHoc.Value = DateTime.Today;
                cboCaHoc.SelectedIndex = 0;
                TaiDuLieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi kết nối CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Nạp dữ liệu ----------

        private void NapCombo()
        {
            DataTable dtMonHoc = monHocBLL.LayDanhSachMonHoc();
            cboMonHoc.DataSource = dtMonHoc;
            cboMonHoc.DisplayMember = "TenMH";
            cboMonHoc.ValueMember = "MaMH";

            DataTable dtSinhVien = sinhVienBLL.LayDanhSachSinhVien();
            cboSinhVien.DataSource = dtSinhVien;
            cboSinhVien.DisplayMember = "HoTen";
            cboSinhVien.ValueMember = "MaSV";
        }

        private void TaiDuLieu()
        {
            try
            {
                dgvLichHoc.DataSource = lichHocBLL.LayDanhSachLichHoc();
                DatTieuDeCot();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi kết nối CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DatTieuDeCot()
        {
            if (dgvLichHoc.Columns.Count == 0) return;

            if (dgvLichHoc.Columns.Contains("MaLich")) dgvLichHoc.Columns["MaLich"].Visible = false;
            if (dgvLichHoc.Columns.Contains("MaMH")) dgvLichHoc.Columns["MaMH"].HeaderText = "Mã MH";
            if (dgvLichHoc.Columns.Contains("TenMH")) dgvLichHoc.Columns["TenMH"].HeaderText = "Tên môn học";
            if (dgvLichHoc.Columns.Contains("MaSV")) dgvLichHoc.Columns["MaSV"].HeaderText = "Mã SV";
            if (dgvLichHoc.Columns.Contains("HoTen")) dgvLichHoc.Columns["HoTen"].HeaderText = "Họ tên";
            if (dgvLichHoc.Columns.Contains("NgayHoc"))
            {
                dgvLichHoc.Columns["NgayHoc"].HeaderText = "Ngày học";
                dgvLichHoc.Columns["NgayHoc"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dgvLichHoc.Columns.Contains("CaHoc")) dgvLichHoc.Columns["CaHoc"].HeaderText = "Ca học";
            if (dgvLichHoc.Columns.Contains("PhongHoc")) dgvLichHoc.Columns["PhongHoc"].HeaderText = "Phòng";
        }

        private LichHocDTO LayDuLieuTuForm()
        {
            return new LichHocDTO
            {
                MaLich = maLichDangChon,
                MaMH = cboMonHoc.SelectedValue?.ToString() ?? "",
                MaSV = cboSinhVien.SelectedValue?.ToString() ?? "",
                NgayHoc = dtpNgayHoc.Value,
                CaHoc = cboCaHoc.Text,
                PhongHoc = txtPhongHoc.Text
            };
        }

        // ---------- Sự kiện ----------

        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!lichHocBLL.ThemLichHoc(LayDuLieuTuForm(), out string error))
                {
                    MessageBox.Show(this, error, "Không thêm được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, "Thêm buổi học thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (maLichDangChon <= 0)
            {
                MessageBox.Show(this, "Hãy chọn một buổi học trong bảng để sửa.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (!lichHocBLL.SuaLichHoc(LayDuLieuTuForm(), out string error))
                {
                    MessageBox.Show(this, error, "Không sửa được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, "Cập nhật buổi học thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (maLichDangChon <= 0)
            {
                MessageBox.Show(this, "Hãy chọn một buổi học trong bảng để xoá.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                this, "Bạn có chắc muốn xoá buổi học này?",
                "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (!lichHocBLL.XoaLichHoc(maLichDangChon, out string error))
                {
                    MessageBox.Show(this, error, "Không xoá được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, "Đã xoá buổi học.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void DgvLichHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvLichHoc.Rows[e.RowIndex];
            if (row.DataBoundItem is not DataRowView item) return;

            maLichDangChon = Convert.ToInt32(item["MaLich"]);

            if (item["MaMH"] != DBNull.Value) cboMonHoc.SelectedValue = item["MaMH"].ToString();
            if (item["MaSV"] != DBNull.Value) cboSinhVien.SelectedValue = item["MaSV"].ToString();

            if (item["NgayHoc"] != DBNull.Value &&
                DateTime.TryParse(item["NgayHoc"].ToString(), out DateTime ngay))
            {
                dtpNgayHoc.Value = ngay;
            }

            cboCaHoc.Text = item["CaHoc"].ToString() ?? "";
            txtPhongHoc.Text = item["PhongHoc"].ToString() ?? "";
        }

        // ---------- Tiện ích ----------

        private void LamMoi()
        {
            maLichDangChon = 0;
            txtPhongHoc.Clear();
            cboCaHoc.SelectedIndex = 0;
            dtpNgayHoc.Value = DateTime.Today;
            if (cboMonHoc.Items.Count > 0) cboMonHoc.SelectedIndex = 0;
            if (cboSinhVien.Items.Count > 0) cboSinhVien.SelectedIndex = 0;
            dgvLichHoc.ClearSelection();
            TaiDuLieu();
        }
    }
}
