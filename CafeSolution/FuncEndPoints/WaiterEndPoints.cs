using System.Collections.Generic;
using CafeSolution.DTO;
using CafeSolution.Interfaces;
using CafeSolution.Models;

namespace CafeSolution.FuncEndPoints;

public class WaiterEndPoints: IWaiterEp
{
    public List<Order> GetAllOrdersPerShift(int shiftId, int employeeId)
    {
        throw new System.NotImplementedException();
    }

    public Order CreateNewOrder(List<Dish> dishes, int table, int numberOfCustomers, int employeeId)
    {
        throw new System.NotImplementedException();
    }

    public Order ChangePaymentStatus(int orderId, int paymentStatusId, int paymentTypeId)
    {
        throw new System.NotImplementedException();
    }

    public BillDto CreateBill(int orderId, int employeeId)
    {
        throw new System.NotImplementedException();
    }

    public List<Order> GetPaidOrdersPerShift(int shiftId, int employeeId)
    {
        throw new System.NotImplementedException();
    }

    public bool CreateReportOrdersPerShift(int shiftId, int employeeId)
    {
        throw new System.NotImplementedException();
    }
}