using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.DTO;

public class ShiftDto
{
    public DateTime ShiftDate { get; set; }
    public ObservableCollection<Employee> EmployeesAtShift { get; set; }
    public decimal AmountByShift { get; set; }
}