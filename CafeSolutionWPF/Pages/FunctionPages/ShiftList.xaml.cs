using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class ShiftList : Page
{
    public ShiftList()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
        
        AdminViewModel newAdmin = new AdminViewModel();
        AdminEndPoints newFunc = new AdminEndPoints();
        foreach (var item in newAdmin.GetAllWorkEmployees())
        {
            ComboBoxEmployee.Items.Insert(0, item.Id);
        }

        foreach (var item in newFunc.GetAllShifts())
        {
            ComboBoxShift.Items.Insert(0, item.Id);
        }

        ListBoxEmployee.Items.Clear();
        if (ComboBoxShift.SelectionBoxItem != "")
        {
            var empInShift = newFunc.GetShiftInfo((int)ComboBoxShift.SelectionBoxItem);
            foreach (var item in empInShift.EmployeesAtShift)
            {
                string newitem = item.SecondName + " " + empInShift.ShiftDate;
                ListBoxEmployee.Items.Insert(ListBoxEmployee.Items.Count, newitem);
            }
        }
        
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        AdminEndPoints newFunc = new AdminEndPoints();
        AdminViewModel newAdmin = new AdminViewModel();
        if (ComboBoxEmployee.SelectionBoxItem != "" || (string)ComboBoxShift.SelectionBoxItem != "")
        {
            newFunc.AddEmployeeToShift((int)ComboBoxShift.SelectionBoxItem, (int)ComboBoxEmployee.SelectionBoxItem);
            ListBoxEmployee.Items.Clear();
            var empInShift = newFunc.GetShiftInfo((int)ComboBoxShift.SelectionBoxItem);
            foreach (var item in empInShift.EmployeesAtShift)
            {
                string newitem = item.SecondName + " " + empInShift.ShiftDate;
                ListBoxEmployee.Items.Insert(ListBoxEmployee.Items.Count, newitem);
            }
        }
    }
}