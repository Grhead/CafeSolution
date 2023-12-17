using System;
using System.Collections.Generic;

namespace CafeSolutionWPF.Models;

public partial class TableCookingStatus
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
