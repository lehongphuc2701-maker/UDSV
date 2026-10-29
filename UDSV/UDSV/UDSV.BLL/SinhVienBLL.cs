using System;
using System.Data;
using Microsoft.Extensions.Logging;
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
        private readonly ISinhVienDAL svDAL;
        private static readonly ILogger log = AppLog.For<SinhVienBLL>();

        /// <summary>Dùng thật trong ứng dụng: DAL nói chuyện với SQL Server.</summary>
        public SinhVienBLL() : this(new SinhVienDAL()) { }

        /// <summary>Dùng cho Unit Test: truyền vào một DAL giả, không cần CSDL.</summary>
        public SinhVienBLL(ISinhVienDAL dal)
        {
            svDAL = dal ?? throw new ArgumentNullException(nameof(dal));
        }

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
            {
                log.LogWarning("Từ chối thêm sinh viên {MaSV}: {Loi}", sv?.MaSV, error);
                return false;
            }

            // Kiểm tra trùng mã trước khi chèn (DB vẫn là chốt chặn cuối nhờ PRIMARY KEY)
            if (svDAL.KiemTraMaTonTai(sv.MaSV))
            {
                error = $"Mã sinh viên '{sv.MaSV}' đã tồn tại!";
                log.LogWarning("Từ chối thêm sinh viên {MaSV}: trùng mã", sv.MaSV);
                return false;
            }

            bool ok = svDAL.ThemSinhVien(sv);
            if (ok) log.LogInformation("Thêm sinh viên {MaSV} - {HoTen} thành công", sv.MaSV, sv.HoTen);
            return ok;
        }

        // 4. Cập nhật sinh viên
        public bool SuaSinhVien(SinhVienDTO sv, out string error)
        {
            if (!ChuanHoaVaKiemTra(sv, out error))
            {
                log.LogWarning("Từ chối sửa sinh viên {MaSV}: {Loi}", sv?.MaSV, error);
                return false;
            }

            bool ok = svDAL.SuaSinhVien(sv);
            if (ok) log.LogInformation("Cập nhật sinh viên {MaSV} thành công", sv.MaSV);
            else log.LogWarning("Sửa sinh viên {MaSV} không tìm thấy dòng nào", sv.MaSV);
            return ok;
        }

        // 5. Xoá sinh viên
        public bool XoaSinhVien(string maSV, out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(maSV))
            {
                error = "Hãy chọn sinh viên cần xoá!";
                log.LogWarning("Từ chối xoá: chưa chọn sinh viên");
                return false;
            }

            bool ok = svDAL.XoaSinhVien(maSV.Trim());
            if (ok) log.LogInformation("Xoá sinh viên {MaSV} thành công", maSV);
            else log.LogWarning("Xoá sinh viên {MaSV} không tìm thấy dòng nào", maSV);
            return ok;
        }

        /// <summary>
        /// Chỉ kiểm tra dữ liệu, không ghi CSDL — dùng cho Unit Test và cho UI muốn báo lỗi sớm.
        /// </summary>
        public static bool KiemTraDuLieu(SinhVienDTO sv, out string error)
        {
            if (sv == null)
            {
                error = "Dữ liệu sinh viên rỗng.";
                return false;
            }
            return ChuanHoaVaKiemTra(sv, out error);
        }

        /// <summary>
        /// Điểm trung bình hệ 4 hợp lệ khi nằm trong đoạn [0, 4].
        /// null = sinh viên chưa có điểm, vẫn hợp lệ.
        /// Tách thành hàm riêng để Unit Test kiểm tra được mà không cần CSDL (Chương 7).
        /// </summary>
        public static bool IsValidGPA(decimal? diemTB)
        {
            if (diemTB == null) return true;
            return diemTB.Value >= 0m && diemTB.Value <= 4m;
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
            if (!IsValidGPA(sv.DiemTB))
            {
                error = "Điểm trung bình phải nằm trong khoảng 0 đến 4.";
                return false;
            }

            return true;
        }
    }
}

