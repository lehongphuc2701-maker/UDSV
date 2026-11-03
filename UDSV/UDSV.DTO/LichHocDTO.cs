using System;

namespace UDSV.DTO
{
    /// <summary>
    /// Một buổi học. Lịch gắn với MÔN (không gắn với khóa, không gắn với sinh viên):
    /// ai đang học môn đó thì thấy buổi học đó — chi tiết truy vấn nằm ở DAL.
    /// </summary>
    public class LichHocDTO
    {
        public int MaLich { get; set; }
        public string MaMH { get; set; }
        public DateTime NgayHoc { get; set; }
        public string CaHoc { get; set; }
        public string PhongHoc { get; set; }

        public LichHocDTO() { }

        public LichHocDTO(int maLich, string maMH, DateTime ngayHoc, string caHoc, string phongHoc)
        {
            MaLich = maLich;
            MaMH = maMH;
            NgayHoc = ngayHoc;
            CaHoc = caHoc;
            PhongHoc = phongHoc;
        }
    }
}
