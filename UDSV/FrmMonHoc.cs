using System;
using System.Data;
using System.Windows.Forms;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV
{
    /// <summary>Quản lý môn học & học phần — kiến trúc 3 lớp: UI → BLL → DAL.</summary>
    public partial class FrmMonHoc : Form
    {
        private readonly MonHocBLL bll = new MonHocBLL();
        private DataTable? dtMonHoc;
        private string maDangChon = "";

        /// <summary>
        /// Giảng viên của môn — màn này không còn ô nhập (giảng viên thuộc lớp của khóa),
        /// nhưng phải giữ nguyên giá trị cũ khi sửa, nếu không cột GiangVien sẽ bị ghi đè thành rỗng.
        /// </summary>
        private string _giangVienCu = "";

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
            btnTimKiem.Click += BtnTimKiem_Click;
            txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
            txtTimKiem.KeyDown += TxtTimKiem_KeyDown;
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

        private void FrmMonHoc_Load(object? sender, EventArgs e)
        {
            TaiDuLieu();
        }

        // ---------- Dữ liệu ----------

        private void TaiDuLieu()
        {
            try
            {
                dtMonHoc = bll.LayDanhSachMonHoc();
                dgvMonHoc.DataSource = dtMonHoc;
                DatTieuDeCot();
                CapNhatThongKe();
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

            if (dgvMonHoc.Columns.Contains("MaMH"))
            {
                dgvMonHoc.Columns["MaMH"].HeaderText = "Mã môn học";
                dgvMonHoc.Columns["MaMH"].FillWeight = 25;
            }
            if (dgvMonHoc.Columns.Contains("TenMH"))
            {
                dgvMonHoc.Columns["TenMH"].HeaderText = "Tên môn học";
                dgvMonHoc.Columns["TenMH"].FillWeight = 50;
            }
            if (dgvMonHoc.Columns.Contains("SoTinChi"))
            {
                dgvMonHoc.Columns["SoTinChi"].HeaderText = "Số tín chỉ";
                dgvMonHoc.Columns["SoTinChi"].FillWeight = 25;
            }
            if (dgvMonHoc.Columns.Contains("GiangVien"))
            {
                dgvMonHoc.Columns["GiangVien"].HeaderText = "Giảng viên";
                dgvMonHoc.Columns["GiangVien"].FillWeight = 35;
            }
        }

        private void CapNhatThongKe()
        {
            if (dtMonHoc == null) return;

            int tongMon = dtMonHoc.Rows.Count;
            int tongTinChi = MonHocBLL.TongTinChi(dtMonHoc);
            int hienThi = dgvMonHoc.Rows.Count;

            if (hienThi == tongMon)
            {
                lblThongKe.Text = $"📊 Tổng: {tongMon} môn học  |  {tongTinChi} tín chỉ";
            }
            else
            {
                lblThongKe.Text = $"🔍 Tìm thấy: {hienThi} / {tongMon} môn học";
            }

            this.Text = $"Quản lý môn học — {tongMon} môn, {tongTinChi} tín chỉ";
        }

        private MonHocDTO LayDuLieuTuForm()
        {
            int.TryParse(txtSoTinChi.Text.Trim(), out int soTinChi);

            return new MonHocDTO
            {
                MaMH = txtMaMH.Text.Trim(),
                TenMH = txtTenMH.Text.Trim(),
                SoTinChi = soTinChi,
                GiangVien = _giangVienCu
            };
        }

        // ---------- Tìm kiếm ----------

        private void ApDungBoLoc()
        {
            if (dtMonHoc == null) return;

            string keyword = txtTimKiem.Text.Trim().Replace("'", "''");
            if (string.IsNullOrEmpty(keyword))
            {
                dtMonHoc.DefaultView.RowFilter = "";
            }
            else
            {
                dtMonHoc.DefaultView.RowFilter = $"MaMH LIKE '%{keyword}%' OR TenMH LIKE '%{keyword}%'";
            }

            CapNhatThongKe();
        }

        private void TxtTimKiem_TextChanged(object? sender, EventArgs e)
        {
            ApDungBoLoc();
        }

        private void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            ApDungBoLoc();
        }

        private void TxtTimKiem_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApDungBoLoc();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                txtTimKiem.Clear();
            }
        }

        // ---------- Thao tác CRUD ----------

        private void BtnThem_Click(object? sender, EventArgs e)
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

        private void BtnSua_Click(object? sender, EventArgs e)
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

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show(this, "Hãy chọn một môn học trong bảng để xoá.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                this, $"Bạn có chắc muốn xoá môn '{maDangChon} - {txtTenMH.Text}'?",
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

                MessageBox.Show(this, "Đã xoá môn học thành công.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            LamMoi();
        }

        private void DgvMonHoc_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvMonHoc.Rows[e.RowIndex];
            if (row.DataBoundItem is not DataRowView item) return;

            maDangChon = item["MaMH"].ToString() ?? "";
            txtMaMH.Text = maDangChon;
            txtTenMH.Text = item["TenMH"].ToString() ?? "";
            txtSoTinChi.Text = item["SoTinChi"].ToString() ?? "";

            _giangVienCu = item.Row.Table.Columns.Contains("GiangVien") && item["GiangVien"] != DBNull.Value
                ? item["GiangVien"].ToString() ?? ""
                : "";

            txtMaMH.ReadOnly = true;
        }

        // ---------- Tiện ích ----------

        private void LamMoi()
        {
            maDangChon = "";
            txtMaMH.Clear();
            txtTenMH.Clear();
            txtSoTinChi.Clear();

            txtTimKiem.Clear();
            txtMaMH.ReadOnly = false;
            dgvMonHoc.ClearSelection();
            txtMaMH.Focus();
            TaiDuLieu();
        }
    }
}