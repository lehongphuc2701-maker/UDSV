using System;
using System.Data;
using Microsoft.Extensions.Logging;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    /// <summary>
    /// Nghiệp vụ lịch học. Một buổi học thuộc về MỘT MÔN: môn phải tồn tại, ngày và ca hợp lệ.
    /// Không còn gắn với sinh viên — thời khoá biểu của sinh viên được suy ra từ kết quả của họ.
    /// </summary>
    public class LichHocBLL
    {
        private readonly ILichHocDAL lhDAL;
        private readonly IMonHocDAL mhDAL;
        private static readonly ILogger log = AppLog.For<LichHocBLL>();

        public LichHocBLL() : this(new LichHocDAL(), new MonHocDAL()) { }

        public LichHocBLL(ILichHocDAL lichHocDal, IMonHocDAL monHocDal)
        {
            lhDAL = lichHocDal ?? throw new ArgumentNullException(nameof(lichHocDal));
            mhDAL = monHocDal ?? throw new ArgumentNullException(nameof(monHocDal));
        }

        public DataTable LayDanhSachLichHoc() => lhDAL.LayDanhSachLichHoc();

        public DataTable LayTheoMon(string maMH) => lhDAL.LayTheoMon(maMH);

        /// <summary>Thời khoá biểu của sinh viên = buổi học của các môn sinh viên đang học.</summary>
        public DataTable LayTheoSinhVien(string maSV) => lhDAL.LayTheoSinhVien(maSV);

        /// <summary>Buổi học trong tuần của một khóa + học kỳ — dữ liệu cho lưới thời khoá biểu.</summary>
        public DataTable LayTheoTuan(string maKhoa, int hocKy, DateTime tuNgay, DateTime denNgay)
            => lhDAL.LayTheoTuan(maKhoa, hocKy, tuNgay, denNgay);

        public bool ThemLichHoc(LichHocDTO lh, out string error)
        {
            if (!KiemTra(lh, out error))
            {
                log.LogWarning("Từ chối thêm buổi học: {Loi}", error);
                return false;
            }

            bool ok = lhDAL.ThemLichHoc(lh);
            if (ok) log.LogInformation("Thêm buổi học môn {MaMH} ngày {Ngay:dd/MM/yyyy}", lh.MaMH, lh.NgayHoc);
            return ok;
        }

        public bool SuaLichHoc(LichHocDTO lh, out string error)
        {
            if (lh.MaLich <= 0)
            {
                error = "Hãy chọn một buổi học trong bảng để sửa.";
                return false;
            }
            if (!KiemTra(lh, out error))
            {
                log.LogWarning("Từ chối sửa buổi học {MaLich}: {Loi}", lh.MaLich, error);
                return false;
            }

            bool ok = lhDAL.SuaLichHoc(lh);
            if (ok) log.LogInformation("Cập nhật buổi học {MaLich}", lh.MaLich);
            return ok;
        }

        public bool XoaLichHoc(int maLich, out string error)
        {
            error = "";
            if (maLich <= 0)
            {
                error = "Hãy chọn một buổi học trong bảng để xoá.";
                return false;
            }

            bool ok = lhDAL.XoaLichHoc(maLich);
            if (ok) log.LogInformation("Xoá buổi học {MaLich}", maLich);
            return ok;
        }

        /// <summary>Quy tắc thuần, không chạm CSDL — dùng cho Unit Test.</summary>
        public static bool KiemTraDuLieu(LichHocDTO lh, out string error)
        {
            error = "";
            if (lh == null)
            {
                error = "Dữ liệu buổi học rỗng.";
                return false;
            }

            lh.MaMH = (lh.MaMH ?? "").Trim();
            lh.CaHoc = (lh.CaHoc ?? "").Trim();
            lh.PhongHoc = (lh.PhongHoc ?? "").Trim();

            if (string.IsNullOrWhiteSpace(lh.MaMH))
            {
                error = "Hãy chọn môn học.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(lh.CaHoc))
            {
                error = "Ca học không được để trống!";
                return false;
            }
            if (lh.CaHoc.Length > 50)
            {
                error = "Ca học không được dài quá 50 ký tự.";
                return false;
            }
            if (lh.PhongHoc.Length > 50)
            {
                error = "Phòng học không được dài quá 50 ký tự.";
                return false;
            }
            if (lh.NgayHoc.Year < 2000)
            {
                error = "Ngày học không hợp lệ.";
                return false;
            }

            return true;
        }

        private bool KiemTra(LichHocDTO lh, out string error)
        {
            if (!KiemTraDuLieu(lh, out error))
                return false;

            if (!mhDAL.KiemTraMaTonTai(lh.MaMH))
            {
                error = $"Môn học '{lh.MaMH}' không tồn tại.";
                return false;
            }

            return true;
        }
    }
}
