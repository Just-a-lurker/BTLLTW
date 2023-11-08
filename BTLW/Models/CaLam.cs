using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class CaLam
{
    public string Maca { get; set; } = null!;

    public string? Tenca { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
