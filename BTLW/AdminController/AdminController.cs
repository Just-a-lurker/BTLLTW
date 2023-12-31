﻿using Azure;
using BTLW.Models;
using BTLW.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.Entity;
using X.PagedList;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Reflection;

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
        [Authentication]
        public IActionResult Index()
        {
            ViewBag.Time = System.DateTime.Now;
            return View();
        }

        public IActionResult Register(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstsp = db.TaiKhoans.AsNoTracking().OrderBy(x => x.TenTk).ToList();
            PagedList<TaiKhoan> a = new PagedList<TaiKhoan>(lstsp, pageNumber, pageSize);
            return View(a);
        }

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

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult ThemTaiKhoan(TaiKhoan user)
        {
            TempData["Message1"] = "";
            var dm = db.TaiKhoans.Where(x => x.TenTk.Equals(user.TenTk)).ToList();
            if (dm.Count > 0)
            {
                TempData["Message1"] = "trung ten tK";
                return RedirectToAction("ThemTaiKhoan", "Admin");
            }
            else
            {
                if(user.TenTk.ToString() != null && user.MatKhau.ToString() != null)
                TempData["Message1"] = "Ok";
                db.TaiKhoans.Add(user);
                db.SaveChanges();
                return RedirectToAction("Register", "Admin");
            }
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTaiKhoan(TaiKhoan user)
        {

            db.Update(user);
            db.SaveChanges();
            return RedirectToAction("Register", "Admin");


        }

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

        public IActionResult DanhMucSanPham(int ?page)
        {
            int pageSize = 8;
            int pageNumber=page==null||page<0?1:page.Value;

            var lstsp=db.DmnoiThats.AsNoTracking().OrderBy(x=>x.TenNoiThat).ToList();
            PagedList<DmnoiThat> a=new PagedList<DmnoiThat>(lstsp,pageNumber,pageSize); 
            return View(a); 
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(DmnoiThat dmnoiThat)
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
                if (dmnoiThat.UploadedFile != null && dmnoiThat.UploadedFile.Length > 0)
                {
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image_Furniture", dmnoiThat.UploadedFile.FileName);

                    using (Stream stream = new FileStream(imagePath, FileMode.Create))
                    {
                        dmnoiThat.UploadedFile.CopyTo(stream);
                    }

                    var anhNoiThat = new AnhNoiThat
                    {
                        MaNoiThat = dmnoiThat.MaNoiThat,
                        TenFileAnh = dmnoiThat.UploadedFile.FileName
                    };
                    dmnoiThat.Anh = dmnoiThat.UploadedFile.FileName;
                    db.AnhNoiThats.Add(anhNoiThat);
                    db.SaveChanges();
                }

                return RedirectToAction("DanhMucSanPham");
            }
        }


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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(DmnoiThat dmnoiThat)
        {
            if (dmnoiThat.UploadedFile != null && dmnoiThat.UploadedFile.Length > 0)
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image_Furniture", dmnoiThat.UploadedFile.FileName);

                using (Stream stream = new FileStream(imagePath, FileMode.Create))
                {
                    dmnoiThat.UploadedFile.CopyTo(stream);
                }

                var anhNoiThat = new AnhNoiThat
                {
                    MaNoiThat = dmnoiThat.MaNoiThat,
                    TenFileAnh = dmnoiThat.UploadedFile.FileName
                };
                dmnoiThat.Anh = dmnoiThat.UploadedFile.FileName;
                db.AnhNoiThats.Update(anhNoiThat);
                db.SaveChanges();
            }
            db.Update(dmnoiThat);   
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "Admin");
        }

        [HttpGet]
        public IActionResult XoaSanPham(string manoithat)
        {
            //Xoa anh trong folder
            //var dmnoiThat = db.DmnoiThats.Find(manoithat);
            //string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Image_Furniture", dmnoiThat.Anh.ToString());
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
            //Xoa anh trong folder
            //var fileStream = new FileStream(
            //imagePath,
            // FileMode.Create,
            //FileAccess.ReadWrite,
            // FileShare.Read,
            //4096,
            //FileOptions.DeleteOnClose);
            //using (fileStream)
            //{
            //    ;
            //}
            TempData["Message"] = manoithat +" deleted";
            return RedirectToAction("DanhMucSanPham", "Admin");
        }

        public IActionResult ChiTietSanPham(string manoithat)
        {
            ViewBag.MaNT = manoithat;
            var sp=db.DmnoiThats.SingleOrDefault(x => x.MaNoiThat == manoithat);
            return View(sp);
        }


        public IActionResult HDN(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstHDN = db.HoaDonNhaps.OrderBy(n => n.SoHdn);
            PagedList<HoaDonNhap> lst = new PagedList<HoaDonNhap>(lstHDN, pageNumber, pageSize);
            return View(lst);
        }
        
 
        [HttpGet]
        public IActionResult ThemHDN()
        {
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "TenNv");
            ViewBag.MaNcc = new SelectList(db.NhaCungCaps.ToList(), "MaNcc", "TenNcc");
            return View();
        }

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


        [HttpGet]
        public IActionResult SuaHDN(string soHDN)
        {
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "TenNv");
            ViewBag.MaNcc = new SelectList(db.NhaCungCaps.ToList(), "MaNcc", "TenNcc");
            ViewBag.SoHDN = soHDN;
            var DK = db.HoaDonNhaps.Find(soHDN);
            return View(DK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaHDN(HoaDonNhap hoaDonNhap)
        {
                db.Update(hoaDonNhap);
                db.SaveChanges();
                return RedirectToAction("HDN");
        }

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


        [HttpGet]
        public IActionResult ThemCTHDN()
        {
            ViewBag.MaNoithat = new SelectList(db.DmnoiThats.ToList(), "MaNoiThat", "MaNoiThat");
            ViewBag.SoHdn = new SelectList(db.HoaDonNhaps.ToList(), "SoHdn", "SoHdn", TempData["soN"]);
            return View();
        }

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


        [HttpGet]
        public IActionResult SuaCTHDN(string soHDN, string maNT)
        {
            var DK = db.ChiTietHdns.Find(maNT, soHDN);
            return View(DK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaCTHDN(ChiTietHdn chiTietHdn)
        {
            db.Update(chiTietHdn);
            db.SaveChanges();
            return RedirectToAction("CTHDN", new { soHDN = chiTietHdn.SoHdn });
        }


        [HttpGet]
        public IActionResult XoaCTHDN(string soHDN, string maNT)
        {
            db.Remove(db.ChiTietHdns.Find(maNT, soHDN));
            db.SaveChanges();
            return RedirectToAction("CTHDN", new { soHDN = soHDN });
        }



        public IActionResult DDH(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstHDN = db.DonDatHangs.OrderBy(n => n.SoDdh);
            PagedList<DonDatHang> lst = new PagedList<DonDatHang>(lstHDN, pageNumber, pageSize);
            return View(lst);
        }


        [HttpGet]
        public IActionResult ThemDDH()
        {
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "TenNv");
            ViewBag.MaKhach = new SelectList(db.KhachHangs.ToList(), "MaKhach", "TenKhach");
            return View();
        }

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


        [HttpGet]
        public IActionResult SuaDDH(string soDDH)
        {
            ViewBag.MaNv = new SelectList(db.NhanViens.ToList(), "MaNv", "TenNv");
            ViewBag.MaKhach = new SelectList(db.KhachHangs.ToList(), "MaKhach", "TenKhach");
            ViewBag.soDDH = soDDH;
            var DK = db.DonDatHangs.Find(soDDH);
            return View(DK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaDDH(DonDatHang donDatHang)
        {
            db.Update(donDatHang);
            db.SaveChanges();
            return RedirectToAction("DDH");
        }


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


        [HttpGet]
        public IActionResult ThemCTDDH()
        {
            ViewBag.MaNoithat = new SelectList(db.DmnoiThats.ToList(), "MaNoiThat", "MaNoiThat");
            ViewBag.SoDdh = new SelectList(db.DonDatHangs.ToList(), "SoDdh", "SoDdh", TempData["soD"]);
            return View();
        }

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


        [HttpGet]
        public IActionResult SuaCTDDH(string soDDH, string maNT)
        {
            var DK = db.ChiTietHddhs.Find(maNT, soDDH);
            return View(DK);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaCTDDH(ChiTietHddh chiTietHddh)
        {
            db.Update(chiTietHddh);
            db.SaveChanges();
            return RedirectToAction("CTDDH", new { soDDH = chiTietHddh.SoDdh });
        }


        [HttpGet]
        public IActionResult XoaCTDDH(string soDDH, string maNT)
        {
            db.Remove(db.ChiTietHddhs.Find(maNT, soDDH));
            db.SaveChanges();
            return RedirectToAction("CTDDH", new { soDDH = soDDH });
        }
    }
}
