using System;
using System.Collections.Generic;

namespace shop.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserAccount { get; set; }

    public string? UserPassword { get; set; }

    public string UserEmail { get; set; } = null!;

    public bool? IsAdmin { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
