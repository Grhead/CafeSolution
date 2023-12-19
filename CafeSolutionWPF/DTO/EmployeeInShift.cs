using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.DTO;

public class EmployeeInShift
{
    public Employee Employee { get; set; }
    public Shift Shift { get; set; }
}