using System.Data;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    public class SinhVienBLL
    {
        private readonly SinhVienDAL svDAL = new SinhVienDAL();

        // 1. Lấy danh sách nạp lên giao diện
        public DataTable LayDanhSachSinhVien()
        {
            return svDAL.LayDanhSachSinhVien();
        }

        // 2. Thêm sinh viên (Kiểm tra rỗng và thang điểm 0 - 4)
        public bool ThemSinhVien(SinhVienDTO sv, out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(sv.MaSV))
            {
                error = "Mã sinh viên không được để trống!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(sv.HoTen))
            {
                error = "Họ tên sinh viên không được để trống!";
                return false;
            }

            if (sv.DiemTB.HasValue && (sv.DiemTB.Value < 0 || sv.DiemTB.Value > 4))
            {
                error = "Điểm trung bình (hệ 4) phải nằm trong khoảng từ 0.0 đến 4.0!";
                return false;
            }

            if (!svDAL.ThemSinhVien(sv))
            {
                error = "Thêm thất bại! Mã sinh viên có thể đã tồn tại.";
                return false;
            }

            return true;
        }

        // 3. Sửa sinh viên
        public bool SuaSinhVien(SinhVienDTO sv, out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(sv.MaSV))
            {
                error = "Vui lòng chọn sinh viên cần sửa!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(sv.HoTen))
            {
                error = "Họ tên không được để trống!";
                return false;
            }

            if (sv.DiemTB.HasValue && (sv.DiemTB.Value < 0 || sv.DiemTB.Value > 4))
            {
                error = "Điểm trung bình (hệ 4) phải nằm trong khoảng từ 0.0 đến 4.0!";
                return false;
            }

            if (!svDAL.SuaSinhVien(sv))
            {
                error = "Cập nhật thông tin sinh viên thất bại!";
                return false;
            }

            return true;
        }

        // 4. Xóa sinh viên
        public bool XoaSinhVien(string maSV, out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(maSV))
            {
                error = "Vui lòng chọn sinh viên cần xóa!";
                return false;
            }

            if (!svDAL.XoaSinhVien(maSV))
            {
                error = "Xóa sinh viên thất bại! (Sinh viên này có thể đang có dữ liệu trong Lịch học).";
                return false;
            }

            return true;
        }
    }
}