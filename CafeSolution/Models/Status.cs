namespace CafeSolution.Models;

public class Status
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public virtual Employee Employee { get; set; }
}