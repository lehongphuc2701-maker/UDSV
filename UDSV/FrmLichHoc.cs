using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using UDSV.BLL;
using UDSV.DAL;

namespace UDSV
{
    /// <summary>
    /// Thời khoá biểu theo tuần: chọn Khóa + Kỳ, xem 7 ngày × 12 tiết.
    /// Mỗi buổi học là một khối chiếm TietBD..TietKT dòng (RowSpan) — giống bảng tính.
    /// </summary>
    public partial class FrmLichHoc : Form
    {
        private readonly LichHocBLL lhBLL = new LichHocBLL();
        private readonly KhoaBLL khoaBLL = new KhoaBLL();
        private readonly ChuongTrinhBLL ctBLL = new ChuongTrinhBLL();

        private const int SoTiet = 12;
        private DateTime ngayDauTuan;      // luôn là thứ 2

        private TableLayoutPanel? tblTuan;

        private static readonly string[] Thu = { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ nhật" };

        public FrmLichHoc()
        {
            InitializeComponent();

            ngayDauTuan = VeThu2(DateTime.Today);

            Load += FrmLichHoc_Load;
            cboKhoa.SelectedIndexChanged += (s, e) => { NapKy(); VeTuan(); };
            cboKy.SelectedIndexChanged += (s, e) => VeTuan();
            btnTruoc.Click += (s, e) => { ngayDauTuan = ngayDauTuan.AddDays(-7); VeTuan(); };
            btnSau.Click += (s, e) => { ngayDauTuan = ngayDauTuan.AddDays(7); VeTuan(); };
            btnTuanNay.Click += (s, e) => { ngayDauTuan = VeThu2(DateTime.Today); VeTuan(); };
        }

        private static DateTime VeThu2(DateTime ngay)
        {
            int lech = ((int)ngay.DayOfWeek + 6) % 7;   // Thứ 2 = 0 … Chủ nhật = 6
            return ngay.Date.AddDays(-lech);
        }

        private void FrmLichHoc_Load(object sender, EventArgs e)
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
                object v = cboKy.SelectedValue;
                if (v is DataRowView dong) v = dong["HocKy"];
                return v != null && int.TryParse(v.ToString(), out int ky) ? ky : 0;
            }
        }

        private void NapKy()
        {
            cboKy.SelectedIndexChanged -= CboKy_Changed;
            cboKy.DataSource = null;

            if (MaKhoaChon.Length > 0)
            {
                DataTable dsKy = ctBLL.LayHocKy(MaKhoaChon);
                cboKy.DataSource = dsKy;
                cboKy.DisplayMember = "HocKy";
                cboKy.ValueMember = "HocKy";
            }

            cboKy.SelectedIndexChanged += CboKy_Changed;
        }

        private void CboKy_Changed(object sender, EventArgs e) => VeTuan();

        // ---------- Dựng lưới ----------

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
            tblTuan.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 58));
            for (int i = 0; i < 7; i++) tblTuan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7));

            tblTuan.RowStyles.Clear();
            tblTuan.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            for (int i = 0; i < SoTiet; i++) tblTuan.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / SoTiet));

            pnlTuan.Controls.Clear();
            pnlTuan.Controls.Add(tblTuan);
        }

        private void VeTuan()
        {
            if (tblTuan == null) return;

            tblTuan.Controls.Clear();

            DateTime cuoiTuan = ngayDauTuan.AddDays(6);
            lblTuan.Text = $"Tuần {ngayDauTuan:dd/MM} – {cuoiTuan:dd/MM/yyyy}";

            // tiêu đề cột
            tblTuan.Controls.Add(new Label
            {
                Text = "Tiết",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            }, 0, 0);

            for (int i = 0; i < 7; i++)
            {
                DateTime ngay = ngayDauTuan.AddDays(i);
                tblTuan.Controls.Add(new Label
                {
                    Text = $"{Thu[i]}\n{ngay:dd/MM/yyyy}",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = SystemColors.Highlight
                }, i + 1, 0);
            }

            // nhãn tiết
            for (int t = 1; t <= SoTiet; t++)
            {
                tblTuan.Controls.Add(new Label
                {
                    Text = $"Tiết {t}",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = Color.DimGray
                }, 0, t);
            }

            if (MaKhoaChon.Length == 0 || KyChon <= 0) return;

            try
            {
                DataTable ds = lhBLL.LayTheoTuan(MaKhoaChon, KyChon, ngayDauTuan, cuoiTuan);

                foreach (DataRow r in ds.Rows)
                {
                    DateTime ngay = Convert.ToDateTime(r["NgayHoc"]);
                    int cot = ((int)ngay.DayOfWeek + 6) % 7 + 1;
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

        /// <summary>Một buổi học: môn, phòng, giờ, giảng viên.</summary>
        private static Panel TaoKhoi(DataRow r)
        {
            string tenMon = r["TenMH"].ToString() ?? "";
            string phong = r["PhongHoc"].ToString() ?? "";
            string gio = r["CaHoc"].ToString() ?? "";
            string gv = r["GiangVien"] == DBNull.Value ? "" : r["GiangVien"].ToString() ?? "";

            Panel khoi = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(1),
                BackColor = Color.FromArgb(198, 239, 206),   // xanh lá nhạt như Excel
                BorderStyle = BorderStyle.FixedSingle
            };

            khoi.Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(3),
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = Color.Black,
                Text = $"{tenMon}\n{phong} · {gio}\n{gv}"
            });

            return khoi;
        }

        private void BaoLoi(Exception ex)
        {
            AppLog.For<FrmLichHoc>().LogError(ex, "Lỗi ở màn thời khoá biểu");
            MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
