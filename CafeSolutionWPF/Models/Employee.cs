using System.Collections.ObjectModel;

namespace CafeSolutionWPF.Models;

public class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public int RoleId { get; set; }

    public int StatusId { get; set; }

    public string? Login { get; set; }

    public string? PassHash { get; set; }

    public string? ContractScan { get; set; } = null!;

    public string? Photo { get; set; } = null!;

    public virtual ICollection<EmployeesAtShift> EmployeesAtShifts { get; set; } =
        new ObservableCollection<EmployeesAtShift>();

    public virtual ICollection<EmployeesAtTable> EmployeesAtTables { get; set; } =
        new ObservableCollection<EmployeesAtTable>();

    public virtual EmployeeRole Role { get; set; } = null!;

    public virtual EmployeeStatus Status { get; set; } = null!;
}