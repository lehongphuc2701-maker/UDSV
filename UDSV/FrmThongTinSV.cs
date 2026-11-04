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
    /// Hồ sơ của chính sinh viên đang đăng nhập: chỉ xem, không sửa được gì.
    /// Kèm phần tổng hợp: tín chỉ đã tích lũy (điểm >= 2.0) trên tổng 120, và điểm trung bình.
    /// </summary>
    public class FrmThongTinSV : Form
    {
        private readonly SinhVienBLL svBLL = new SinhVienBLL();
        private readonly KetQuaBLL kqBLL = new KetQuaBLL();
        private readonly string maSV;

        private const int TongTinChiChuongTrinh = 120;

        public FrmThongTinSV(string maSV)
        {
            this.maSV = maSV ?? "";
            KhoiTaoGiaoDien();
        }

        private void KhoiTaoGiaoDien()
        {
            Text = "Thông tin sinh viên";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(720, 520);
            BackColor = Color.FromArgb(246, 248, 252);
            Font = new Font("Segoe UI", 10F);

            var tieuDe = new Label
            {
                Text = "THÔNG TIN SINH VIÊN",
                Dock = DockStyle.Top,
                Height = 56,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(79, 70, 229),
                BackColor = Color.White
            };
            Controls.Add(tieuDe);

            var luoi = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                ColumnCount = 2,
                RowCount = 9,
                BackColor = Color.White
            };
            luoi.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170));
            luoi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            string[] nhan = { "Mã sinh viên", "Họ và tên", "Ngày sinh", "Giới tính",
                              "Mã khóa", "Tên khóa", "Số điện thoại",
                              "Tín chỉ tích lũy", "Điểm trung bình" };
            foreach (var n in nhan)
            {
                luoi.Controls.Add(new Label
                {
                    Text = n + ":",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    ForeColor = Color.FromArgb(100, 116, 139),
                    Font = new Font("Segoe UI", 9.5F)
                });
                luoi.Controls.Add(new Label
                {
                    Name = "vl_" + n.Replace(" ", ""),
                    Text = "—",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 41, 59)
                });
            }
            Controls.Add(luoi);

            var dong = new Button
            {
                Text = "Đóng",
                Dock = DockStyle.Bottom,
                Height = 48,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(79, 70, 229),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            dong.Click += (s, e) => Close();
            Controls.Add(dong);

            Load += FrmThongTinSV_Load;
        }

        private void FrmThongTinSV_Load(object? sender, EventArgs e)
        {
            try
            {
                DataTable sv = svBLL.LayTheoMa(maSV);
                if (sv.Rows.Count == 0)
                {
                    MessageBox.Show(this, $"Không tìm thấy sinh viên {maSV}.", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataRow r = sv.Rows[0];

                Gan("Mãsinhviên", r["MaSV"]?.ToString());
                Gan("Họvàtên", r["HoTen"]?.ToString());
                Gan("Ngàysinh", r["NgaySinh"] == DBNull.Value
                    ? "—" : Convert.ToDateTime(r["NgaySinh"]).ToString("dd/MM/yyyy"));
                Gan("Giớitính", r["GioiTinh"]?.ToString());
                Gan("Mãkhóa", r["MaKhoa"]?.ToString());
                Gan("Tênkhóa", r.Table.Columns.Contains("TenKhoa") ? r["TenKhoa"]?.ToString() : "");
                Gan("Sốđiệnthoại", r["SDT"]?.ToString());

                // tổng hợp từ bảng điểm
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

                Gan("Tínchítíchlũy", $"{tcTichLuy} / {TongTinChiChuongTrinh} TC");
                Gan("Điểmtrungbình", tcTinhDiem == 0
                    ? "—"
                    : (tongDiem / tcTinhDiem).ToString("0.00") + " / 4.00");
            }
            catch (Exception ex)
            {
                AppLog.For<FrmThongTinSV>().LogError(ex, "Lỗi khi nạp thông tin sinh viên");
                MessageBox.Show(this, ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Gan(string khoa, string? giaTri)
        {
            var lbl = Controls.Find("vl_" + khoa, true).FirstOrDefault() as Label;
            if (lbl != null) lbl.Text = string.IsNullOrWhiteSpace(giaTri) ? "—" : giaTri;
        }
    }
}
