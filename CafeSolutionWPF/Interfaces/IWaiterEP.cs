using System.Collections.ObjectModel;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.Interfaces;

public interface IWaiterEp
{
    ObservableCollection<Order> GetAllOrdersPerShift(int shiftId, int employeeId);
    Order CreateNewOrder(ObservableCollection<Dish> dishes, int table, int numberOfCustomers);
    Order ChangePaymentStatus(int orderId, int paymentStatusId, int paymentTypeId);

    BillDto CreateBill(int orderId, int employeeId, string savePath);
    ObservableCollection<Order> GetPaidOrdersPerShift(int shiftId, int employeeId);

    //nothing to return. Bool is process result
    bool CreateReportOrdersPerShift(int shiftId, int employeeId, string savePath);

    Order GetOrder(int orderId);
}