using System.Collections.Generic;
using System.Collections.ObjectModel;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.Interfaces;

public interface ICookEp
{
    ObservableCollection<Order> GetAllOrdersPerShift();
    Order GetOrder(int orderId);
    Order ChangeCookingStatus(int orderId, int cookingStatusId);
    ObservableCollection<TableCookingStatus> AllStatuses();
}