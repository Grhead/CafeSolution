using CafeSolutionWPF.DTO;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Pages;
using CafeSolutionWPF.Pages.CardPages;
using CafeSolutionWPF.Pages.FunctionPages;

namespace CafeSolutionWPF.ViewModels;

public class AdminViewModel: UpdateProperty
{
    public AdminViewModel()
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
    public List<EmployeeDto> EmployeeList { get; set; }
    public Employee selectedEmployee { get; set; }
    
    public List<Order> OrderList { get; set; }
    public Order selectedOrder { get; set; }
    
    public List<Shift> ShiftListView { get; set; }
    public ShiftDto SelectedShiftDto { get; set; }
    
    private RelayCommand _employeeList;
    private RelayCommand _orderList;
    private RelayCommand _shiftList;
    private RelayCommand _dismiss;
    private RelayCommand _report;
    private RelayCommand _exit;
    private RelayCommand _getemployee;
    private RelayCommand _newShift;

    public RelayCommand ExitBtn => _exit ?? (_exit = new RelayCommand(x =>
    {
        Navigation.mainFrame.Navigate(new AuthPage());
        Navigation.ClientSession = null;
    }));
    
    public RelayCommand EmployeeListBtn => _employeeList ?? (_employeeList = new RelayCommand(x =>
    {
        AdminEndPoints newAdmin = new AdminEndPoints();
        Navigation.adminFrame.Navigate(new EmployeeList());
        SelectedPage = "Список сотрудников";
        EmployeeList = newAdmin.GetEmployeesList();
    }));
    public RelayCommand OrderListBtn => _orderList ?? (_orderList = new RelayCommand(x =>
    {
        AdminEndPoints newAdmin = new AdminEndPoints();
        Navigation.adminFrame.Navigate(new OrdersList());
        OrderList = newAdmin.GetAllOrders();
        SelectedPage = "Список заказов";
    }));
    public RelayCommand ShiftListBtn => _shiftList ?? (_shiftList = new RelayCommand(x =>
    {
        AdminEndPoints newAdmin = new AdminEndPoints();
        Navigation.adminFrame.Navigate(new ShiftList());
        ShiftListView = newAdmin.GtAllShifts();
        SelectedPage = "Список смен";
    }));
    public RelayCommand DismissBtn => _dismiss ?? (_dismiss = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new DismissEmployee());
        SelectedPage = "Уволить";
    }));
    public RelayCommand ReportBtn => _report ?? (_report = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new ReportPage());
        SelectedPage = "Отчёт";
    }));

    public RelayCommand GetEmployeeBtn => _getemployee ?? (_getemployee = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new GetEmployeePage());
        SelectedPage = "Нанять";
    }));

    public RelayCommand NewShiftBtn => _newShift ?? (_newShift = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new ShiftCreate());
        SelectedPage = "Создать смену";
    }));

    public List<EmployeeDto> EmployeeListDismiss { get; set; }
    public Employee EmployeeDismiss { get; }

    private RelayCommand _confirmDismiss;
    public RelayCommand ConfirmDismissBtn => _confirmDismiss ?? (_confirmDismiss = new RelayCommand(x =>
    {
        AdminEndPoints newAdmin = new AdminEndPoints();
        EmployeeListDismiss = newAdmin.GetEmployeesList();
        newAdmin.Dismiss(EmployeeDismiss.Id);
    }));
    
    private RelayCommand _viewEmployee;
    public RelayCommand ViewEmployeeBtn => _viewEmployee ?? (_viewEmployee = new RelayCommand(x =>
    {
        Navigation.cookFrame.Navigate(new EmployeeCard());
        selectedEmployee = Navigation.selectedEmployee;
        SelectedPage = "Карточка сотрудника";
    }));
    
    private RelayCommand _viewOrder;
    public RelayCommand ViewOrderBtn => _viewOrder ?? (_viewOrder = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new OrderCard());
        selectedOrder = Navigation.selectedOrder;
        SelectedPage = "Карточка заказа";
    }));
    
    private RelayCommand _viewShift;
    public RelayCommand ViewShiftBtn => _viewShift ?? (_viewShift = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new ShiftCard());
        SelectedShiftDto = Navigation.selectedShift;
        SelectedPage = "Карточка смены";
    }));
}