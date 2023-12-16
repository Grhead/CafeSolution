using System;
using System.Collections.Generic;
using CafeSolution.Models;

namespace CafeSolution.DTO;

public class ShiftDto
{
    public DateTime ShiftDate { get; set; }
    public List<Employee> EmployeesAtShift { get; set; }
    public decimal AmountByShift { get; set; }
}