using System.Collections.ObjectModel;

namespace CafeSolutionWPF.Models;

public class Shift
{
    public int Id { get; set; }

    public DateTime ShiftDate { get; set; }

    public virtual ICollection<EmployeesAtShift> EmployeesAtShifts { get; set; } =
        new ObservableCollection<EmployeesAtShift>();
}