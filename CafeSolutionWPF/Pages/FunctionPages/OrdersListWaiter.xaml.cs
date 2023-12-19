using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages.FunctionPages;

public partial class OrdersListWaiter : Page
{
    public OrdersListWaiter()
    {
        InitializeComponent();
        DataContext = new CookViewModel();
        var newWaiter = new WaiterEndPoints();
        foreach (var item in newWaiter.GetAllOrdersPerShift(GeneralEndPoints.GetCurrentShift().Id,
                     Navigation.ClientSession.Id))
        {
            var newOrderString = item.Id + " Столик: " + item.Table.TableNumber + " Способ оплаты: " +
                                 item.PaymentStatus.Title + " Клиенты:  " + item.NumberOfCustomers;
            ListBoxOrders.Items.Insert(ListBoxOrders.Items.Count, newOrderString);
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var newAdmin = new AdminViewModel();
        var newAdminFunc = new AdminEndPoints();
        if (ListBoxOrders.SelectedItem != null)
        {
            Navigation.selectedOrder =
                newAdminFunc.GetOrder(Convert.ToInt32(ListBoxOrders.SelectedItem.ToString().Split()[0]));

            Navigation.waiterFrame.Navigate(new BillPage());
            newAdmin.SelectedPage = "Карточка заказа";
        }
    }
}