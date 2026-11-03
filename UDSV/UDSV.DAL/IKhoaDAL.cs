using System.Data;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Hợp đồng tầng dữ liệu cho khóa.</summary>
    public interface IKhoaDAL
    {
        DataTable LayDanhSach();
        KhoaDTO? LayTheoMa(string maKhoa);
        bool KiemTraTonTai(string maKhoa);

        /// <summary>Số sinh viên thuộc khóa — dùng để chặn xoá khóa.</summary>
        int DemSinhVien(string maKhoa);

        bool Them(KhoaDTO khoa);
        bool Sua(KhoaDTO khoa);
        bool Xoa(string maKhoa);

        /// <summary>
        /// Chốt chương trình: ghi NgayKhoa + NguoiKhoa, và ghi luôn tổng tín chỉ đã chốt.
        /// Chỉ có tác dụng khi khóa CHƯA khoá (NgayKhoa còn NULL).
        /// </summary>
        bool KhoaChuongTrinh(string maKhoa, string nguoiKhoa, int tongTCBatBuoc, int tongTCTuChon);
    }
}
