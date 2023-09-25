using System;
using System.Collections.Generic;

namespace DAL.Model;

public partial class CrosswordsUser
{
    public int CrosswordCode { get; set; }

    public string CrosswordName { get; set; } = null!;

    public int UserCode { get; set; }

    public DateTime ProductionDate { get; set; }

    public int Length { get; set; }

    public int Width { get; set; }

    public virtual ICollection<AllCrossword> AllCrosswords { get; } = new List<AllCrossword>();

    public virtual User UserCodeNavigation { get; set; } = null!;
}
