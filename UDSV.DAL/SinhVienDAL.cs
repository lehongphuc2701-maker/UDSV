using System.Data;
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

        // 2. Thêm sinh viên mới
        public bool ThemSinhVien(SinhVienDTO sv)
        {
            string query = string.Format(
                "INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Lop, SDT) VALUES ('{0}', N'{1}', '{2}', N'{3}', '{4}', '{5}')",
                sv.MaSV, sv.HoTen, sv.NgaySinh.ToString("yyyy-MM-dd"), sv.GioiTinh, sv.Lop, sv.SDT
            );
            return DatabaseHelper.ExecuteQuery(query);
        }

        // 3. Xóa sinh viên theo Mã SV
        public bool XoaSinhVien(string maSV)
        {
            string query = "DELETE FROM SinhVien WHERE MaSV = '" + maSV + "'";
            return DatabaseHelper.ExecuteQuery(query);
        }
    }
}