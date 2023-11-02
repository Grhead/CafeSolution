namespace CafeSolution.Models;

public class Table
{
    public int Id { get; set; }
    public int TableNumber { get; set; }
    
    public virtual EmployeesAtTable EmployeesAtTable { get; set; }
    public virtual Order Order { get; set; }
}