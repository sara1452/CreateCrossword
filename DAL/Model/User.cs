using System;
using System.Collections.Generic;

namespace DAL.Model;

public partial class User
{
    public int UserCode { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<CrosswordsUser> CrosswordsUsers { get; } = new List<CrosswordsUser>();
}
