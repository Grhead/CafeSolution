using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class EmployeeCard : Page
{
    public EmployeeCard()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
    }
}