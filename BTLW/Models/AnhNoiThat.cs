using System;
using System.Collections.Generic;

namespace BTLW.Models;

public partial class AnhNoiThat
{
    public string MaNoiThat { get; set; } = null!;

    public string TenFileAnh { get; set; } = null!;

    public virtual DmnoiThat MaNoiThatNavigation { get; set; } = null!;
}
