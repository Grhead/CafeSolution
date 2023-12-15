using System.Collections.Generic;
using CafeSolution.Models;

namespace CafeSolution.Interfaces;

public interface ICookEp
{
    List<Order> GetAllOrdersPerShift(int shiftId);
    Order GetOrder(int orderId);
    Order ChangeCookingStatus(int orderId, int cookingStatusId);

}