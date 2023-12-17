using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Pages;

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
    public Employee ViewEmployee { get; }
    public ShiftDto ShiftDtoCard { get; }
    
    private RelayCommand _employeeList;
    private RelayCommand _orderList;
    private RelayCommand _shiftList;
    private RelayCommand _dismiss;
    private RelayCommand _report;
    private RelayCommand _exit;
    
    public RelayCommand ExitBtn => _exit ?? (_exit = new RelayCommand(x =>
    {
        Navigation.mainFrame.Navigate(new AuthPage());
        Navigation.ClientSession = null;
    }));
    
    public RelayCommand EmployeeListBtn => _employeeList ?? (_employeeList = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new EmployeeList());
        SelectedPage = "Список сотрудников";
    }));
    public RelayCommand OrderListBtn => _orderList ?? (_orderList = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new OrdersList());
        SelectedPage = "Список заказов";
    }));
    public RelayCommand ShiftListBtn => _shiftList ?? (_shiftList = new RelayCommand(x =>
    {
        Navigation.adminFrame.Navigate(new ShiftList());
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
}