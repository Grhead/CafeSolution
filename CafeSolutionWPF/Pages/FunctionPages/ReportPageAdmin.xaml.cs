using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class ReportPageAdmin : Page
{
    public ReportPageAdmin()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
    }
}