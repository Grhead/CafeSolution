using System;
using System.Collections.Generic;

namespace CafeSolutionWPF.Models;

public partial class Table
{
    public int Id { get; set; }

    public int TableNumber { get; set; }

    public virtual ICollection<EmployeesAtTable> EmployeesAtTables { get; set; } = new List<EmployeesAtTable>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
