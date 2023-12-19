using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages.FunctionPages;

public partial class ShiftCreate : Page
{
    public ShiftCreate()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
        var newAdmin = new AdminEndPoints();
        foreach (var item in newAdmin.GetWorkEmployeesList())
            ListBoxAll.Items.Insert(ListBoxAll.Items.Count, item.Login);
    }

    private void AddEmployeeBtn_OnClick(object sender, RoutedEventArgs e)
    {
        ListBoxCreate.Items.Insert(ListBoxCreate.Items.Count, ListBoxAll.SelectedItem);
        ListBoxAll.Items.Remove(ListBoxAll.SelectedItem);
    }

    private void CreateShiftBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var newAdmin = new AdminEndPoints();
        var pushCollection = new ObservableCollection<Employee>();
        foreach (var item in ListBoxCreate.Items)
            pushCollection.Add(newAdmin.GetWholeEmployee(newAdmin.GetEmployee(item.ToString())));
        newAdmin.CreateShift(Convert.ToDateTime(DateTimeUpDownPicker.Text), pushCollection);
    }
}