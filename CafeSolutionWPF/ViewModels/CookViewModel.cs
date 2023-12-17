using CafeSolutionWPF.Models;
using CafeSolutionWPF.Pages;

namespace CafeSolutionWPF.ViewModels;

public class CookViewModel: UpdateProperty
{
    public CookViewModel()
    {
        SelfLogin = Navigation.ClientSession.Login;
    }

    private string _selectedPage;
    public string SelfLogin { get; set; }

    public string SelectedPage
    {
        get => _selectedPage;
        set
        {
            _selectedPage = value;
            OnPropertyChanged();
        }
    }
    public object selectedOrder { get; }
    public Order OrderView { get; }
    
    private RelayCommand _orderList;
    public RelayCommand OrderListBtn => _orderList ?? (_orderList = new RelayCommand(x =>
    {
        Navigation.cookFrame.Navigate(new OrdersList());
        SelectedPage = "Список заказов";
    }));
    
    private RelayCommand _exit;
    
    public RelayCommand ExitBtn => _exit ?? (_exit = new RelayCommand(x =>
    {
        Navigation.mainFrame.Navigate(new AuthPage());
        Navigation.ClientSession = null;
    }));
}