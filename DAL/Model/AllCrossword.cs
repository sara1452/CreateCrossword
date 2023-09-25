using System;
using System.Collections.Generic;

namespace DAL.Model;

public partial class AllCrossword
{
    public int Id { get; set; }

    public int CrosswordCode { get; set; }

    public int NumberLocation { get; set; }

    public int DefinitionCode { get; set; }

    public bool Across { get; set; }

    public bool Down { get; set; }

    public int I { get; set; }

    public int J { get; set; }

    public int AmountLetters { get; set; }

    public string Solve { get; set; } = null!;

    public virtual CrosswordsUser CrosswordCodeNavigation { get; set; } = null!;

    public virtual WordAndDefinition DefinitionCodeNavigation { get; set; } = null!;
}
