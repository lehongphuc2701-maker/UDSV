using System;
using System.Data;
using System.Windows.Forms;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV
{
    /// <summary>Quản lý môn học — cùng cấu trúc với FrmSinhVien: UI → BLL → DAL.</summary>
    public partial class FrmMonHoc : Form
    {
        private readonly MonHocBLL bll = new MonHocBLL();
        private string maDangChon = "";

        public FrmMonHoc()
        {
            InitializeComponent();
            GanSuKien();
            CauHinhLuoi();
        }

        private void GanSuKien()
        {
            Load += FrmMonHoc_Load;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;
            dgvMonHoc.CellClick += DgvMonHoc_CellClick;
        }

        private void CauHinhLuoi()
        {
            dgvMonHoc.AutoGenerateColumns = true;
            dgvMonHoc.ReadOnly = true;
            dgvMonHoc.AllowUserToAddRows = false;
            dgvMonHoc.AllowUserToDeleteRows = false;
            dgvMonHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMonHoc.MultiSelect = false;
            dgvMonHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FrmMonHoc_Load(object sender, EventArgs e)
        {
            TaiDuLieu();
        }

        // ---------- Dữ liệu ----------

        private void TaiDuLieu()
        {
            try
            {
                DataTable dt = bll.LayDanhSachMonHoc();
                dgvMonHoc.DataSource = dt;
                DatTieuDeCot();

                // Cho người dùng thấy ngay tổng tín chỉ của chương trình
                this.Text = $"Quản lí môn học — {dt.Rows.Count} môn, {MonHocBLL.TongTinChi(dt)} tín chỉ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi kết nối CSDL",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DatTieuDeCot()
        {
            if (dgvMonHoc.Columns.Count == 0) return;

            if (dgvMonHoc.Columns.Contains("MaMH")) dgvMonHoc.Columns["MaMH"].HeaderText = "Mã MH";
            if (dgvMonHoc.Columns.Contains("TenMH")) dgvMonHoc.Columns["TenMH"].HeaderText = "Tên môn học";
            if (dgvMonHoc.Columns.Contains("SoTinChi")) dgvMonHoc.Columns["SoTinChi"].HeaderText = "Số tín chỉ";
            if (dgvMonHoc.Columns.Contains("GiangVien")) dgvMonHoc.Columns["GiangVien"].HeaderText = "Giảng viên";
        }

        private MonHocDTO LayDuLieuTuForm()
        {
            int.TryParse(txtSoTinChi.Text.Trim(), out int soTinChi);

            return new MonHocDTO
            {
                MaMH = txtMaMH.Text,
                TenMH = txtTenMH.Text,
                SoTinChi = soTinChi,
                GiangVien = txtGiangVien.Text
            };
        }

        // ---------- Sự kiện ----------

        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bll.ThemMonHoc(LayDuLieuTuForm(), out string error))
                {
                    MessageBox.Show(this, error, "Không thêm được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, "Thêm môn học thành công!", "Thành công",
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
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show(this, "Hãy chọn một môn học trong bảng để sửa.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (!bll.SuaMonHoc(LayDuLieuTuForm(), out string error))
                {
                    MessageBox.Show(this, error, "Không sửa được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, "Cập nhật môn học thành công!", "Thành công",
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
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show(this, "Hãy chọn một môn học trong bảng để xoá.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                this, $"Bạn có chắc muốn xoá môn {maDangChon} - {txtTenMH.Text}?",
                "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (!bll.XoaMonHoc(maDangChon, out string error))
                {
                    MessageBox.Show(this, error, "Không xoá được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show(this, "Đã xoá môn học.", "Thành công",
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

        private void DgvMonHoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvMonHoc.Rows[e.RowIndex];
            if (row.DataBoundItem is not DataRowView item) return;

            maDangChon = item["MaMH"].ToString() ?? "";
            txtMaMH.Text = maDangChon;
            txtTenMH.Text = item["TenMH"].ToString() ?? "";
            txtSoTinChi.Text = item["SoTinChi"].ToString() ?? "";
            txtGiangVien.Text = item["GiangVien"].ToString() ?? "";

            txtMaMH.ReadOnly = true;
        }

        // ---------- Tiện ích ----------

        private void LamMoi()
        {
            maDangChon = "";
            txtMaMH.Clear();
            txtTenMH.Clear();
            txtSoTinChi.Clear();
            txtGiangVien.Clear();
            txtMaMH.ReadOnly = false;
            dgvMonHoc.ClearSelection();
            txtMaMH.Focus();
            TaiDuLieu();
        }
    }
}
