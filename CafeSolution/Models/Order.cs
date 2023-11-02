using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace CafeSolution.Models;

public class Order
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int NumberOfCustomers { get; set; }
    public int PaymentStatus { get; set; }
    public int CookingStatus { get; set; }
    public int PaymentType { get; set; }
    public decimal Amount { get; set; }
    
    public virtual DishesInOrder DishesInOrder { get; set; }
    
    public virtual List<Table> Tables { get; set; }
    public virtual List<PaymentStatus> PaymentStatuses { get; set; }
    public virtual List<CookingStatus> CookingStatuses { get; set; }
    public virtual List<PaymentType> PaymentTypes { get; set; }
}