using System.Collections.ObjectModel;
using CafeSolutionWPF.Data;
using CafeSolutionWPF.Interfaces;
using CafeSolutionWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeSolutionWPF.FuncEndPoints;

public class CookEndPoints : ICookEp
{
    public ObservableCollection<Order> GetAllOrdersPerShift()
    {
        using var db = new DatabaseContext();
        var allOrdersPerShift = new ObservableCollection<Order>(db.Orders
            .Include(x => x.CookingStatus)
            .Include(x => x.PaymentType)
            .Include(x => x.PaymentStatus)
            .Include(x => x.DishesInOrders)
            .ThenInclude(x => x.Dish)
            .Include(x => x.Table)
            .ThenInclude(x => x.EmployeesAtTables)
            .ThenInclude(x => x.Employee)
            .ThenInclude(x => x.EmployeesAtShifts)
            .ThenInclude(x => x.Shift));
        return allOrdersPerShift;
    }

    public Order GetOrder(int orderId)
    {
        using var db = new DatabaseContext();
        var selectedOrder = db.Orders.FirstOrDefault(x => x.Id == orderId);
        return selectedOrder;
    }

    public Order ChangeCookingStatus(int orderId, int cookingStatusId)
    {
        using var db = new DatabaseContext();
        var selectedOrder = db.Orders.FirstOrDefault(x => x.Id == orderId);
        selectedOrder.CookingStatusId = cookingStatusId;
        db.SaveChanges();
        return selectedOrder;
    }

    public ObservableCollection<TableCookingStatus> AllStatuses()
    {
        using var db = new DatabaseContext();
        var statuses = new ObservableCollection<TableCookingStatus>(db.TableCookingStatuses.ToList());
        return statuses;
    }

    public int GetDishId(string dishTitle)
    {
        using var db = new DatabaseContext();
        return db.TableCookingStatuses.FirstOrDefault(x => x.Title == dishTitle).Id;
    }

    public ObservableCollection<Dish> GetDishesInOrder(int orderId)
    {
        using var db = new DatabaseContext();
        return new ObservableCollection<Dish>(db.DishesInOrders
            .Include(x => x.Dish)
            .Where(x => x.OrderId == orderId)
            .Select(x => new Dish
            {
                Title = x.Dish.Title
            }));
    }
}