using BTLW.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLW.Areas.Admin.Controllers
{
	public class AccessController : Controller
	{
		Lttqnhom6Context db=new Lttqnhom6Context();
		[HttpGet]
		public IActionResult Login()
		{
			if(HttpContext.Session.GetString("TenTK")==null)
			{
				return View();
			}else
			{
				return RedirectToAction("Index","Home");
			}
			
		}
		[HttpPost]
		public IActionResult Login(TaiKhoan user)
		{
			if(HttpContext.Session.GetString("TenTK")==null)
			{
				var u=db.TaiKhoans.Where(x=>x.TenTk.Equals(user.TenTk)&& x.MatKhau.Equals(user.MatKhau)).FirstOrDefault();
				if (u!=null)
				{
					HttpContext.Session.SetString("TenTK", u.TenTk.ToString());
                    return RedirectToAction("Index", "Home");
                }
			}
			return View();
		}
	}
}
