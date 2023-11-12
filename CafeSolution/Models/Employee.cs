using System;
using System.Collections.Generic;

namespace CafeSolution.Models;

public class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public int Role { get; set; }

    public int Status { get; set; }

    public string? Login { get; set; }
    
    public string? PassHash { get; set; }

    public virtual Document? Document { get; set; }

    public virtual ICollection<EmployeesAtShift> EmployeesAtShifts { get; set; } = new List<EmployeesAtShift>();

    public virtual ICollection<EmployeesAtTable> EmployeesAtTables { get; set; } = new List<EmployeesAtTable>();

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual Status StatusNavigation { get; set; } = null!;
}
