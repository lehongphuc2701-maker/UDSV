using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Logging;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    /// <summary>
    /// Nghiệp vụ kết quả học tập. Điểm hệ 4, đạt từ 2.0. Mỗi (sinh viên, môn) chỉ một dòng —
    /// điểm là cuối cùng, không có học lại; nhập lại chỉ để SỬA lỗi gõ.
    /// </summary>
    public class KetQuaBLL
    {
        private readonly IKetQuaDAL kqDAL;
        private readonly ISinhVienDAL svDAL;
        private readonly IMonHocDAL mhDAL;
        private static readonly ILogger log = AppLog.For<KetQuaBLL>();

        /// <summary>Ngưỡng đạt trên thang 4 của đồ án.</summary>
        public const decimal DiemDat = 2.0m;

        public KetQuaBLL() : this(new KetQuaDAL(), new SinhVienDAL(), new MonHocDAL()) { }

        public KetQuaBLL(IKetQuaDAL ketQuaDal, ISinhVienDAL sinhVienDal, IMonHocDAL monHocDal)
        {
            kqDAL = ketQuaDal ?? throw new ArgumentNullException(nameof(ketQuaDal));
            svDAL = sinhVienDal ?? throw new ArgumentNullException(nameof(sinhVienDal));
            mhDAL = monHocDal ?? throw new ArgumentNullException(nameof(monHocDal));
        }

        public DataTable LayTheoSinhVien(string maSV) => kqDAL.LayTheoSinhVien(maSV);

        public DataTable LayTheoMon(string maMH) => kqDAL.LayTheoMon(maMH);

        /// <summary>Tín chỉ tích luỹ — chỉ tính môn thuộc chương trình của khóa và điểm đạt.</summary>
        public int TinhTinChiTichLuy(string maSV) => kqDAL.TinhTinChiTichLuy(maSV);

        /// <summary>Bảng điểm của một môn: mọi sinh viên của khóa, kèm điểm (nếu có).</summary>
        public DataTable LayBangDiem(string maKhoa, string maMH) => kqDAL.LayBangDiem(maKhoa, maMH);

        /// <summary>
        /// Lưu cả bảng điểm một lần. Kiểm tra xong mới ghi; ghi trong một transaction.
        /// Ô để trống = chưa có điểm (xoá kết quả).
        /// </summary>
        public bool LuuBangDiem(string maMH, IList<DongDiem> danhSach, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maMH))
            {
                error = "Hãy chọn môn học trước khi lưu.";
                return false;
            }
            if (danhSach == null || danhSach.Count == 0)
            {
                error = "Chưa có sinh viên nào trong bảng điểm.";
                return false;
            }
            if (!mhDAL.KiemTraMaTonTai(maMH))
            {
                error = $"Môn học '{maMH}' không tồn tại.";
                return false;
            }

            foreach (DongDiem d in danhSach)
            {
                if (string.IsNullOrWhiteSpace(d.MaSV))
                {
                    error = "Có dòng thiếu mã sinh viên.";
                    return false;
                }
                if (!KiemTraDiem(d.Diem, out string loiDiem))
                {
                    error = $"Sinh viên {d.MaSV}: {loiDiem}";
                    return false;
                }
            }

            bool ok = kqDAL.LuuBangDiem(maMH, danhSach);
            if (ok)
            {
                int coDiem = 0;
                foreach (DongDiem d in danhSach) if (d.Diem != null) coDiem++;
                log.LogInformation("Lưu bảng điểm môn {MaMH}: {CoDiem}/{Tong} sinh viên có điểm",
                                   maMH, coDiem, danhSach.Count);
            }
            return ok;
        }

        /// <summary>
        /// Nhập điểm cho một (sinh viên, môn). Nếu đã có dòng thì SỬA, chưa có thì THÊM —
        /// nhưng không bao giờ tạo dòng thứ hai.
        /// </summary>
        public bool NhapDiem(string maSV, string maMH, decimal? diem, out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(maSV))
            {
                error = "Hãy chọn sinh viên.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(maMH))
            {
                error = "Hãy chọn môn học.";
                return false;
            }
            if (!KiemTraDiem(diem, out error))
                return false;

            if (!svDAL.KiemTraMaTonTai(maSV))
            {
                error = $"Sinh viên '{maSV}' không tồn tại.";
                return false;
            }
            if (!mhDAL.KiemTraMaTonTai(maMH))
            {
                error = $"Môn học '{maMH}' không tồn tại.";
                return false;
            }

            KetQuaDTO kq = new KetQuaDTO(maSV, maMH, diem);
            bool daCo = kqDAL.KiemTraDaCo(maSV, maMH);
            bool ok = daCo ? kqDAL.Sua(kq) : kqDAL.Them(kq);

            if (ok)
                log.LogInformation("{HanhDong} điểm: SV {MaSV} - môn {MaMH} = {Diem}",
                                   daCo ? "Sửa" : "Nhập", maSV, maMH, diem?.ToString("0.00") ?? "chưa có");
            return ok;
        }

        public bool XoaKetQua(string maSV, string maMH, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(maSV) || string.IsNullOrWhiteSpace(maMH))
            {
                error = "Hãy chọn sinh viên và môn học.";
                return false;
            }

            bool ok = kqDAL.Xoa(maSV, maMH);
            if (ok) log.LogInformation("Xoá kết quả SV {MaSV} - môn {MaMH}", maSV, maMH);
            return ok;
        }

        /// <summary>Quy tắc thuần: điểm hệ 4 nằm trong [0, 4]; null = chưa có điểm, hợp lệ.</summary>
        public static bool KiemTraDiem(decimal? diem, out string error)
        {
            error = "";
            if (diem == null) return true;

            if (diem.Value < 0m || diem.Value > 4m)
            {
                error = "Điểm phải nằm trong khoảng 0 đến 4.";
                return false;
            }
            return true;
        }

        /// <summary>Điểm này có được tính tín chỉ không (đạt từ 2.0).</summary>
        public static bool LaDiemDat(decimal? diem) => diem.HasValue && diem.Value >= DiemDat;
    }
}
