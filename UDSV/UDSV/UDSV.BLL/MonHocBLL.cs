using System;
using System.Data;
using Microsoft.Extensions.Logging;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    /// <summary>Nghiệp vụ môn học: kiểm tra dữ liệu trước khi ghi, chặn xoá môn đang có lịch học.</summary>
    public class MonHocBLL
    {
        private readonly MonHocDAL mhDAL = new MonHocDAL();
        private static readonly ILogger log = AppLog.For<MonHocBLL>();

        public DataTable LayDanhSachMonHoc()
        {
            return mhDAL.LayDanhSachMonHoc();
        }

        /// <summary>
        /// Tổng số tín chỉ của danh sách môn — hiển thị trên tiêu đề FrmMonHoc.
        /// Tách thành hàm thuần (không chạm CSDL) để Unit Test kiểm tra được phép cộng.
        /// </summary>
        public static int TongTinChi(DataTable danhSach)
        {
            if (danhSach == null) return 0;

            int tong = 0;
            foreach (DataRow dong in danhSach.Rows)
            {
                if (dong["SoTinChi"] != DBNull.Value)
                    tong += Convert.ToInt32(dong["SoTinChi"]);
            }
            return tong;
        }

        public bool ThemMonHoc(MonHocDTO mh, out string error)
        {
            if (!ChuanHoaVaKiemTra(mh, out error))
            {
                log.LogWarning("Từ chối thêm môn học {MaMH}: {Loi}", mh?.MaMH, error);
                return false;
            }

            if (mhDAL.KiemTraMaTonTai(mh.MaMH))
            {
                error = $"Mã môn học '{mh.MaMH}' đã tồn tại!";
                log.LogWarning("Từ chối thêm môn học {MaMH}: trùng mã", mh.MaMH);
                return false;
            }

            bool ok = mhDAL.ThemMonHoc(mh);
            if (ok) log.LogInformation("Thêm môn học {MaMH} - {TenMH} thành công", mh.MaMH, mh.TenMH);
            return ok;
        }

        public bool SuaMonHoc(MonHocDTO mh, out string error)
        {
            if (!ChuanHoaVaKiemTra(mh, out error))
            {
                log.LogWarning("Từ chối sửa môn học {MaMH}: {Loi}", mh?.MaMH, error);
                return false;
            }

            bool ok = mhDAL.SuaMonHoc(mh);
            if (ok) log.LogInformation("Cập nhật môn học {MaMH} thành công", mh.MaMH);
            else log.LogWarning("Sửa môn học {MaMH} không tìm thấy dòng nào", mh.MaMH);
            return ok;
        }

        public bool XoaMonHoc(string maMH, out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(maMH))
            {
                error = "Hãy chọn môn học cần xoá!";
                log.LogWarning("Từ chối xoá môn học: chưa chọn dòng");
                return false;
            }

            // Không cho xoá khi còn lịch học tham chiếu (khoá ngoại sẽ chặn, nhưng báo lỗi khó hiểu)
            int soLich = mhDAL.DemLichHocTheoMon(maMH);
            if (soLich > 0)
            {
                error = $"Không thể xoá: môn '{maMH}' đang có {soLich} buổi trong lịch học.\n" +
                        "Hãy xoá các buổi học đó trước.";
                log.LogWarning("Từ chối xoá môn học {MaMH}: còn {SoLich} buổi học tham chiếu", maMH, soLich);
                return false;
            }

            bool xoaOk = mhDAL.XoaMonHoc(maMH);
            if (xoaOk) log.LogInformation("Xoá môn học {MaMH} thành công", maMH);
            return xoaOk;
        }

        private static bool ChuanHoaVaKiemTra(MonHocDTO mh, out string error)
        {
            error = "";

            mh.MaMH = (mh.MaMH ?? "").Trim();
            mh.TenMH = (mh.TenMH ?? "").Trim();
            mh.GiangVien = (mh.GiangVien ?? "").Trim();

            if (string.IsNullOrWhiteSpace(mh.MaMH))
            {
                error = "Mã môn học không được để trống!";
                return false;
            }
            if (mh.MaMH.Length > 20)
            {
                error = "Mã môn học không được dài quá 20 ký tự.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(mh.TenMH))
            {
                error = "Tên môn học không được để trống!";
                return false;
            }
            if (mh.TenMH.Length > 100)
            {
                error = "Tên môn học không được dài quá 100 ký tự.";
                return false;
            }
            if (mh.SoTinChi < 1 || mh.SoTinChi > 10)
            {
                error = "Số tín chỉ phải nằm trong khoảng 1 đến 10.";
                return false;
            }
            if (mh.GiangVien.Length > 100)
            {
                error = "Tên giảng viên không được dài quá 100 ký tự.";
                return false;
            }

            return true;
        }
    }
}
