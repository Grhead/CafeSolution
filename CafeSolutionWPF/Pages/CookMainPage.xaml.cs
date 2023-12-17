using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class CookMainPage : Page
{
    public CookMainPage()
    {
        InitializeComponent();
        Navigation.cookFrame = CookFrame;
        DataContext = new CookViewModel();
    }
}