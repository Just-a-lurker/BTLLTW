using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class KieuDang
{
    public string Makieu { get; set; } = null!;

    public string? Tenkieu { get; set; }

    public virtual ICollection<DmnoiThat> DmnoiThats { get; set; } = new List<DmnoiThat>();
}
