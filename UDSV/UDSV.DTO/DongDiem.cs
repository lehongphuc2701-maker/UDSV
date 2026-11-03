namespace UDSV.DTO
{
    /// <summary>
    /// Một dòng trên bảng nhập điểm: sinh viên nào, điểm bao nhiêu.
    /// Diem = null nghĩa là "chưa có điểm" → khi lưu sẽ xoá dòng kết quả.
    /// </summary>
    public class DongDiem
    {
        public string MaSV { get; set; }
        public decimal? Diem { get; set; }

        public DongDiem() { }

        public DongDiem(string maSV, decimal? diem)
        {
            MaSV = maSV;
            Diem = diem;
        }
    }
}
