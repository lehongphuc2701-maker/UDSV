using System.Data;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Hợp đồng tầng dữ liệu cho kết quả học tập.</summary>
    public interface IKetQuaDAL
    {
        /// <summary>Điểm của một sinh viên, kèm tên môn và số tín chỉ.</summary>
        DataTable LayTheoSinhVien(string maSV);

        /// <summary>Điểm của tất cả sinh viên ở một môn — dùng cho màn nhập điểm theo môn.</summary>
        DataTable LayTheoMon(string maMH);

        bool KiemTraDaCo(string maSV, string maMH);
        bool Them(KetQuaDTO kq);
        bool Sua(KetQuaDTO kq);
        bool Xoa(string maSV, string maMH);

        /// <summary>
        /// Tín chỉ tích luỹ: chỉ tính môn thuộc chương trình của khóa sinh viên đang học
        /// và điểm >= 2.0 (hệ 4).
        /// </summary>
        int TinhTinChiTichLuy(string maSV);
    }
}
