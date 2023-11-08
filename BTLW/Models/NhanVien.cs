using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class NhanVien
{
    public string MaNv { get; set; } = null!;

    public string? TenNv { get; set; }

    public string? GioiTinh { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? DienThoai { get; set; }

    public string? DiaChi { get; set; }

    public string Maca { get; set; } = null!;

    public string MaCv { get; set; } = null!;

    public virtual ICollection<DonDatHang> DonDatHangs { get; set; } = new List<DonDatHang>();

    public virtual ICollection<HoaDonNhap> HoaDonNhaps { get; set; } = new List<HoaDonNhap>();

    public virtual CongViec MaCvNavigation { get; set; } = null!;

    public virtual CaLam MacaNavigation { get; set; } = null!;
}
