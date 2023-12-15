using System.Security.Cryptography;
using System.Text;
using CafeSolution.Interfaces;
using CafeSolution.Models;
using CafeSolutionWPF.Data;

namespace CafeSolution.FuncEndPoints;

public class GeneralEndPoints: IGeneralEp
{
    public string Auth(string login, string password)
    {
        using DatabaseContext db = new DatabaseContext();
        string enteredEmployee = db.Employees.FirstOrDefault(x => x.Login == login && x.PassHash == CreateHash(password)).Id.ToString();
        return enteredEmployee;
    }
    
    public static string CreateHash(string input)
    {
        using SHA256 hash = SHA256.Create();
        return Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(input)));
    }
}