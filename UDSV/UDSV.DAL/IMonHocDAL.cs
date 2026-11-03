using System.Data;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Hợp đồng tầng dữ liệu cho môn học.</summary>
    public interface IMonHocDAL
    {
        DataTable LayDanhSachMonHoc();
        bool KiemTraMaTonTai(string maMH);
        bool ThemMonHoc(MonHocDTO mh);
        bool SuaMonHoc(MonHocDTO mh);
        bool XoaMonHoc(string maMH);

        /// <summary>Số buổi học đang tham chiếu tới môn — để BLL chặn xoá.</summary>
        int DemLichHocTheoMon(string maMH);
    }
}
