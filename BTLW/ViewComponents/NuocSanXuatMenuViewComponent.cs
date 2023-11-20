using BTLW.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BTLW.ViewComponents
{
    public class NuocSanXuatMenuViewComponent : ViewComponent
    {
        private readonly INuocSanXuatRepository _nuocSanXuat;

        public NuocSanXuatMenuViewComponent(INuocSanXuatRepository nuocSanXuatRepository)
        {
            _nuocSanXuat = nuocSanXuatRepository;
        }

        public IViewComponentResult Invoke()
        {
            var noiThat = _nuocSanXuat.GetAllNuocSx().OrderBy(x => x.Tennuocsx);
            return View(noiThat);
        }
    }
}
