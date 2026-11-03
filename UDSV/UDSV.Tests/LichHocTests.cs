using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV.Tests
{
    [TestClass]
    public class LichHocTests
    {
        private static LichHocBLL TaoBll() =>
            new LichHocBLL(new LichHocDALGia(), new MonHocDALGia(("MH01", 3)));

        private static LichHocDTO HopLe() =>
            new LichHocDTO(0, "MH01", new DateTime(2026, 9, 7), "Ca 1 (07:00 - 11:00)", "P.301");

        [TestMethod]
        public void LichHoc_HopLe_TraVeTrue() =>
            Assert.IsTrue(LichHocBLL.KiemTraDuLieu(HopLe(), out string error), error);

        [TestMethod]
        public void LichHoc_ChuaChonMon_TraVeFalse()
        {
            LichHocDTO lh = HopLe();
            lh.MaMH = "";
            Assert.IsFalse(LichHocBLL.KiemTraDuLieu(lh, out string error));
            StringAssert.Contains(error, "môn học");
        }

        [TestMethod]
        public void LichHoc_CaHocRong_TraVeFalse()
        {
            LichHocDTO lh = HopLe();
            lh.CaHoc = "   ";
            Assert.IsFalse(LichHocBLL.KiemTraDuLieu(lh, out string error));
            StringAssert.Contains(error, "Ca học");
        }

        [TestMethod]
        public void LichHoc_NgayKhongHopLe_TraVeFalse()
        {
            LichHocDTO lh = HopLe();
            lh.NgayHoc = new DateTime(1999, 1, 1);
            Assert.IsFalse(LichHocBLL.KiemTraDuLieu(lh, out string error));
        }

        [TestMethod]
        public void LichHoc_MonKhongTonTai_BiTuChoi()
        {
            LichHocBLL bll = TaoBll();
            LichHocDTO lh = HopLe();
            lh.MaMH = "MH999";

            Assert.IsFalse(bll.ThemLichHoc(lh, out string error));
            StringAssert.Contains(error, "không tồn tại");
        }

        [TestMethod]
        public void LichHoc_ThemHopLe_ThanhCong()
        {
            LichHocBLL bll = TaoBll();
            Assert.IsTrue(bll.ThemLichHoc(HopLe(), out string error), error);
        }
    }
}
