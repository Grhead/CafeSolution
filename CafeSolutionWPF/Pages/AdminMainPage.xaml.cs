using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class AdminMainPage : Page
{
    public AdminMainPage()
    {
        InitializeComponent();
        Navigation.adminFrame = AdminFrame;
        DataContext = new AdminViewModel();
    }
}