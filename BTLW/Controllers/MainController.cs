using BTLW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using BTLW.Models.Authentication;

namespace BTLW.Controllers
{
	public class MainController : Controller
	{
		Lttqnhom6Context db = new Lttqnhom6Context();

		[Authentication]
		public IActionResult Shop(int? page)
		{
			int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var noiThat = db.DmnoiThats.AsNoTracking().OrderBy(x => x.TenNoiThat);
            PagedList<DmnoiThat> lstNoiThat = new PagedList<DmnoiThat>(noiThat, pageNumber, pageSize);
            return View(lstNoiThat);
		}

        [HttpGet]
        public IActionResult Timkiem()
        {
			return View();
        }

        [HttpPost]
        public IActionResult Timkiem(DmnoiThat dm)
        {
            return RedirectToAction("Timkiem2", dm);
        }


        //[ValidateAntiForgeryToken]
        public IActionResult Timkiem2(int? page, DmnoiThat dm)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var noiThat = db.DmnoiThats.Where(x => x.TenNoiThat.Contains(dm.Timkiem));
            ViewBag.maLoai = dm.Timkiem;
            PagedList<DmnoiThat> lstNoiThat = new PagedList<DmnoiThat>(noiThat, pageNumber, pageSize);
            return View(lstNoiThat);
        }
        public IActionResult ShopTheoLoai(int? page, string maLoai)
		{
			int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var noiThat = db.DmnoiThats.Where(x=>x.Maloai==maLoai);
            PagedList<DmnoiThat> lstNoiThat = new PagedList<DmnoiThat>(noiThat, pageNumber, pageSize);
			ViewBag.maLoai = maLoai;
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

		public IActionResult IndexTheoNuocSanXuat(int? page, string maNuocSx) 
		{
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var noiThat = db.DmnoiThats.Where(x => x.Manuocsx==maNuocSx);
            PagedList<DmnoiThat> lstNoiThat = new PagedList<DmnoiThat>(noiThat, pageNumber, pageSize);
			ViewBag.maNuocSx = maNuocSx;
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
