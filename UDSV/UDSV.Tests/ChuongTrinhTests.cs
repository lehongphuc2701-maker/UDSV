using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV.Tests
{
    /// <summary>
    /// Chương trình khung theo luật chốt 2026-11-03:
    ///  R1 chưa khoá → sửa tự do, không trần tín chỉ
    ///  R2 bấm KHOÁ   → chốt tổng, ghi người/giờ, không mở lại
    ///  R3 đã khoá    → lưu được nếu tổng tín chỉ giữ nguyên
    ///  R4 đã khoá    → không bỏ môn mà sinh viên của khóa đã có điểm
    ///  R5 nhân bản   → copy toàn bộ dòng sang khóa mới (chưa khoá)
    /// </summary>
    [TestClass]
    public class ChuongTrinhTests
    {
        private const string K24 = "K24";

        private static (ChuongTrinhBLL Bll, ChuongTrinhDALGia Ct, KhoaDALGia Khoa) Tao(
            bool daKhoa = false,
            int soSinhVien = 0,
            params (string MaMH, bool BatBuoc, int HocKy)[] coSan)
        {
            MonHocDALGia monHoc = new MonHocDALGia(("MH01", 3), ("MH02", 3), ("MH03", 3), ("MH04", 4), ("MH05", 2));
            KhoaDALGia khoa = new KhoaDALGia(new KhoaDTO(K24, "CNTT K24", 2024, 2028, 6, 0));
            khoa.DatSoSinhVien(K24, soSinhVien);

            ChuongTrinhDALGia ct = new ChuongTrinhDALGia(monHoc);
            foreach ((string ma, bool bb, int hk) in coSan)
                ct.ThemSan(new ChuongTrinhDTO(K24, ma, bb, hk));

            if (daKhoa)
                khoa.KhoaChuongTrinh(K24, "admin", ct.TongTinChi(K24, true), ct.TongTinChi(K24, false));

            return (new ChuongTrinhBLL(ct, khoa, monHoc), ct, khoa);
        }

        private static List<ChuongTrinhDTO> Ds(params (string MaMH, bool BatBuoc, int HocKy)[] dong)
        {
            List<ChuongTrinhDTO> ds = new List<ChuongTrinhDTO>();
            foreach ((string ma, bool bb, int hk) in dong)
                ds.Add(new ChuongTrinhDTO(K24, ma, bb, hk));
            return ds;
        }

        // ---------- R1: chưa khoá thì tự do ----------

        [TestMethod]
        public void ChuaKhoa_LuuTuDo_KhongCoTran()
        {
            (ChuongTrinhBLL bll, _, _) = Tao(false, 0, ("MH01", true, 1));

            bool ok = bll.LuuChuongTrinh(K24,
                Ds(("MH01", true, 1), ("MH02", true, 2), ("MH03", true, 3), ("MH04", true, 4), ("MH05", true, 5)),
                out string error);

            Assert.IsTrue(ok, "Chưa khoá thì không được chặn: " + error);
        }

        [TestMethod]
        public void ThemSinhVienVaoKhoa_KhongTuDongKhoa()
        {
            // Luật bị bác bỏ: thêm sinh viên không được làm khoá chương trình
            (ChuongTrinhBLL bll, _, _) = Tao(false, 8, ("MH01", true, 1));

            Assert.IsFalse(bll.DaKhoa(K24), "Không được tự khoá chỉ vì khóa có sinh viên");
        }

        // ---------- R2: khoá ----------

        [TestMethod]
        public void KhoaChuongTrinh_ChotDungTongTinChi()
        {
            (ChuongTrinhBLL bll, _, KhoaDALGia khoa) = Tao(false, 0, ("MH01", true, 1), ("MH02", true, 2));

            Assert.IsTrue(bll.KhoaChuongTrinh(K24, "admin", out string error), error);

            KhoaDTO? k = khoa.LayTheoMa(K24);
            Assert.IsTrue(k!.DaKhoa);
            Assert.AreEqual(6, k.TongTCBatBuoc);
            Assert.AreEqual(0, k.TongTCTuChon);
            Assert.AreEqual("admin", k.NguoiKhoa);
        }

        [TestMethod]
        public void KhoaChuongTrinh_LanHai_BiTuChoi()
        {
            (ChuongTrinhBLL bll, _, _) = Tao(false, 0, ("MH01", true, 1));
            Assert.IsTrue(bll.KhoaChuongTrinh(K24, "admin", out _));

            Assert.IsFalse(bll.KhoaChuongTrinh(K24, "phuc", out string error));
            StringAssert.Contains(error, "đã được khoá");
        }

        [TestMethod]
        public void KhoaChuongTrinh_KhiTrong_BiTuChoi()
        {
            (ChuongTrinhBLL bll, _, _) = Tao();

            Assert.IsFalse(bll.KhoaChuongTrinh(K24, "admin", out string error));
            StringAssert.Contains(error, "trống");
        }

        // ---------- R3: sửa sau khi khoá ----------

        [TestMethod]
        public void DaKhoa_LuuKhiTongDoi_BiTuChoi()
        {
            (ChuongTrinhBLL bll, _, _) = Tao(true, 0, ("MH01", true, 1), ("MH02", true, 2));

            bool ok = bll.LuuChuongTrinh(K24,
                Ds(("MH01", true, 1), ("MH02", true, 2), ("MH03", true, 3)), out string error);

            Assert.IsFalse(ok, "Đã khoá thì tổng không được đổi");
            StringAssert.Contains(error, "giữ nguyên");
        }

        [TestMethod]
        public void DaKhoa_LuuKhiTongGiuNguyen_ThanhCong()
        {
            (ChuongTrinhBLL bll, _, _) = Tao(true, 0, ("MH01", true, 1), ("MH02", true, 2));

            bool ok = bll.LuuChuongTrinh(K24,
                Ds(("MH01", true, 1), ("MH02", true, 3)), out string error);

            Assert.IsTrue(ok, error);
        }

        // ---------- R4: không bỏ môn đã có điểm ----------

        [TestMethod]
        public void DaKhoa_BoMonDaCoDiem_BiTuChoi()
        {
            (ChuongTrinhBLL bll, ChuongTrinhDALGia ct, _) =
                Tao(true, 0, ("MH01", true, 1), ("MH02", true, 2));

            ct.SoSinhVienCoDiem[K24 + "|MH02"] = 3;

            bool ok = bll.LuuChuongTrinh(K24,
                Ds(("MH01", true, 1), ("MH03", true, 2)), out string error);

            Assert.IsFalse(ok, "Không được bỏ môn đã có điểm");
            StringAssert.Contains(error, "MH02");
        }

        // ---------- R5: nhân bản ----------

        [TestMethod]
        public void NhanBan_CopyDuDong_SangKhoaMoi()
        {
            (ChuongTrinhBLL bll, _, KhoaDALGia khoa) =
                Tao(false, 0, ("MH01", true, 1), ("MH02", true, 2), ("MH03", false, 3));

            Assert.IsTrue(bll.NhanBan(K24, "K27", "CNTT K27", 2027, 2031, out string error), error);

            Assert.IsTrue(khoa.KiemTraTonTai("K27"));
            Assert.AreEqual(3, bll.LayTheoKhoa("K27").Rows.Count);
            Assert.IsFalse(bll.DaKhoa("K27"), "Khóa mới phải ở trạng thái chưa khoá");
        }

        [TestMethod]
        public void NhanBan_TrungMaKhoa_BiTuChoi()
        {
            (ChuongTrinhBLL bll, _, _) = Tao(false, 0, ("MH01", true, 1));

            Assert.IsFalse(bll.NhanBan(K24, K24, "trùng", 2027, 2031, out string error));
            StringAssert.Contains(error, "đã tồn tại");
        }

        // ---------- Kiểm tra dòng ----------

        [TestMethod]
        public void KiemTraDong_TrungMon_BiTuChoi()
        {
            Assert.IsFalse(ChuongTrinhBLL.KiemTraDong(
                Ds(("MH01", true, 1), ("MH01", true, 2)), out string error));
            StringAssert.Contains(error, "trùng");
        }

        [TestMethod]
        public void KiemTraDong_HocKyNgoai1Den10_BiTuChoi()
        {
            Assert.IsFalse(ChuongTrinhBLL.KiemTraDong(
                Ds(("MH01", true, 11)), out string error));
            StringAssert.Contains(error, "học kỳ");
        }

        [TestMethod]
        public void KiemTraDong_Rong_BiTuChoi()
        {
            Assert.IsFalse(ChuongTrinhBLL.KiemTraDong(new List<ChuongTrinhDTO>(), out string error));
            StringAssert.Contains(error, "ít nhất");
        }
    }
}
