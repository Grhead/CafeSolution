using System;
using System.Collections.Generic;

namespace CafeSolution.Models;

public partial class CookingStatus
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
