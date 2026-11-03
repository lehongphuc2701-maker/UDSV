using System;

namespace UDSV.DTO
{
    /// <summary>
    /// Khóa (niên khóa). Mỗi khóa mang một chương trình riêng và số tín chỉ cố định —
    /// đây là "quyền theo khóa" (catalog year): sinh viên học theo chương trình của khóa mình.
    /// </summary>
    public class KhoaDTO
    {
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public int NamBatDau { get; set; }
        public int NamKetThuc { get; set; }

        /// <summary>Tổng tín chỉ bắt buộc phải đạt để tốt nghiệp — cố định theo khóa.</summary>
        public int TongTCBatBuoc { get; set; }

        /// <summary>Tổng tín chỉ tự chọn của khóa.</summary>
        public int TongTCTuChon { get; set; }

        public KhoaDTO() { }

        public KhoaDTO(string maKhoa, string tenKhoa, int namBatDau, int namKetThuc,
                       int tongTCBatBuoc, int tongTCTuChon)
        {
            MaKhoa = maKhoa;
            TenKhoa = tenKhoa;
            NamBatDau = namBatDau;
            NamKetThuc = namKetThuc;
            TongTCBatBuoc = tongTCBatBuoc;
            TongTCTuChon = tongTCTuChon;
        }
    }
}
