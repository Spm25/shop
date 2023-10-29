using System;
using System.Collections.Generic;

namespace shop.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
