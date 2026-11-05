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

        // Hai điều khiển thêm bằng mã (không sửa file Designer): chọn cách xem + nút Phục hồi
        private Label lblXem = null!;
        private ComboBox cboXem = null!;
        private Button btnPhucHoi = null!;

        /// <summary>
        /// Giảng viên của môn — màn này không còn ô nhập (giảng viên thuộc lớp của khóa),
        /// nhưng phải giữ nguyên giá trị cũ khi sửa, nếu không cột GiangVien sẽ bị ghi đè thành rỗng.
        /// </summary>
        private string _giangVienCu = "";

        public FrmMonHoc()
        {
            InitializeComponent();
            TaoBoLocXem();
            TaoNutPhucHoi();
            CapNhatNutPhucHoi();   // ban đầu xem "Danh sách" nên nút Phục hồi phải tắt
            GanSuKien();
            CauHinhLuoi();
        }

        /// <summary>true khi đang xem thùng rác (môn đã xoá mềm).</summary>
        private bool DangXemDaXoa => cboXem.SelectedIndex == 1;

        /// <summary>
        /// Dải "Hiển thị: Danh sách / Đã xóa" ngay trên lưới. Lưới được đẩy xuống một chút
        /// để nhường chỗ, mép dưới giữ nguyên nên phần còn lại của form không phải sửa.
        /// </summary>
        private void TaoBoLocXem()
        {
            lblXem = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 116, 139),
                Name = "lblXem",
                Text = "Hiển thị:"
            };

            cboXem = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                FormattingEnabled = true,
                Name = "cboXem",
                Size = new Size(170, 31)
            };
            cboXem.Items.AddRange(new object[] { "Danh sách", "Đã xóa" });

            cardData.Controls.Add(lblXem);
            cardData.Controls.Add(cboXem);

            int mepTrenLuoi = dgvMonHoc.Top;
            int chieuCaoDai = cboXem.Height + 12;
            dgvMonHoc.Top = mepTrenLuoi + chieuCaoDai;
            dgvMonHoc.Height -= chieuCaoDai;

            // Đo chữ để đặt ô chọn ngay sau nhãn mà không phụ thuộc vào AutoSize
            Size rongNhan = TextRenderer.MeasureText(lblXem.Text, lblXem.Font);
            lblXem.Location = new Point(dgvMonHoc.Left, mepTrenLuoi + 5);
            cboXem.Location = new Point(dgvMonHoc.Left + rongNhan.Width + 10, mepTrenLuoi);

            // Đặt mục mặc định rồi mới gắn sự kiện: mở form chỉ tải dữ liệu một lần (ở Load)
            cboXem.SelectedIndex = 0;
            cboXem.SelectedIndexChanged += CboXem_Changed;
        }

        /// <summary>Nút Phục hồi xếp sau nút Làm mới trong hàng nút của thẻ nhập liệu.</summary>
        private void TaoNutPhucHoi()
        {
            btnPhucHoi = new Button
            {
                BackColor = Color.FromArgb(13, 148, 136),
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(btnLamMoi.Right + 15, btnLamMoi.Top),
                Name = "btnPhucHoi",
                Size = new Size(125, 40),
                Text = "♻️ Phục hồi",
                UseVisualStyleBackColor = false
            };
            btnPhucHoi.FlatAppearance.BorderSize = 0;

            cardForm.Controls.Add(btnPhucHoi);
        }

        private void GanSuKien()
        {
            Load += FrmMonHoc_Load;
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnPhucHoi.Click += BtnPhucHoi_Click;
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
                // Đang xem danh sách hay thùng rác là do ô "Hiển thị" quyết định
                dtMonHoc = bll.LayDanhSach(DangXemDaXoa);
                dgvMonHoc.DataSource = dtMonHoc;
                DatTieuDeCot();
                ApDungBoLoc();   // áp lại từ khoá tìm kiếm + cập nhật dòng thống kê
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, ex.Message, "Lỗi kết nối CSDL",
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

            // Cột cờ xoá mềm là chi tiết kỹ thuật — cách xem "Danh sách / Đã xóa" đã nói lên điều đó
            if (dgvMonHoc.Columns.Contains("DaXoa"))
                dgvMonHoc.Columns["DaXoa"].Visible = false;
        }

        private void CapNhatThongKe()
        {
            if (dtMonHoc == null) return;

            int tongMon = dtMonHoc.Rows.Count;
            int tongTinChi = MonHocBLL.TongTinChi(dtMonHoc);
            int hienThi = dgvMonHoc.Rows.Count;
            string nhan = DangXemDaXoa ? "🗑 Thùng rác" : "📊 Tổng";

            if (hienThi == tongMon)
            {
                lblThongKe.Text = $"{nhan}: {tongMon} môn học  |  {tongTinChi} tín chỉ";
            }
            else
            {
                lblThongKe.Text = $"🔍 Tìm thấy: {hienThi} / {tongMon} môn học";
            }

            this.Text = (DangXemDaXoa ? "Môn học đã xóa" : "Quản lý môn học") +
                        $" — {tongMon} môn, {tongTinChi} tín chỉ";
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
                    ThongBao.Show(this, error, "Không thêm được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ThongBao.Show(this, "Thêm môn học thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                ThongBao.Show(this, "Hãy chọn một môn học trong bảng để sửa.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (!bll.SuaMonHoc(LayDuLieuTuForm(), out string error))
                {
                    ThongBao.Show(this, error, "Không sửa được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ThongBao.Show(this, "Cập nhật môn học thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xoá MỀM: chỉ bật cờ DaXoa, môn vẫn nằm trong bảng để có thể phục hồi.
        /// BLL từ chối khi môn còn nằm trong chương trình của khóa nào — thông báo của BLL
        /// được hiện nguyên văn vì nó đã nói rõ đang vướng những khóa nào.
        /// </summary>
        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                ThongBao.Show(this, "Hãy chọn một môn học trong bảng để xoá.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = ThongBao.Show(
                this, $"Bạn có chắc muốn xoá môn '{maDangChon} - {txtTenMH.Text}'?\n\n" +
                      "Môn sẽ được chuyển vào 'Đã xóa' (không mất dữ liệu) và có thể phục hồi sau.",
                "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (!bll.XoaMem(maDangChon, out string error))
                {
                    ThongBao.Show(this, error, "Không xoá được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ThongBao.Show(this, "Đã chuyển môn học vào 'Đã xóa'.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Phục hồi môn đã xoá mềm — chỉ dùng được khi đang xem "Đã xóa".</summary>
        private void BtnPhucHoi_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                ThongBao.Show(this, "Hãy chọn một môn học trong danh sách 'Đã xóa' để phục hồi.",
                    "Chưa chọn dòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = ThongBao.Show(
                this, $"Phục hồi môn '{maDangChon} - {txtTenMH.Text}'?",
                "Xác nhận phục hồi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                if (!bll.PhucHoi(maDangChon, out string error))
                {
                    ThongBao.Show(this, error, "Không phục hồi được",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ThongBao.Show(this, "Đã phục hồi môn học.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LamMoi();
            }
            catch (Exception ex)
            {
                ThongBao.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboXem_Changed(object? sender, EventArgs e)
        {
            CapNhatNutPhucHoi();

            // Đổi cách xem thì dòng đang chọn không còn ý nghĩa
            maDangChon = "";
            dgvMonHoc.ClearSelection();

            TaiDuLieu();
        }

        /// <summary>Nút Phục hồi chỉ bật khi đang xem thùng rác; tắt thì chuyển sang màu xám.</summary>
        private void CapNhatNutPhucHoi()
        {
            bool bat = DangXemDaXoa;

            btnPhucHoi.Enabled = bat;
            btnPhucHoi.Cursor = bat ? Cursors.Hand : Cursors.Default;
            btnPhucHoi.BackColor = bat ? Color.FromArgb(13, 148, 136) : Color.FromArgb(203, 213, 225);
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