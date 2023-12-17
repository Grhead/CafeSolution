using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class EmployeeList : Page
{
    public EmployeeList()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
    }
}