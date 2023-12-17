using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages.CardPages;

public partial class OrderCard : Page
{
    public OrderCard()
    {
        InitializeComponent();
        DataContext = new CookViewModel();
    }
}