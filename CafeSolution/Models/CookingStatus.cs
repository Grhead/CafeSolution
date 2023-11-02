namespace CafeSolution.Models;

public class CookingStatus
{
    public int Id { get; set; }
    public string Title { get; set; }

    public virtual Order Order { get; set; }
}