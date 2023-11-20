using Microsoft.EntityFrameworkCore;

namespace BTLW.Models.ProductModel
{
    public class Product
    {
        public string MaNoiThat { get; set; } = null!;

        public string? TenNoiThat { get; set; }

        public string Maloai { get; set; } = null!;

        public string Manuocsx { get; set; } = null!;

        public int? SoLuong { get; set; }

        public int? DonGiaNhap { get; set; }

        public int? DonGiaBan { get; set; }

        public string? Anh { get; set; }
    }
}
