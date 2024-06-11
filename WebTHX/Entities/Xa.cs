using System;
using System.Collections.Generic;

namespace WebTHX.Entities;

public partial class Xa
{
    public int MaX { get; set; }

    public string? Ten { get; set; }

    public string? Cap { get; set; }

    public int? MaH { get; set; }

    public int? MaT { get; set; }

    public virtual Huyen? MaHNavigation { get; set; }

    public virtual Tinh? MaTNavigation { get; set; }
}
