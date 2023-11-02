using System.Collections.Generic;
using System.Drawing.Printing;

namespace CafeSolution.Models;

public class DishesInOrder
{
    public int Id { get; set; }
    public int DishId { get; set; }
    public int OrderId { get; set; }
    
    public virtual List<Dish> Dishes { get; set; }
    public virtual List<Order> Orders { get; set; }
}