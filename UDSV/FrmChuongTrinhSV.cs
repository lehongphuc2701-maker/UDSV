using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using UDSV.BLL;
using UDSV.DAL;

namespace UDSV
{
    /// <summary>
    /// Chương trình đào tạo của khóa — dành cho sinh viên: toàn màn hình, CHỈ XEM,
    /// thêm cột Điểm và Trạng thái đạt/chưa đạt của chính sinh viên đó.
    /// </summary>
    public class FrmChuongTrinhSV : Form
    {
        private readonly ChuongTrinhBLL ctBLL = new ChuongTrinhBLL();
        private readonly KetQuaBLL kqBLL = new KetQuaBLL();

        private readonly string maSV;
        private readonly string maKhoa;
        private readonly string tenKhoa;

        private ComboBox cboHocKy = null!;
        private DataGridView dgv = null!;
        private Label lblTong = null!;
        private Label lblDem = null!;

        private const int TongTinChiChuongTrinh = 120;

        public FrmChuongTrinhSV(string maSV, string maKhoa, string tenKhoa)
        {
            this.maSV = maSV ?? "";
            this.maKhoa = maKhoa ?? "";
            this.tenKhoa = tenKhoa ?? "";

            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Chương trình đào tạo khóa";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;      // phủ toàn màn hình
            BackColor = Color.FromArgb(246, 248, 252);
            Font = new Font("Segoe UI", 10F);

            // ---- đầu trang ----
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 92,
                BackColor = Color.FromArgb(109, 76, 219),
                Padding = new Padding(28, 14, 28, 10)
            };
            header.Controls.Add(new Label
            {
                Text = "CHƯƠNG TRÌNH ĐÀO TẠO KHÓA",
                Dock = DockStyle.Top,
                Height = 40,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 19F, FontStyle.Bold)
            });
            var phu = new Label
            {
                Name = "lblPhu",
                Text = $"Khóa {maKhoa} — {tenKhoa} · Sinh viên: {maSV}",
                Dock = DockStyle.Top,
                Height = 26,
                ForeColor = Color.FromArgb(226, 232, 240),
                Font = new Font("Segoe UI", 10F)
            };
            header.Controls.Add(phu);

            // ---- thanh lọc học kỳ ----
            var loc = new Panel { Dock = DockStyle.Top, Height = 58, BackColor = Color.White, Padding = new Padding(28, 14, 28, 8) };
            loc.Controls.Add(new Label
            {
                Text = "Học kỳ:",
                AutoSize = true,
                Location = new Point(28, 18),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(79, 70, 229)
            });
            cboHocKy = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(100, 14),
                Width = 110,
                Font = new Font("Segoe UI", 10F)
            };
            cboHocKy.Items.AddRange(Enumerable.Range(1, 10).Select(i => (object)i).ToArray());
            cboHocKy.SelectedIndex = 0;
            cboHocKy.SelectedIndexChanged += (s, e) => NapLuoi();
            loc.Controls.Add(cboHocKy);

            lblDem = new Label
            {
                AutoSize = true,
                Location = new Point(240, 18),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(5, 150, 105)
            };
            loc.Controls.Add(lblDem);

            var ghiChu = new Label
            {
                Text = "🔒 Chỉ xem — sinh viên không sửa được chương trình",
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(148, 163, 184)
            };
            loc.Controls.Add(ghiChu);
            loc.Resize += (s, e) => ghiChu.Location = new Point(loc.Width - ghiChu.Width - 28, 20);

            // ---- lưới ----
            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,                       // không cho sửa
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                Font = new Font("Segoe UI", 9.5F)
            };
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(238, 242, 255);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(49, 46, 129);
            dgv.EnableHeadersVisualStyles = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.ColumnHeadersHeight = 42;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.RowTemplate.Height = 34;

            // Giống lưới bên màn quản trị: tiêu đề tím, chữ trắng.
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(109, 76, 219);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(109, 76, 219);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(4, 0, 4, 0);

            // ---- dòng tổng kết ----
            var footer = new Panel { Dock = DockStyle.Bottom, Height = 54, BackColor = Color.White };
            lblTong = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(28, 0, 0, 0),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59)
            };
            footer.Controls.Add(lblTong);

            // Thứ tự thêm quyết định thứ tự dock: Fill trước, rồi Bottom, rồi Top (Top sau cùng nằm trên cùng).
            Controls.Add(dgv);
            Controls.Add(footer);
            Controls.Add(loc);
            Controls.Add(header);

            Load += (s, e) => { ChonHocKyMacDinh(); NapLuoi(); };
        }

        /// <summary>Chọn sẵn học kỳ đang học theo ngày hiện tại; không tính được thì HK1.</summary>
        private void ChonHocKyMacDinh()
        {
            try
            {
                DataTable sv = new SinhVienBLL().LayTheoMa(maSV);
                if (sv.Rows.Count == 0) return;

                int tuNam = 2024;
                var k = new KhoaBLL().LayDanhSach();
                var dong = k.Select($"MaKhoa = '{maKhoa.Replace("'", "''")}'").FirstOrDefault();
                if (dong != null) tuNam = Convert.ToInt32(dong["TuNam"]);

                // 3 học kỳ / năm: HK1 từ tháng 9, HK2 từ tháng 1, HK3 từ tháng 5
                int thang = DateTime.Now.Month;
                int namHoc = DateTime.Now.Year - tuNam;
                int ky = thang >= 9 ? namHoc * 3 + 1
                        : thang >= 5 ? namHoc * 3
                        : namHoc * 3 - 1;

                ky = Math.Clamp(ky, 1, 10);
                cboHocKy.SelectedIndex = ky - 1;
            }
            catch { /* để nguyên HK1 */ }
        }

        private void NapLuoi()
        {
            try
            {
                int ky = cboHocKy.SelectedItem is int so ? so : 1;
                DataTable ds = ctBLL.LayChuongTrinhCoDiem(maKhoa, ky, maSV);

                dgv.DataSource = null;
                dgv.Columns.Clear();

                var bang = new DataTable();
                foreach (var cot in new[] { "Mã MH", "Tên môn", "TC", "Bắt buộc", "Thứ",
                                            "Tiết BĐ", "Tiết KT", "Giảng viên", "Phòng học",
                                            "Điểm", "Trạng thái" })
                    bang.Columns.Add(cot);

                int soMon = 0;
                foreach (DataRow r in ds.Rows)
                {
                    soMon++;
                    decimal? diem = r["Diem"] == DBNull.Value ? null : Convert.ToDecimal(r["Diem"]);
                    int thu = r["Thu"] == DBNull.Value ? 0 : Convert.ToInt32(r["Thu"]);

                    bang.Rows.Add(
                        r["MaMH"]?.ToString(),
                        r["TenMH"]?.ToString(),
                        r["SoTinChi"]?.ToString(),
                        Convert.ToBoolean(r["BatBuoc"]) ? "✔" : "",
                        thu == 0 ? "" : (thu == 8 ? "CN" : $"T{thu}"),
                        r["TietBD"] == DBNull.Value ? "" : r["TietBD"]?.ToString(),
                        r["TietKT"] == DBNull.Value ? "" : r["TietKT"]?.ToString(),
                        r["GiangVien"] == DBNull.Value ? "" : r["GiangVien"]?.ToString(),
                        r["PhongHoc"] == DBNull.Value ? "" : r["PhongHoc"]?.ToString(),
                        diem.HasValue ? diem.Value.ToString("0.0") : "",
                        diem.HasValue ? (diem.Value >= 2.0m ? "Đạt" : "Chưa đạt") : "Chưa có điểm");
                }

                dgv.DataSource = bang;

                if (dgv.Columns.Contains("Điểm")) dgv.Columns["Điểm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgv.Columns.Contains("TC")) dgv.Columns["TC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                lblDem.Text = soMon == 0
                    ? $"Học kỳ {ky}: chưa có môn nào"
                    : $"Học kỳ {ky}: {soMon} môn";

                CapNhatTong();
            }
            catch (Exception ex)
            {
                AppLog.For<FrmChuongTrinhSV>().LogError(ex, "Lỗi khi nạp chương trình của sinh viên");
                MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Tín chỉ tích lũy + điểm trung bình tính trên toàn bộ điểm đã có (không chỉ học kỳ đang xem).</summary>
        private void CapNhatTong()
        {
            DataTable diem = kqBLL.LayTheoSinhVien(maSV);
            int tcTichLuy = 0, tcTinhDiem = 0;
            decimal tong = 0m;

            foreach (DataRow d in diem.Rows)
            {
                if (d["Diem"] == DBNull.Value) continue;
                decimal diemMon = Convert.ToDecimal(d["Diem"]);
                int tc = Convert.ToInt32(d["SoTinChi"]);

                tcTinhDiem += tc;
                tong += diemMon * tc;
                if (diemMon >= 2.0m) tcTichLuy += tc;
            }

            string gpa = tcTinhDiem == 0 ? "—" : (tong / tcTinhDiem).ToString("0.00") + " / 4.00";
            lblTong.Text = $"Tín chỉ tích lũy: {tcTichLuy} / {TongTinChiChuongTrinh} TC";
            lblTong.Text += $"        ·        Điểm trung bình: {gpa}";
        }
    }
}
