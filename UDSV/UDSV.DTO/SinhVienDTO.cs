using System;

namespace UDSV.DTO
{
    public class SinhVienDTO
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Lop { get; set; }
        public string SDT { get; set; }
        public decimal? DiemTB { get; set; } // Dấu ? cho phép nhận giá trị NULL từ CSDL

        public SinhVienDTO() { }

        public SinhVienDTO(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string lop, string sdt, decimal? diemTB = null)
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            Lop = lop;
            SDT = sdt;
            DiemTB = diemTB;
        }
    }
}