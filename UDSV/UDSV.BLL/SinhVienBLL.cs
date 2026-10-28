using System;
using System.Data;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    /// <summary>
    /// Tầng nghiệp vụ của sinh viên: kiểm tra dữ liệu trước khi cho DAL chạm vào CSDL.
    /// Quy tắc ở đây chạy giống nhau dù người dùng vào bằng WinForms hay bất kỳ giao diện nào khác.
    /// </summary>
    public class SinhVienBLL
    {
        private readonly SinhVienDAL svDAL = new SinhVienDAL();

        // 1. Danh sách sinh viên cho lưới
        public DataTable LayDanhSachSinhVien()
        {
            return svDAL.LayDanhSachSinhVien();
        }

        // 2. Tìm kiếm: từ khoá rỗng thì trả về tất cả
        public DataTable TimKiemSinhVien(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return svDAL.LayDanhSachSinhVien();

            return svDAL.TimKiemSinhVien(keyword.Trim());
        }

        // 3. Thêm sinh viên mới
        public bool ThemSinhVien(SinhVienDTO sv, out string error)
        {
            if (!ChuanHoaVaKiemTra(sv, out error))
                return false;

            // Kiểm tra trùng mã trước khi chèn (DB vẫn là chốt chặn cuối nhờ PRIMARY KEY)
            if (svDAL.KiemTraMaTonTai(sv.MaSV))
            {
                error = $"Mã sinh viên '{sv.MaSV}' đã tồn tại!";
                return false;
            }

            return svDAL.ThemSinhVien(sv);
        }

        // 4. Cập nhật sinh viên
        public bool SuaSinhVien(SinhVienDTO sv, out string error)
        {
            if (!ChuanHoaVaKiemTra(sv, out error))
                return false;

            return svDAL.SuaSinhVien(sv);
        }

        // 5. Xoá sinh viên
        public bool XoaSinhVien(string maSV, out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(maSV))
            {
                error = "Hãy chọn sinh viên cần xoá!";
                return false;
            }

            return svDAL.XoaSinhVien(maSV.Trim());
        }

        /// <summary>
        /// Cắt khoảng trắng thừa và áp toàn bộ quy tắc nghiệp vụ.
        /// Trả về false kèm câu thông báo tiếng Việt để UI hiển thị thẳng cho người dùng.
        /// </summary>
        private static bool ChuanHoaVaKiemTra(SinhVienDTO sv, out string error)
        {
            error = "";

            sv.MaSV = (sv.MaSV ?? "").Trim();
            sv.HoTen = (sv.HoTen ?? "").Trim();
            sv.Lop = (sv.Lop ?? "").Trim();
            sv.SDT = (sv.SDT ?? "").Trim();
            sv.GioiTinh = (sv.GioiTinh ?? "").Trim();

            if (string.IsNullOrWhiteSpace(sv.MaSV))
            {
                error = "Mã sinh viên không được để trống!";
                return false;
            }
            if (!sv.MaSV.StartsWith("SV", StringComparison.OrdinalIgnoreCase))
            {
                error = "Mã sinh viên phải bắt đầu bằng SV (ví dụ: SV001).";
                return false;
            }
            if (sv.MaSV.Length > 20)
            {
                error = "Mã sinh viên không được dài quá 20 ký tự.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(sv.HoTen))
            {
                error = "Họ tên sinh viên không được để trống!";
                return false;
            }
            if (sv.HoTen.Length > 100)
            {
                error = "Họ tên không được dài quá 100 ký tự.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(sv.Lop))
            {
                error = "Lớp không được để trống!";
                return false;
            }
            if (sv.Lop.Length > 50)
            {
                error = "Tên lớp không được dài quá 50 ký tự.";
                return false;
            }
            if (sv.NgaySinh.Date > DateTime.Today)
            {
                error = "Ngày sinh không được ở tương lai.";
                return false;
            }
            if (sv.NgaySinh.Year < 1900)
            {
                error = "Ngày sinh không hợp lệ.";
                return false;
            }
            if (string.IsNullOrWhiteSpace(sv.GioiTinh))
            {
                error = "Hãy chọn giới tính.";
                return false;
            }
            if (sv.SDT.Length > 15)
            {
                error = "Số điện thoại không được dài quá 15 ký tự.";
                return false;
            }
            foreach (char c in sv.SDT)
            {
                if (!char.IsDigit(c))
                {
                    error = "Số điện thoại chỉ được chứa chữ số.";
                    return false;
                }
            }

            return true;
        }
    }
}
