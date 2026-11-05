namespace UDSV.DTO
{
    /// <summary>
    /// Một dòng trong chương trình khung của khóa: môn nào, thuộc học kỳ nào (1..10),
    /// bắt buộc hay tự chọn, và lịch học hằng tuần của lớp đó (1 buổi/tuần).
    /// </summary>
    public class ChuongTrinhDTO
    {
        public string MaKhoa { get; set; }
        public string MaMH { get; set; }
        public string? TenMH { get; set; }

        /// <summary>true = bắt buộc, false = tự chọn.</summary>
        public bool BatBuoc { get; set; }

        /// <summary>Học kỳ trong chương trình (1..10) — khác với học kỳ niên khóa.</summary>
        public int HocKy { get; set; } = 1;

        /// <summary>2 = Thứ 2 … 8 = Chủ nhật. null = chưa xếp lịch.</summary>
        public int? Thu { get; set; }

        /// <summary>Tiết bắt đầu (1..12) trong buổi đó.</summary>
        public int? TietBD { get; set; }

        /// <summary>Tiết kết thúc (1..12), phải >= TietBD.</summary>
        public int? TietKT { get; set; }

        /// <summary>Giảng viên dạy lớp này của khóa (không phải thông tin chung của môn).</summary>
        public string GiangVien { get; set; }

        /// <summary>Phòng học của buổi hằng tuần.</summary>
        public string PhongHoc { get; set; }

        public ChuongTrinhDTO() { }

        public ChuongTrinhDTO(string maKhoa, string maMH, bool batBuoc, int hocKy = 1,
                              int? thu = null, int? tietBD = null, int? tietKT = null,
                              string giangVien = null, string phongHoc = null, string tenMH = null)
        {
            MaKhoa = maKhoa;
            MaMH = maMH;
            BatBuoc = batBuoc;
            HocKy = hocKy;
            Thu = thu;
            TietBD = tietBD;
            TietKT = tietKT;
            GiangVien = giangVien;
            PhongHoc = phongHoc;
            TenMH = tenMH;
        }
    }
}
