using BTLW.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BTLW.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route("admin")]
	[Route("admin/homeadmin")]
	public class HomeAdminController : Controller
	{
		[Route("")]
		[Route("index")]
		[Authentication]
		public IActionResult Index()
		{
			return View();
		}
	}
}
