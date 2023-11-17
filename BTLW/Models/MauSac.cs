using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class MauSac
{
    public string Mamau { get; set; } = null!;

    public string? Tenmau { get; set; }

    public virtual ICollection<DmnoiThat> DmnoiThats { get; set; } = new List<DmnoiThat>();
}
