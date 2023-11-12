using System;
using System.Collections.Generic;
using CafeSolution.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CafeSolution.DTO;

public class ShiftDto
{
    public DateTime Shiftdate { get; set; }
    public List<Employee> EmployeesAtShift { get; set; }
    public List<Order> OrdersByShift { get; set; }
    public decimal AmountByShift { get; set; }
    public string ContractScan { get; set; }
    public string Photo { get; set; }
}