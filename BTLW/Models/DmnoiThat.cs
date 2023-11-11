﻿using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class DmnoiThat
{
    public string MaNoiThat { get; set; } = null!;

    public string? TenNoiThat { get; set; }

    public string Maloai { get; set; } = null!;

    public string Makieu { get; set; } = null!;

    public string Mamau { get; set; } = null!;

    public string Machatlieu { get; set; } = null!;

    public string Manuocsx { get; set; } = null!;

    public int? SoLuong { get; set; }

    public int? DonGiaNhap { get; set; }

    public int? DonGiaBan { get; set; }

    public string? Anh { get; set; }

    public DateTime? ThoiGianBaoHanh { get; set; }

    public virtual ICollection<ChiTietHddh> ChiTietHddhs { get; set; } = new List<ChiTietHddh>();

    public virtual ICollection<ChiTietHdn> ChiTietHdns { get; set; } = new List<ChiTietHdn>();

    public virtual ChatLieu MachatlieuNavigation { get; set; } = null!;

    public virtual KieuDang MakieuNavigation { get; set; } = null!;

    public virtual TheLoai MaloaiNavigation { get; set; } = null!;

    public virtual MauSac MamauNavigation { get; set; } = null!;

    public virtual NuocSx ManuocsxNavigation { get; set; } = null!;
}