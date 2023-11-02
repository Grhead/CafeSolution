using System;
using System.Collections.Generic;

namespace CafeSolution.Models;

public partial class Order
{
    public int Id { get; set; }

    public int TableId { get; set; }

    public int? NumberOfCustomers { get; set; }

    public int PaymentStatus { get; set; }

    public int CookingStatus { get; set; }

    public int PaymentType { get; set; }

    public decimal? Amount { get; set; }

    public virtual CookingStatus CookingStatusNavigation { get; set; } = null!;

    public virtual ICollection<DishesInOrder> DishesInOrders { get; set; } = new List<DishesInOrder>();

    public virtual PaymentStatus PaymentStatusNavigation { get; set; } = null!;

    public virtual PaymentType PaymentTypeNavigation { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;
}
