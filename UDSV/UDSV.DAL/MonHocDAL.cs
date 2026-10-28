using System;
using System.Data;
using Microsoft.Data.SqlClient;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Truy cập bảng MonHoc.</summary>
    public class MonHocDAL
    {
        private const string SelectColumns = "MaMH, TenMH, SoTinChi, GiangVien";

        // 1. Danh sách môn học
        public DataTable LayDanhSachMonHoc()
        {
            string query = $"SELECT {SelectColumns} FROM MonHoc ORDER BY MaMH";
            return DatabaseHelper.GetData(query);
        }

        // 2. Mã môn học đã tồn tại chưa
        public bool KiemTraMaTonTai(string maMH)
        {
            const string query = "SELECT COUNT(*) FROM MonHoc WHERE MaMH = @MaMH";
            SqlParameter p = new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = maMH };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, p)) > 0;
        }

        // 3. Thêm môn học
        public bool ThemMonHoc(MonHocDTO mh)
        {
            const string query = @"INSERT INTO MonHoc (MaMH, TenMH, SoTinChi, GiangVien)
                                   VALUES (@MaMH, @TenMH, @SoTinChi, @GiangVien)";
            return DatabaseHelper.ExecuteNonQuery(query, BuildParameters(mh)) == 1;
        }

        // 4. Cập nhật môn học
        public bool SuaMonHoc(MonHocDTO mh)
        {
            const string query = @"UPDATE MonHoc
                                   SET TenMH = @TenMH, SoTinChi = @SoTinChi, GiangVien = @GiangVien
                                   WHERE MaMH = @MaMH";
            return DatabaseHelper.ExecuteNonQuery(query, BuildParameters(mh)) == 1;
        }

        // 5. Xoá môn học
        public bool XoaMonHoc(string maMH)
        {
            const string query = "DELETE FROM MonHoc WHERE MaMH = @MaMH";
            SqlParameter p = new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = maMH };
            return DatabaseHelper.ExecuteNonQuery(query, p) == 1;
        }

        // 6. Đếm số lịch học đang tham chiếu tới môn này (để BLL chặn xoá)
        public int DemLichHocTheoMon(string maMH)
        {
            const string query = "SELECT COUNT(*) FROM LichHoc WHERE MaMH = @MaMH";
            SqlParameter p = new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = maMH };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, p));
        }

        private static SqlParameter[] BuildParameters(MonHocDTO mh)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = mh.MaMH },
                new SqlParameter("@TenMH", SqlDbType.NVarChar, 100) { Value = mh.TenMH },
                new SqlParameter("@SoTinChi", SqlDbType.Int) { Value = mh.SoTinChi },
                new SqlParameter("@GiangVien", SqlDbType.NVarChar, 100)
                    { Value = (object)mh.GiangVien ?? DBNull.Value }
            };
        }
    }
}
