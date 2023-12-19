using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages.CardPages;

public partial class ShiftCard : Page
{
    public ShiftCard()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
        TextBlockShiftNumber.Text = "-";
        TextBlockShiftAmount.Text = "-";
        var newAdmin = new AdminEndPoints();
        var empInShift = newAdmin.GetAllShifts();
        foreach (var item in empInShift) ListBoxShifts.Items.Insert(ListBoxShifts.Items.Count, item.Id);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (ListBoxShifts.SelectedItem.ToString() != "")
        {
            ListBoxEmployees.Items.Clear();
            var newAdmin = new AdminEndPoints();
            var newWaiter = new WaiterEndPoints();
            var empInShift = newAdmin.GetShiftInfo((int)ListBoxShifts.SelectedItem);
            TextBlockShiftNumber.Text = ListBoxShifts.SelectedItem.ToString();
            TextBlockShiftAmount.Text = newWaiter.CalcTotalAmount((int)ListBoxShifts.SelectedItem).ToString();
            foreach (var item in empInShift.EmployeesAtShift)
                ListBoxEmployees.Items.Insert(ListBoxEmployees.Items.Count, item.Login);
        }
    }
}