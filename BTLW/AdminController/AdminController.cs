using BTLW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace BTLW.AdminController
{
	public class AdminController : Controller
	{
		Lttqnhom6Context db = new Lttqnhom6Context();
		public IActionResult Index()
		{
			return View();
		}

        [Route("HDN")]
        public IActionResult HDN(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstHDN = db.HoaDonNhaps.OrderBy(n => n.SoHdn);
            PagedList<HoaDonNhap> lst = new PagedList<HoaDonNhap>(lstHDN, pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemHDN")]
        [HttpGet]
        public IActionResult ThemHDN()
        {
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "TenNv");
            ViewBag.MaNcc = new SelectList(db.NhaCungCaps.ToList(), "MaNcc", "TenNcc");
            return View();
        }
        [Route("ThemHDN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemHDN(HoaDonNhap hoaDonNhap)
        {
            TempData["Message"] = "";
            var checkMa = db.HoaDonNhaps.Where(x => x.SoHdn == hoaDonNhap.SoHdn).ToList();
            if (checkMa.Count() > 0)
            {
                TempData["Message"] = "Khong them duoc vi trung ma hd";
                return RedirectToAction("ThemHDN", "Admin");
            }
            else
            {
                db.HoaDonNhaps.Add(hoaDonNhap);
                db.SaveChanges();
                return RedirectToAction("HDN");
            }
        }

        [Route("SuaHDN")]
        [HttpGet]
        public IActionResult SuaHDN(string soHDN)
        {
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "TenNv");
            ViewBag.MaNcc = new SelectList(db.NhaCungCaps.ToList(), "MaNcc", "TenNcc");
            ViewBag.SoHDN = soHDN;
            var DK = db.HoaDonNhaps.Find(soHDN);
            return View(DK);
        }
        [Route("SuaHDN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaDK(HoaDonNhap hoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                db.Update(hoaDonNhap);
                db.SaveChanges();
                return RedirectToAction("HDN");
            }
            return View(hoaDonNhap);
        }

        [Route("XoaHDN")]
        [HttpGet]
        public IActionResult XoaHDN(string soHDN)
        {
            TempData["Message1"] = "";
            var checkMa = db.ChiTietHdns.Where(x => x.SoHdn == soHDN).ToList();
            if (checkMa.Count() > 0)
            {
                TempData["Message1"] = "Xóa hết chi tiết HĐN trước.";
                return RedirectToAction("HDN", "Admin");
            }
            db.Remove(db.HoaDonNhaps.Find(soHDN));
            db.SaveChanges();
            TempData["Message1"] = "Da xoa sp";
            return RedirectToAction("HDN", "Admin");
        }
    }
}
