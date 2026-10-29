using System.Data;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>
    /// Hợp đồng của tầng dữ liệu sinh viên.
    /// Có interface để BLL test được bằng một DAL giả (in-memory) mà không cần SQL Server —
    /// nhờ vậy Unit Test ở Chương 7 chạy được trên máy bất kỳ, kể cả máy chấm bài.
    /// </summary>
    public interface ISinhVienDAL
    {
        DataTable LayDanhSachSinhVien();
        DataTable TimKiemSinhVien(string keyword);
        DataTable LaySinhVienTheoMa(string maSV);
        bool KiemTraMaTonTai(string maSV);
        bool ThemSinhVien(SinhVienDTO sv);
        bool SuaSinhVien(SinhVienDTO sv);
        bool XoaSinhVien(string maSV);
    }
}
