using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages.FunctionPages;

public partial class ShiftCreate : Page
{
    public ShiftCreate()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
    }
}