using System.Security.Cryptography;
using System.Text;
using CafeSolution.DTO;
using CafeSolution.Interfaces;
using CafeSolution.Models;
using CafeSolutionWPF.Data;

namespace CafeSolution.FuncEndPoints;

public class GeneralEndPoints: IGeneralEp
{
    public AuthDto Auth(string login, string password)
    {
        using DatabaseContext db = new DatabaseContext();
        Employee enteredEmployee = db.Employees.FirstOrDefault(x => x.Login == login && x.PassHash == CreateHash(password));
        return new AuthDto
        {
            Id = enteredEmployee.Id.ToString(), 
            Role = enteredEmployee.RoleNavigation.Title
        };
    }
    
    public static string CreateHash(string input)
    {
        using SHA256 hash = SHA256.Create();
        return Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(input)));
    }
}