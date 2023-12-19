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
    public AdminViewModel()
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

    private RelayCommand _employeeListRelayCommand;
    public RelayCommand EmployeeListBtn => _employeeListRelayCommand ?? (_employeeListRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new EmployeeList());
        SelectedPage = "Список сотрудников";
    }));

    private RelayCommand _orderListRelayCommand;
    public RelayCommand OrderListBtn => _orderListRelayCommand ?? (_orderListRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new OrdersListAdmin());
        SelectedPage = "Список заказов";
    }));

    private RelayCommand _shiftListRelayCommand;
    public RelayCommand ShiftListBtn => _shiftListRelayCommand ?? (_shiftListRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new SetTablesPage());
        SelectedPage = "Список смен";
    }));

    private RelayCommand _dismissRelayCommand;
    public RelayCommand DismissBtn => _dismissRelayCommand ?? (_dismissRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new DismissEmployee());
        SelectedPage = "Уволить";
    }));

    private RelayCommand _reportRelayCommand;
    public RelayCommand ReportBtn => _reportRelayCommand ?? (_reportRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new ReportPageAdmin());
        SelectedPage = "Отчёт";
    }));
    
    private RelayCommand _hireEmployeeRelayCommand;
    public RelayCommand HireEmployeeBtn => _hireEmployeeRelayCommand ?? (_hireEmployeeRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new CreateEmployeePage());
        SelectedPage = "Нанять";
    }));

    private RelayCommand _newShiftRelayCommand;
    public RelayCommand NewShiftBtn => _newShiftRelayCommand ?? (_newShiftRelayCommand = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new ShiftCreate());
        SelectedPage = "Создать смену";
    }));
    
    private RelayCommand _shiftInfoBtn;
    public RelayCommand ShiftInfoBtn => _shiftInfoBtn ?? (_shiftInfoBtn = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new ShiftCard());
        SelectedPage = "Смены информация";
    }));
    
    private RelayCommand _tablesListBtn;
    public RelayCommand TablesListBtn => _tablesListBtn ?? (_tablesListBtn = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new TablesList());
        SelectedPage = "Столики";
    }));

    
    private Employee _selectedEmployee;
    public Employee SelectedEmployee
    {
        get => _selectedEmployee;
        set {
            _selectedEmployee = value;
            OnPropertyChanged();
        }
    }
    
    private Order _selectedOrder;
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
    
    private ShiftDto _selectedShiftDto;
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
    
    private ObservableCollection<EmployeeDto> _employeeList;
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
    
    private ObservableCollection<Order> _orderList;
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

    
    private ObservableCollection<Shift> _shiftListView;
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
    
    private Employee _selectEmployeeDismiss;
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

    private Employee _createEmployeeFN;
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
    
    
    private DateTime _selectedDateTimeBirthday;
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
    public RelayCommand ViewOrderBtn => _viewOrder ?? (_viewOrder = new RelayCommand(x =>
    {
        if (SelectedOrder != null)
        {
            Navigation.adminFrame.Navigate(new OrderCard());
            Navigation.selectedOrder = SelectedOrder;
            SelectedPage = "Карточка заказа";
        }
    }));

    private RelayCommand _viewShift;
    public RelayCommand ViewShiftBtn => _viewShift ?? (_viewShift = new RelayCommand(x =>
    {
        if (SelectedShiftDto != null)
        {
            Navigation.adminFrame.Navigate(new ShiftCard());
            Navigation.selectedShift = SelectedShiftDto;
            SelectedPage = "Карточка смены";
        }
    }));
    
    private RelayCommand _confirmDismiss;
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
        AdminEndPoints newAdmin = new AdminEndPoints();
        EmployeeList = newAdmin.GetEmployeesList();
        return EmployeeList;
    }
}