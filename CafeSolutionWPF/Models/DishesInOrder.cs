using System;
using System.Collections.Generic;

namespace CafeSolutionWPF.Models;

public partial class DishesInOrder
{
    public int Id { get; set; }

    public int DishId { get; set; }

    public int OrderId { get; set; }

    public virtual Dish Dish { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
