using System.Collections.Generic;

namespace CafeSolution.Models;

public class EmployeesAtShift
{
    public int Id { get; set; }
    public int ShiftId { get; set; }
    public int EmployeeId { get; set; }
    
    public virtual List<Shift> Shifts { get; set; }
    public virtual List<Employee> Employees { get; set; }
}