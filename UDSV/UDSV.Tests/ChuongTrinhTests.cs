using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV.Tests
{
    /// <summary>
    /// Chương trình của khóa — gồm LUẬT KHOÁ (catalog year), luật quan trọng nhất của đồ án:
    /// khóa đã có sinh viên thì không được sửa chương trình nữa, và không được đẩy tổng
    /// tín chỉ vượt trần đã khai báo của khóa.
    /// </summary>
    [TestClass]
    public class ChuongTrinhTests
    {
        private static MonHocDALGia MonHoc() =>
            new MonHocDALGia(("MH01", 3), ("MH02", 3), ("MH03", 3), ("MH04", 3));

        /// <summary>Dựng BLL trên dữ liệu giả, có thể dựng sẵn vài môn trong chương trình.</summary>
        private static ChuongTrinhBLL Tao(string maKhoa, int capBatBuoc, int capTuChon, int soSinhVien,
                                         params (string MaMH, bool BatBuoc)[] coSan)
        {
            KhoaDTO khoa = new KhoaDTO(maKhoa, "Khóa " + maKhoa, 2024, 2028, capBatBuoc, capTuChon);
            KhoaDALGia khoaDal = new KhoaDALGia(khoa);
            khoaDal.DatSoSinhVien(maKhoa, soSinhVien);

            MonHocDALGia monHoc = MonHoc();
            ChuongTrinhDALGia ct = new ChuongTrinhDALGia(monHoc);
            foreach ((string ma, bool bb) in coSan)
                ct.ThemSan(new ChuongTrinhDTO(maKhoa, ma, bb));

            return new ChuongTrinhBLL(ct, khoaDal, monHoc);
        }

        // ---------- Luật khoá ----------

        [TestMethod]
        public void ChuongTrinh_KhoaDaCoSinhVien_ThiDaKhoa()
        {
            Assert.IsTrue(Tao("K24", 27, 6, soSinhVien: 1).DaKhoa("K24"));
        }

        [TestMethod]
        public void ChuongTrinh_KhoaChuaCoSinhVien_ThiChuaKhoa()
        {
            Assert.IsFalse(Tao("K26", 27, 6, soSinhVien: 0).DaKhoa("K26"));
        }

        [TestMethod]
        public void ChuongTrinh_ThemMonKhiDaKhoa_BiTuChoi()
        {
            ChuongTrinhBLL bll = Tao("K24", 27, 6, soSinhVien: 3);

            Assert.IsFalse(bll.ThemMonVaoChuongTrinh(new ChuongTrinhDTO("K24", "MH01", true), out string error));
            StringAssert.Contains(error, "đã có sinh viên");
        }

        [TestMethod]
        public void ChuongTrinh_XoaMonKhiDaKhoa_BiTuChoi()
        {
            ChuongTrinhBLL bll = Tao("K24", 27, 6, soSinhVien: 3, ("MH01", true));

            Assert.IsFalse(bll.XoaMonKhoiChuongTrinh("K24", "MH01", out string error));
            StringAssert.Contains(error, "đã có sinh viên");
        }

        // ---------- Khi chưa khoá ----------

        [TestMethod]
        public void ChuongTrinh_ThemMonKhiChuaKhoa_ThanhCong()
        {
            ChuongTrinhBLL bll = Tao("K26", 27, 6, soSinhVien: 0);

            Assert.IsTrue(bll.ThemMonVaoChuongTrinh(new ChuongTrinhDTO("K26", "MH01", true), out string error), error);
        }

        [TestMethod]
        public void ChuongTrinh_ThemMonTrung_BiTuChoi()
        {
            ChuongTrinhBLL bll = Tao("K26", 27, 6, soSinhVien: 0, ("MH01", true));

            Assert.IsFalse(bll.ThemMonVaoChuongTrinh(new ChuongTrinhDTO("K26", "MH01", true), out string error));
            StringAssert.Contains(error, "đã có trong chương trình");
        }

        [TestMethod]
        public void ChuongTrinh_ThemMonKhongTonTai_BiTuChoi()
        {
            ChuongTrinhBLL bll = Tao("K26", 27, 6, soSinhVien: 0);

            Assert.IsFalse(bll.ThemMonVaoChuongTrinh(new ChuongTrinhDTO("K26", "MH999", true), out string error));
            StringAssert.Contains(error, "không tồn tại");
        }

        // ---------- Trần tín chỉ ----------

        /// <summary>
        /// Không được thêm môn nếu tổng tín chỉ bắt buộc VƯỢT mức đã khai báo của khóa.
        /// Đây là "trần" mà người dùng yêu cầu: vượt trần thì phải bớt một môn khác trước.
        /// </summary>
        [TestMethod]
        public void ChuongTrinh_ThemMonVuotTranTinChi_BiTuChoi()
        {
            // Trần 6 TC; đã có 3 + 3 = 6 → thêm môn 3 TC nữa là 9, phải bị chặn
            ChuongTrinhBLL bll = Tao("K26", capBatBuoc: 6, capTuChon: 6, soSinhVien: 0,
                                     ("MH01", true), ("MH02", true));

            bool ok = bll.ThemMonVaoChuongTrinh(new ChuongTrinhDTO("K26", "MH03", true), out string error);

            Assert.IsFalse(ok, $"Đã cho thêm môn làm tổng vượt trần 6 TC (hiện 6 + 3). Lỗi trả về: '{error}'");
            StringAssert.Contains(error, "vượt trần");
        }

        [TestMethod]
        public void ChuongTrinh_ThemMonDungBangTran_ThanhCong()
        {
            // Trần 6 TC; đã có 3 → thêm 3 nữa là vừa đúng trần, được phép
            ChuongTrinhBLL bll = Tao("K26", capBatBuoc: 6, capTuChon: 6, soSinhVien: 0, ("MH01", true));

            Assert.IsTrue(bll.ThemMonVaoChuongTrinh(new ChuongTrinhDTO("K26", "MH02", true), out string error), error);
        }

        [TestMethod]
        public void ChuongTrinh_ThemMonTuChon_KhongBiTinhVaoTranBatBuoc()
        {
            // Trần bắt buộc 3 TC (đã đầy), nhưng tự chọn 6 TC còn trống → thêm tự chọn được
            ChuongTrinhBLL bll = Tao("K26", capBatBuoc: 3, capTuChon: 6, soSinhVien: 0, ("MH01", true));

            Assert.IsTrue(bll.ThemMonVaoChuongTrinh(new ChuongTrinhDTO("K26", "MH02", false), out string error), error);
        }
    }
}
