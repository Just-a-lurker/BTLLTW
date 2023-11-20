using BTLW.Models;
using BTLW.Models.ProductModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTLW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product2APIController : ControllerBase
    {
        Lttqnhom6Context db = new Lttqnhom6Context();   

        public IEnumerable<Product> GetAllProducts()
        {
            var sanPham = (from p in db.DmnoiThats
                           select new Product
                           {
                               MaNoiThat = p.MaNoiThat,

                               TenNoiThat = p.TenNoiThat,

                               Maloai = p.Maloai,

                               Manuocsx = p.Manuocsx,

                               SoLuong = p.SoLuong,

                               DonGiaNhap = p.DonGiaNhap,

                               DonGiaBan = p.DonGiaBan,

                               Anh = p.Anh

                           }).ToList();

            return sanPham;
        }

        [HttpGet("{manuocsx}")]

        public IEnumerable<Product> GetProductsByNaltion(string manuocsx)
        {
            var sanPham = (from p in db.DmnoiThats
                           where p.Manuocsx == manuocsx
                           select new Product
                           {
                               MaNoiThat = p.MaNoiThat,

                               TenNoiThat = p.TenNoiThat,

                               Maloai = p.Maloai,

                               Manuocsx = p.Manuocsx,

                               SoLuong = p.SoLuong,

                               DonGiaNhap = p.DonGiaNhap,

                               DonGiaBan = p.DonGiaBan,

                               Anh = p.Anh

                           }).ToList();

            return sanPham;
        }
    }
}
