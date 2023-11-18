using BTLW.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BTLW.ViewComponents
{
    public class LoaiNoiThatMenuViewComponent : ViewComponent
    {
        private readonly ILoaiNoiThatRepository _loaiNoiThat;

        public LoaiNoiThatMenuViewComponent(ILoaiNoiThatRepository loaiNoiThatRepository)
        {
            _loaiNoiThat = loaiNoiThatRepository;
        }

        public IViewComponentResult Invoke()
        {
            var noiThat = _loaiNoiThat.GetAllTheLoai().OrderBy(x => x.Tenloai);
            return View(noiThat);
        }
    }
}
