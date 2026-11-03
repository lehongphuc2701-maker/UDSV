using System;

namespace UDSV.DTO
{
    /// <summary>
    /// Kết quả của một sinh viên ở một môn. Mỗi cặp (sinh viên, môn) chỉ có tối đa MỘT dòng —
    /// điểm là cuối cùng, không có học lại. Điểm NULL = đang học, chưa có điểm.
    /// </summary>
    public class KetQuaDTO
    {
        public string MaSV { get; set; }
        public string MaMH { get; set; }

        /// <summary>Điểm hệ 4 (0.00 .. 4.00). null = chưa có điểm. Đạt khi >= 2.0.</summary>
        public decimal? Diem { get; set; }

        public DateTime LanNhap { get; set; }

        public KetQuaDTO() { }

        public KetQuaDTO(string maSV, string maMH, decimal? diem)
        {
            MaSV = maSV;
            MaMH = maMH;
            Diem = diem;
        }
    }
}
