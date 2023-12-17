using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.DTO;

public class AuthDto
{
    public string Id { get; set; }
    public Employee Employee { get; set; }
    public int Role { get; set; }
    public string RoleTitle { get; set; }
}