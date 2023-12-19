namespace CafeSolutionWPF.Models;

public class EmployeesAtTable
{
    public int Id { get; set; }

    public int TableId { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;
}