using System;

namespace UDSV.DTO
{
    public class LichHocDTO
    {
        public int MaLich { get; set; }
        public string MaMH { get; set; }
        public string MaSV { get; set; }
        public DateTime NgayHoc { get; set; }
        public string CaHoc { get; set; }
        public string PhongHoc { get; set; }

        public LichHocDTO() { }

        public LichHocDTO(int maLich, string maMH, string maSV, DateTime ngayHoc, string caHoc, string phongHoc)
        {
            MaLich = maLich;
            MaMH = maMH;
            MaSV = maSV;
            NgayHoc = ngayHoc;
            CaHoc = caHoc;
            PhongHoc = phongHoc;
        }
    }
}