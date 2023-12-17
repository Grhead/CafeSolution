using System;
using System.Collections.Generic;

namespace CafeSolutionWPF.Models;

public partial class EmployeesAtTable
{
    public int Id { get; set; }

    public int TableId { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;
}
