using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.Pages.FunctionPages;

public partial class TablesList : Page
{
    public TablesList()
    {
        InitializeComponent();
        AdminEndPoints newAdmin = new AdminEndPoints();
        WaiterEndPoints newWaiter = new WaiterEndPoints();
        ObservableCollection<Employee> waitersList = new ObservableCollection<Employee>();
        foreach (var item in newAdmin.GetWorkEmployeesList())
        {
            if (item.RoleId == 3)
            {
                waitersList.Add(item);
                ComboBoxEmployee.Items.Add(item.Login);
            }
        }

        foreach (var item in newWaiter.GetAllTables())
        {
            ComboBoxTables.Items.Add(item.TableNumber);
        }
        
        foreach (var item in newAdmin.GetEmployeesAtTables())
        {
            string tempString = item.Table.TableNumber + " " + item.Employee.Login;
            ListBoxTables.Items.Insert(ListBoxTables.Items.Count, tempString);
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        AdminEndPoints newAdmin = new AdminEndPoints();
        WaiterEndPoints newWaiter = new WaiterEndPoints();
        newAdmin.SetEmployeeToTable(newAdmin.GetEmployee(ComboBoxEmployee.SelectedItem.ToString()), newWaiter.GetTable((int)ComboBoxTables.SelectedItem).Id);
        foreach (var item in newAdmin.GetEmployeesAtTables())
        {
            string tempString = item.Table.TableNumber + " " + item.Employee.Login;
            ListBoxTables.Items.Insert(ListBoxTables.Items.Count, tempString);
        }
    }
}