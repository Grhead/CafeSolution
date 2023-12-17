using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Pages;

namespace CafeSolutionWPF.ViewModels;

public class GeneralViewModel
{
    private string _login;
    private string _password;
    private RelayCommand _applyBtn;
    public string Login { get { return _login; } set { _login = value; } }
    public RelayCommand ApplyBtn => _applyBtn ?? (_applyBtn = new RelayCommand(x =>
    {
        var passwordBox = x as PasswordBox;
        var password = passwordBox.Password;
        var tempAuth = GeneralEndPoints.Auth(_login, password);
        if (tempAuth.Id != "")
        {
            if (tempAuth.Role == 1)
            {
                Navigation.ClientSession = tempAuth.Employee;
                Navigation.mainFrame.Navigate(new AdminMainPage());
            } else if (tempAuth.Role == 2)
            {
                Navigation.ClientSession = tempAuth.Employee;
                Navigation.mainFrame.Navigate(new CookMainPage());
            } else if (tempAuth.Role == 3)
            {
                Navigation.ClientSession = tempAuth.Employee;
                Navigation.mainFrame.Navigate(new WaiterMainPage());
            }
        }
    }));
}