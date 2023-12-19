using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class SetTablesPage : Page
{
    public SetTablesPage()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();

        var newAdmin = new AdminViewModel();
        var newFunc = new AdminEndPoints();
        foreach (var item in newAdmin.GetAllWorkEmployees()) ComboBoxEmployee.Items.Insert(0, item.Id);

        foreach (var item in newFunc.GetAllShifts()) ComboBoxShift.Items.Insert(0, item.Id);

        ListBoxEmployee.Items.Clear();
        if (ComboBoxShift.SelectionBoxItem != "")
        {
            var empInShift = newFunc.GetShiftInfo((int)ComboBoxShift.SelectionBoxItem);
            foreach (var item in empInShift.EmployeesAtShift)
            {
                var newitem = item.SecondName + " " + empInShift.ShiftDate;
                ListBoxEmployee.Items.Insert(ListBoxEmployee.Items.Count, newitem);
            }
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var newFunc = new AdminEndPoints();
        var newAdmin = new AdminViewModel();
        if (ComboBoxEmployee.SelectionBoxItem != "" || (string)ComboBoxShift.SelectionBoxItem != "")
        {
            newFunc.AddEmployeeToShift((int)ComboBoxShift.SelectionBoxItem, (int)ComboBoxEmployee.SelectionBoxItem);
            ListBoxEmployee.Items.Clear();
            var empInShift = newFunc.GetShiftInfo((int)ComboBoxShift.SelectionBoxItem);
            foreach (var item in empInShift.EmployeesAtShift)
            {
                var newitem = item.SecondName + " " + empInShift.ShiftDate;
                ListBoxEmployee.Items.Insert(ListBoxEmployee.Items.Count, newitem);
            }
        }
    }
}