using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class WaiterMainPage : Page
{
    public WaiterMainPage()
    {
        InitializeComponent();
        Navigation.waiterFrame = WaiterFrame;
        DataContext = new WaiterViewModel();
    }
}