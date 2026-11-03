using System;

namespace UDSV.DTO
{
    /// <summary>
    /// Một buổi học. Lịch gắn với MÔN (không gắn với khóa, không gắn với sinh viên):
    /// ai đang học môn đó thì thấy buổi học đó.
    /// TietBD/TietKT là tiết trong ngày (1..12) — dùng để vẽ lưới tuần, khối chiếm nhiều dòng.
    /// </summary>
    public class LichHocDTO
    {
        public int MaLich { get; set; }
        public string MaMH { get; set; }
        public DateTime NgayHoc { get; set; }
        public string CaHoc { get; set; }
        public int TietBD { get; set; } = 1;
        public int TietKT { get; set; } = 1;
        public string PhongHoc { get; set; }

        public LichHocDTO() { }

        public LichHocDTO(int maLich, string maMH, DateTime ngayHoc, string caHoc, string phongHoc,
                          int tietBD = 1, int tietKT = 1)
        {
            MaLich = maLich;
            MaMH = maMH;
            NgayHoc = ngayHoc;
            CaHoc = caHoc;
            PhongHoc = phongHoc;
            TietBD = tietBD;
            TietKT = tietKT;
        }
    }
}
