using System;
using System.Data;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    /// <summary>Nghiệp vụ môn học: kiểm tra dữ liệu trước khi ghi, chặn xoá môn đang có lịch học.</summary>
    public class MonHocBLL
    {
        private readonly MonHocDAL mhDAL = new MonHocDAL();

        public DataTable LayDanhSachMonHoc()
        {
            return mhDAL.LayDanhSachMonHoc();
        }

        public bool ThemMonHoc(MonHocDTO mh, out string error)
        {
            if (!ChuanHoaVaKiemTra(mh, out error))
                return false;

            if (mhDAL.KiemTraMaTonTai(mh.MaMH))
            {
                error = $"Mã môn học '{mh.MaMH}' đã tồn tại!";
                return false;
            }

            return mhDAL.ThemMonHoc(mh);
        }

        public bool SuaMonHoc(MonHocDTO mh, out string error)
        {
            if (!ChuanHoaVaKiemTra(mh, out error))
                return false;

            return mhDAL.SuaMonHoc(mh);
        }

        public bool XoaMonHoc(string maMH, out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(maMH))
            {
                error = "Hãy chọn môn học cần xoá!";
                return false;
            }

            // Không cho xoá khi còn lịch học tham chiếu (khoá ngoại sẽ chặn, nhưng báo lỗi khó hiểu)
            int soLich = mhDAL.DemLichHocTheoMon(maMH);
            if (soLich > 0)
            {
                error = $"Không thể xoá: môn '{maMH}' đang có {soLich} buổi trong lịch học.\n" +
                        "Hãy xoá các buổi học đó trước.";
                return false;
            }

            return mhDAL.XoaMonHoc(maMH);
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
