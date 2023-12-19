using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Pages.CardPages;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class EmployeeList : Page
{
    public EmployeeList()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
        var newAdmin = new AdminViewModel();
        foreach (var item in newAdmin.GetAllEmployees()) ListBoxOrders.Items.Insert(ListBoxOrders.Items.Count, item);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var newAdmin = new AdminViewModel();
        if (ListBoxOrders.SelectedItem != null)
        {
            Navigation.selectedEmployee = (EmployeeDto)ListBoxOrders.SelectedItem;
            Navigation.adminFrame.Navigate(new EmployeeCard());
            newAdmin.SelectedPage = "Карточка сотрудника";
        }
    }
}