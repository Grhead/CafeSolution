using CafeSolutionWPF.Pages;
using CafeSolutionWPF.Pages.FunctionPages;

namespace CafeSolutionWPF.ViewModels;

public class WaiterViewModel : UpdateProperty
{
    public WaiterViewModel()
    {
        SelfLogin = Navigation.ClientSession.Login;
    }

    public string SelfLogin { get; set; }

    private string _selectedPage;

    public string SelectedPage
    {
        get => _selectedPage;
        set
        {
            _selectedPage = value;
            OnPropertyChanged();
        }
    }

    private RelayCommand _exit;

    public RelayCommand ExitBtn => _exit ?? (_exit = new RelayCommand(x =>
    {
        Navigation.mainFrame.Navigate(new AuthPage());
        Navigation.ClientSession = null;
    }));

    private RelayCommand _createOrder;

    public RelayCommand CreateOrderBtn => _createOrder ?? (_createOrder = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new OrderCreate());
        SelectedPage = "Создание заказа";
    }));

    private RelayCommand _orderList;

    public RelayCommand OrderListBtn => _orderList ?? (_orderList = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new OrdersList());
        SelectedPage = "Список заказов";
    }));

    private RelayCommand _bill;

    public RelayCommand Bill => _bill ?? (_bill = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new BillPage());
        SelectedPage = "Чек";
    }));

    private RelayCommand _report;

    public RelayCommand Report => _report ?? (_report = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new ReportPageAdmin());
        SelectedPage = "Отчёт";
    }));
}