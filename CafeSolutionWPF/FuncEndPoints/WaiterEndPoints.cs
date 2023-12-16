using System.Collections.Generic;
using CafeSolution.DTO;
using CafeSolution.Interfaces;
using CafeSolution.Models;
using CafeSolutionWPF.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeSolution.FuncEndPoints;

public class WaiterEndPoints: IWaiterEp
{
    public List<Order> GetAllOrdersPerShift(int shiftId, int employeeId)
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
            .Where(x => x.Table.EmployeesAtTables.Any(x => x.EmployeeId == employeeId))
            .ToList();
        return AllOrdersPerShift;
    }

    public Order CreateNewOrder(List<Dish> dishes, int table, int numberOfCustomers, int employeeId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order newOrder = new Order()
        {
            TableId = table,
            NumberOfCustomers = numberOfCustomers,
            PaymentStatus = 1,
            CookingStatus = 1,
            PaymentType = null
        };
        db.Orders.Add(newOrder);
        db.SaveChanges();
        foreach (var item in dishes)
        {
            DishesInOrder dishesInNewOrder = new DishesInOrder
            {
                DishId = item.Id,
                OrderId = newOrder.Id
            };
            db.DishesInOrders.Add(dishesInNewOrder);
        }
        db.SaveChanges();
        return newOrder;
    }

    public Order ChangePaymentStatus(int orderId, int paymentStatusId, int paymentTypeId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order selectedOrder = db.Orders.FirstOrDefault(x => x.Id == orderId);
        selectedOrder.PaymentStatus = paymentStatusId;
        selectedOrder.PaymentType = paymentTypeId;
        db.SaveChanges();
        return selectedOrder;
    }

    public BillDto CreateBill(int orderId, int employeeId)
    {
        throw new System.NotImplementedException();
    }

    //TODO Test result
    public List<Order> GetPaidOrdersPerShift(int shiftId, int employeeId)
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
            .Where(x => x.PaymentStatus == 2 && x.Table.EmployeesAtTables.Any(x => x.EmployeeId == employeeId))
            .ToList();
        return AllOrdersPerShift;
    }

    public bool CreateReportOrdersPerShift(int shiftId, int employeeId)
    {
        throw new System.NotImplementedException();
    }
}