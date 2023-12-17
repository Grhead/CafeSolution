using CafeSolutionWPF.Pages;

namespace CafeSolutionWPF.ViewModels;

public class WaiterViewModel: UpdateProperty
{
    public WaiterViewModel()
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
    
    private RelayCommand _orderList;
    private RelayCommand _createOrder;
    private RelayCommand _bill;
    private RelayCommand _report;
    public RelayCommand CreateOrderBtn => _createOrder ?? (_createOrder = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new OrdersList());
        SelectedPage = "Список заказов";
    }));
    public RelayCommand OrderListBtn => _orderList ?? (_orderList = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new OrdersList());
        SelectedPage = "Список заказов";
    }));
    public RelayCommand Bill => _bill ?? (_bill = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new OrdersList());
        SelectedPage = "Список заказов";
    }));
    public RelayCommand Report => _report ?? (_report = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new OrdersList());
        SelectedPage = "Список заказов";
    }));
    
    private RelayCommand _exit;
    public RelayCommand ExitBtn => _exit ?? (_exit = new RelayCommand(x =>
    {
        Navigation.mainFrame.Navigate(new AuthPage());
        Navigation.ClientSession = null;
    }));
    
}