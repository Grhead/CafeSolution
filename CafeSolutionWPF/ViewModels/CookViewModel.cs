using System.Collections.ObjectModel;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Pages;
using CafeSolutionWPF.Pages.CardPages;

namespace CafeSolutionWPF.ViewModels;

public class CookViewModel: UpdateProperty
{
    public CookViewModel()
    {
        SelfLogin = Navigation.ClientSession.Login;
    }

    public string SelfLogin { get; set; }
    
    private string _selectedPage;
    public string SelectedPage
    {
        get => _selectedPage;
        set
        {
            _selectedPage = value;
            OnPropertyChanged();
        }
    }
    
    public Order selectedOrder { get; set; }

    public ObservableCollection<Order> _orderListView;

    public ObservableCollection<Order> OrderListView
    {
        get => _orderListView;
        set
        {
            _orderListView = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Dish> _dishesListView { get; set; }
    
    public ObservableCollection<Dish> DishesListView
    {
        get => _dishesListView;
        set
        {
            _dishesListView = value;
            OnPropertyChanged();
        }
    }

    public TableCookingStatus _comboBoxSelectedItem { get; set; }
    
    public TableCookingStatus ComboBoxSelectedItem
    {
        get => _comboBoxSelectedItem;
        set
        {
            _comboBoxSelectedItem = value;
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<TableCookingStatus> AllCookingStatuses { get; set; }

    private void GetAllStatuses()
    {
        CookEndPoints newCook = new CookEndPoints();
        AllCookingStatuses = newCook.AllStatuses();
    }
    
    private RelayCommand _exit;
    public RelayCommand ExitBtn => _exit ?? (_exit = new RelayCommand(x =>
    {
        Navigation.mainFrame.Navigate(new AuthPage());
        Navigation.ClientSession = null;
    }));
    
    private RelayCommand _orderList;
    public RelayCommand OrderListBtn => _orderList ?? (_orderList = new RelayCommand(x =>
    {
        CookEndPoints newCook = new CookEndPoints();
        Navigation.cookFrame.Navigate(new OrdersList());
        SelectedPage = "Список заказов";
        OrderListView = newCook.GetAllOrdersPerShift();
    }));
    
    private RelayCommand _viewOrder;
    public RelayCommand ViewOrderBtn => _viewOrder ?? (_viewOrder = new RelayCommand(x =>
    {
        if (Navigation.selectedOrder != null)
        {
            Navigation.cookFrame.Navigate(new OrderCard());
            Navigation.selectedOrder = selectedOrder;   
            SelectedPage = "Карточка заказа";
        }
    }));
    
    private RelayCommand _changeStatus;
    public RelayCommand ChangeStatusBtn => _changeStatus ?? (_changeStatus = new RelayCommand(x =>
    {
        CookEndPoints newCook = new CookEndPoints();
        newCook.ChangeCookingStatus(selectedOrder.Id, ComboBoxSelectedItem.Id);
    }));
}