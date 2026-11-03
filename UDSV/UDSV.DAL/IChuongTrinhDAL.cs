using System.Collections.Generic;
using System.Data;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Hợp đồng tầng dữ liệu cho chương trình khung của khóa.</summary>
    public interface IChuongTrinhDAL
    {
        /// <summary>Các dòng của chương trình, kèm tên môn và số tín chỉ, sắp theo học kỳ.</summary>
        DataTable LayTheoKhoa(string maKhoa);

        bool KiemTraMonDaCo(string maKhoa, string maMH);
        bool Them(ChuongTrinhDTO dong);
        bool Xoa(string maKhoa, string maMH);

        /// <summary>Tổng tín chỉ theo loại (bắt buộc / tự chọn).</summary>
        int TongTinChi(string maKhoa, bool batBuoc);

        /// <summary>
        /// Ghi đè toàn bộ chương trình của một khóa trong MỘT transaction:
        /// xoá hết dòng cũ rồi thêm danh sách mới. Dùng khi bấm LƯU trên lưới.
        /// </summary>
        bool LuuCaChuongTrinh(string maKhoa, IList<ChuongTrinhDTO> danhSach);

        /// <summary>Số sinh viên của khóa đã có điểm ở môn này — dùng cho luật "không bỏ môn đã có điểm".</summary>
        int DemSinhVienDaCoDiem(string maKhoa, string maMH);

        /// <summary>Mỗi môn một buổi/tuần: Thứ, Tiết BĐ, Tiết KT, Giảng viên — nguồn của lưới tuần.</summary>
        DataTable LayTuanMau(string maKhoa, int hocKy);

        /// <summary>Các học kỳ (1..10) mà chương trình của khóa có môn.</summary>
        DataTable LayHocKy(string maKhoa);

        /// <summary>Môn học của một khóa trong một học kỳ — dùng cho combo ở màn nhập điểm.</summary>
        DataTable LayMonTheoKy(string maKhoa, int hocKy);
    }
}
