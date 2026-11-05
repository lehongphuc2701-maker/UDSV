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
    /// Hồ sơ của chính sinh viên đang đăng nhập: Giao diện thẻ sinh viên (Profile Card)
    /// hiện đại, sang trọng, hiển thị KPI tiến độ tín chỉ và điểm trung bình tích lũy.
    /// </summary>
    public class FrmThongTinSV : Form
    {
        private readonly SinhVienBLL svBLL = new SinhVienBLL();
        private readonly KetQuaBLL kqBLL = new KetQuaBLL();
        private readonly string maSV;

        private const int TongTinChiChuongTrinh = 120;

        // Các control hiển thị động
        private Label lblProfileHoTen = null!;
        private Label lblProfileMaSV = null!;
        private Label lblStatusVal = null!;
        private Label lblKhoaVal = null!;
        private Label lblNganhVal = null!;
        private Label lblGioiTinhVal = null!;

        private Label lblTCVal = null!;
        private Label lblTCProgress = null!;
        private Label lblGPAVal = null!;
        private Label lblGPAXepLoai = null!;

        // Các nhãn chi tiết trong bảng thông tin
        private Label valMaSV = null!;
        private Label valHoTen = null!;
        private Label valNgaySinh = null!;
        private Label valGioiTinh = null!;
        private Label valSDT = null!;
        private Label valMaKhoa = null!;
        private Label valTenKhoa = null!;
        private Label valTrangThai = null!;

        public FrmThongTinSV(string maSV)
        {
            this.maSV = maSV ?? "";
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Hồ sơ sinh viên - " + (string.IsNullOrWhiteSpace(maSV) ? "UDSV" : maSV);
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(900, 600);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
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
                Text = "🎓",
                Font = new Font("Segoe UI Emoji", 26F),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(20, 12),
                Size = new Size(54, 56)
            };
            var lblHeaderTitle = new Label
            {
                Text = "HỒ SƠ SINH VIÊN",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(78, 15),
                AutoSize = true
            };
            var lblHeaderSub = new Label
            {
                Text = "Thông tin hồ sơ cá nhân, chỉ số học tập & tiến độ tích lũy chính thức",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(224, 231, 255),
                BackColor = Color.Transparent,
                Location = new Point(80, 48),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblHeaderIcon);
            pnlHeader.Controls.Add(lblHeaderTitle);
            pnlHeader.Controls.Add(lblHeaderSub);
            Controls.Add(pnlHeader);

            // 2. Cột trái: Thẻ Sinh Viên (Profile Card)
            var cardProfile = new CardPanel
            {
                Location = new Point(24, 102),
                Size = new Size(320, 474),
                Radius = 16,
                BackColor = Color.White
            };

            // Avatar tròn
            var pnlAvatar = new Panel
            {
                Size = new Size(84, 84),
                Location = new Point((320 - 84) / 2, 20),
                BackColor = Color.FromArgb(238, 242, 255)
            };
            pnlAvatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var p = new Pen(Color.FromArgb(199, 210, 254), 2f);
                e.Graphics.DrawEllipse(p, 1, 1, pnlAvatar.Width - 3, pnlAvatar.Height - 3);
            };
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, 84, 84);
                pnlAvatar.Region = new Region(gp);
            }
            var lblAvatarIcon = new Label
            {
                Text = "👨‍🎓",
                Font = new Font("Segoe UI Emoji", 34F),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            pnlAvatar.Controls.Add(lblAvatarIcon);
            cardProfile.Controls.Add(pnlAvatar);

            lblProfileHoTen = new Label
            {
                Text = "Đang tải...",
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(12, 114),
                Size = new Size(296, 28),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true
            };
            cardProfile.Controls.Add(lblProfileHoTen);

            // Badge Mã SV
            var pnlMaSVBadge = new Panel
            {
                Size = new Size(130, 26),
                Location = new Point((320 - 130) / 2, 148),
                BackColor = Color.FromArgb(238, 242, 255)
            };
            lblProfileMaSV = new Label
            {
                Text = maSV,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(79, 70, 229),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlMaSVBadge.Controls.Add(lblProfileMaSV);
            cardProfile.Controls.Add(pnlMaSVBadge);

            // Đường kẻ ngăn
            var div1 = new Panel
            {
                Location = new Point(20, 186),
                Size = new Size(280, 1),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            cardProfile.Controls.Add(div1);

            // Các mục tóm tắt ở cột trái
            int startY = 200;
            void TaoMucNho(string tieuDe, out Label valLabel)
            {
                var lblT = new Label
                {
                    Text = tieuDe,
                    Location = new Point(24, startY),
                    Size = new Size(110, 20),
                    Font = new Font("Segoe UI", 8.8F),
                    ForeColor = Color.FromArgb(100, 116, 139)
                };
                valLabel = new Label
                {
                    Text = "—",
                    Location = new Point(134, startY),
                    Size = new Size(166, 20),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    AutoEllipsis = true
                };
                cardProfile.Controls.Add(lblT);
                cardProfile.Controls.Add(valLabel);
                startY += 34;
            }

            TaoMucNho("Trạng thái:", out lblStatusVal);
            TaoMucNho("Khóa học:", out lblKhoaVal);
            TaoMucNho("Chương trình:", out lblNganhVal);
            TaoMucNho("Giới tính:", out lblGioiTinhVal);

            var div2 = new Panel
            {
                Location = new Point(20, startY + 6),
                Size = new Size(280, 1),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            cardProfile.Controls.Add(div2);

            var lblProfileNote = new Label
            {
                Text = "🔒 Hồ sơ sinh viên chính quy\nTrường Đại học Bà Rịa - Vũng Tàu (BVU)",
                Font = new Font("Segoe UI", 8.2F),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(12, startY + 16),
                Size = new Size(296, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };
            cardProfile.Controls.Add(lblProfileNote);
            Controls.Add(cardProfile);

            // 3. Cột phải: Thống kê KPI & Chi tiết hồ sơ
            // Card 1: Tín chỉ tích lũy
            var cardTC = new CardPanel
            {
                Location = new Point(362, 102),
                Size = new Size(244, 115),
                Radius = 14,
                BackColor = Color.FromArgb(236, 253, 245)
            };
            var lblTCTitle = new Label
            {
                Text = "📚  TÍN CHỈ TÍCH LŨY",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(6, 95, 70),
                Location = new Point(16, 14),
                AutoSize = true
            };
            lblTCVal = new Label
            {
                Text = "0 / 120 TC",
                Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                ForeColor = Color.FromArgb(4, 120, 87),
                Location = new Point(14, 40),
                Size = new Size(216, 36)
            };
            lblTCProgress = new Label
            {
                Text = "Tiến độ: 0% chương trình",
                Font = new Font("Segoe UI", 8.8F),
                ForeColor = Color.FromArgb(5, 150, 105),
                Location = new Point(16, 82),
                Size = new Size(216, 20)
            };
            cardTC.Controls.Add(lblTCTitle);
            cardTC.Controls.Add(lblTCVal);
            cardTC.Controls.Add(lblTCProgress);
            Controls.Add(cardTC);

            // Card 2: Điểm trung bình GPA
            var cardGPA = new CardPanel
            {
                Location = new Point(622, 102),
                Size = new Size(254, 115),
                Radius = 14,
                BackColor = Color.FromArgb(254, 243, 199)
            };
            var lblGPATitle = new Label
            {
                Text = "⭐  ĐIỂM TRUNG BÌNH (GPA)",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(146, 64, 14),
                Location = new Point(16, 14),
                AutoSize = true
            };
            lblGPAVal = new Label
            {
                Text = "— / 4.00",
                Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 83, 9),
                Location = new Point(14, 40),
                Size = new Size(226, 36)
            };
            lblGPAXepLoai = new Label
            {
                Text = "Xếp loại học lực: —",
                Font = new Font("Segoe UI", 8.8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 83, 9),
                Location = new Point(16, 82),
                Size = new Size(226, 20)
            };
            cardGPA.Controls.Add(lblGPATitle);
            cardGPA.Controls.Add(lblGPAVal);
            cardGPA.Controls.Add(lblGPAXepLoai);
            Controls.Add(cardGPA);

            // Card 3: Chi tiết hồ sơ cá nhân
            var cardDetails = new CardPanel
            {
                Location = new Point(362, 230),
                Size = new Size(514, 276),
                Radius = 16,
                BackColor = Color.White
            };
            var lblDetailsHeader = new Label
            {
                Text = "📋  THÔNG TIN CÁ NHÂN & ĐÀO TẠO",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(18, 14),
                AutoSize = true
            };
            var divDetails = new Panel
            {
                Location = new Point(18, 44),
                Size = new Size(478, 1),
                BackColor = Color.FromArgb(241, 245, 249)
            };
            cardDetails.Controls.Add(lblDetailsHeader);
            cardDetails.Controls.Add(divDetails);

            // Bố cục bảng 2 cột chia đôi
            int col1X = 20, col2X = 265;
            int rY = 58;

            void TaoDongChiTiet(int x, int y, string icon, string nhan, out Label valLbl)
            {
                var lNhan = new Label
                {
                    Text = $"{icon} {nhan}:",
                    Location = new Point(x, y),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 8.8F),
                    ForeColor = Color.FromArgb(100, 116, 139)
                };
                valLbl = new Label
                {
                    Text = "—",
                    Location = new Point(x, y + 20),
                    Size = new Size(230, 22),
                    Font = new Font("Segoe UI", 9.8F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    AutoEllipsis = true
                };
                cardDetails.Controls.Add(lNhan);
                cardDetails.Controls.Add(valLbl);
            }

            TaoDongChiTiet(col1X, rY, "🏷️", "Mã sinh viên", out valMaSV);
            TaoDongChiTiet(col2X, rY, "👤", "Họ và tên", out valHoTen);
            rY += 52;

            TaoDongChiTiet(col1X, rY, "🎂", "Ngày sinh", out valNgaySinh);
            TaoDongChiTiet(col2X, rY, "⚥", "Giới tính", out valGioiTinh);
            rY += 52;

            TaoDongChiTiet(col1X, rY, "📱", "Số điện thoại", out valSDT);
            TaoDongChiTiet(col2X, rY, "🏛️", "Mã khóa học", out valMaKhoa);
            rY += 52;

            TaoDongChiTiet(col1X, rY, "🎓", "Chương trình đào tạo", out valTenKhoa);
            TaoDongChiTiet(col2X, rY, "📌", "Trạng thái học tập", out valTrangThai);

            Controls.Add(cardDetails);

            // Nút Đóng & dòng chú thích bảo mật dưới cùng
            var lblSecurity = new Label
            {
                Text = "🔒 Dữ liệu đồng bộ & xác thực từ hệ thống BVU",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(362, 528),
                Size = new Size(365, 22),
                AutoEllipsis = true
            };
            Controls.Add(lblSecurity);

            var btnDong = new GradientButton
            {
                Text = "Đóng hồ sơ",
                Location = new Point(746, 518),
                Size = new Size(130, 40),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                GradientStart = Color.FromArgb(79, 70, 229),
                GradientEnd = Color.FromArgb(124, 58, 237),
                Radius = 12,
                Cursor = Cursors.Hand
            };
            btnDong.Click += (s, e) => Close();
            Controls.Add(btnDong);

            Load += FrmThongTinSV_Load;
        }

        private void FrmThongTinSV_Load(object? sender, EventArgs e)
        {
            try
            {
                DataTable sv = svBLL.LayTheoMa(maSV);
                if (sv.Rows.Count == 0)
                {
                    ThongBao.Show(this, $"Không tìm thấy thông tin sinh viên có mã '{maSV}'.", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataRow r = sv.Rows[0];

                string hoTen = r["HoTen"]?.ToString() ?? "—";
                string ngaySinh = r["NgaySinh"] == DBNull.Value ? "—" : Convert.ToDateTime(r["NgaySinh"]).ToString("dd/MM/yyyy");
                string gioiTinh = r["GioiTinh"]?.ToString() ?? "—";
                string maKhoa = r["MaKhoa"]?.ToString() ?? "—";
                string tenKhoa = r.Table.Columns.Contains("TenKhoa") ? r["TenKhoa"]?.ToString() ?? "" : "";
                string sdt = r["SDT"]?.ToString() ?? "—";
                string trangThai = r.Table.Columns.Contains("TrangThai") ? r["TrangThai"]?.ToString() ?? "Chưa tốt nghiệp" : "Chưa tốt nghiệp";

                // Gán dữ liệu thẻ bên trái
                lblProfileHoTen.Text = hoTen;
                lblProfileMaSV.Text = $"🏷️  Mã SV: {maSV}";
                lblKhoaVal.Text = maKhoa;
                lblNganhVal.Text = string.IsNullOrWhiteSpace(tenKhoa) ? maKhoa : tenKhoa;
                lblGioiTinhVal.Text = gioiTinh;

                // Gán dữ liệu bảng chi tiết bên phải
                valMaSV.Text = maSV;
                valHoTen.Text = hoTen;
                valNgaySinh.Text = ngaySinh;
                valGioiTinh.Text = gioiTinh;
                valSDT.Text = string.IsNullOrWhiteSpace(sdt) ? "(Chưa cập nhật)" : sdt;
                valMaKhoa.Text = maKhoa;
                valTenKhoa.Text = string.IsNullOrWhiteSpace(tenKhoa) ? maKhoa : tenKhoa;

                // Tính toán tổng hợp từ bảng điểm
                DataTable diem = kqBLL.LayTheoSinhVien(maSV);
                int tcTichLuy = 0, tcTinhDiem = 0;
                decimal tongDiem = 0m;

                foreach (DataRow d in diem.Rows)
                {
                    if (d["Diem"] == DBNull.Value) continue;
                    decimal diemMon = Convert.ToDecimal(d["Diem"]);
                    int tc = Convert.ToInt32(d["SoTinChi"]);

                    tcTinhDiem += tc;
                    tongDiem += diemMon * tc;
                    if (diemMon >= 2.0m) tcTichLuy += tc;
                }

                // Gán thẻ tín chỉ
                lblTCVal.Text = $"{tcTichLuy} / {TongTinChiChuongTrinh} TC";
                int ptHoanThanh = (int)Math.Round((double)tcTichLuy / TongTinChiChuongTrinh * 100);
                lblTCProgress.Text = $"Tiến độ: {ptHoanThanh}% chương trình";

                // Gán thẻ GPA
                decimal gpa = 0m;
                if (tcTinhDiem == 0)
                {
                    lblGPAVal.Text = "— / 4.00";
                    lblGPAXepLoai.Text = "Xếp loại học lực: Chưa có điểm";
                }
                else
                {
                    gpa = Math.Round(tongDiem / tcTinhDiem, 2);
                    lblGPAVal.Text = $"{gpa:0.00} / 4.00";

                    string xepLoai = "Yếu";
                    Color mauXepLoai = Color.FromArgb(239, 68, 68);
                    if (gpa >= 3.6m) { xepLoai = "Xuất sắc"; mauXepLoai = Color.FromArgb(168, 85, 247); }
                    else if (gpa >= 3.2m) { xepLoai = "Giỏi"; mauXepLoai = Color.FromArgb(16, 185, 129); }
                    else if (gpa >= 2.5m) { xepLoai = "Khá"; mauXepLoai = Color.FromArgb(14, 165, 233); }
                    else if (gpa >= 2.0m) { xepLoai = "Trung bình"; mauXepLoai = Color.FromArgb(245, 158, 11); }

                    lblGPAXepLoai.Text = $"Xếp loại học lực: {xepLoai}";
                    lblGPAXepLoai.ForeColor = mauXepLoai;
                }

                // TỰ ĐỘNG XÉT TỐT NGHIỆP HAI CHIỀU: Đủ 120 TC & GPA >= 2.0 thì Tốt nghiệp, thiếu thì Chưa tốt nghiệp
                if (trangThai != "Đã thôi học" && trangThai != "Đã xóa")
                {
                    if (tcTichLuy >= TongTinChiChuongTrinh && gpa >= 2.0m)
                    {
                        if (trangThai != "Đã tốt nghiệp")
                        {
                            trangThai = "Đã tốt nghiệp";
                            try { new SinhVienDAL().DoiTrangThai(maSV, "Đã tốt nghiệp"); } catch { }
                        }
                    }
                    else
                    {
                        if (trangThai == "Đã tốt nghiệp")
                        {
                            trangThai = "Chưa tốt nghiệp";
                            try { new SinhVienDAL().DoiTrangThai(maSV, "Chưa tốt nghiệp"); } catch { }
                        }
                    }
                }

                // Cập nhật nhãn trạng thái chính xác
                if (trangThai == "Đã tốt nghiệp")
                {
                    lblStatusVal.Text = "🎓 Đã tốt nghiệp";
                    lblStatusVal.ForeColor = Color.FromArgb(16, 185, 129);
                    valTrangThai.Text = "Đã tốt nghiệp";
                    valTrangThai.ForeColor = Color.FromArgb(16, 185, 129);
                }
                else if (trangThai == "Đã thôi học" || trangThai == "Đã xóa")
                {
                    lblStatusVal.Text = "🔴 Đã thôi học";
                    lblStatusVal.ForeColor = Color.FromArgb(220, 38, 38);
                    valTrangThai.Text = "Đã thôi học";
                    valTrangThai.ForeColor = Color.FromArgb(220, 38, 38);
                }
                else
                {
                    lblStatusVal.Text = "🟢 Đang học";
                    lblStatusVal.ForeColor = Color.FromArgb(5, 150, 105);
                    valTrangThai.Text = "Chưa tốt nghiệp";
                    valTrangThai.ForeColor = Color.FromArgb(70, 80, 95);
                }
            }
            catch (Exception ex)
            {
                AppLog.For<FrmThongTinSV>().LogError(ex, "Lỗi khi nạp thông tin sinh viên");
                ThongBao.Show(this, "Không thể tải hồ sơ sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
