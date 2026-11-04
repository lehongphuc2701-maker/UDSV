using System;
using System.Data;
using Microsoft.Data.SqlClient;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>Truy cập bảng MonHoc.</summary>
    public class MonHocDAL : IMonHocDAL
    {
        private const string SelectColumns = "MaMH, TenMH, SoTinChi, DaXoa";   // Giảng viên giờ thuộc lớp của khóa

        // 1. Danh sách môn học
        /// <summary>Bật/tắt cờ xoá mềm.</summary>
        public bool DatCoXoa(string maMH, bool daXoa)
        {
            const string query = "UPDATE MonHoc SET DaXoa = @DaXoa WHERE MaMH = @MaMH";
            SqlParameter[] ps =
            {
                new SqlParameter("@DaXoa", SqlDbType.Bit) { Value = daXoa },
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = maMH ?? "" }
            };
            return DatabaseHelper.ExecuteNonQuery(query, ps) > 0;
        }

        /// <summary>Các khóa đang có môn này trong chương trình (rỗng = được phép xoá).</summary>
        public DataTable CacKhoaDangDung(string maMH)
        {
            const string query = @"SELECT DISTINCT c.MaKhoa FROM ChuongTrinhChiTiet c WHERE c.MaMH = @MaMH ORDER BY c.MaKhoa";
            SqlParameter p = new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = maMH ?? "" };
            return DatabaseHelper.GetData(query, p);
        }

        /// <summary>Danh sách môn: daXoa = false là đang dùng, true là thùng rác.</summary>
        public DataTable LayDanhSachMonHoc(bool daXoa)
        {
            string query = $"SELECT {SelectColumns} FROM MonHoc WHERE DaXoa = @DaXoa ORDER BY MaMH";
            SqlParameter p = new SqlParameter("@DaXoa", SqlDbType.Bit) { Value = daXoa };
            return DatabaseHelper.GetData(query, p);
        }
        public DataTable LayDanhSachMonHoc()
        {
            string query = $"SELECT {SelectColumns} FROM MonHoc WHERE DaXoa = 0 ORDER BY MaMH";
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
