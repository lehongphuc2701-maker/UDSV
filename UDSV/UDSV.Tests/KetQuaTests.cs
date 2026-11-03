using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV.Tests
{
    /// <summary>
    /// Kết quả học tập: điểm hệ 4 (0..4, đạt từ 2.0), mỗi cặp (sinh viên, môn) một dòng,
    /// không có học lại — lưu lần hai là sửa.
    /// </summary>
    [TestClass]
    public class KetQuaTests
    {
        private static (KetQuaBLL Bll, KetQuaDALGia Kq, SinhVienDALGia Sv) TaoBll()
        {
            KetQuaDALGia kq = new KetQuaDALGia(("MH01", 3), ("MH02", 3), ("MH03", 2));
            SinhVienDALGia sv = new SinhVienDALGia();
            sv.ThemSan(new SinhVienDTO("SV001", "Nguyễn Văn An", new DateTime(2006, 5, 12), "Nam", "K24", "0901"));
            MonHocDALGia mh = new MonHocDALGia(("MH01", 3), ("MH02", 3), ("MH03", 2));
            return (new KetQuaBLL(kq, sv, mh), kq, sv);
        }

        // ---------- Quy tắc điểm ----------

        [TestMethod]
        public void Diem_Zero_TraVeTrue() =>
            Assert.IsTrue(KetQuaBLL.KiemTraDiem(0m, out string error), error);

        [TestMethod]
        public void Diem_Bon_TraVeTrue() =>
            Assert.IsTrue(KetQuaBLL.KiemTraDiem(4m, out string error), error);

        [TestMethod]
        public void Diem_Am_TraVeFalse() =>
            Assert.IsFalse(KetQuaBLL.KiemTraDiem(-0.01m, out _));

        [TestMethod]
        public void Diem_LonHonBon_TraVeFalse() =>
            Assert.IsFalse(KetQuaBLL.KiemTraDiem(4.01m, out _));

        [TestMethod]
        public void Diem_ChuaCoDiem_TraVeTrue() =>
            Assert.IsTrue(KetQuaBLL.KiemTraDiem(null, out string error), error);

        [TestMethod]
        public void Diem_DungHai_TinhLaDat() =>
            Assert.IsTrue(KetQuaBLL.LaDiemDat(2.0m));

        [TestMethod]
        public void Diem_MotChinChin_KhongTinhLaDat() =>
            Assert.IsFalse(KetQuaBLL.LaDiemDat(1.99m));

        [TestMethod]
        public void Diem_TrongKhiChuaCoDiem_KhongTinhLaDat() =>
            Assert.IsFalse(KetQuaBLL.LaDiemDat(null));

        // ---------- Nhập điểm ----------

        [TestMethod]
        public void NhapDiem_LanDau_ThemDongMoi()
        {
            (KetQuaBLL bll, KetQuaDALGia kq, _) = TaoBll();

            Assert.IsTrue(bll.NhapDiem("SV001", "MH01", 3.2m, out string error), error);
            Assert.AreEqual(1, kq.TongSoDong);
        }

        [TestMethod]
        public void NhapDiem_LanHai_SuaChuKhongTaoDongMoi()
        {
            (KetQuaBLL bll, KetQuaDALGia kq, _) = TaoBll();

            bll.NhapDiem("SV001", "MH01", 3.2m, out _);
            bll.NhapDiem("SV001", "MH01", 3.5m, out string error);

            Assert.AreEqual(1, kq.TongSoDong, "không được sinh dòng thứ hai: " + error);
            Assert.AreEqual(3.5m, kq.LayTheoSinhVien("SV001").Rows[0]["Diem"]);
        }

        [TestMethod]
        public void NhapDiem_MonKhongTonTai_BiTuChoi()
        {
            (KetQuaBLL bll, _, _) = TaoBll();

            Assert.IsFalse(bll.NhapDiem("SV001", "MH999", 3m, out string error));
            StringAssert.Contains(error, "không tồn tại");
        }

        [TestMethod]
        public void NhapDiem_SinhVienKhongTonTai_BiTuChoi()
        {
            (KetQuaBLL bll, _, _) = TaoBll();

            Assert.IsFalse(bll.NhapDiem("SV999", "MH01", 3m, out string error));
            StringAssert.Contains(error, "không tồn tại");
        }

        [TestMethod]
        public void NhapDiem_ThieuMaSinhVien_BiTuChoi()
        {
            (KetQuaBLL bll, _, _) = TaoBll();

            Assert.IsFalse(bll.NhapDiem("", "MH01", 3m, out string error));
            StringAssert.Contains(error, "sinh viên");
        }

        // ---------- Tín chỉ tích luỹ ----------

        [TestMethod]
        public void TinhTinChiTichLuy_ChiTinhMonDat()
        {
            (KetQuaBLL bll, KetQuaDALGia kq, _) = TaoBll();

            kq.DatDiem("SV001", "MH01", 3.2m);   // đạt, 3 TC
            kq.DatDiem("SV001", "MH02", 2.5m);   // đạt, 3 TC
            kq.DatDiem("SV001", "MH03", 1.8m);   // hỏng, 0 TC

            Assert.AreEqual(6, bll.TinhTinChiTichLuy("SV001"));
        }

        [TestMethod]
        public void TinhTinChiTichLuy_ChuaCoDiem_BangKhong()
        {
            (KetQuaBLL bll, KetQuaDALGia kq, _) = TaoBll();
            kq.DatDiem("SV001", "MH01", null);

            Assert.AreEqual(0, bll.TinhTinChiTichLuy("SV001"));
        }
    }
}
