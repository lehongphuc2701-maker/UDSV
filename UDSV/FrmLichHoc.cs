using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using UDSV.BLL;
using UDSV.DAL;

namespace UDSV
{
    /// <summary>
    /// Thời khoá biểu mẫu theo tuần: chọn Khóa + Kỳ, xem 7 ngày × 12 tiết.
    /// Dữ liệu lấy từ chương trình của khóa (mỗi môn một buổi/tuần: Thứ, Tiết BĐ, Tiết KT, GV).
    /// </summary>
    public partial class FrmLichHoc : Form
    {
        private readonly ChuongTrinhBLL ctBLL = new ChuongTrinhBLL();
        private readonly KhoaBLL khoaBLL = new KhoaBLL();

        private const int SoTiet = 12;
        private TableLayoutPanel? tblTuan;

        /// <summary>Đang vẽ: chặn các lần gọi VeTuan() lồng nhau (mỗi lần là một lần nháy).</summary>
        private bool _dangVe;

        private static readonly string[] Thu =
            { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật" };

        /// <summary>Khóa cố định khi mở ở chế độ sinh viên (bỏ hẳn dropdown chọn khóa).</summary>
        private readonly string? _maKhoaCoDinh;

        public FrmLichHoc() : this(null) { }

        public FrmLichHoc(string? maKhoaCoDinh)
        {
            _maKhoaCoDinh = maKhoaCoDinh;
            InitializeComponent();

            // Bộ lọc chuột: thấy MỌI cú click trước khi bất kỳ control nào nuốt mất.
            // Đây là đường chắc chắn nhất để mở hộp thoại chi tiết buổi học.
            Application.AddMessageFilter(new LocChuot(this));

            Load += FrmLichHoc_Load;
            cboKhoa.SelectedIndexChanged += (s, e) => { NapKy(); VeTuan(); };   // NapKy đã gỡ CboKy_Changed nên chỉ vẽ 1 lần
            cboKy.SelectedIndexChanged += CboKy_Changed;
        }

        private void FrmLichHoc_Load(object? sender, EventArgs e)
        {
            try
            {
                TaoLuoi();

                cboKhoa.DataSource = khoaBLL.LayDanhSach();
                cboKhoa.DisplayMember = "MaKhoa";
                cboKhoa.ValueMember = "MaKhoa";

                if (!string.IsNullOrWhiteSpace(_maKhoaCoDinh))
                {
                    // Chế độ sinh viên: khóa cố định, không cho chọn.
                    cboKhoa.SelectedValue = _maKhoaCoDinh;
                    cboKhoa.Visible = false;
                    AnNhanKhoa();
                }

                NapKy();
                VeTuan();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private string MaKhoaChon => cboKhoa.SelectedValue?.ToString() ?? "";

        private int KyChon
        {
            get
            {
                object? v = cboKy.SelectedValue;
                if (v is DataRowView dong) v = dong["HocKy"];
                return v != null && int.TryParse(v.ToString(), out int ky) ? ky : 0;
            }
        }

        /// <summary>Ẩn nhãn "Khóa học" đi kèm khi dropdown khóa bị bỏ.</summary>
        private void AnNhanKhoa()
        {
            foreach (Control c in TimTat(this))
            {
                if (c is Label l && (l.Text ?? "").Contains("Khóa học", StringComparison.OrdinalIgnoreCase))
                    l.Visible = false;
            }
        }

        private static System.Collections.Generic.IEnumerable<Control> TimTat(Control goc)
        {
            foreach (Control c in goc.Controls)
            {
                yield return c;
                foreach (var x in TimTat(c)) yield return x;
            }
        }

        private void NapKy()
        {
            cboKy.SelectedIndexChanged -= CboKy_Changed;
            cboKy.DataSource = null;
            cboKy.Items.Clear();
            cboKy.DisplayMember = "HocKy";
            cboKy.ValueMember = "HocKy";

            if (MaKhoaChon.Length > 0)
            {
                DataTable dsKy = ctBLL.LayHocKy(MaKhoaChon);
                if (dsKy.Rows.Count > 0) cboKy.DataSource = dsKy;
            }

            cboKy.SelectedIndexChanged += CboKy_Changed;
        }

        private void CboKy_Changed(object? sender, EventArgs e) => VeTuan();

        // ---------- Lưới ----------

        private void TaoLuoi()
        {
            tblTuan = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                ColumnCount = 8,
                RowCount = SoTiet + 1,
                BackColor = Color.White
            };

            // Bật vẽ kép: giảm hẳn hiện tượng nháy khi dựng lại nhiều control.
            typeof(Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(tblTuan, true);

            tblTuan.ColumnStyles.Clear();
            tblTuan.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 68));
            for (int i = 0; i < 7; i++)
            {
                tblTuan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7));
            }

            tblTuan.RowStyles.Clear();
            tblTuan.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            for (int i = 0; i < SoTiet; i++) tblTuan.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / SoTiet));

            pnlTuan.Controls.Clear();
            pnlTuan.Controls.Add(tblTuan);

            DungKhung();
        }

        /// <summary>
        /// Khung cố định của lưới (ô góc + 7 cột thứ + 12 hàng tiết).
        /// Chỉ dựng MỘT lần: trước đây mỗi lần đổi Khóa/Kỳ lại xoá và tạo lại 20 nhãn này,
        /// cộng thêm không tạm dừng layout nên màn hình nháy nhiều lần.
        /// </summary>
        private void DungKhung()
        {
            if (tblTuan == null) return;

            tblTuan.SuspendLayout();
            try
            {
                tblTuan.Controls.Add(new Label
                {
                    Text = "Tiết \\ Thứ",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    BackColor = Color.FromArgb(241, 245, 249)
                }, 0, 0);

                for (int i = 0; i < 7; i++)
                {
                    bool isCuoiTuan = (i >= 5);
                    tblTuan.Controls.Add(new Label
                    {
                        Text = Thu[i],
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                        ForeColor = isCuoiTuan ? Color.FromArgb(180, 83, 9) : Color.FromArgb(30, 41, 59),
                        BackColor = isCuoiTuan ? Color.FromArgb(254, 243, 199) : Color.FromArgb(248, 250, 252)
                    }, i + 1, 0);
                }

                for (int t = 1; t <= SoTiet; t++)
                {
                    tblTuan.Controls.Add(new Label
                    {
                        Text = $"Tiết {t}",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 8.2F, FontStyle.Regular),
                        ForeColor = Color.FromArgb(100, 116, 139),
                        BackColor = Color.FromArgb(248, 250, 252)
                    }, 0, t);
                }
            }
            finally { tblTuan.ResumeLayout(false); }
        }

        /// <summary>Chỉ thay các khối buổi học; khung lưới giữ nguyên nên không nháy.</summary>
        private void VeTuan()
        {
            if (tblTuan == null || _dangVe) return;

            _dangVe = true;
            tblTuan.SuspendLayout();
            try
            {
                // Bỏ các khối cũ (control có Tag là DataRow), giữ khung.
                for (int i = tblTuan.Controls.Count - 1; i >= 0; i--)
                {
                    if (tblTuan.Controls[i].Tag is DataRow) tblTuan.Controls.RemoveAt(i);
                }

                string maKhoa = MaKhoaChon;
                int ky = KyChon;

                if (maKhoa.Length == 0 || ky <= 0)
                {
                    lblThongTin.Text = "Chọn Khóa và Kỳ để xem thời khóa biểu";
                    lblThongTin.ForeColor = Color.FromArgb(100, 116, 139);
                    return;
                }

                DataTable ds = ctBLL.LayTuanMau(maKhoa, ky);

                if (ds.Rows.Count == 0)
                {
                    lblThongTin.Text = $"Khóa {maKhoa} - Kỳ {ky}: Chưa có lịch học mẫu";
                    lblThongTin.ForeColor = Color.FromArgb(239, 68, 68);
                    return;
                }

                lblThongTin.Text = $"Khóa {maKhoa} - Kỳ {ky}: Có {ds.Rows.Count} buổi học / tuần";
                lblThongTin.ForeColor = Color.FromArgb(5, 150, 105);

                foreach (DataRow r in ds.Rows)
                {
                    int cot = Convert.ToInt32(r["Thu"]) - 1;   // Thu 2..8 → cột 1..7
                    if (cot < 1 || cot > 7) continue;

                    int hang = Convert.ToInt32(r["TietBD"]);
                    int den = Convert.ToInt32(r["TietKT"]);
                    if (hang < 1) hang = 1;
                    if (hang > SoTiet) continue;
                    if (den > SoTiet) den = SoTiet;
                    if (den < hang) den = hang;

                    Panel khoi = TaoKhoi(r);
                    tblTuan.Controls.Add(khoi, cot, hang);
                    tblTuan.SetRowSpan(khoi, den - hang + 1);
                }
            }
            catch (Exception ex) { BaoLoi(ex); }
            finally
            {
                tblTuan.ResumeLayout(true);
                _dangVe = false;
            }
        }
        /// <summary>Khối lịch học hiển thị dạng Card phẳng với màu nhấn trang nhã</summary>
        private Panel TaoKhoi(DataRow r)
        {
            string tenMon = r["TenMH"]?.ToString() ?? "";
            string gv = r["GiangVien"] == DBNull.Value ? "" : r["GiangVien"]?.ToString() ?? "";
            string phong = r["PhongHoc"] == DBNull.Value ? "" : r["PhongHoc"]?.ToString() ?? "";
            int tietBD = Convert.ToInt32(r["TietBD"]);
            int tietKT = Convert.ToInt32(r["TietKT"]);
            string tiet = $"Tiết {tietBD} - {tietKT}";

            // Bảng màu Pastel cho từng môn học phân biệt
            Color[] bgColors = new Color[]
            {
                Color.FromArgb(238, 242, 255), // Indigo light
                Color.FromArgb(236, 253, 245), // Emerald light
                Color.FromArgb(254, 243, 199), // Amber light
                Color.FromArgb(243, 232, 255), // Purple light
                Color.FromArgb(255, 241, 242), // Rose light
                Color.FromArgb(224, 242, 254), // Sky light
                Color.FromArgb(254, 249, 195)  // Yellow light
            };
            Color[] borderColors = new Color[]
            {
                Color.FromArgb(99, 102, 241),
                Color.FromArgb(16, 185, 129),
                Color.FromArgb(245, 158, 11),
                Color.FromArgb(168, 85, 247),
                Color.FromArgb(244, 63, 94),
                Color.FromArgb(14, 165, 233),
                Color.FromArgb(234, 179, 8)
            };
            Color[] textColors = new Color[]
            {
                Color.FromArgb(49, 46, 129),
                Color.FromArgb(6, 78, 59),
                Color.FromArgb(120, 53, 15),
                Color.FromArgb(88, 28, 135),
                Color.FromArgb(136, 19, 55),
                Color.FromArgb(12, 74, 110),
                Color.FromArgb(113, 63, 18)
            };

            int idx = Math.Abs(tenMon.GetHashCode()) % bgColors.Length;
            Color bg = bgColors[idx];
            Color border = borderColors[idx];
            Color txtColor = textColors[idx];

            Panel khoi = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(2),
                BackColor = bg,
                Padding = new Padding(6, 4, 4, 4)
            };

            // Vẽ dải màu nhấn bên trái
            khoi.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var pen = new Pen(border, 1.2f);
                e.Graphics.DrawRectangle(pen, 0, 0, khoi.Width - 1, khoi.Height - 1);
                using var barBrush = new SolidBrush(border);
                e.Graphics.FillRectangle(barBrush, 0, 0, 4, khoi.Height);
            };

            string thongTinHien = $"{tenMon}\n({tiet})";
            if (!string.IsNullOrWhiteSpace(phong)) thongTinHien += $"\n🏫 {phong}";
            if (!string.IsNullOrWhiteSpace(gv)) thongTinHien += $"\n👤 {gv}";

            var lblContent = new Label
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 8.8F, FontStyle.Bold),
                ForeColor = txtColor,
                TextAlign = ContentAlignment.TopLeft,
                Text = thongTinHien
            };

            khoi.Controls.Add(lblContent);

            // Bấm vào khối (hoặc vào nhãn nằm trên khối) để xem đầy đủ thông tin.
            khoi.Tag = r;
            khoi.Cursor = Cursors.Hand;
            lblContent.Cursor = Cursors.Hand;
            khoi.Click += Khoi_Click;
            lblContent.Click += Khoi_Click;
            return khoi;
        }

        private void BaoLoi(Exception ex)
        {
            AppLog.For<FrmLichHoc>().LogError(ex, "Lỗi ở màn thời khoá biểu");
            MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>Bấm vào một buổi học: hiện hộp thoại đầy đủ (khối nhỏ hay bị cắt chữ).</summary>
        private void Khoi_Click(object? sender, EventArgs e)
        {
            if ((sender as Control)?.Tag is not DataRow r) return;
            HienChiTiet(r);
        }

        /// <summary>Hiện hộp thoại đầy đủ cho một buổi học.</summary>
        internal void HienChiTiet(DataRow r)
        {

            string tenMon = r["TenMH"]?.ToString() ?? "";
            string maMH = r["MaMH"]?.ToString() ?? "";
            int thu = Convert.ToInt32(r["Thu"]);
            string thuTen = thu == 8 ? "Chủ nhật" : $"Thứ {thu}";
            string gv = r["GiangVien"] == DBNull.Value ? "(chưa phân công)" : r["GiangVien"]?.ToString() ?? "";
            string phong = r["PhongHoc"] == DBNull.Value ? "(chưa xếp phòng)" : r["PhongHoc"]?.ToString() ?? "";

            string noiDung =
                $"Môn học     : {tenMon}\r\n" +
                $"Mã môn      : {maMH}\r\n" +
                $"Lớp         : {MaKhoaChon} — Học kỳ {KyChon}\r\n" +
                $"Thứ         : {thuTen}\r\n" +
                $"Tiết        : {r["TietBD"]} - {r["TietKT"]}\r\n" +
                $"Phòng học   : {phong}\r\n" +
                $"Giảng viên  : {gv}";

            MessageBox.Show(this, noiDung, "Chi tiết buổi học",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>Bắt WM_LBUTTONUP ở tầng ứng dụng rồi tự tìm khối dưới con trỏ.</summary>
        private sealed class LocChuot : IMessageFilter
        {
            private const int WM_LBUTTONUP = 0x0202;
            private readonly FrmLichHoc _form;

            public LocChuot(FrmLichHoc form) { _form = form; }

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg != WM_LBUTTONUP) return false;

                var diem = Cursor.Position;
                foreach (Control c in TimTat(_form))
                {
                    if (c is Panel p && p.Tag is DataRow r &&
                        p.RectangleToScreen(p.ClientRectangle).Contains(diem))
                    {
                        _form.HienChiTiet(r);
                        return true;      // nuốt luôn, tránh mở hai lần
                    }
                }
                return false;
            }

            private static System.Collections.Generic.IEnumerable<Control> TimTat(Control goc)
            {
                foreach (Control c in goc.Controls)
                {
                    yield return c;
                    foreach (var x in TimTat(c)) yield return x;
                }
            }
        }
    }
}
