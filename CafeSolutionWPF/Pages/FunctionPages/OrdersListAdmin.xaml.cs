using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Pages.CardPages;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages.FunctionPages;

public partial class OrdersListAdmin : Page
{
    public OrdersListAdmin()
    {
        InitializeComponent();
        DataContext = new CookViewModel();
        CookEndPoints newCook = new CookEndPoints();
        foreach (var item in newCook.GetAllOrdersPerShift())
        {
            var newOrderString = item.Id;
            ListBoxOrders.Items.Insert(ListBoxOrders.Items.Count, newOrderString);
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        AdminViewModel newAdmin = new AdminViewModel();
        AdminEndPoints newAdminFunc = new AdminEndPoints();
        if (ListBoxOrders.SelectedItem != null)
        {
            Navigation.selectedOrder = newAdminFunc.GetOrder((int)ListBoxOrders.SelectedItem);
            Navigation.adminFrame.Navigate(new OrderCardAdmin());
            newAdmin.SelectedPage = "Карточка заказа";
        }
    }
}