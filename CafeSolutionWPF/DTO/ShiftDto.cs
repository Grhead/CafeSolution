using System;
using System.Collections.Generic;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.DTO;

public class ShiftDto
{
    public DateTime ShiftDate { get; set; }
    public List<Employee> EmployeesAtShift { get; set; }
    public decimal AmountByShift { get; set; }
}