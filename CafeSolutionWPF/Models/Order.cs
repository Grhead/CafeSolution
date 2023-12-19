using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CafeSolutionWPF.Models;

public partial class Order
{
    public int Id { get; set; }

    public int TableId { get; set; }

    public int? NumberOfCustomers { get; set; }

    public int? PaymentStatusId { get; set; }

    public int CookingStatusId { get; set; }

    public int? PaymentTypeId { get; set; }

    public virtual TableCookingStatus CookingStatus { get; set; } = null!;

    public virtual ICollection<DishesInOrder> DishesInOrders { get; set; } = new ObservableCollection<DishesInOrder>();

    public virtual TablePaymentStatus PaymentStatus { get; set; } = null!;

    public virtual TablePaymentType PaymentType { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;
}
