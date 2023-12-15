using System;
using System.Collections.Generic;
using CafeSolution.Models;

namespace CafeSolution.DTO;

// TODO rework, because ... just rework 
public class ShiftDto
{
    public DateTime Shiftdate { get; set; }
    public List<Employee> EmployeesAtShift { get; set; }
    public List<Order> OrdersByShift { get; set; }
    public decimal AmountByShift { get; set; }
    public string ContractScan { get; set; }
    public string Photo { get; set; }
}