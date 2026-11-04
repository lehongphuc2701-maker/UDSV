using UDSV.BLL;

namespace UDSV.Tests
{
    /// <summary>
    /// Luật xoá mềm môn học: chỉ được đánh dấu xoá khi môn không còn nằm trong
    /// chương trình của bất kỳ khóa nào. Còn vướng khóa thì BLL phải từ chối và
    /// nói rõ đang vướng khóa nào, để người dùng biết phải gỡ ở đâu trước.
    /// </summary>
    [TestClass]
    public class MonHocXoaMemTests
    {
        [TestMethod]
        public void MonHoc_XoaMemKhiConTrongChuongTrinh_BiTuChoi()
        {
            MonHocDALGia dal = new MonHocDALGia(("IT101", 3));
            dal.DatKhoaDangDung("IT101", "K24", "K25");
            MonHocBLL bll = new MonHocBLL(dal);

            Assert.IsFalse(bll.XoaMem("IT101", out string error), "Môn còn trong chương trình thì không được xoá.");
            StringAssert.Contains(error, "K24", "Thông báo phải nêu tên khóa đang dùng môn.");
            StringAssert.Contains(error, "K25", "Thông báo phải nêu đủ mọi khóa đang dùng môn.");
            Assert.IsFalse(dal.DaXoa("IT101"), "Bị từ chối thì không được bật cờ xoá.");
        }

        [TestMethod]
        public void MonHoc_XoaMemKhiKhongConChuongTrinh_ThanhCong()
        {
            MonHocDALGia dal = new MonHocDALGia(("IT101", 3));
            MonHocBLL bll = new MonHocBLL(dal);

            Assert.IsTrue(bll.XoaMem("IT101", out string error), error);
            Assert.IsTrue(dal.DaXoa("IT101"), "Xoá mềm phải bật cờ DaXoa của môn.");
        }
    }
}
