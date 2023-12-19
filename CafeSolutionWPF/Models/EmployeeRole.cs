using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CafeSolutionWPF.Models;

public partial class EmployeeRole
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
}
