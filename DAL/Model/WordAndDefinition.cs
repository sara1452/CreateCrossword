using System;
using System.Collections.Generic;

namespace DAL.Model;

public partial class WordAndDefinition
{
    public int WordCode { get; set; }

    public string Word { get; set; } = null!;

    public string Definition { get; set; } = null!;

    public virtual ICollection<AllCrossword> AllCrosswords { get; } = new List<AllCrossword>();
}
