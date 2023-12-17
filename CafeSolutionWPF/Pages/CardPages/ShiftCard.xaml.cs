using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages.CardPages;

public partial class ShiftCard : Page
{
    public ShiftCard()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
    }
}