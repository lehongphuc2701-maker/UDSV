using Microsoft.VisualStudio.TestTools.UnitTesting;
using UDSV.BLL;
using UDSV.DTO;

namespace UDSV.Tests
{
    /// <summary>Quy tắc của khóa: dữ liệu hợp lệ, không trùng mã, không xoá khóa đang có sinh viên.</summary>
    [TestClass]
    public class KhoaTests
    {
        private static KhoaDTO HopLe() => new KhoaDTO("K24", "Công nghệ thông tin K24", 2024, 2028, 27, 6);

        [TestMethod]
        public void Khoa_HopLe_TraVeTrue()
        {
            Assert.IsTrue(KhoaBLL.KiemTraDuLieu(HopLe(), out string error), error);
        }

        [TestMethod]
        public void Khoa_MaRong_TraVeFalse()
        {
            KhoaDTO k = HopLe();
            k.MaKhoa = "";
            Assert.IsFalse(KhoaBLL.KiemTraDuLieu(k, out string error));
            StringAssert.Contains(error, "Mã khóa");
        }

        [TestMethod]
        public void Khoa_TenRong_TraVeFalse()
        {
            KhoaDTO k = HopLe();
            k.TenKhoa = "  ";
            Assert.IsFalse(KhoaBLL.KiemTraDuLieu(k, out string error));
            StringAssert.Contains(error, "Tên khóa");
        }

        [TestMethod]
        public void Khoa_NamKetThucNhoHonNamBatDau_TraVeFalse()
        {
            KhoaDTO k = HopLe();
            k.NamKetThuc = 2023;
            Assert.IsFalse(KhoaBLL.KiemTraDuLieu(k, out string error));
            StringAssert.Contains(error, "Năm kết thúc");
        }

        [TestMethod]
        public void Khoa_TongTCBatBuocBangKhong_TraVeFalse()
        {
            KhoaDTO k = HopLe();
            k.TongTCBatBuoc = 0;
            Assert.IsFalse(KhoaBLL.KiemTraDuLieu(k, out string error));
            StringAssert.Contains(error, "tín chỉ bắt buộc");
        }

        [TestMethod]
        public void Khoa_TongTCTuChonAm_TraVeFalse()
        {
            KhoaDTO k = HopLe();
            k.TongTCTuChon = -1;
            Assert.IsFalse(KhoaBLL.KiemTraDuLieu(k, out string error));
        }

        [TestMethod]
        public void Khoa_ThemTrungMa_BiTuChoi()
        {
            KhoaBLL bll = new KhoaBLL(new KhoaDALGia(HopLe()));

            Assert.IsFalse(bll.Them(HopLe(), out string error));
            StringAssert.Contains(error, "đã tồn tại");
        }

        [TestMethod]
        public void Khoa_XoaKhiConSinhVien_BiTuChoi()
        {
            KhoaDALGia dal = new KhoaDALGia(HopLe());
            dal.DatSoSinhVien("K24", 8);
            KhoaBLL bll = new KhoaBLL(dal);

            Assert.IsFalse(bll.Xoa("K24", out string error));
            StringAssert.Contains(error, "đang có 8 sinh viên");
        }

        [TestMethod]
        public void Khoa_XoaKhiKhongConSinhVien_ThanhCong()
        {
            KhoaBLL bll = new KhoaBLL(new KhoaDALGia(HopLe()));

            Assert.IsTrue(bll.Xoa("K24", out string error), error);
        }
    }
}
