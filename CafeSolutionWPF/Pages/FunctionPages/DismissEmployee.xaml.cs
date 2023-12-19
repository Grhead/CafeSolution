using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class DismissEmployee : Page
{
    public DismissEmployee()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
        var newAdmin = new AdminViewModel();
        foreach (var item in newAdmin.GetAllWorkEmployees()) ComboBoxDismissEmployee.Items.Insert(0, item.Id);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var newAdmin = new AdminViewModel();
        if (ComboBoxDismissEmployee.SelectionBoxItem != null)
        {
            newAdmin.DismissEmployee((int)ComboBoxDismissEmployee.SelectionBoxItem);
            ComboBoxDismissEmployee.Items.Clear();
            foreach (var item in newAdmin.GetAllWorkEmployees()) ComboBoxDismissEmployee.Items.Insert(0, item.Id);
        }
    }
}