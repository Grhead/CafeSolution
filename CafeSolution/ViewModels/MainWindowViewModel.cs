using System;
using CafeSolution.Data;
using CafeSolution.Models;

namespace CafeSolution.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting => Convert.ToString(Test())+"IDCON";

    private int Test()
    {
        DatabaseContext db = new DatabaseContext();
        Role role1 = new Role { Title = "admin" };
        Role role2 = new Role { Title = "chef" };
        Role role3 = new Role { Title = "waiter" };

        db.Roles.AddRange(role1, role2, role3);
        db.SaveChanges();
        return 101;
    }
}