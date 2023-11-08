using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class HoaDonNhap
{
    public string SoHdn { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public string MaNcc { get; set; } = null!;

    public DateTime? NgayNhap { get; set; }

    public int? TongTien { get; set; }

    public virtual ICollection<ChiTietHdn> ChiTietHdns { get; set; } = new List<ChiTietHdn>();

    public virtual NhaCungCap MaNccNavigation { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
