using System;

namespace CafeSolutionWPF.DTO;

public class EmployeeDto
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthday { get; set; }
    public string Role { get; set; }
    public string Status { get; set; }
    public string Login { get; set; }
}