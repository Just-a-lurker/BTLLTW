using Microsoft.AspNetCore.Mvc;

namespace BTLW.AdminController
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
