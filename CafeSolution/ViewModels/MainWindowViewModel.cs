using System;
using System.Linq;
using CafeSolution.Data;
using CafeSolution.FuncEndPoints;

namespace CafeSolution.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting => Test();

    private string Test()
    {
        //DatabaseContext db = new DatabaseContext();
        // Role role1 = new Role { Title = "admin12" };
        // Role role2 = new Role { Title = "chef12" };
        // Role role3 = new Role { Title = "waiter12" };
        //
        // db.Roles.AddRange(role1, role2, role3);
        // db.SaveChanges();
        //var qwe = db.Roles.FirstOrDefault(x => x.Id == 4).Title;
        AdminEndPoints classq2 = new AdminEndPoints();
        var qwe = classq2.GetEmployeesList();
        return qwe[0].Status;
    }
}