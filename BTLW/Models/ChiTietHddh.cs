using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class ChiTietHddh
{
    public string MaNoithat { get; set; } = null!;

    public string SoDdh { get; set; } = null!;

    public int? SoLuong { get; set; }

    public int? GiamGia { get; set; }

    public int? ThanhTien { get; set; }

    public virtual DmnoiThat MaNoithatNavigation { get; set; } = null!;

    public virtual DonDatHang SoDdhNavigation { get; set; } = null!;
}
