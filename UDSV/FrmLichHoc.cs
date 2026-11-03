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
    /// Thời khoá biểu mẫu theo tuần: chọn Khóa + Kỳ, xem 7 ngày × 12 tiết.
    /// Dữ liệu lấy từ chương trình của khóa (mỗi môn một buổi/tuần: Thứ, Tiết BĐ, Tiết KT, GV) —
    /// KHÔNG dùng ngày tháng, vì buổi học lặp lại hằng tuần.
    /// Mỗi buổi là một khối chiếm TietBD..TietKT dòng (RowSpan) — giống bảng tính.
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

        private void CboKy_Changed(object sender, EventArgs e) => VeTuan();

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
            tblTuan.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 58));
            for (int i = 0; i < 7; i++) tblTuan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7));

            tblTuan.RowStyles.Clear();
            tblTuan.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
            for (int i = 0; i < SoTiet; i++) tblTuan.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / SoTiet));

            pnlTuan.Controls.Clear();
            pnlTuan.Controls.Add(tblTuan);
        }

        private void VeTuan()
        {
            if (tblTuan == null) return;

            tblTuan.Controls.Clear();

            tblTuan.Controls.Add(new Label
            {
                Text = "Tiết",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            }, 0, 0);

            for (int i = 0; i < 7; i++)
            {
                tblTuan.Controls.Add(new Label
                {
                    Text = Thu[i],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = SystemColors.Highlight
                }, i + 1, 0);
            }

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
                DataTable ds = ctBLL.LayTuanMau(MaKhoaChon, KyChon);

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

        /// <summary>Một buổi học: môn, giảng viên, và khoảng tiết (đã thể hiện bằng chiều cao khối).</summary>
        private static Panel TaoKhoi(DataRow r)
        {
            string tenMon = r["TenMH"].ToString() ?? "";
            string gv = r["GiangVien"] == DBNull.Value ? "" : r["GiangVien"].ToString() ?? "";
            string tiet = $"Tiết {r["TietBD"]}-{r["TietKT"]}";

            Panel khoi = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(1),
                BackColor = Color.FromArgb(198, 239, 206),
                BorderStyle = BorderStyle.FixedSingle
            };

            khoi.Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(3),
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Black,
                Text = $"{tenMon}\n{GVHien(tenMon, gv, tiet)}"
            });

            return khoi;
        }

        private static string GVHien(string tenMon, string gv, string tiet)
            => string.IsNullOrWhiteSpace(gv) ? tiet : $"{tiet} · {gv}";

        private void BaoLoi(Exception ex)
        {
            AppLog.For<FrmLichHoc>().LogError(ex, "Lỗi ở màn thời khoá biểu");
            MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
