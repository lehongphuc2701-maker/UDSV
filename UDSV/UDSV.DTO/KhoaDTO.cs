using System;

namespace UDSV.DTO
{
    /// <summary>
    /// Khóa (niên khóa). Mỗi khóa mang một chương trình riêng và số tín chỉ cố định —
    /// đây là "quyền theo khóa" (catalog year).
    /// </summary>
    public class KhoaDTO
    {
        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public int NamBatDau { get; set; }
        public int NamKetThuc { get; set; }

        /// <summary>Tổng tín chỉ bắt buộc — chốt tại thời điểm khoá chương trình.</summary>
        public int TongTCBatBuoc { get; set; }

        /// <summary>Tổng tín chỉ tự chọn.</summary>
        public int TongTCTuChon { get; set; }

        /// <summary>null = chương trình còn sửa được. Có giá trị = đã chốt, không mở lại.</summary>
        public DateTime? NgayKhoa { get; set; }

        /// <summary>Tài khoản bấm nút khoá.</summary>
        public string NguoiKhoa { get; set; }

        /// <summary>Chương trình đã bị chốt hay chưa.</summary>
        public bool DaKhoa => NgayKhoa.HasValue;

        public KhoaDTO() { }

        public KhoaDTO(string maKhoa, string tenKhoa, int namBatDau, int namKetThuc,
                       int tongTCBatBuoc, int tongTCTuChon,
                       DateTime? ngayKhoa = null, string nguoiKhoa = null)
        {
            MaKhoa = maKhoa;
            TenKhoa = tenKhoa;
            NamBatDau = namBatDau;
            NamKetThuc = namKetThuc;
            TongTCBatBuoc = tongTCBatBuoc;
            TongTCTuChon = tongTCTuChon;
            NgayKhoa = ngayKhoa;
            NguoiKhoa = nguoiKhoa;
        }
    }
}
