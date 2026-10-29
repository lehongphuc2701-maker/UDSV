using System;
using System.Data;
using System.Globalization;
using UDSV.DTO;

namespace UDSV.DAL
{
    public class SinhVienDAL
    {
        // 1. Lấy toàn bộ danh sách sinh viên từ SQL lên dưới dạng DataTable
        public DataTable LayDanhSachSinhVien()
        {
            string query = "SELECT * FROM SinhVien";
            return DatabaseHelper.GetData(query);
        }

        // 2. Thêm sinh viên mới (đã bổ sung DiemTB)
        public bool ThemSinhVien(SinhVienDTO sv)
        {
            // Nếu có điểm thì định dạng số thập phân chuẩn (dấu chấm), nếu không có thì gán NULL
            string giaTriDiem = sv.DiemTB.HasValue
                ? sv.DiemTB.Value.ToString(CultureInfo.InvariantCulture)
                : "NULL";

            string query = string.Format(
                "INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Lop, SDT, DiemTB) " +
                "VALUES ('{0}', N'{1}', '{2}', N'{3}', '{4}', '{5}', {6})",
                sv.MaSV, sv.HoTen, sv.NgaySinh.ToString("yyyy-MM-dd"), sv.GioiTinh, sv.Lop, sv.SDT, giaTriDiem
            );
            return DatabaseHelper.ExecuteQuery(query);
        }

        // 3. Sửa thông tin sinh viên
        public bool SuaSinhVien(SinhVienDTO sv)
        {
            string giaTriDiem = sv.DiemTB.HasValue
                ? sv.DiemTB.Value.ToString(CultureInfo.InvariantCulture)
                : "NULL";

            string query = string.Format(
                "UPDATE SinhVien SET HoTen = N'{1}', NgaySinh = '{2}', GioiTinh = N'{3}', Lop = '{4}', SDT = '{5}', DiemTB = {6} " +
                "WHERE MaSV = '{0}'",
                sv.MaSV, sv.HoTen, sv.NgaySinh.ToString("yyyy-MM-dd"), sv.GioiTinh, sv.Lop, sv.SDT, giaTriDiem
            );
            return DatabaseHelper.ExecuteQuery(query);
        }

        // 4. Xóa sinh viên theo Mã SV
        public bool XoaSinhVien(string maSV)
        {
            string query = "DELETE FROM SinhVien WHERE MaSV = '" + maSV + "'";
            return DatabaseHelper.ExecuteQuery(query);
        }
    }
}