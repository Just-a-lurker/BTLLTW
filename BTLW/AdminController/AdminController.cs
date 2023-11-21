using Azure;
using BTLW.Models;
using BTLW.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using X.PagedList;
using Microsoft.AspNetCore.Hosting;

namespace BTLW.AdminController
{
    //[Route("admin")]
    public class AdminController : Controller
	{ 
		Lttqnhom6Context db = new Lttqnhom6Context();
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.Time = System.DateTime.Now;
            return View();
        }
        [Route("DanhMucTaiKhoan")]
        public IActionResult Register(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstsp = db.TaiKhoans.AsNoTracking().OrderBy(x => x.TenTk).ToList();
            PagedList<TaiKhoan> a = new PagedList<TaiKhoan>(lstsp, pageNumber, pageSize);
            return View(a);
        }
        [Route("ThemTaiKhoan")]
        [HttpGet]
        public IActionResult ThemTaiKhoan()
        {
            ViewBag.Maloai = new SelectList(
                            new List<SelectListItem>
                            {
                                new SelectListItem { Text = "Admin", Value = "True"},
                                new SelectListItem {Text = "User", Value = "False"},
                            }, "Value", "Text");
            return View();
        }
        [Route("ThemTaiKhoan")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult ThemTaiKhoan(TaiKhoan user)
        {
            /*empData["Message"] = "";*/
            var dm = db.TaiKhoans.Where(x => x.TenTk.Equals(user.TenTk)).ToList();
            if (dm.Count > 0)
            {
                //TempData["Message"] = "trung ma tK";
                return RedirectToAction("ThemTaiKhoan", "Admin");
            }
            else
            {
                //TempData["Message"] = "Ok";
                db.TaiKhoans.Add(user);
                db.SaveChanges();
                return RedirectToAction("Register", "Admin");
            }
        }
        [Route("SuaTaiKhoan")]
        [HttpGet]
        public IActionResult SuaTaiKhoan(int mataikhoan)
        {
            ViewBag.Maloai = new SelectList(
                 new List<SelectListItem>
                 {
                                new SelectListItem { Text = "Admin", Value = "True"},
                                new SelectListItem {Text = "User", Value = "False"},
                 }, "Value", "Text");
            ViewBag.manoithat = mataikhoan;
            var sp = db.TaiKhoans.Find(mataikhoan);

            return View(sp);
        }
        [Route("SuaTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTaiKhoan(TaiKhoan user)
        {

            db.Update(user);
            db.SaveChanges();
            return RedirectToAction("Register", "Admin");


        }
        [Route("XoaTaiKhoan")]
        [HttpGet]
        public IActionResult XoaTaiKhoan(int mataikhoan)
        {
            TempData["Message"] = "";
            var ct = db.TaiKhoans.Where(x => x.MaTk == mataikhoan).ToList();

            db.Remove(db.TaiKhoans.Find(mataikhoan));
            db.SaveChanges();
            TempData["Message"] = "TK[" + mataikhoan + "] deleted";
            return RedirectToAction("Register", "Admin");
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
        /*[Route("ThemSanPhamMoi")]
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
        }*/
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(DmnoiThat dmnoiThat, IFormFile anhFile)
        {
            TempData["Message"] = "";
            var dm = db.DmnoiThats.Where(x => x.MaNoiThat.Equals(dmnoiThat.MaNoiThat)).ToList();
            if (dm.Count > 0)
            {
                TempData["Message"] = "Trùng mã nội thất";
                return RedirectToAction("ThemMoiSanPham", "Admin");
            }
            else
            {
                db.DmnoiThats.Add(dmnoiThat);
                db.SaveChanges();

                if (anhFile != null && anhFile.Length > 0)
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedImages", anhFile.FileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        anhFile.CopyTo(stream);
                    }

                    var anhNoiThat = new AnhNoiThat
                    {
                        MaNoiThat = dmnoiThat.MaNoiThat,
                        TenFileAnh = anhFile.FileName
                    };

                    db.AnhNoiThats.Add(anhNoiThat);
                    db.SaveChanges();
                }

                return RedirectToAction("DanhMucSanPham");
            }
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
            TempData["Message"] = manoithat +" deleted";
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
                TempData["Message"] = "Không thêm được vì trùng số hóa đơn nhập";
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
            TempData["soN"] = soHDN;
            return View(lst);
        }

        [Route("ThemCTHDN")]
        [HttpGet]
        public IActionResult ThemCTHDN()
        {
            ViewBag.MaNoithat = new SelectList(db.DmnoiThats.ToList(), "MaNoiThat", "MaNoiThat");
            ViewBag.SoHdn = new SelectList(db.HoaDonNhaps.ToList(), "SoHdn", "SoHdn", TempData["soN"]);
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
                TempData["Message"] = "Không thêm được vì trùng số hóa đơn nhập và mã nội thất";
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


        [Route("DDH")]
        public IActionResult DDH(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstHDN = db.DonDatHangs.OrderBy(n => n.SoDdh);
            PagedList<DonDatHang> lst = new PagedList<DonDatHang>(lstHDN, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemDDH")]
        [HttpGet]
        public IActionResult ThemDDH()
        {
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "TenNv");
            ViewBag.MaKhach = new SelectList(db.KhachHangs.ToList(), "MaKhach", "TenKhach");
            return View();
        }
        [Route("ThemDDH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemDDH(DonDatHang donDatHang)
        {
            TempData["Message"] = "";
            var checkMa = db.DonDatHangs.Where(x => x.SoDdh == donDatHang.SoDdh).ToList();
            if (checkMa.Count() > 0)
            {
                TempData["Message"] = "Không thêm được vì trùng số hóa đơn đặt hàng";
                return RedirectToAction("ThemDDH", "Admin");
            }
            else
            {
                db.DonDatHangs.Add(donDatHang);
                db.SaveChanges();
                return RedirectToAction("DDH");
            }
        }

        [Route("SuaDDH")]
        [HttpGet]
        public IActionResult SuaDDH(string soDDH)
        {
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "TenNv");
            ViewBag.MaKhach = new SelectList(db.KhachHangs.ToList(), "MaKhach", "TenKhach");
            ViewBag.soDDH = soDDH;
            var DK = db.DonDatHangs.Find(soDDH);
            return View(DK);
        }
        [Route("SuaDDH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaDDH(DonDatHang donDatHang)
        {
            db.Update(donDatHang);
            db.SaveChanges();
            return RedirectToAction("DDH");
        }

        [Route("XoaDDH")]
        [HttpGet]
        public IActionResult XoaDDH(string soDDH)
        {
            TempData["Message1"] = "";
            var checkMa = db.ChiTietHddhs.Where(x => x.SoDdh == soDDH).ToList();
            if (checkMa.Count() > 0)
            {
                TempData["Message1"] = "Xóa hết chi tiết ĐĐH trước.";
                return RedirectToAction("CTDDH", "Admin", new
                {
                    soDDH = soDDH
                });
            }
            db.Remove(db.DonDatHangs.Find(soDDH));
            db.SaveChanges();
            return RedirectToAction("DDH", "Admin");
        }

        public IActionResult CTDDH(string soDDH, int? page)
        {
            ViewBag.soDDH = soDDH;
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstCTDDH = db.ChiTietHddhs.Where(x => x.SoDdh.Equals(soDDH)).OrderBy(n => n.SoDdh);
            PagedList<ChiTietHddh> lst = new PagedList<ChiTietHddh>(lstCTDDH, pageNumber, pageSize);
            TempData["soD"] = soDDH;
            return View(lst);
        }

        [Route("ThemCTDDH")]
        [HttpGet]
        public IActionResult ThemCTDDH()
        {
            ViewBag.MaNoithat = new SelectList(db.DmnoiThats.ToList(), "MaNoiThat", "MaNoiThat");
            ViewBag.SoDdh = new SelectList(db.DonDatHangs.ToList(), "SoDdh", "SoDdh", TempData["soD"]);
            return View();
        }
        [Route("ThemCTDDH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemCTDDH(ChiTietHddh chiTietHddh)
        {
            TempData["Message"] = "";
            var checkMa = db.ChiTietHddhs.Where(x => x.SoDdh == chiTietHddh.SoDdh && x.MaNoithat == chiTietHddh.MaNoithat).ToList();
            if (checkMa.Count() > 0)
            {
                TempData["Message"] = "Không thêm được vì trùng số hóa đơn đặt hàng và mã nội thất";
                return RedirectToAction("ThemCTDDH", "Admin");
            }
            else
            {
                string temp = chiTietHddh.SoDdh;
                db.ChiTietHddhs.Add(chiTietHddh);
                db.SaveChanges();
                return RedirectToAction("CTDDH", new { soDDH = temp });
            }
        }

        [Route("SuaCTDDH")]
        [HttpGet]
        public IActionResult SuaCTDDH(string soDDH, string maNT)
        {
            var DK = db.ChiTietHddhs.Find(maNT, soDDH);
            return View(DK);
        }
        [Route("SuaCTDDH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaCTDDH(ChiTietHddh chiTietHddh)
        {
            db.Update(chiTietHddh);
            db.SaveChanges();
            return RedirectToAction("CTDDH", new { soDDH = chiTietHddh.SoDdh });
        }

        [Route("XoaCTDDH")]
        [HttpGet]
        public IActionResult XoaCTDDH(string soDDH, string maNT)
        {
            db.Remove(db.ChiTietHddhs.Find(maNT, soDDH));
            db.SaveChanges();
            return RedirectToAction("CTDDH", new { soDDH = soDDH });
        }
    }
}
