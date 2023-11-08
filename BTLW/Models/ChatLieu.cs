using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class ChatLieu
{
    public string Machatlieu { get; set; } = null!;

    public string? Tenchatlieu { get; set; }

    public virtual ICollection<DmnoiThat> DmnoiThats { get; set; } = new List<DmnoiThat>();
}
