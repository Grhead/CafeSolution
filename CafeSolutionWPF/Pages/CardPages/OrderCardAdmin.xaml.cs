using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.ViewModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CafeSolutionWPF.Pages.CardPages;

public partial class OrderCardAdmin : Page
{
    public OrderCardAdmin()
    {
        InitializeComponent();
        DataContext = new CookViewModel();
        var newCook = new CookEndPoints();
        var newWaiter = new WaiterEndPoints();
        foreach (var item in newCook.AllStatuses())
            ComboBoxStatuses.Items.Insert(ComboBoxStatuses.Items.Count, item.Title);
        ListBoxDishes.Items.Clear();
        foreach (var item in newCook.GetDishesInOrder(Navigation.selectedOrder.Id))
            ListBoxDishes.Items.Insert(ListBoxDishes.Items.Count, item.Title);
        ListBoxAddDishes.Items.Clear();
        foreach (var item in newWaiter.GetAllDishes())
            ListBoxAddDishes.Items.Insert(ListBoxAddDishes.Items.Count, item.Title);

        foreach (var item in newWaiter.GetAllTables())
            ComboBoxTableNumber.Items.Insert(ComboBoxTableNumber.Items.Count, item.TableNumber);

        TextBlockOrderId.Text = Navigation.selectedOrder.Id.ToString();
        TextBlockStatus.Text = Navigation.selectedOrder.CookingStatusId.ToString();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var newAdmin = new AdminEndPoints();
        var newCook = new CookEndPoints();
        newAdmin.ChangeOrderDetails(Navigation.selectedOrder.Id, Convert.ToInt32(TextBoxCustomersCount.Text),
            (int)ComboBoxTableNumber.SelectionBoxItem, newCook.GetDishId(ComboBoxStatuses.SelectionBoxItem.ToString()));
    }

    private void AddDishBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var newCook = new CookEndPoints();
        var newWaiter = new WaiterEndPoints();
        ObservableCollection<Dish> dishes = new ObservableCollectionListSource<Dish>();
        dishes.Add(newWaiter.GetDish(ListBoxAddDishes.SelectedItem.ToString()));
        newWaiter.AddDishToOrder(dishes, Navigation.selectedOrder.Id);

        ListBoxDishes.Items.Clear();
        foreach (var item in newCook.GetDishesInOrder(Navigation.selectedOrder.Id))
            ListBoxDishes.Items.Insert(ListBoxDishes.Items.Count, item.Title);
    }
}