using System;
using System.Data;
using Microsoft.Extensions.Logging;
using UDSV.DAL;
using UDSV.DTO;

namespace UDSV.BLL
{
    /// <summary>
    /// Nghiệp vụ chương trình của khóa.
    ///
    /// LUẬT QUAN TRỌNG NHẤT CỦA ĐỒ ÁN — "quyền theo khóa" (catalog year):
    /// chương trình bị KHOÁ ngay khi khóa đã có sinh viên. Muốn đổi yêu cầu thì tạo khóa mới,
    /// không sửa chương trình mà sinh viên đang học — đó là cách tránh việc sinh viên năm 3
    /// bỗng nhiều/ít tín chỉ hơn so với lúc nhập học.
    /// </summary>
    public class ChuongTrinhBLL
    {
        private readonly IChuongTrinhDAL ctDAL;
        private readonly IKhoaDAL khoaDAL;
        private readonly IMonHocDAL mhDAL;
        private static readonly ILogger log = AppLog.For<ChuongTrinhBLL>();

        public ChuongTrinhBLL() : this(new ChuongTrinhDAL(), new KhoaDAL(), new MonHocDAL()) { }

        public ChuongTrinhBLL(IChuongTrinhDAL chuongTrinhDal, IKhoaDAL khoaDal, IMonHocDAL monHocDal)
        {
            ctDAL = chuongTrinhDal ?? throw new ArgumentNullException(nameof(chuongTrinhDal));
            khoaDAL = khoaDal ?? throw new ArgumentNullException(nameof(khoaDal));
            mhDAL = monHocDal ?? throw new ArgumentNullException(nameof(monHocDal));
        }

        public DataTable LayTheoKhoa(string maKhoa) => ctDAL.LayTheoKhoa(maKhoa);

        public int TongTinChi(string maKhoa, bool batBuoc) => ctDAL.TongTinChi(maKhoa, batBuoc);

        /// <summary>true = chương trình đã bị khoá vì khóa đã có sinh viên.</summary>
        public bool DaKhoa(string maKhoa) => khoaDAL.DemSinhVien(maKhoa) > 0;

        public bool ThemMonVaoChuongTrinh(ChuongTrinhDTO dong, out string error)
        {
            error = "";
            if (dong == null || string.IsNullOrWhiteSpace(dong.MaKhoa) || string.IsNullOrWhiteSpace(dong.MaMH))
            {
                error = "Thiếu khóa hoặc môn học.";
                return false;
            }

            if (DaKhoa(dong.MaKhoa))
            {
                error = $"Không sửa được chương trình của khóa '{dong.MaKhoa}': khóa đã có sinh viên.\n" +
                        "Muốn thay đổi yêu cầu, hãy tạo khóa mới.";
                log.LogWarning("Từ chối thêm môn {MaMH} vào chương trình đã khoá {MaKhoa}", dong.MaMH, dong.MaKhoa);
                return false;
            }

            if (ctDAL.KiemTraMonDaCo(dong.MaKhoa, dong.MaMH))
            {
                error = $"Môn '{dong.MaMH}' đã có trong chương trình của khóa.";
                return false;
            }

            KhoaDTO? khoa = khoaDAL.LayTheoMa(dong.MaKhoa);
            if (khoa == null)
            {
                error = $"Khóa '{dong.MaKhoa}' không tồn tại.";
                return false;
            }

            int? tinChi = TimTinChi(dong.MaMH);
            if (tinChi == null)
            {
                error = $"Môn học '{dong.MaMH}' không tồn tại.";
                return false;
            }

            // TRẦN TÍN CHỈ: không được đẩy tổng vượt mức đã khai báo của khóa.
            // Muốn thêm môn mới thì phải bớt một môn khác trước — đó là cách giữ cho
            // yêu cầu tốt nghiệp của khóa không bị trôi.
            int tran = dong.BatBuoc ? khoa.TongTCBatBuoc : khoa.TongTCTuChon;
            int hienTai = ctDAL.TongTinChi(dong.MaKhoa, dong.BatBuoc);

            if (hienTai + tinChi.Value > tran)
            {
                error = $"Không thêm được: chương trình sẽ vượt trần {tran} tín chỉ " +
                        $"(hiện có {hienTai}, môn này {tinChi.Value}).\n" +
                        "Hãy bớt một môn khác trước khi thêm.";
                log.LogWarning("Từ chối thêm {MaMH}: {HienTai}+{TinChi} > trần {Tran} của {MaKhoa}",
                               dong.MaMH, hienTai, tinChi.Value, tran, dong.MaKhoa);
                return false;
            }

            bool ok = ctDAL.Them(dong);
            if (ok) log.LogInformation("Thêm môn {MaMH} vào chương trình {MaKhoa}", dong.MaMH, dong.MaKhoa);
            return ok;
        }

        /// <summary>Tra số tín chỉ của một môn trong danh mục (null nếu không có).</summary>
        private int? TimTinChi(string maMH)
        {
            foreach (System.Data.DataRow r in mhDAL.LayDanhSachMonHoc().Rows)
            {
                if (string.Equals(r["MaMH"]?.ToString(), maMH, StringComparison.OrdinalIgnoreCase))
                    return Convert.ToInt32(r["SoTinChi"]);
            }
            return null;
        }

        public bool XoaMonKhoiChuongTrinh(string maKhoa, string maMH, out string error)
        {
            error = "";
            if (DaKhoa(maKhoa))
            {
                error = $"Không sửa được chương trình của khóa '{maKhoa}': khóa đã có sinh viên.";
                log.LogWarning("Từ chối xoá môn {MaMH} khỏi chương trình đã khoá {MaKhoa}", maMH, maKhoa);
                return false;
            }

            bool ok = ctDAL.Xoa(maKhoa, maMH);
            if (ok) log.LogInformation("Xoá môn {MaMH} khỏi chương trình {MaKhoa}", maMH, maKhoa);
            return ok;
        }
    }
}
