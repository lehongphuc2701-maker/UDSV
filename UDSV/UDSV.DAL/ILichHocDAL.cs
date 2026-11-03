using System.Data;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Hợp đồng tầng dữ liệu cho lịch học (gắn với môn).</summary>
    public interface ILichHocDAL
    {
        DataTable LayDanhSachLichHoc();
        DataTable LayTheoMon(string maMH);

        /// <summary>Thời khoá biểu của sinh viên = buổi học của các môn sinh viên đang có kết quả.</summary>
        DataTable LayTheoSinhVien(string maSV);

        bool ThemLichHoc(LichHocDTO lh);
        bool SuaLichHoc(LichHocDTO lh);
        bool XoaLichHoc(int maLich);
    }
}
