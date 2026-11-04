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

        private static readonly string[] Thu =
            { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật" };

        public FrmLichHoc()
        {
            InitializeComponent();

            Load += FrmLichHoc_Load;
            cboKhoa.SelectedIndexChanged += (s, e) => { NapKy(); VeTuan(); };
            cboKy.SelectedIndexChanged += (s, e) => VeTuan();
        }

        private void FrmLichHoc_Load(object? sender, EventArgs e)
        {
            try
            {
                TaoLuoi();

                cboKhoa.DataSource = khoaBLL.LayDanhSach();
                cboKhoa.DisplayMember = "MaKhoa";
                cboKhoa.ValueMember = "MaKhoa";

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

            tblTuan.ColumnStyles.Clear();
            tblTuan.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 68));
            for (int i = 0; i < 7; i++)
            {
                tblTuan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7));
            }

            tblTuan.RowStyles.Clear();
            tblTuan.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            for (int i = 0; i < SoTiet; i++)
            {
                tblTuan.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / SoTiet));
            }

            pnlTuan.Controls.Clear();
            pnlTuan.Controls.Add(tblTuan);
        }

        private void VeTuan()
        {
            if (tblTuan == null) return;

            tblTuan.Controls.Clear();

            // Ô góc trên bên trái: Tiết
            var lblGocTiet = new Label
            {
                Text = "Tiết \\ Thứ",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            tblTuan.Controls.Add(lblGocTiet, 0, 0);

            // Các cột Thứ 2 đến Chủ nhật
            for (int i = 0; i < 7; i++)
            {
                bool isCuoiTuan = (i >= 5);
                var lblThu = new Label
                {
                    Text = Thu[i],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = isCuoiTuan ? Color.FromArgb(180, 83, 9) : Color.FromArgb(30, 41, 59),
                    BackColor = isCuoiTuan ? Color.FromArgb(254, 243, 199) : Color.FromArgb(248, 250, 252)
                };
                tblTuan.Controls.Add(lblThu, i + 1, 0);
            }

            // Các hàng Tiết 1 đến Tiết 12
            for (int t = 1; t <= SoTiet; t++)
            {
                var lblTiet = new Label
                {
                    Text = $"Tiết {t}",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8.2F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    BackColor = Color.FromArgb(248, 250, 252)
                };
                tblTuan.Controls.Add(lblTiet, 0, t);
            }

            if (MaKhoaChon.Length == 0 || KyChon <= 0)
            {
                lblThongTin.Text = "Chọn Khóa và Kỳ để xem thời khóa biểu";
                lblThongTin.ForeColor = Color.FromArgb(100, 116, 139);
                return;
            }

            try
            {
                DataTable ds = ctBLL.LayTuanMau(MaKhoaChon, KyChon);

                if (ds.Rows.Count == 0)
                {
                    lblThongTin.Text = $"Khóa {MaKhoaChon} - Kỳ {KyChon}: Chưa có lịch học mẫu";
                    lblThongTin.ForeColor = Color.FromArgb(239, 68, 68);
                    return;
                }

                lblThongTin.Text = $"Khóa {MaKhoaChon} - Kỳ {KyChon}: Có {ds.Rows.Count} buổi học / tuần";
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
        }

        /// <summary>Khối lịch học hiển thị dạng Card phẳng với màu nhấn trang nhã</summary>
        private static Panel TaoKhoi(DataRow r)
        {
            string tenMon = r["TenMH"]?.ToString() ?? "";
            string gv = r["GiangVien"] == DBNull.Value ? "" : r["GiangVien"]?.ToString() ?? "";
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

            string thongTinHien = string.IsNullOrWhiteSpace(gv)
                ? $"{tenMon}\n({tiet})"
                : $"{tenMon}\n({tiet})\n👤 {gv}";

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
            return khoi;
        }

        private void BaoLoi(Exception ex)
        {
            AppLog.For<FrmLichHoc>().LogError(ex, "Lỗi ở màn thời khoá biểu");
            MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}