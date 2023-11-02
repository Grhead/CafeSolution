using System;
using System.Collections.Generic;

namespace CafeSolution.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public int Role { get; set; }
    public int Status { get; set; }
    
    public virtual EmployeesAtShift EmployeesAtShift { get; set; }
    public virtual EmployeesAtTable EmployeesAtTable { get; set; }
    public virtual Document Document { get; set; }
    
    public virtual List<Role> Roles { get; set; }
    public virtual List<Status> Statuses { get; set; }
}