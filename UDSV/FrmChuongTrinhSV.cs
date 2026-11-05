using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using UDSV.BLL;
using UDSV.Controls;
using UDSV.DAL;

namespace UDSV
{
    /// <summary>
    /// Chương trình đào tạo của khóa — dành cho sinh viên: Giao diện hiện đại,
    /// chuẩn nhận diện BVU / UDSV, trực quan hóa tiến độ tích lũy và kết quả học tập.
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
        private Label lblDem = null!;
        private Label lblKpiTC = null!;
        private Label lblKpiGPA = null!;

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
            Text = $"Chương trình đào tạo - Khóa {maKhoa} ({tenKhoa})";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1200, 720);
            MinimumSize = new Size(1050, 650);
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 9.5F);

            // 1. Header Gradient Banner
            var pnlHeader = new GradientPanel
            {
                Dock = DockStyle.Top,
                Height = 84,
                Angle = 135f,
                ColorStart = Color.FromArgb(79, 70, 229),
                ColorEnd = Color.FromArgb(124, 58, 237)
            };

            var lblHeaderIcon = new Label
            {
                Text = "🏛️",
                Font = new Font("Segoe UI Emoji", 26F),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(20, 12),
                Size = new Size(54, 56)
            };
            var lblHeaderTitle = new Label
            {
                Text = "CHƯƠNG TRÌNH ĐÀO TẠO KHÓA HỌC",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(78, 15),
                AutoSize = true
            };
            var lblHeaderSub = new Label
            {
                Text = $"Khóa: {maKhoa} — {tenKhoa}  •  Sinh viên: {maSV}  •  Trường ĐH Bà Rịa - Vũng Tàu (BVU)",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(224, 231, 255),
                BackColor = Color.Transparent,
                Location = new Point(80, 48),
                AutoSize = true
            };

            var lblBadgePhanHe = new Label
            {
                Text = "🎓 Phân hệ Sinh viên",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(40, 255, 255, 255),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(1020, 26),
                Size = new Size(150, 32),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlHeader.Controls.Add(lblHeaderIcon);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Controls.Add(lblHeaderSub);
            pnlHeader.Controls.Add(lblBadgePhanHe);
            pnlHeader.Resize += (s, e) => lblBadgePhanHe.Location = new Point(pnlHeader.Width - 170, 26);

            // 2. Thanh lọc học kỳ & KPI tổng quan (Filter Card)
            var pnlTopFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(24, 14, 24, 0),
                BackColor = Color.Transparent
            };

            var cardFilter = new CardPanel
            {
                Dock = DockStyle.Fill,
                Radius = 14,
                BackColor = Color.White
            };

            var lblChonKy = new Label
            {
                Text = "📅 Chọn học kỳ:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(79, 70, 229),
                Location = new Point(20, 18),
                AutoSize = true
            };
            cardFilter.Controls.Add(lblChonKy);

            cboHocKy = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(155, 14),
                Width = 130,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            for (int i = 1; i <= 10; i++) cboHocKy.Items.Add($"Học kỳ {i}");
            cboHocKy.SelectedIndex = 0;
            cboHocKy.SelectedIndexChanged += (s, e) => NapLuoi();
            cardFilter.Controls.Add(cboHocKy);

            lblDem = new Label
            {
                AutoSize = true,
                Location = new Point(305, 18),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(5, 150, 105),
                Text = "Đang nạp dữ liệu..."
            };
            cardFilter.Controls.Add(lblDem);

            // 2 Thẻ pill KPI góc phải
            lblKpiGPA = new Label
            {
                Text = "⭐  GPA: — / 4.00",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 83, 9),
                BackColor = Color.FromArgb(254, 243, 199),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(cardFilter.Width - 190, 14),
                Size = new Size(175, 34),
                TextAlign = ContentAlignment.MiddleCenter
            };
            cardFilter.Controls.Add(lblKpiGPA);

            lblKpiTC = new Label
            {
                Text = "📚  Tích lũy: 0 / 120 TC",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(4, 120, 87),
                BackColor = Color.FromArgb(236, 253, 245),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(cardFilter.Width - 380, 14),
                Size = new Size(180, 34),
                TextAlign = ContentAlignment.MiddleCenter
            };
            cardFilter.Controls.Add(lblKpiTC);

            cardFilter.Resize += (s, e) =>
            {
                lblKpiGPA.Location = new Point(cardFilter.Width - 190, 14);
                lblKpiTC.Location = new Point(cardFilter.Width - 380, 14);
            };

            pnlTopFilter.Controls.Add(cardFilter);

            // 3. Thanh Footer dưới cùng
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(24, 10, 24, 10)
            };

            var lblFooterInfo = new Label
            {
                Text = "💡 Điểm môn học ≥ 2.0 được tính vào tín chỉ tích lũy  •  Chuẩn chương trình: 120 TC  •  ĐH Bà Rịa - Vũng Tàu (BVU)",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(24, 18),
                AutoSize = true
            };
            pnlFooter.Controls.Add(lblFooterInfo);

            var btnDong = new GradientButton
            {
                Text = "✕  Đóng",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlFooter.Width - 145, 10),
                Size = new Size(125, 40),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                GradientStart = Color.FromArgb(79, 70, 229),
                GradientEnd = Color.FromArgb(124, 58, 237),
                Radius = 10,
                Cursor = Cursors.Hand
            };
            btnDong.Click += (s, e) => Close();
            pnlFooter.Controls.Add(btnDong);
            pnlFooter.Resize += (s, e) => btnDong.Location = new Point(pnlFooter.Width - 145, 10);

            // 4. Bảng dữ liệu chính (Lồng trong CardPanel hiện đại)
            var pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 14, 24, 14),
                BackColor = Color.Transparent
            };

            var cardTable = new CardPanel
            {
                Dock = DockStyle.Fill,
                Radius = 14,
                BackColor = Color.White,
                Padding = new Padding(8)
            };

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(241, 245, 249),
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                Font = new Font("Segoe UI", 9.5F)
            };

            // Bật DoubleBuffered để cuộn mượt mà
            typeof(Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(dgv, true);

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 44;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(79, 70, 229);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(79, 70, 229);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 6, 0);

            dgv.RowTemplate.Height = 40;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);

            // Tô màu kết quả học tập tự động
            dgv.CellFormatting += Dgv_CellFormatting;

            cardTable.Controls.Add(dgv);
            pnlBody.Controls.Add(cardTable);

            // Gắn vào Form theo đúng thứ tự WinForms docking layout:
            // Controls được xử lý theo thứ tự ngược lại (index cao nhất chiếm viền trước):
            // 1. pnlHeader: Dock = Top (sát mép trên cùng Y=0)
            // 2. pnlTopFilter: Dock = Top (ngay dưới Header Y=84)
            // 3. pnlFooter: Dock = Bottom (sát mép dưới cùng)
            // 4. pnlBody: Dock = Fill (lấp đầy toàn bộ vùng giữa không bị đè)
            Controls.Add(pnlBody);
            Controls.Add(pnlFooter);
            Controls.Add(pnlTopFilter);
            Controls.Add(pnlHeader);

            Load += (s, e) => { ChonHocKyMacDinh(); NapLuoi(); };
        }

        private void Dgv_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string colName = dgv.Columns[e.ColumnIndex].Name;

            if (colName == "Kết quả" && e.Value != null)
            {
                string val = e.Value.ToString() ?? "";
                if (val == "Đạt")
                {
                    e.CellStyle!.ForeColor = Color.FromArgb(16, 185, 129);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else if (val == "Chưa đạt")
                {
                    e.CellStyle!.ForeColor = Color.FromArgb(239, 68, 68);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle!.ForeColor = Color.FromArgb(148, 163, 184);
                }
            }
            else if (colName == "Điểm số" && e.Value != null)
            {
                e.CellStyle!.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            }
            else if (colName == "Loại môn" && e.Value != null)
            {
                string val = e.Value.ToString() ?? "";
                if (val == "Bắt buộc")
                {
                    e.CellStyle!.ForeColor = Color.FromArgb(79, 70, 229);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle!.ForeColor = Color.FromArgb(100, 116, 139);
                }
            }
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
                if (dong != null && dong.Table.Columns.Contains("TuNam")) tuNam = Convert.ToInt32(dong["TuNam"]);

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
                int ky = cboHocKy.SelectedIndex >= 0 ? cboHocKy.SelectedIndex + 1 : 1;
                DataTable ds = ctBLL.LayChuongTrinhCoDiem(maKhoa, ky, maSV);

                dgv.DataSource = null;
                dgv.Columns.Clear();

                var bang = new DataTable();
                bang.Columns.Add("STT");
                bang.Columns.Add("Mã học phần");
                bang.Columns.Add("Tên môn học");
                bang.Columns.Add("Số TC");
                bang.Columns.Add("Loại môn");
                bang.Columns.Add("Thời khóa biểu");
                bang.Columns.Add("Phòng học");
                bang.Columns.Add("Giảng viên");
                bang.Columns.Add("Điểm số");
                bang.Columns.Add("Kết quả");

                int stt = 0;
                int tongTCKy = 0;

                foreach (DataRow r in ds.Rows)
                {
                    stt++;
                    decimal? diem = r["Diem"] == DBNull.Value ? null : Convert.ToDecimal(r["Diem"]);
                    int thu = r["Thu"] == DBNull.Value ? 0 : Convert.ToInt32(r["Thu"]);
                    int soTC = Convert.ToInt32(r["SoTinChi"]);
                    tongTCKy += soTC;

                    string thuTen = thu == 0 ? "—" : (thu == 8 ? "CN" : $"Thứ {thu}");
                    string tkb = (thu > 0 && r["TietBD"] != DBNull.Value)
                        ? $"{thuTen} (T{r["TietBD"]}-{r["TietKT"]})"
                        : "—";

                    string loaiMon = Convert.ToBoolean(r["BatBuoc"]) ? "Bắt buộc" : "Tự chọn";
                    string phong = r["PhongHoc"] == DBNull.Value || string.IsNullOrWhiteSpace(r["PhongHoc"]?.ToString())
                        ? "—" : r["PhongHoc"]!.ToString()!;
                    string gv = r["GiangVien"] == DBNull.Value || string.IsNullOrWhiteSpace(r["GiangVien"]?.ToString())
                        ? "—" : r["GiangVien"]!.ToString()!;

                    bang.Rows.Add(
                        stt.ToString(),
                        r["MaMH"]?.ToString(),
                        r["TenMH"]?.ToString(),
                        soTC.ToString(),
                        loaiMon,
                        tkb,
                        phong,
                        gv,
                        diem.HasValue ? diem.Value.ToString("0.0") : "—",
                        diem.HasValue ? (diem.Value >= 2.0m ? "Đạt" : "Chưa đạt") : "Chưa có điểm");
                }

                dgv.DataSource = bang;

                // Căn chỉnh cột đẹp mắt, không bị co giật hay tràn chữ
                if (dgv.Columns.Contains("STT"))
                {
                    dgv.Columns["STT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgv.Columns["STT"].Width = 50;
                    dgv.Columns["STT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgv.Columns.Contains("Mã học phần"))
                {
                    dgv.Columns["Mã học phần"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgv.Columns["Mã học phần"].Width = 115;
                    dgv.Columns["Mã học phần"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.Columns["Mã học phần"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    dgv.Columns["Mã học phần"].DefaultCellStyle.ForeColor = Color.FromArgb(79, 70, 229);
                }
                if (dgv.Columns.Contains("Tên môn học"))
                {
                    dgv.Columns["Tên môn học"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgv.Columns["Tên môn học"].FillWeight = 220;
                    dgv.Columns["Tên môn học"].MinimumWidth = 180;
                    dgv.Columns["Tên môn học"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                if (dgv.Columns.Contains("Số TC"))
                {
                    dgv.Columns["Số TC"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgv.Columns["Số TC"].Width = 65;
                    dgv.Columns["Số TC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgv.Columns.Contains("Loại môn"))
                {
                    dgv.Columns["Loại môn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgv.Columns["Loại môn"].Width = 95;
                    dgv.Columns["Loại môn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgv.Columns.Contains("Thời khóa biểu"))
                {
                    dgv.Columns["Thời khóa biểu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgv.Columns["Thời khóa biểu"].Width = 135;
                    dgv.Columns["Thời khóa biểu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgv.Columns.Contains("Phòng học"))
                {
                    dgv.Columns["Phòng học"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgv.Columns["Phòng học"].Width = 95;
                    dgv.Columns["Phòng học"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgv.Columns.Contains("Giảng viên"))
                {
                    dgv.Columns["Giảng viên"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgv.Columns["Giảng viên"].FillWeight = 140;
                    dgv.Columns["Giảng viên"].MinimumWidth = 130;
                }
                if (dgv.Columns.Contains("Điểm số"))
                {
                    dgv.Columns["Điểm số"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgv.Columns["Điểm số"].Width = 80;
                    dgv.Columns["Điểm số"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgv.Columns.Contains("Kết quả"))
                {
                    dgv.Columns["Kết quả"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgv.Columns["Kết quả"].Width = 115;
                    dgv.Columns["Kết quả"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                lblDem.Text = stt == 0
                    ? $"Học kỳ {ky}: Chưa có học phần trong chương trình"
                    : $"📘 Học kỳ {ky}: {stt} học phần  •  Tổng {tongTCKy} tín chỉ";

                CapNhatTong();
            }
            catch (Exception ex)
            {
                AppLog.For<FrmChuongTrinhSV>().LogError(ex, "Lỗi khi nạp chương trình của sinh viên");
                ThongBao.Show(this, "Không thể tải chương trình đào tạo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Tín chỉ tích lũy + điểm trung bình tính trên toàn bộ điểm đã có.</summary>
        private void CapNhatTong()
        {
            try
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

                string gpa = tcTinhDiem == 0 ? "—" : (tong / tcTinhDiem).ToString("0.00");
                lblKpiTC.Text = $"📚  Tích lũy: {tcTichLuy} / {TongTinChiChuongTrinh} TC";
                lblKpiGPA.Text = $"⭐  GPA: {gpa} / 4.00";
            }
            catch { }
        }
    }
}
