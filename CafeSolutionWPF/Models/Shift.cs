using System;
using System.Collections.Generic;

namespace CafeSolutionWPF.Models;

public partial class Shift
{
    public int Id { get; set; }

    public DateTime ShiftDate { get; set; }

    public virtual ICollection<EmployeesAtShift> EmployeesAtShifts { get; set; } = new List<EmployeesAtShift>();
}
