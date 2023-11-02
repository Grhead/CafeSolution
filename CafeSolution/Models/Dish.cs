namespace CafeSolution.Models;

public class Dish
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    
    public virtual DishesInOrder DishesInOrder { get; set; }
}