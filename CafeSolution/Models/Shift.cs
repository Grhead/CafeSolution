using System;

namespace CafeSolution.Models;

public class Shift
{   
    public int Id { get; set; }
    public DateTime ShiftDate { get; set; }
    
    public virtual EmployeesAtShift EmployeesAtShift { get; set; }
}