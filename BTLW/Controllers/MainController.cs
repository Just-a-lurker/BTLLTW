using BTLW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTLW.Controllers
{
	public class MainController : Controller
	{
		Lttqnhom6Context db = new Lttqnhom6Context();

		public IActionResult Shop(int? page)
		{
			int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var noiThat = db.DmnoiThats.AsNoTracking().OrderBy(x => x.TenNoiThat);
            PagedList<DmnoiThat> lstNoiThat = new PagedList<DmnoiThat>(noiThat, pageNumber, pageSize);
            return View(lstNoiThat);
		}

		public IActionResult ShopTheoLoai(int? page, string maLoai)
		{
			int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var noiThat = db.DmnoiThats.Where(x=>x.Maloai==maLoai);
            PagedList<DmnoiThat> lstNoiThat = new PagedList<DmnoiThat>(noiThat, pageNumber, pageSize);
            return View(lstNoiThat);
		}

		public IActionResult Detail(string maNoiThat)
		{
			var noiThat = db.DmnoiThats.SingleOrDefault(x=>x.MaNoiThat==maNoiThat);
			var anh = db.AnhNoiThats.Where(x=>x.MaNoiThat==maNoiThat).ToList();
			ViewBag.anh = anh;
			return View(noiThat);
		}

		public IActionResult Index(int? page)
		{
			int pageSize = 8;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var noiThat = db.DmnoiThats.AsNoTracking().OrderBy(x => x.TenNoiThat);
			PagedList<DmnoiThat> lstNoiThat = new PagedList<DmnoiThat>(noiThat, pageNumber, pageSize);
            return View(lstNoiThat);
		}

		public IActionResult Blog()
		{
			return View();
		}

		public IActionResult ContactUs()
		{
			return View();
		}

		public IActionResult AboutUs()
		{
			return View();
		}
	}
}
