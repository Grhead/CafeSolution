using System;
using System.Collections.Generic;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.DTO;

public class BillDto
{
    public DateTime BillDate { get; set; }
    public string Employee { get; set; }
    
    public string PaymentStatus { get; set; }
    
    public string PaymentType { get; set; }
    public List<Dish> DishesInBill { get; set; }
    public decimal? Amount { get; set; }
    
}