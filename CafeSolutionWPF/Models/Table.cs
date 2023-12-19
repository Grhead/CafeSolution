using System.Collections.ObjectModel;

namespace CafeSolutionWPF.Models;

public class Table
{
    public int Id { get; set; }

    public int TableNumber { get; set; }

    public virtual ICollection<EmployeesAtTable> EmployeesAtTables { get; set; } =
        new ObservableCollection<EmployeesAtTable>();

    public virtual ICollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
}