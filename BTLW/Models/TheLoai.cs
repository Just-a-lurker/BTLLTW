using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class TheLoai
{
    public string Maloai { get; set; } = null!;

    public string? Tenloai { get; set; }

    public virtual ICollection<DmnoiThat> DmnoiThats { get; set; } = new List<DmnoiThat>();
}
