using System;
using System.Collections.Generic;

namespace CafeSolution.Models;

public partial class Shift
{
    public int Id { get; set; }

    public DateTime ShiftDate { get; set; }

    public virtual ICollection<EmployeesAtShift> EmployeesAtShifts { get; set; } = new List<EmployeesAtShift>();
}
