using Azure;
using BTLW.Models;
using BTLW.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using X.PagedList;

namespace BTLW.AdminController
{
    //[Route("admin")]
    public class AdminController : Controller
	{ 
		Lttqnhom6Context db = new Lttqnhom6Context();
        //[Route("")]
        //[Route("index")]
       
        public IActionResult Index()
		{
			return View();
		}
        [Route("DanhMucSanPham")]
        public IActionResult DanhMucSanPham(int ?page)
        {
            int pageSize = 8;
            int pageNumber=page==null||page<0?1:page.Value;

            var lstsp=db.DmnoiThats.AsNoTracking().OrderBy(x=>x.TenNoiThat).ToList();
            PagedList<DmnoiThat> a=new PagedList<DmnoiThat>(lstsp,pageNumber,pageSize); 
            return View(a); 
        }
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.Maloai = new SelectList(db.TheLoais.ToList(), "Maloai", "Tenloai");
            ViewBag.Makieu = new SelectList(db.KieuDangs.ToList(), "Makieu", "Tenkieu");
            ViewBag.Mamau = new SelectList(db.MauSacs.ToList(), "Mamau", "Tenmau");
            ViewBag.Machatlieu = new SelectList(db.ChatLieus.ToList(), "Machatlieu", "Tenchatlieu");
            ViewBag.Manuocsx = new SelectList(db.NuocSxes.ToList(), "Manuocsx", "Tennuocsx");
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(DmnoiThat dmnoiThat)
        {
            TempData["Message"] = "";
            var dm=db.DmnoiThats.Where(x=>x.MaNoiThat.Equals(dmnoiThat.MaNoiThat)).ToList();
            if(dm.Count > 0)
            {
                TempData["Message"] = "trung ma noi that";
                return RedirectToAction("ThemMoiSanPham","Admin");   
            }else
            {
                db.DmnoiThats.Add(dmnoiThat);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
           
            
            //return View(dmnoiThat);
        }
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string manoithat)
        {
            ViewBag.Maloai = new SelectList(db.TheLoais.ToList(), "Maloai", "Tenloai");
            ViewBag.Makieu = new SelectList(db.KieuDangs.ToList(), "Makieu", "Tenkieu");
            ViewBag.Mamau = new SelectList(db.MauSacs.ToList(), "Mamau", "Tenmau");
            ViewBag.Machatlieu = new SelectList(db.ChatLieus.ToList(), "Machatlieu", "Tenchatlieu");
            ViewBag.Manuocsx = new SelectList(db.NuocSxes.ToList(), "Manuocsx", "Tennuocsx");
            ViewBag.MaNoiThat = manoithat;
            var sp = db.DmnoiThats.Find(manoithat);

            return View(sp);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(DmnoiThat dmnoiThat)
        {
            
                db.Update(dmnoiThat);   
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "Admin");
        
       
        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string manoithat)
        {
            TempData["Message"] = "";
            var ct=db.ChiTietHdns.Where(x=>x.MaNoithat== manoithat).ToList();
            if(ct.Count()>0)
            {
                TempData["Message"] = manoithat + " cant delete";
                return RedirectToAction("DanhMucSanPham", "Admin");
                
            }
            var asp=db.AnhNoiThats.Where(x=>x.MaNoiThat==manoithat);
            if (asp.Any()) db.RemoveRange(asp);
            db.Remove(db.DmnoiThats.Find(manoithat));
            db.SaveChanges();
            TempData["Message"] = ct+" deleted";
            return RedirectToAction("DanhMucSanPham", "Admin");
        }
        [Route("ChiTietSanPham")]
        public IActionResult ChiTietSanPham(string manoithat)
        {
            ViewBag.MaNT = manoithat;
            var sp=db.DmnoiThats.SingleOrDefault(x => x.MaNoiThat == manoithat);
            return View(sp);
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
    }
}
