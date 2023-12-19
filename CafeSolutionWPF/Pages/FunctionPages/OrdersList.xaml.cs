using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Pages.CardPages;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class OrdersList : Page
{
    public OrdersList()
    {
        InitializeComponent();
        DataContext = new CookViewModel();
        var newCook = new CookEndPoints();
        foreach (var item in newCook.GetAllOrdersPerShift())
        {
            var newOrderString = item.Id;
            ListBoxOrders.Items.Insert(ListBoxOrders.Items.Count, newOrderString);
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var newAdmin = new AdminViewModel();
        var newAdminFunc = new AdminEndPoints();
        if (ListBoxOrders.SelectedItem != null)
        {
            Navigation.selectedOrder = newAdminFunc.GetOrder((int)ListBoxOrders.SelectedItem);
            Navigation.cookFrame.Navigate(new OrderCard());
            newAdmin.SelectedPage = "Карточка заказа";
        }
    }
}