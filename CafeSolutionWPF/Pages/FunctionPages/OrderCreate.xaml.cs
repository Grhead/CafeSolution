using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages.FunctionPages;

public partial class OrderCreate : Page
{
    public OrderCreate()
    {
        InitializeComponent();
        DataContext = new WaiterViewModel();
        var newWaiter = new WaiterEndPoints();
        foreach (var item in newWaiter.GetAllDishes())
            AllDishesList.Items.Insert(AllDishesList.Items.Count, item.Title);
        foreach (var item in newWaiter.GetAllAvailableTables())
            ComboBoxTable.Items.Insert(ComboBoxTable.Items.Count, item.TableNumber);
    }

    private void AddDish_OnClick(object sender, RoutedEventArgs e)
    {
        if (AllDishesList.SelectedItem != null)
            DishesList.Items.Insert(DishesList.Items.Count, AllDishesList.SelectedItem);
    }

    private void CreateOrderBtn_OnClick(object sender, RoutedEventArgs e)
    {
        var newWaiter = new WaiterEndPoints();
        var dishes = new ObservableCollection<Dish>();
        foreach (var item in DishesList.Items) dishes.Add(newWaiter.GetDish(item.ToString()));
        newWaiter.CreateNewOrder(dishes, (int)ComboBoxTable.SelectionBoxItem,
            Convert.ToInt32(TextBoxCustomersCount.Text));
    }
}