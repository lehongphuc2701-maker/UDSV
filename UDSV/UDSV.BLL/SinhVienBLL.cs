using System.Data;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    public class SinhVienBLL
    {
        private SinhVienDAL svDAL = new SinhVienDAL();

        // 1. Lấy danh sách sinh viên phục vụ cho UI
        public DataTable LayDanhSachSinhVien()
        {
            return svDAL.LayDanhSachSinhVien();
        }

        // 2. Kiểm tra nghiệp vụ trước khi thêm sinh viên mới
        public bool ThemSinhVien(SinhVienDTO sv, out string error)
        {
            error = "";

            // Kiểm tra ràng buộc cơ bản
            if (string.IsNullOrEmpty(sv.MaSV))
            {
                error = "Mã sinh viên không được để trống!";
                return false;
            }
            if (string.IsNullOrEmpty(sv.HoTen))
            {
                error = "Họ tên sinh viên không được để trống!";
                return false;
            }

            // Nếu hợp lệ thì gọi DAL để thêm vào CSDL
            return svDAL.ThemSinhVien(sv);
        }

        // 3. Xóa sinh viên
        public bool XoaSinhVien(string maSV)
        {
            return svDAL.XoaSinhVien(maSV);
        }
    }
}