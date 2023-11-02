namespace CafeSolution.Models;

public class PaymentStatus
{
    public int Id { get; set; }
    public string Title { get; set; }

    public virtual Order Order { get; set; }
}