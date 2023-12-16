using System.Collections.Generic;
using CafeSolution.DTO;
using CafeSolution.Interfaces;
using CafeSolution.Models;
using CafeSolutionWPF.Data;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace CafeSolution.FuncEndPoints;

public class WaiterEndPoints: IWaiterEp
{
    public List<Order> GetAllOrdersPerShift(int shiftId, int employeeId)
    {
        using DatabaseContext db = new DatabaseContext();
        List<Order> AllOrdersPerShift = db.Orders
            .Include(x => x.CookingStatus)
            .Include(x => x.PaymentType)
            .Include(x => x.PaymentStatus)
            .Include(x => x.DishesInOrders)
            .ThenInclude(x => x.Dish)
            .Include(x => x.Table)
            .ThenInclude(x => x.EmployeesAtTables)
            .ThenInclude(x => x.Employee)
            .ThenInclude(x => x.EmployeesAtShifts)
            .ThenInclude(x => x.Shift)
            .Where(x => x.Table.EmployeesAtTables.Any(x => x.EmployeeId == employeeId))
            .ToList();
        return AllOrdersPerShift;
    }

    public Order CreateNewOrder(List<Dish> dishes, int table, int numberOfCustomers, int employeeId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order newOrder = new Order()
        {
            TableId = table,
            NumberOfCustomers = numberOfCustomers,
            PaymentStatus = 1,
            CookingStatus = 1,
            PaymentType = null
        };
        db.Orders.Add(newOrder);
        db.SaveChanges();
        foreach (var item in dishes)
        {
            DishesInOrder dishesInNewOrder = new DishesInOrder
            {
                DishId = item.Id,
                OrderId = newOrder.Id
            };
            db.DishesInOrders.Add(dishesInNewOrder);
        }
        db.SaveChanges();
        return newOrder;
    }

    public Order ChangePaymentStatus(int orderId, int paymentStatusId, int paymentTypeId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order selectedOrder = db.Orders.FirstOrDefault(x => x.Id == orderId);
        selectedOrder.PaymentStatus = paymentStatusId;
        selectedOrder.PaymentType = paymentTypeId;
        db.SaveChanges();
        return selectedOrder;
    }

    // TODO check format output
    public BillDto CreateBill(int orderId, int employeeId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order getOrder = GetOrder(orderId);
        decimal? totalAmount = 0;
        List<Dish> dishesInOrders = new List<Dish>();
        foreach (var item in getOrder.DishesInOrders)
        {
            totalAmount += item.Dish.Price;
            dishesInOrders.Add(item.Dish);
        }
        Employee servingWaiter = db.Employees.FirstOrDefault(x => x.Id == employeeId);
        BillDto resultBill = new BillDto
        {
            BillDate = DateTime.Now.ToLocalTime(),
            Employee = servingWaiter.SecondName + " " + servingWaiter.FirstName + " " + servingWaiter.LastName,
            DishesInBill = dishesInOrders,
            PaymentStatus = getOrder.PaymentStatusNavigation.Title,
            PaymentType = getOrder.PaymentStatusNavigation.Title,
            Amount = totalAmount
        };
        
        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        document.Info.Title = "Created with PDFsharp";
        var filename = ($"{orderId}_{DateTime.Now.ToUniversalTime()}.pdf");
        var font = new XFont("Times New Roman", 20, XFontStyleEx.BoldItalic);
        int height = 50;
        int width = 0;
        gfx.DrawString($"Заказ №{orderId}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
        width = width + 120;
        gfx.DrawString($"Дата {resultBill.BillDate}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
        height = height + 20;
        width = 0;
        gfx.DrawString($"Статус оплаты {resultBill.PaymentStatus}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
        height = height + 20;
        width = 0;
        gfx.DrawString($"Метод оплаты {resultBill.PaymentType}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
        height = height + 20;
        width = 0;
        gfx.DrawString($"Блюда:", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
        height = height + 20;
        width = 80;
        foreach (var item in resultBill.DishesInBill)
        {
            gfx.DrawString($": {item.Title}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
            height = height + 20;
            width = 80;
        }
        gfx.DrawString($"Сумма: {resultBill.Amount}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
        height = height + 20;
        width = 0;
        return resultBill;
    }

    public List<Order> GetPaidOrdersPerShift(int shiftId, int employeeId)
    {
        using DatabaseContext db = new DatabaseContext();
        List<Order> AllOrdersPerShift = db.Orders
            .Include(x => x.CookingStatus)
            .Include(x => x.PaymentType)
            .Include(x => x.PaymentStatus)
            .Include(x => x.DishesInOrders)
            .ThenInclude(x => x.Dish)
            .Include(x => x.Table)
            .ThenInclude(x => x.EmployeesAtTables)
            .ThenInclude(x => x.Employee)
            .ThenInclude(x => x.EmployeesAtShifts)
            .ThenInclude(x => x.Shift)
            .Where(x => x.PaymentStatus == 2 && x.Table.EmployeesAtTables.Any(x => x.EmployeeId == employeeId))
            .ToList();
        return AllOrdersPerShift;
    }
    
    // TODO check format output
    public bool CreateReportOrdersPerShift(int shiftId, int employeeId)
    {
        List<Order> allOrders = GetPaidOrdersPerShift(shiftId, employeeId);
        try
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            document.Info.Title = "Created with PDFsharp";
            var filename = ($"{shiftId}_{DateTime.Now.ToUniversalTime()}.pdf");
            
            var font = new XFont("Times New Roman", 20, XFontStyleEx.BoldItalic);
            int height = 50;
            int width = 0;
            gfx.DrawString($"Смена №{shiftId}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
            width = width + 120;
            gfx.DrawString($"Сотрудник №{employeeId}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
            height = height + 20;
            width = 0;
            foreach (var item in allOrders)
            {
                gfx.DrawString($"Заказ №{item.Id}; Способ оплаты {item.PaymentStatus}; Тип оплаты{item.PaymentType}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
                height = height + 23;
                width = 0;
                foreach (var dish in item.DishesInOrders)
                {
                    gfx.DrawString($"Блюдо: {dish.Dish.Title} Стоимость {dish.Dish.Price}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
                    height = height + 13;
                    width = 0;
                }
            }
            document.Save(filename);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public Order GetOrder(int orderId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order selectedOrder = db.Orders
            .Include(x => x.PaymentStatus)
            .Include(x => x.PaymentType)
            .FirstOrDefault(x => x.Id == orderId);
        return selectedOrder;
    }
}