using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Pages;
using CafeSolutionWPF.Pages.CardPages;
using CafeSolutionWPF.Pages.FunctionPages;

namespace CafeSolutionWPF.ViewModels;

public class WaiterViewModel : UpdateProperty
{
    private RelayCommand _bill;

    private RelayCommand _createOrder;

    private RelayCommand _exit;

    private RelayCommand _myPage;

    private RelayCommand _orderList;

    private RelayCommand _report;

    private string _selectedPage;

    public WaiterViewModel()
    {
        SelfLogin = Navigation.ClientSession.Login;
    }

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

    public RelayCommand ExitBtn => _exit ?? (_exit = new RelayCommand(x =>
    {
        Navigation.mainFrame.Navigate(new AuthPage());
        Navigation.ClientSession = null;
    }));

    public RelayCommand MyPage => _myPage ?? (_myPage = new RelayCommand(x =>
    {
        Navigation.selectedEmployee = new EmployeeDto
        {
            FirstName = Navigation.ClientSession.FirstName,
            SecondName = Navigation.ClientSession.SecondName,
            LastName = Navigation.ClientSession.LastName,
            Birthday = Navigation.ClientSession.Birthday,
            Login = Navigation.ClientSession.Login,
            Role = Navigation.ClientSession.Role.Title,
            Status = Navigation.ClientSession.Status.Title
        };
        Navigation.waiterFrame.Navigate(new EmployeeCard());
        SelectedPage = "Страница сотрудника";
    }));

    public RelayCommand CreateOrderBtn => _createOrder ?? (_createOrder = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new OrderCreate());
        SelectedPage = "Создание заказа";
    }));

    public RelayCommand OrderListBtn => _orderList ?? (_orderList = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new OrdersListWaiter());
        SelectedPage = "Список заказов";
    }));

    public RelayCommand Bill => _bill ?? (_bill = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new BillPage());
        SelectedPage = "Чек";
    }));

    public RelayCommand Report => _report ?? (_report = new RelayCommand(x =>
    {
        Navigation.waiterFrame.Navigate(new ReportPageWaiter());
        SelectedPage = "Отчёт";
    }));
}