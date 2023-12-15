using System;
using System.Collections.Generic;

namespace CafeSolution.Models;

public partial class Dish
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal? Price { get; set; }

    public virtual ICollection<DishesInOrder> DishesInOrders { get; set; } = new List<DishesInOrder>();
}
