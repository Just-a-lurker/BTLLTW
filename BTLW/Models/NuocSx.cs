using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class NuocSx
{
    public string Manuocsx { get; set; } = null!;

    public string? Tennuocsx { get; set; }

    public virtual ICollection<DmnoiThat> DmnoiThats { get; set; } = new List<DmnoiThat>();
}
