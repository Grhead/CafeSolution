using System.Collections.Generic;
using CafeSolution.Models;

namespace CafeSolution.Interfaces;

public interface IWaiterEp
{
    List<Order> GetAllOrdersPerShift(int shiftId, int employeeId);
    Order CreateNewOrder(List<Dish> dishes, int table, int numberOfCustomers, int employee);
    Order ChangePaymentStatus(int orderId, int paymentStatusId, int paymentTypeId);
    
    //TODO Create bill method (need DTO)
    List<Order> GetPaidOrdersPerShift(int shiftId, int employeeId);
    
    //TODO change void to report type
    void CreateReportOrdersPerShift(int shiftId, int employeeId);
    
    
}