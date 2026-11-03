using System.Data;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>
    /// Hợp đồng của tầng dữ liệu cho khóa. Có interface để BLL unit-test được bằng DAL giả —
    /// nhờ vậy test chạy trên máy chấm bài mà không cần SQL Server.
    /// </summary>
    public interface IKhoaDAL
    {
        DataTable LayDanhSach();
        KhoaDTO? LayTheoMa(string maKhoa);
        bool KiemTraTonTai(string maKhoa);

        /// <summary>Số sinh viên thuộc khóa — dùng cho luật "khóa đã có sinh viên thì khoá chương trình".</summary>
        int DemSinhVien(string maKhoa);

        bool Them(KhoaDTO khoa);
        bool Sua(KhoaDTO khoa);
        bool Xoa(string maKhoa);
    }
}
