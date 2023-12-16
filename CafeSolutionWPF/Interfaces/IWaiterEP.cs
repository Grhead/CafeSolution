using System.Collections.Generic;
using CafeSolution.DTO;
using CafeSolution.Models;

namespace CafeSolution.Interfaces;

public interface IWaiterEp
{
    List<Order> GetAllOrdersPerShift(int shiftId, int employeeId);
    Order CreateNewOrder(List<Dish> dishes, int table, int numberOfCustomers, int employeeId);
    Order ChangePaymentStatus(int orderId, int paymentStatusId, int paymentTypeId);
    
    BillDto CreateBill(int orderId, int employeeId);
    List<Order> GetPaidOrdersPerShift(int shiftId, int employeeId);
    
    //nothing to return. Bool is process result
    bool CreateReportOrdersPerShift(int shiftId, int employeeId);
    
    Order GetOrder(int orderId);
    
    
}