using BTLW.Models;
using BTLW.ViewModel;
using System.Security.Cryptography;
using System.Text;
//using system.web.mvc;
using Microsoft.AspNetCore.Mvc;
namespace BTLW.Controllers
{
    public class AccountController : Controller
    {
        private DB_Entities db = new DB_Entities();
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangky(User _user)
        {    
                var check = db.Users.FirstOrDefault(s => s.TenTK == _user.TenTK);
                if (check == null)
                {
                    _user.MatKhau = GetMD5(_user.MatKhau);
                    db.Users.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Login","Access");
               
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }

            return View();


        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }


    }
}
