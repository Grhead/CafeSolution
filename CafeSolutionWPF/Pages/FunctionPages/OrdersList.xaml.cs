using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class OrdersList : Page
{
    public OrdersList()
    {
        InitializeComponent();
        DataContext = new CookViewModel();
        Navigation.selectedOrder = ListBoxOrders.SelectedItem as Models.Order;
    }
}