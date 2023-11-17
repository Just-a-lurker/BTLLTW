using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class ChiTietHdn
{
    public string MaNoithat { get; set; } = null!;

    public string SoHdn { get; set; } = null!;

    public int? SoLuong { get; set; }

    public int? DonGia { get; set; }

    public int? GiamGia { get; set; }

    public int? ThanhTien { get; set; }

    public virtual DmnoiThat MaNoithatNavigation { get; set; } = null!;

    public virtual HoaDonNhap SoHdnNavigation { get; set; } = null!;
}
