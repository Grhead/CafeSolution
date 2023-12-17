using System.Collections.Generic;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.Interfaces;

public interface ICookEp
{
    List<Order> GetAllOrdersPerShift(int shiftId);
    Order GetOrder(int orderId);
    Order ChangeCookingStatus(int orderId, int cookingStatusId);

}