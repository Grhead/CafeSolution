using System.Collections.Generic;
using CafeSolution.Interfaces;
using CafeSolution.Models;

namespace CafeSolution.FuncEndPoints;

public class CookEndPoints: ICookEp
{
    public List<Order> GetAllOrdersPerShift(int shiftId)
    {
        throw new System.NotImplementedException();
    }

    public Order GetOrder(int orderId)
    {
        throw new System.NotImplementedException();
    }

    public Order ChangeCookingStatus(int orderId, int cookingStatusId)
    {
        throw new System.NotImplementedException();
    }
}