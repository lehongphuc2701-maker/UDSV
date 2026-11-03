using System;

namespace UDSV.DTO
{
    /// <summary>
    /// Sinh viên. Không còn điểm trung bình ở đây — điểm nằm ở KetQua (mỗi môn một dòng),
    /// vì một con số duy nhất không nói được môn nào đã đạt.
    /// "Lớp sinh hoạt" (CNTT01) đã bỏ: khóa (Khoa) mới là thứ quyết định chương trình.
    /// </summary>
    public class SinhVienDTO
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }

        /// <summary>Khóa của sinh viên — quyết định chương trình nào áp cho người này.</summary>
        public string MaKhoa { get; set; }

        public string SDT { get; set; }

        public SinhVienDTO() { }

        public SinhVienDTO(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh,
                           string maKhoa, string sdt)
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            MaKhoa = maKhoa;
            SDT = sdt;
        }
    }
}
