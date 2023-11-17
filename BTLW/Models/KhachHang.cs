using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class KhachHang
{
    public string MaKhach { get; set; } = null!;

    public string? TenKhach { get; set; }

    public string? DiaChi { get; set; }

    public string? DienThoai { get; set; }

    public virtual ICollection<DonDatHang> DonDatHangs { get; set; } = new List<DonDatHang>();
}
