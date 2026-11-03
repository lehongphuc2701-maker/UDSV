namespace UDSV.DTO
{
    /// <summary>
    /// Một dòng trong chương trình khung của khóa: môn nào, thuộc học kỳ nào (1..10),
    /// bắt buộc hay tự chọn.
    /// </summary>
    public class ChuongTrinhDTO
    {
        public string MaKhoa { get; set; }
        public string MaMH { get; set; }

        /// <summary>true = bắt buộc, false = tự chọn.</summary>
        public bool BatBuoc { get; set; }

        /// <summary>Học kỳ trong chương trình (1..10) — khác với học kỳ niên khóa.</summary>
        public int HocKy { get; set; } = 1;

        public ChuongTrinhDTO() { }

        public ChuongTrinhDTO(string maKhoa, string maMH, bool batBuoc, int hocKy = 1)
        {
            MaKhoa = maKhoa;
            MaMH = maMH;
            BatBuoc = batBuoc;
            HocKy = hocKy;
        }
    }
}
