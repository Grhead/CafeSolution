using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using Aspose.Cells;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Interfaces;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using PdfSharp.Drawing;
using Font = iTextSharp.text.Font;
using PdfDocument = PdfSharp.Pdf.PdfDocument;

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

    public decimal CalcTotalAmount(int orderId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order getOrder = GetOrder(orderId);
        decimal totalAmount = 0;
        var q = getOrder.DishesInOrders;
        foreach (var item in getOrder.DishesInOrders)
        {
            totalAmount += (decimal)item.Dish.Price;
        }

        return totalAmount;
    }

    public BillDto CreateBill(int orderId, int employeeId, string savePath)
    {
        using DatabaseContext db = new DatabaseContext();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Order getOrder = GetOrder(orderId);
        ObservableCollection<Dish> dishesInOrders =
            new ObservableCollection<Dish>(db.DishesInOrders.Include(x => x.Dish).Where(x => x.OrderId == orderId).Select(x => new Dish()
            {
                Title = x.Dish.Title
            }).ToList());
        decimal totalAmount = CalcTotalAmount(orderId);
        
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
        
        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
        BaseFont baseFont = BaseFont.CreateFont(@"C:/windows/fonts/arial.ttf", "windows-1251", BaseFont.EMBEDDED);
        Font font = new Font(baseFont, 16);
        document.Open();
        document.Add(new Paragraph(new Phrase($"Дата {resultBill.BillDate}", font)));
        document.Add(new Paragraph(new Phrase($"Заказ №{orderId}", font)));
        document.Add(new Paragraph(new Phrase($"Статус оплаты {resultBill.PaymentStatus}", font)));
        document.Add(new Paragraph(new Phrase($"Метод оплаты {resultBill.PaymentType}", font)));
        document.Add(new Paragraph(new Phrase("Блюда:", font)));
        foreach (var item in resultBill.DishesInBill)
        {
            document.Add(new Paragraph(new Phrase($"   - {item.Title}", font)));
        }
        document.Add(new Paragraph(new Phrase($"Сумма: {resultBill.Amount}", font)));
        document.Close();
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
    
    public bool CreateReportOrdersPerShift(int shiftId, int employeeId, string savePath)
    {
        ObservableCollection<Order> allOrders = GetPaidOrdersPerShift(shiftId, employeeId);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
            BaseFont baseFont = BaseFont.CreateFont(@"C:/windows/fonts/arial.ttf", "windows-1251", BaseFont.EMBEDDED);
            Font font = new Font(baseFont, 16);
            document.Open();
            
            document.Add(new Paragraph(new Phrase($"Смена №{shiftId}", font)));
            document.Add(new Paragraph(new Phrase($"Сотрудник №{employeeId}", font)));

            foreach (var item in allOrders)
            {
                document.Add(new Paragraph(new Phrase($"Заказ №{item.Id}; Способ оплаты {item.PaymentStatus.Title}; Тип оплаты {item.PaymentType.Title}", font)));
                foreach (var dish in item.DishesInOrders)
                {
                    document.Add(new Paragraph(new Phrase($"    - Блюдо: {dish.Dish.Title} Стоимость {dish.Dish.Price}", font)));
                }
            }
            document.Close();
            
        return true;
    }
    
    public bool CreateReportOrdersPerShiftXLSX (int shiftId, int employeeId, string savePath)
    {
        ObservableCollection<Order> allOrders = GetPaidOrdersPerShift(shiftId, employeeId);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        Workbook wb = new Workbook();
        Worksheet sheet = wb.Worksheets[0];

        Cell cell = sheet.Cells["A1"];
        cell.PutValue($"Смена №{shiftId}");
        cell = sheet.Cells["A2"];
        cell.PutValue($"Сотрудник №{employeeId}");

        int countC = 1;
        foreach (var item in allOrders)
        {
            cell = sheet.Cells[$"C{countC}"];
            cell.PutValue($"Заказ №{item.Id}; Способ оплаты {item.PaymentStatus.Title}; Тип оплаты {item.PaymentType.Title}");
            foreach (var dish in item.DishesInOrders)
            {
                cell = sheet.Cells[$"D{countC}"];
                cell.PutValue($"Блюдо: {dish.Dish.Title} Стоимость {dish.Dish.Price}");
                countC += 1;
            }

            countC += 2;
        }
        wb.Save(savePath, SaveFormat.Xlsx);
            
        return true;
    }

    public Order GetOrder(int orderId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order selectedOrder = db.Orders
            .Include(x => x.PaymentStatus)
            .Include(x => x.PaymentType)
            .Include(x => x.DishesInOrders)
            .ThenInclude(x => x.Dish)
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
    
    public Table GetTable(int tableTitle)
    {
        using DatabaseContext db = new DatabaseContext();
        Table selectedTable = db.Tables.FirstOrDefault(x => x.TableNumber == tableTitle);
        return selectedTable;
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
    public ObservableCollection<TablePaymentType> GetAllPaymentTypes()
    {
        using DatabaseContext db = new DatabaseContext();
        return new ObservableCollection<TablePaymentType>(db.TablePaymentTypes.ToList());
    }
    public int GetStatusId(string title)
    {
        using DatabaseContext db = new DatabaseContext();
        return db.TablePaymentTypes.FirstOrDefault(x => x.Title == title).Id;
    }
}