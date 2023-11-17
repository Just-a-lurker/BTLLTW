using Microsoft.AspNetCore.Mvc;
using BTLW.Models;
using BTLW.ViewModel;
namespace BTLW.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        //public IActionResult Register()
        //{
        //    return View();
        //}
    }
}
