using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Interfaces;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Data;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace CafeSolutionWPF.FuncEndPoints;

public class WaiterEndPoints: IWaiterEp
{
    public ObservableCollection<Order> GetAllOrdersPerShift(int shiftId, int employeeId)
    {
        using DatabaseContext db = new DatabaseContext();
        ObservableCollection<Order> allOrdersPerShift = new ObservableCollection<Order>(db.Orders
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
            .ToList());
        return allOrdersPerShift;
    }

    public Order CreateNewOrder(ObservableCollection<Dish> dishes, int table, int numberOfCustomers)
    {
        using DatabaseContext db = new DatabaseContext();
        Order newOrder = new Order()
        {
            TableId = table,
            NumberOfCustomers = numberOfCustomers,
            PaymentStatusId = 1,
            CookingStatusId = 1,
            PaymentTypeId = null
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
        selectedOrder.PaymentStatusId = paymentStatusId;
        selectedOrder.PaymentTypeId = paymentTypeId;
        db.SaveChanges();
        return selectedOrder;
    }

    // TODO check format output
    public BillDto CreateBill(int orderId, int employeeId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order getOrder = GetOrder(orderId);
        decimal? totalAmount = 0;
        ObservableCollection<Dish> dishesInOrders = new ObservableCollection<Dish>();
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
            PaymentStatus = getOrder.PaymentStatus.Title,
            PaymentType = getOrder.PaymentType.Title,
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
        document.Save(filename);
        Process.Start(filename);
        return resultBill;
    }

    public ObservableCollection<Order> GetPaidOrdersPerShift(int shiftId, int employeeId)
    {
        using DatabaseContext db = new DatabaseContext();
        ObservableCollection<Order> AllOrdersPerShift = new ObservableCollection<Order>(db.Orders
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
            .Where(x => x.PaymentStatusId == 2 && x.Table.EmployeesAtTables.Any(x => x.EmployeeId == employeeId))
            .ToList());
        return AllOrdersPerShift;
    }
    
    // TODO check format output
    public bool CreateReportOrdersPerShift(int shiftId, int employeeId)
    {
        ObservableCollection<Order> allOrders = GetPaidOrdersPerShift(shiftId, employeeId);
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
    
    public Dish GetDish(string dishTitle)
    {
        using DatabaseContext db = new DatabaseContext();
        Dish selectedDish = db.Dishes
            .FirstOrDefault(x => x.Title == dishTitle);
        return selectedDish;
    }

    public ObservableCollection<DishesInOrder> AddDishToOrder(ObservableCollection<Dish> dishes, int orderId)
    {
        using DatabaseContext db = new DatabaseContext();
        foreach (var item in dishes)
        {
            DishesInOrder dishesInNewOrder = new DishesInOrder
            {
                DishId = item.Id,
                OrderId = orderId
            };
            db.DishesInOrders.Add(dishesInNewOrder);
        }
        db.SaveChanges();
        return new ObservableCollection<DishesInOrder> (db.DishesInOrders
            .Include(x => x.Dish)
            .Include(x => x.Order).ToList());
    }
    
    public ObservableCollection<Dish> GetAllDishes()
    {
        using DatabaseContext db = new DatabaseContext();
        return new ObservableCollection<Dish>(db.Dishes.ToList());
    }
    public ObservableCollection<Table> GetAllTables()
    {
        using DatabaseContext db = new DatabaseContext();
        return new ObservableCollection<Table>(db.Tables.ToList());
    }
    public ObservableCollection<Table> GetAllAvailableTables()
    {
        using DatabaseContext db = new DatabaseContext();
        return new ObservableCollection<Table>(db.EmployeesAtTables
            .Where(x => x.EmployeeId == Navigation.ClientSession.Id)
            .Select(x => new Table()
            {
                TableNumber = x.Table.TableNumber
            }));
    }
}