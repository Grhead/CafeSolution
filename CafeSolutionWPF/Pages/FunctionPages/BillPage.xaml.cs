using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.ViewModels;
using Microsoft.Win32;

namespace CafeSolutionWPF.Pages.FunctionPages;

public partial class BillPage : Page
{
    public BillPage()
    {
        InitializeComponent();
        DataContext = new WaiterViewModel();
        var newWaiter = new WaiterEndPoints();
        TextBlockOrder.Text = Navigation.selectedOrder.Id.ToString();
        TextBlockAmount.Text = newWaiter.CalcTotalAmount(Navigation.selectedOrder.Id).ToString();
        TextBlockCustomersCount.Text = Navigation.selectedOrder.NumberOfCustomers.ToString();
        TextBlockTable.Text = Navigation.selectedOrder.Table.TableNumber.ToString();
        TextBlockStatus.Text = Navigation.selectedOrder.CookingStatus.Title;
        foreach (var item in Navigation.selectedOrder.DishesInOrders)
            ListBoxDishesInOrder.Items.Insert(ListBoxDishesInOrder.Items.Count, item.Dish.Title);

        foreach (var item in newWaiter.GetAllPaymentTypes())
            ComboBoxStatuses.Items.Insert(ComboBoxStatuses.Items.Count, item.Title);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var newWaiter = new WaiterEndPoints();

        var saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == true)
        {
            var folderName = saveFileDialog.FileName;
            newWaiter.CreateBill(Navigation.selectedOrder.Id, Navigation.ClientSession.Id, folderName);
            newWaiter.ChangePaymentStatus(Navigation.selectedOrder.Id, 4,
                newWaiter.GetStatusId(ComboBoxStatuses.SelectionBoxItem.ToString()));
        }
    }
}