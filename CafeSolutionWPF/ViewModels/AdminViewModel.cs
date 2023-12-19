using System.Collections.ObjectModel;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Pages;
using CafeSolutionWPF.Pages.CardPages;
using CafeSolutionWPF.Pages.FunctionPages;

namespace CafeSolutionWPF.ViewModels;

public class AdminViewModel : UpdateProperty
{
    private RelayCommand _confirmDismiss;

    private Employee _createEmployeeFN;

    private RelayCommand _dismissRelayCommand;

    private ObservableCollection<EmployeeDto> _employeeList;

    private RelayCommand _employeeListRelayCommand;

    private RelayCommand _exit;

    private RelayCommand _hireEmployeeRelayCommand;

    private RelayCommand _newShiftRelayCommand;

    private ObservableCollection<Order> _orderList;

    private RelayCommand _orderListRelayCommand;

    private RelayCommand _reportRelayCommand;


    private DateTime _selectedDateTimeBirthday;


    private Employee _selectedEmployee;

    private Order _selectedOrder;

    private string _selectedPage;

    private ShiftDto _selectedShiftDto;

    private Employee _selectEmployeeDismiss;

    private RelayCommand _shiftInfoBtn;

    private RelayCommand _shiftListRelayCommand;


    private ObservableCollection<Shift> _shiftListView;

    private RelayCommand _tablesListBtn;


    // private RelayCommand _viewEmployee;
    // public RelayCommand ViewEmployeeBtn => _viewEmployee ?? (_viewEmployee = new RelayCommand(x =>
    // {
    //     if (SelectedEmployee != null)
    //     {
    //         Navigation.cookFrame.Navigate(new EmployeeCard());
    //         Navigation.selectedEmployee = SelectedEmployee;
    //         SelectedPage = "Карточка сотрудника";
    //     }
    // }));

    private RelayCommand _viewOrder;

    private RelayCommand _viewShift;

    public AdminViewModel()
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

    public RelayCommand EmployeeListBtn => _employeeListRelayCommand ?? (_employeeListRelayCommand = new RelayCommand(
        x =>
        {
            Navigation.adminFrame.Navigate(new EmployeeList());
            SelectedPage = "Список сотрудников";
        }));

    public RelayCommand OrderListBtn => _orderListRelayCommand ?? (_orderListRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new OrdersListAdmin());
        SelectedPage = "Список заказов";
    }));

    public RelayCommand ShiftListBtn => _shiftListRelayCommand ?? (_shiftListRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new SetTablesPage());
        SelectedPage = "Список смен";
    }));

    public RelayCommand DismissBtn => _dismissRelayCommand ?? (_dismissRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new DismissEmployee());
        SelectedPage = "Уволить";
    }));

    public RelayCommand ReportBtn => _reportRelayCommand ?? (_reportRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new ReportPageAdmin());
        SelectedPage = "Отчёт";
    }));

    public RelayCommand HireEmployeeBtn => _hireEmployeeRelayCommand ?? (_hireEmployeeRelayCommand = new RelayCommand(
        x =>
        {
            Navigation.adminFrame.Navigate(new CreateEmployeePage());
            SelectedPage = "Нанять";
        }));

    public RelayCommand NewShiftBtn => _newShiftRelayCommand ?? (_newShiftRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new ShiftCreate());
        SelectedPage = "Создать смену";
    }));

    public RelayCommand ShiftInfoBtn => _shiftInfoBtn ?? (_shiftInfoBtn = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new ShiftCard());
        SelectedPage = "Смены информация";
    }));

    public RelayCommand TablesListBtn => _tablesListBtn ?? (_tablesListBtn = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new TablesList());
        SelectedPage = "Столики";
    }));

    public Employee SelectedEmployee
    {
        get => _selectedEmployee;
        set
        {
            _selectedEmployee = value;
            OnPropertyChanged();
        }
    }

    public Order SelectedOrder
    {
        get => _selectedOrder;
        set
        {
            // Safe?
            if (Equals(value, _selectedOrder)) return;
            _selectedOrder = value;
            OnPropertyChanged();
        }
    }

    public ShiftDto SelectedShiftDto
    {
        get => _selectedShiftDto;
        set
        {
            if (Equals(value, _selectedShiftDto)) return;
            _selectedShiftDto = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<EmployeeDto> EmployeeList
    {
        get => _employeeList;
        set
        {
            if (Equals(value, _employeeList)) return;
            _employeeList = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Order> OrderList
    {
        get => _orderList;
        set
        {
            if (Equals(value, _orderList)) return;
            _orderList = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Shift> ShiftListView
    {
        get => _shiftListView;
        set
        {
            if (Equals(value, _shiftListView)) return;
            _shiftListView = value;
            OnPropertyChanged();
        }
    }

    public Employee SelectEmployeeDismiss
    {
        get => _selectEmployeeDismiss;
        set
        {
            if (Equals(value, _selectEmployeeDismiss)) return;
            _selectEmployeeDismiss = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ConfirmDismissBtn));
        }
    }

    public Employee CreateEmployeeFN
    {
        get => _createEmployeeFN;
        set
        {
            if (Equals(value, _createEmployeeFN)) return;
            _createEmployeeFN = value;
            OnPropertyChanged();
        }
    }

    public DateTime SelectedDateTimeBirthday
    {
        get => _selectedDateTimeBirthday;
        set
        {
            if (Equals(value, _selectedDateTimeBirthday)) return;
            _selectedDateTimeBirthday = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ViewOrderBtn => _viewOrder ?? (_viewOrder = new RelayCommand(x =>
    {
        if (SelectedOrder != null)
        {
            Navigation.adminFrame.Navigate(new OrderCard());
            Navigation.selectedOrder = SelectedOrder;
            SelectedPage = "Карточка заказа";
        }
    }));

    public RelayCommand ViewShiftBtn => _viewShift ?? (_viewShift = new RelayCommand(x =>
    {
        if (SelectedShiftDto != null)
        {
            Navigation.adminFrame.Navigate(new ShiftCard());
            Navigation.selectedShift = SelectedShiftDto;
            SelectedPage = "Карточка смены";
        }
    }));

    public RelayCommand ConfirmDismissBtn => _confirmDismiss ?? (_confirmDismiss = new RelayCommand(x =>
    {
        var newAdmin = new AdminEndPoints();
        EmployeeList = newAdmin.GetEmployeesList();
        newAdmin.Dismiss(SelectEmployeeDismiss.Id);
    }));

    // private RelayCommand _createEmployeeBtn;
    // public RelayCommand CreateEmployeeBtn => _createEmployeeBtn ?? (_createEmployeeBtn = new RelayCommand(x =>
    // {
    //     var qwe = CreateEmployee;
    //     if (CreateEmployeePassword != null)
    //     {
    //         var newAdmin = new AdminEndPoints();
    //         CreateEmployee.PassHash = GeneralEndPoints.CreateHash(CreateEmployeePassword);
    //         EmployeeDto newEmployee = newAdmin.CreateEmployee(CreateEmployee);
    //     }
    // }));
    public void CreateEmployee(Employee employeeData)
    {
        var newAdmin = new AdminEndPoints();
        newAdmin.CreateEmployee(employeeData);
    }

    public bool DismissEmployee(int employeeId)
    {
        var newAdmin = new AdminEndPoints();
        return newAdmin.Dismiss(employeeId);
    }

    public ObservableCollection<Employee> GetAllWorkEmployees()
    {
        var newAdmin = new AdminEndPoints();
        return newAdmin.GetWorkEmployeesList();
    }

    public ObservableCollection<EmployeeDto> GetAllEmployees()
    {
        var newAdmin = new AdminEndPoints();
        EmployeeList = newAdmin.GetEmployeesList();
        return EmployeeList;
    }
}