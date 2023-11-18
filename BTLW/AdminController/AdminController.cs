using Azure;
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
        public IActionResult SuaHDN(HoaDonNhap hoaDonNhap)
        {
                db.Update(hoaDonNhap);
                db.SaveChanges();
                return RedirectToAction("HDN");
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
                return RedirectToAction("CTHDN", "Admin", new
                {
                    soHDN = soHDN
                });
            }
            db.Remove(db.HoaDonNhaps.Find(soHDN));
            db.SaveChanges();
            return RedirectToAction("HDN", "Admin");
        }

        public IActionResult CTHDN(string soHDN, int? page)
        {
            ViewBag.SoHDN = soHDN;
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstCTHDN = db.ChiTietHdns.Where(x => x.SoHdn.Equals(soHDN)).OrderBy(n => n.SoHdn);
            PagedList<ChiTietHdn> lst = new PagedList<ChiTietHdn>(lstCTHDN, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemCTHDN")]
        [HttpGet]
        public IActionResult ThemCTHDN()
        {
            ViewBag.MaNoithat = new SelectList(db.DmnoiThats.ToList(), "MaNoiThat", "TenNoiThat");
            ViewBag.SoHdn = new SelectList(db.HoaDonNhaps.ToList(), "SoHdn", "SoHdn");
            return View();
        }
        [Route("ThemCTHDN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemCTHDN(ChiTietHdn chiTietHdn)
        {
            TempData["Message"] = "";
            var checkMa = db.ChiTietHdns.Where(x => x.SoHdn == chiTietHdn.SoHdn && x.MaNoithat == chiTietHdn.MaNoithat).ToList();
            if (checkMa.Count() > 0)
            {
                TempData["Message"] = "Khong them duoc vi trung ma HD va ma noi that";
                return RedirectToAction("ThemCTHDN", "Admin");
            }
            else
            {
                string temp = chiTietHdn.SoHdn;
                db.ChiTietHdns.Add(chiTietHdn);
                db.SaveChanges();
                return RedirectToAction("CTHDN", new { soHDN = temp });
            }
        }

        [Route("SuaCTHDN")]
        [HttpGet]
        public IActionResult SuaCTHDN(string soHDN, string maNT)
        {
            var DK = db.ChiTietHdns.Find(maNT, soHDN);
            return View(DK);
        }
        [Route("SuaCTHDN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaCTHDN(ChiTietHdn chiTietHdn)
        {
            db.Update(chiTietHdn);
            db.SaveChanges();
            return RedirectToAction("CTHDN", new { soHDN = chiTietHdn.SoHdn });
        }

        [Route("XoaCTHDN")]
        [HttpGet]
        public IActionResult XoaCTHDN(string soHDN, string maNT)
        {
            db.Remove(db.ChiTietHdns.Find(maNT, soHDN));
            db.SaveChanges();
            return RedirectToAction("CTHDN", new { soHDN = soHDN });
        }
    }
}
