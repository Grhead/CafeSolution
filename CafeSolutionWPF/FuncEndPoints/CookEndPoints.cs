using System.Collections.Generic;
using CafeSolution.Interfaces;
using CafeSolution.Models;
using CafeSolutionWPF.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeSolution.FuncEndPoints;

public class CookEndPoints: ICookEp
{
    public List<Order> GetAllOrdersPerShift(int shiftId)
    {
        using DatabaseContext db = new DatabaseContext();
        List<Order> AllOrdersPerShift = db.Orders
            .Include(x => x.CookingStatus)
            .Include(x => x.PaymentType)
            .Include(x => x.PaymentStatus)
            .Include(x => x.DishesInOrders)
            .ThenInclude(x => x.Dish)
            .Include(x => x.Table)
            .ThenInclude(x => x.EmployeesAtTables)
            .ThenInclude(x => x.Employee)
            .ThenInclude(x => x.EmployeesAtShifts)
            .ThenInclude(x => x.Shift)
            .ToList();
        return AllOrdersPerShift;
    }

    public Order GetOrder(int orderId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order selectedOrder = db.Orders.FirstOrDefault(x => x.Id == orderId);
        return selectedOrder;
    }

    public Order ChangeCookingStatus(int orderId, int cookingStatusId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order selectedOrder = db.Orders.FirstOrDefault(x => x.Id == orderId);
        selectedOrder.CookingStatus = cookingStatusId;
        db.SaveChanges();
        return selectedOrder;
    }
}