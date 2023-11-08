using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class DonDatHang
{
    public string SoDdh { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public string MaKhach { get; set; } = null!;

    public DateTime? NgayDat { get; set; }

    public DateTime? NgayGiao { get; set; }

    public int? DatCoc { get; set; }

    public int? Thue { get; set; }

    public int? TongTien { get; set; }

    public virtual ICollection<ChiTietHddh> ChiTietHddhs { get; set; } = new List<ChiTietHddh>();

    public virtual KhachHang MaKhachNavigation { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
