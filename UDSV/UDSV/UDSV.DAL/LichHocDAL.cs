using System;
using System.Data;
using Microsoft.Data.SqlClient;
using UDSV.DTO;

namespace UDSV.DAL
{
    /// <summary>
    /// Truy cập bảng LichHoc. Bảng này có hai khoá ngoại (MaMH, MaSV) nên khi đọc
    /// phải JOIN để lưới hiển thị được tên môn và tên sinh viên, không chỉ mã.
    /// </summary>
    public class LichHocDAL
    {
        // 1. Danh sách lịch học kèm tên môn + tên sinh viên
        public DataTable LayDanhSachLichHoc()
        {
            const string query = @"
                SELECT lh.MaLich,
                       lh.MaMH,
                       mh.TenMH,
                       lh.MaSV,
                       sv.HoTen,
                       lh.NgayHoc,
                       lh.CaHoc,
                       lh.PhongHoc
                FROM LichHoc lh
                INNER JOIN MonHoc mh ON mh.MaMH = lh.MaMH
                INNER JOIN SinhVien sv ON sv.MaSV = lh.MaSV
                ORDER BY lh.NgayHoc, lh.CaHoc";

            return DatabaseHelper.GetData(query);
        }

        // 2. Thêm một buổi học
        public bool ThemLichHoc(LichHocDTO lh)
        {
            const string query = @"INSERT INTO LichHoc (MaMH, MaSV, NgayHoc, CaHoc, PhongHoc)
                                   VALUES (@MaMH, @MaSV, @NgayHoc, @CaHoc, @PhongHoc)";
            return DatabaseHelper.ExecuteNonQuery(query, BuildParameters(lh)) == 1;
        }

        // 3. Sửa một buổi học (MaLich là khoá chính nên không đổi)
        public bool SuaLichHoc(LichHocDTO lh)
        {
            const string query = @"UPDATE LichHoc
                                   SET MaMH = @MaMH, MaSV = @MaSV, NgayHoc = @NgayHoc,
                                       CaHoc = @CaHoc, PhongHoc = @PhongHoc
                                   WHERE MaLich = @MaLich";

            SqlParameter[] parameters = BuildParameters(lh);
            Array.Resize(ref parameters, parameters.Length + 1);
            parameters[parameters.Length - 1] =
                new SqlParameter("@MaLich", SqlDbType.Int) { Value = lh.MaLich };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) == 1;
        }

        // 4. Xoá một buổi học
        public bool XoaLichHoc(int maLich)
        {
            const string query = "DELETE FROM LichHoc WHERE MaLich = @MaLich";
            SqlParameter p = new SqlParameter("@MaLich", SqlDbType.Int) { Value = maLich };
            return DatabaseHelper.ExecuteNonQuery(query, p) == 1;
        }

        private static SqlParameter[] BuildParameters(LichHocDTO lh)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaMH", SqlDbType.VarChar, 20) { Value = lh.MaMH },
                new SqlParameter("@MaSV", SqlDbType.VarChar, 20) { Value = lh.MaSV },
                new SqlParameter("@NgayHoc", SqlDbType.Date) { Value = lh.NgayHoc },
                new SqlParameter("@CaHoc", SqlDbType.NVarChar, 50) { Value = lh.CaHoc },
                new SqlParameter("@PhongHoc", SqlDbType.VarChar, 50)
                    { Value = (object)lh.PhongHoc ?? DBNull.Value }
            };
        }
    }
}
