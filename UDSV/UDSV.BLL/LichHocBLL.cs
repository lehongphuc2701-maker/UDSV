using System;
using System.Data;
using Microsoft.Extensions.Logging;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    /// <summary>
    /// Nghiệp vụ lịch học. Mỗi buổi học nối một môn với một sinh viên,
    /// nên phải kiểm tra cả hai mã có thật trước khi ghi.
    /// </summary>
    public class LichHocBLL
    {
        private readonly LichHocDAL lhDAL = new LichHocDAL();
        private readonly SinhVienDAL svDAL = new SinhVienDAL();
        private readonly MonHocDAL mhDAL = new MonHocDAL();
        private static readonly ILogger log = AppLog.For<LichHocBLL>();

        public DataTable LayDanhSachLichHoc()
        {
            return lhDAL.LayDanhSachLichHoc();
        }

        public bool ThemLichHoc(LichHocDTO lh, out string error)
        {
            if (!KiemTra(lh, out error))
            {
                log.LogWarning("Từ chối thêm lịch học: {Loi}", error);
                return false;
            }

            bool ok = lhDAL.ThemLichHoc(lh);
            if (ok) log.LogInformation("Thêm lịch học: môn {MaMH}, SV {MaSV}, ngày {Ngay:dd/MM/yyyy}",
                                       lh.MaMH, lh.MaSV, lh.NgayHoc);
            return ok;
        }

        public bool SuaLichHoc(LichHocDTO lh, out string error)
        {
            if (lh.MaLich <= 0)
            {
                error = "Hãy chọn một buổi học trong bảng để sửa.";
                log.LogWarning("Từ chối sửa lịch học: chưa chọn dòng");
                return false;
            }
            if (!KiemTra(lh, out error))
            {
                log.LogWarning("Từ chối sửa lịch học {MaLich}: {Loi}", lh.MaLich, error);
                return false;
            }

            bool ok = lhDAL.SuaLichHoc(lh);
            if (ok) log.LogInformation("Cập nhật lịch học {MaLich} thành công", lh.MaLich);
            return ok;
        }

        public bool XoaLichHoc(int maLich, out string error)
        {
            error = "";

            if (maLich <= 0)
            {
                error = "Hãy chọn một buổi học trong bảng để xoá.";
                log.LogWarning("Từ chối xoá lịch học: chưa chọn dòng");
                return false;
            }

            bool ok = lhDAL.XoaLichHoc(maLich);
            if (ok) log.LogInformation("Xoá lịch học {MaLich} thành công", maLich);
            return ok;
        }

        private bool KiemTra(LichHocDTO lh, out string error)
        {
            error = "";

            lh.MaMH = (lh.MaMH ?? "").Trim();
            lh.MaSV = (lh.MaSV ?? "").Trim();
            lh.CaHoc = (lh.CaHoc ?? "").Trim();
            lh.PhongHoc = (lh.PhongHoc ?? "").Trim();

            if (string.IsNullOrWhiteSpace(lh.MaMH))
            {
                error = "Hãy chọn môn học.";
                return false;
            }
            if (!mhDAL.KiemTraMaTonTai(lh.MaMH))
            {
                error = $"Môn học '{lh.MaMH}' không tồn tại.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(lh.MaSV))
            {
                error = "Hãy chọn sinh viên.";
                return false;
            }
            if (!svDAL.KiemTraMaTonTai(lh.MaSV))
            {
                error = $"Sinh viên '{lh.MaSV}' không tồn tại.";
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
    }
}
