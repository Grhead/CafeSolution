using System;
using System.Collections.Generic;

namespace CafeSolutionWPF.Models;

public partial class EmployeeStatus
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
