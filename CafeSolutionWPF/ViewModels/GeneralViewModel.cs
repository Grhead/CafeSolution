using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Pages;

namespace CafeSolutionWPF.ViewModels;

public class GeneralViewModel
{
    private RelayCommand _applyBtn;
    public string Login { get; set; }

    public RelayCommand ApplyBtn => _applyBtn ?? (_applyBtn = new RelayCommand(x =>
    {
        var passwordBox = x as PasswordBox;
        var password = passwordBox?.Password;
        if (password != null)
        {
            var tempAuth = GeneralEndPoints.Auth(Login, password);
            if (tempAuth.Id != "")
            {
                if (tempAuth.Role == 1)
                {
                    Navigation.ClientSession = tempAuth.Employee;
                    Navigation.mainFrame.Navigate(new AdminMainPage());
                }
                else if (tempAuth.Role == 2)
                {
                    Navigation.ClientSession = tempAuth.Employee;
                    Navigation.mainFrame.Navigate(new CookMainPage());
                }
                else if (tempAuth.Role == 3)
                {
                    Navigation.ClientSession = tempAuth.Employee;
                    Navigation.mainFrame.Navigate(new WaiterMainPage());
                }
            }
        }
    }));
}