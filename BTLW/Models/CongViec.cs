using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class CongViec
{
    public string MaCv { get; set; } = null!;

    public string? TenCv { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
