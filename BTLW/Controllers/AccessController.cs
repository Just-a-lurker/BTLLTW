using BTLW.Models;
using BTLW.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace BTLW.Controllers
{
    public class AccessController : Controller
    {

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
                    var b=db.TaiKhoans.Where(x=>x.TenTk.Equals(user.TenTk)&&x.LoaiTk==false).FirstOrDefault();
                    if (b != null)
                    {
                        return RedirectToAction("index", "main");

                    }
                    else
                    {
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
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterVM Model)
        {
            TempData["Message"] = "";
            var a = db.TaiKhoans.Where(x => x.TenTk.Equals(Model.TenTK)).ToList();
            if(a.Count()>0)
            {
                TempData["Message"] = "Loi,trung ma";
                return RedirectToAction("Register", "Access");
            }else
            {
                TempData["Message"] = "OK";
                var user = new TaiKhoan();
                    user.TenTk = Model.TenTK;
                    user.MatKhau = MD5Hash(Model.MatKhau);
                    user.LoaiTk = Model.LoaiTK;
                    db.TaiKhoans.Add(user);
                    db.SaveChanges();
                    ViewBag.Success = "Successful Registration";
                    Model = new RegisterVM();
                return RedirectToAction("Login", "Access");

            }
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(TaiKhoan user)
        {
            TempData["Message"] = "";
            var a = db.TaiKhoans.FirstOrDefault(x => x.TenTk.Equals(user.TenTk));
            if (a!=null)
            {
                TempData["Message"] = "LOI";
                return RedirectToAction("Signup", "Access");
            }
            else
            {
                TempData["Message"] = "OK";
                user.MatKhau = MD5Hash(user.MatKhau);
                db.TaiKhoans.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "Access");
            }
            //return View(user);
        }
        public string MD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
