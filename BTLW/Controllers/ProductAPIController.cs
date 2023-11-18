using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTLW.Models;
using Microsoft.EntityFrameworkCore;
using BTLW.Models.ProductModel;

namespace BTLW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
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

                               SoLuong = p.SoLuong,

                               DonGiaNhap = p.DonGiaNhap,

                               DonGiaBan = p.DonGiaBan,

                               Anh = p.Anh

                           }).ToList();

            return sanPham;
        }

        [HttpGet("{maloai}")]

        public IEnumerable<Product> GetProductsByCategory(string maloai)
        {
            var sanPham = (from p in db.DmnoiThats
                           where p.Maloai == maloai
                           select new Product
                           {
                               MaNoiThat = p.MaNoiThat,

                               TenNoiThat = p.TenNoiThat,

                               Maloai = p.Maloai,

                               SoLuong = p.SoLuong,

                               DonGiaNhap = p.DonGiaNhap,

                               DonGiaBan = p.DonGiaBan,

                               Anh = p.Anh

                           }).ToList();

            return sanPham;
        }
    }
}
