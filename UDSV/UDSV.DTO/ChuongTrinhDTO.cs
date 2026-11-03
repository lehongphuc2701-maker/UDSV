namespace UDSV.DTO
{
    /// <summary>
    /// Một dòng trong chương trình của khóa: môn nào thuộc khóa nào, bắt buộc hay tự chọn.
    /// Bảng này bị khoá khi khóa đã có sinh viên (luật thực thi ở BLL).
    /// </summary>
    public class ChuongTrinhDTO
    {
        public string MaKhoa { get; set; }
        public string MaMH { get; set; }

        /// <summary>true = bắt buộc, false = tự chọn.</summary>
        public bool BatBuoc { get; set; }

        public ChuongTrinhDTO() { }

        public ChuongTrinhDTO(string maKhoa, string maMH, bool batBuoc)
        {
            MaKhoa = maKhoa;
            MaMH = maMH;
            BatBuoc = batBuoc;
        }
    }
}
