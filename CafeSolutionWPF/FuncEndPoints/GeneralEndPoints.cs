using System.Security.Cryptography;
using System.Text;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Interfaces;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeSolutionWPF.FuncEndPoints;

public class GeneralEndPoints
{
    public static AuthDto Auth(string login, string password)
    {
        using DatabaseContext db = new DatabaseContext();
        var enteredEmployee = db.Employees
            .Include(employee => employee.Role)
            .Include(x => x.Status)
            .FirstOrDefault(x => x.Login == login 
                                 && x.PassHash == CreateHash(password));
        if (enteredEmployee != null && enteredEmployee.StatusId == 1)
        {
            return new AuthDto
            {
                Id = enteredEmployee.Id.ToString(), 
                Employee = enteredEmployee,
                RoleTitle = enteredEmployee.Role.Title,
                Role = enteredEmployee.RoleId
            };
        }
        return new AuthDto
        {
                Id = null, 
                Employee = null,
                RoleTitle = "",
                Role = 0
            };
    }
    
    public static string CreateHash(string input)
    {
        using SHA256 hash = SHA256.Create();
        return Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(input)));
    }
    
    public static Shift GetCurrentShift()
    {
        DateTime currentDate = DateTime.Today;
        using DatabaseContext db = new DatabaseContext();
        // Shift currentShift = db.Shifts.FirstOrDefault(x => x.ShiftDate == currentDate);
        Shift currentShift = db.Shifts.FirstOrDefault(x => x.ShiftDate < currentDate);
        return currentShift;
    }
}