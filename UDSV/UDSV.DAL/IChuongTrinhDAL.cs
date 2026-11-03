using System.Data;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Hợp đồng tầng dữ liệu cho chương trình của khóa.</summary>
    public interface IChuongTrinhDAL
    {
        /// <summary>Chương trình của một khóa, kèm tên môn và số tín chỉ để hiển thị.</summary>
        DataTable LayTheoKhoa(string maKhoa);

        bool KiemTraMonDaCo(string maKhoa, string maMH);
        bool Them(ChuongTrinhDTO dong);
        bool Xoa(string maKhoa, string maMH);

        /// <summary>Tổng tín chỉ theo loại (bắt buộc / tự chọn) — dùng để đối chiếu số khai báo của khóa.</summary>
        int TongTinChi(string maKhoa, bool batBuoc);
    }
}
