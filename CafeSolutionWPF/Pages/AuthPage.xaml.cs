using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class AuthPage : Page
{
    public AuthPage()
    {
        InitializeComponent();
        DataContext = new GeneralViewModel();
    }
}