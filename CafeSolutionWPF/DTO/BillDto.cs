using System;
using System.Collections.Generic;
using CafeSolution.Models;

namespace CafeSolution.DTO;

public class BillDto
{
    public DateTime BillDate { get; set; }
    public Employee Employee { get; set; }
    public List<Dish> DishesInBill { get; set; }
    public decimal Amount { get; set; }
    
}