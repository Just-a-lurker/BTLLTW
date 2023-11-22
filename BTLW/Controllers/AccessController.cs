using BTLW.Models;
using BTLW.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNet.Identity;

namespace BTLW.Controllers
{
    public class AccessController : Controller
    {
        public string uname = "";
        Lttqnhom6Context db = new Lttqnhom6Context();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("TenTK") == null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("index", "admin");
            }

        }
        [HttpPost]
        public IActionResult Login(TaiKhoan user)
        {
            bool a = true;
            if (HttpContext.Session.GetString("TenTK") == null)
            {
                var u = db.TaiKhoans.Where(x => x.TenTk.Equals(user.TenTk) && x.MatKhau.Equals(user.MatKhau)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("TenTK", u.TenTk.ToString());
                    var b = db.TaiKhoans.Where(x => x.TenTk.Equals(user.TenTk) && x.LoaiTk == false).FirstOrDefault();
                    if (b != null)
                    {
                        Response.Cookies.Append("UserLogin", b.TenTk.ToString(), new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(5)
                        });
                        Response.Cookies.Append("Password", b.MatKhau.ToString(), new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(5)
                        });
                        return RedirectToAction("index", "main");
                    }
                    else
                    {
                        TempData["Name"] = user.TenTk;
                        uname = user.TenTk;
                        ViewBag.TenTK = uname;
                        ViewData["UserName"] = user.TenTk;
                        return RedirectToAction("index", "admin");

                    }

                }
            }
            ViewBag.LoginFail = "Login Failed";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("TenTK");
            return RedirectToAction("Login", "Access");
        }

        [HttpGet]
        public IActionResult ThemTaiKhoan()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult ThemTaiKhoan(TaiKhoan user)
        {
            var dm = db.TaiKhoans.Where(x => x.TenTk.Equals(user.TenTk)).ToList();
            if (dm.Count > 0)
            {
                TempData["Message1"] = "trung ten tK";
                return RedirectToAction("ThemTaiKhoan", "Access");
            }
            else
            {
                //if (user.TenTk.ToString() != null && user.MatKhau.ToString() != null)
                user.LoaiTk = false;
                db.TaiKhoans.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "Access");
            }
        
    }
    }
}