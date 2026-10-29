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

        /// <summary>
        /// Điểm trung bình hệ 4 (0..4). NULL = sinh viên chưa có điểm.
        /// Quy tắc 0..4 nằm ở BLL, không phải ở DTO (Chương 9).
        /// </summary>
        public decimal? DiemTB { get; set; }

        public SinhVienDTO() { }

        public SinhVienDTO(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string lop, string sdt,
                           decimal? diemTB = null)
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
