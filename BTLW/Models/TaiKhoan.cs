using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class TaiKhoan
{
    public int MaTk { get; set; }

    public string? TenTk { get; set; }

    public string? MatKhau { get; set; }

    public bool? LoaiTk { get; set; }
}
