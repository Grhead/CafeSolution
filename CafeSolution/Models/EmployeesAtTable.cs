using System.Collections.Generic;

namespace CafeSolution.Models;

public class EmployeesAtTable
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int EmployeeId { get; set; }
    
    public virtual List<Table> Tables { get; set; }
    public virtual List<Employee> Employees { get; set; }
}