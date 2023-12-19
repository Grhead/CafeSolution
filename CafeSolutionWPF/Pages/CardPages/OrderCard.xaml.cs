using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages.CardPages;

public partial class OrderCard : Page
{
    public OrderCard()
    {
        InitializeComponent();
        DataContext = new CookViewModel();
        CookEndPoints newCook = new CookEndPoints();
        foreach (var item in newCook.AllStatuses())
        {
            ComboBoxDishes.Items.Insert(ComboBoxDishes.Items.Count, item.Title);
        }
        ListBoxDishes.Items.Clear();
        foreach (var item in newCook.GetDishesInOrder(Navigation.selectedOrder.Id))
        {
            ListBoxDishes.Items.Insert(ListBoxDishes.Items.Count, item.Title);
        }

        TextBlockOrderId.Text = Navigation.selectedOrder.Id.ToString();
        TextBlockStatus.Text = Navigation.selectedOrder.CookingStatusId.ToString();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        CookEndPoints newCook = new CookEndPoints();
        // int selectedId = newCook.GetDishId((string)ComboBoxDishes.SelectedItem);
        newCook.ChangeCookingStatus(Navigation.selectedOrder.Id,newCook.GetDishId(ComboBoxDishes.SelectedItem.ToString()));
    }
}