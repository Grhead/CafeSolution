using System;
using System.Collections.Generic;

namespace CafeSolutionWPF.Models;

public partial class EmployeesAtShift
{
    public int Id { get; set; }

    public int ShiftId { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Shift Shift { get; set; } = null!;
}
