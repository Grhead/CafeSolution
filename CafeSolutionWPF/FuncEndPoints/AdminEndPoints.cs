using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Aspose.Cells;
using CafeSolutionWPF.Data;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Interfaces;
using CafeSolutionWPF.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Font = iTextSharp.text.Font;

namespace CafeSolutionWPF.FuncEndPoints;

public class AdminEndPoints : IAdminEp
{
    public ObservableCollection<EmployeeDto> GetEmployeesList()
    {
        using var db = new DatabaseContext();
        var employees = new ObservableCollection<EmployeeDto>(db.Employees
            .Include(x => x.Status)
            .Include(x => x.Role)
            .Select(x => new EmployeeDto
            {
                FirstName = x.FirstName,
                SecondName = x.SecondName,
                LastName = x.LastName,
                Birthday = x.Birthday,
                Role = x.Role.Title,
                Status = x.Status.Title,
                Login = x.Login
            }).ToList());
        return employees;
    }

    public ObservableCollection<Employee> GetWorkEmployeesList()
    {
        using var db = new DatabaseContext();
        var employees = new ObservableCollection<Employee>(db.Employees
            .Include(x => x.Status)
            .Include(x => x.Role)
            .ToList());
        return employees;
    }

    public ObservableCollection<Order> GetAllOrders()
    {
        using var db = new DatabaseContext();
        var allOrders = new ObservableCollection<Order>(db.Orders
            .Include(x => x.Table)
            .Include(x => x.PaymentStatus)
            .Include(x => x.CookingStatus)
            .Include(x => x.PaymentStatus)
            .ToList());
        return allOrders;
    }

    public ObservableCollection<Shift> GetAllShifts()
    {
        using var db = new DatabaseContext();
        var allShifts = new ObservableCollection<Shift>(db.Shifts.ToList());
        return allShifts;
    }

    public Order GetOrder(int orderId)
    {
        using var db = new DatabaseContext();
        var selectedOrder = db.Orders
            .Include(x => x.Table)
            .Include(x => x.CookingStatus)
            .Include(x => x.DishesInOrders)
            .ThenInclude(x => x.Dish)
            .Where(x => x.Id == orderId)
            .FirstOrDefault();
        return selectedOrder;
    }

    public EmployeeDto CreateEmployee(Employee employee)
    {
        using var db = new DatabaseContext();
        db.Employees.Add(employee);
        db.SaveChanges();
        var checkEmployee = db.Employees
            .Include(x => x.Status)
            .Include(x => x.Role)
            .Select(x => new EmployeeDto
            {
                FirstName = x.FirstName,
                SecondName = x.SecondName,
                LastName = x.LastName,
                Birthday = x.Birthday,
                Role = x.Role.Title,
                Status = x.Status.Title
            }).Where(x => x.FirstName == employee.FirstName
                          && x.SecondName == employee.SecondName
                          && x.Birthday == employee.Birthday).FirstOrDefault();
        return checkEmployee;
    }

    public bool AddEmployeePhoto(string photo, int employeeId)
    {
        using var db = new DatabaseContext();
        try
        {
            var updateEmployee = db.Employees.FirstOrDefault(x => x.Id == employeeId);
            updateEmployee.Photo = photo;
            db.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public bool AddEmployeeScan(string photo, int employeeId)
    {
        using var db = new DatabaseContext();
        try
        {
            var updateEmployee = db.Employees.FirstOrDefault(x => x.Id == employeeId);
            updateEmployee.ContractScan = photo;
            db.SaveChanges();
            db.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public string GetEmployeePhoto(int employeeId)
    {
        using var db = new DatabaseContext();
        var employeePhoto = db.Employees.FirstOrDefault(x => x.Id == employeeId).Photo;
        return employeePhoto;
    }

    public string GetEmployeeScan(int employeeId)
    {
        using var db = new DatabaseContext();
        var employeeScan = db.Employees.FirstOrDefault(x => x.Id == employeeId).ContractScan;
        return employeeScan;
    }

    public ObservableCollection<Order> GetAllOrdersPerShift(int shiftId)
    {
        using var db = new DatabaseContext();
        var AllOrdersPerShift = new ObservableCollection<Order>(db.Orders
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
            .Where(x => x.PaymentTypeId != null)
            .ToList());
        return AllOrdersPerShift;
    }

    public bool CreateReportOrdersPerShift(int shiftId, string savePath)
    {
        var allOrders = GetAllOrdersPerShift(shiftId);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var document = new Document();
        PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
        var baseFont = BaseFont.CreateFont(@"C:/windows/fonts/arial.ttf", "windows-1251", BaseFont.EMBEDDED);
        var font = new Font(baseFont, 16);
        document.Open();
        document.Add(new Paragraph(new Phrase($"Смена №{shiftId}", font)));

        foreach (var item in allOrders)
        {
            document.Add(new Paragraph(new Phrase(
                $"Заказ №{item.Id}; Способ оплаты {item.PaymentStatus.Title}; Тип оплаты {item.PaymentType.Title}",
                font)));
            foreach (var dish in item.DishesInOrders)
                document.Add(new Paragraph(new Phrase($"    - Блюдо: {dish.Dish.Title} Стоимость {dish.Dish.Price}",
                    font)));
        }

        document.Close();

        return true;
    }

    public Shift CreateShift(DateTime shiftDate, ObservableCollection<Employee> employees)
    {
        if (shiftDate < DateTime.Today.AddDays(5) && employees.Count > 3 && employees.Count < 8)
        {
            using var db = new DatabaseContext();
            var newShift = new Shift
            {
                ShiftDate = shiftDate
            };
            db.Shifts.Add(newShift);
            db.SaveChanges();
            foreach (var item in employees)
            {
                var addEmployeeAtShift = new EmployeesAtShift
                {
                    ShiftId = newShift.Id,
                    EmployeeId = item.Id
                };
                db.EmployeesAtShifts.Add(addEmployeeAtShift);
            }

            db.SaveChanges();
            return newShift;
        }

        return null;
    }

    public EmployeeDto GetEmployeeInfoDto(int employeeId)
    {
        using var db = new DatabaseContext();
        var getEmployee = db.Employees
            .Include(x => x.Status)
            .Include(x => x.Role)
            .Where(x => x.Id == employeeId)
            .Select(x => new EmployeeDto
            {
                FirstName = x.FirstName,
                SecondName = x.SecondName,
                LastName = x.LastName,
                Birthday = x.Birthday,
                Role = x.Role.Title,
                Status = x.Status.Title
            }).FirstOrDefault();
        return getEmployee;
    }

    public Employee GetEmployeeInfo(string employeeLogin)
    {
        using var db = new DatabaseContext();
        var getEmployee = db.Employees
            .Include(x => x.Status)
            .Include(x => x.Role)
            .Where(x => x.Login == employeeLogin)
            .FirstOrDefault();
        return getEmployee;
    }

    public ShiftDto GetShiftInfo(int shiftId)
    {
        using var db = new DatabaseContext();
        var getShift = db.Shifts
            .Include(x => x.EmployeesAtShifts)
            .ThenInclude(x => x.Employee)
            .Select(x => new ShiftDto
            {
                ShiftDate = x.ShiftDate
            })
            .FirstOrDefault();

        getShift.EmployeesAtShift = new ObservableCollection<Employee>(db.EmployeesAtShifts
            .Where(x => x.ShiftId == shiftId)
            .Select(x => x.Employee)
            .ToList());
        decimal totalAmount = 0;
        foreach (var item in getShift.EmployeesAtShift)
            totalAmount += (decimal)db.Dishes
                .Include(x => x.DishesInOrders)
                .ThenInclude(x => x.Order)
                .ThenInclude(x => x.Table)
                .ThenInclude(x => x.EmployeesAtTables)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.EmployeesAtShifts)
                .ThenInclude(x => x.Shift)
                .Sum(x => x.Price);

        getShift.AmountByShift = totalAmount;
        return getShift;
    }


    // TODO Duplicate with ~AddDish WaiterEndPoints
    public bool ChangeOrderDetails(int orderId, ObservableCollection<Dish> newDishInOrder)
    {
        using var db = new DatabaseContext();
        var orderLetsChange = db.Orders.FirstOrDefault(x => x.Id == orderId);
        try
        {
            if (orderLetsChange.PaymentStatusId != 2)
                foreach (var item in newDishInOrder)
                {
                    var newDish = new DishesInOrder();
                    newDish.OrderId = orderId;
                    newDish.Dish = item;
                    db.DishesInOrders.Add(newDish);
                    db.SaveChanges();
                }
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public ObservableCollection<EmployeeInShift> GetEmployeesByShift(int shiftId)
    {
        // using DatabaseContext db = new DatabaseContext();
        // ObservableCollection<EmployeeInShift> newList = new ObservableCollection<EmployeeInShift>(db.EmployeesAtShifts
        //     .Include(x => x.Employee)
        //     .Include(x => x.Shift)
        //     .Select());
        var newList = new ObservableCollection<EmployeeInShift>();
        return newList;
    }

    public bool AddEmployeeToShift(int shiftId, int employeeId)
    {
        try
        {
            using var db = new DatabaseContext();
            var newEmpInShift = new EmployeesAtShift();
            newEmpInShift.ShiftId = shiftId;
            newEmpInShift.EmployeeId = employeeId;
            db.EmployeesAtShifts.Add(newEmpInShift);
            db.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public bool Dismiss(int employeeId)
    {
        try
        {
            using var db = new DatabaseContext();
            var nextDismiss = db.Employees.FirstOrDefault(x => x.Id == employeeId);
            nextDismiss.StatusId = 2;
            db.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public bool CreateReportOrdersPerShiftXLSX(int shiftId, string savePath)
    {
        var allOrders = GetAllOrdersPerShift(shiftId);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var wb = new Workbook();
        var sheet = wb.Worksheets[0];

        var cell = sheet.Cells["A1"];
        cell.PutValue($"Смена №{shiftId}");

        var countC = 1;
        foreach (var item in allOrders)
        {
            cell = sheet.Cells[$"C{countC}"];
            cell.PutValue(
                $"Заказ №{item.Id}; Способ оплаты {item.PaymentStatus.Title}; Тип оплаты {item.PaymentType.Title}");
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

    public void ChangeOrderDetails(int orderId, int customersCount, int tableNumber, int statusCook)
    {
        using var db = new DatabaseContext();
        var order = db.Orders.FirstOrDefault(x => x.Id == orderId);
        order.TableId = db.Tables.FirstOrDefault(x => x.TableNumber == tableNumber).Id;
        order.NumberOfCustomers = customersCount;
        order.CookingStatusId = statusCook;
        db.SaveChanges();
    }

    public void SetEmployeeToTable(int employeeId, int tableId)
    {
        using var db = new DatabaseContext();
        var newEmpAtTable = new EmployeesAtTable();
        newEmpAtTable.TableId = tableId;
        newEmpAtTable.EmployeeId = employeeId;
        db.EmployeesAtTables.Add(newEmpAtTable);
        db.SaveChanges();
    }

    public ObservableCollection<EmployeesAtTable> GetEmployeesAtTables()
    {
        using var db = new DatabaseContext();
        var newList = new ObservableCollection<EmployeesAtTable>(db.EmployeesAtTables
            .Include(x => x.Employee)
            .Include(x => x.Table));
        return newList;
    }

    public int GetEmployee(string login)
    {
        using var db = new DatabaseContext();
        return db.Employees.FirstOrDefault(x => x.Login == login).Id;
    }

    public Employee GetWholeEmployee(int employeeId)
    {
        using var db = new DatabaseContext();
        return db.Employees.FirstOrDefault(x => x.Id == employeeId);
    }

    public ObservableCollection<Order> GetPaidOrdersPerShift(int shiftId)
    {
        using var db = new DatabaseContext();
        var AllOrdersPerShift = new ObservableCollection<Order>(db.Orders
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
            .Where(x => x.PaymentStatusId == 2)
            .ToList());
        return AllOrdersPerShift;
    }

    public bool CreateReportPaidOrdersPerShift(int shiftId, string savePath)
    {
        var allOrders = GetPaidOrdersPerShift(shiftId);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var document = new Document();
        PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
        var baseFont = BaseFont.CreateFont(@"C:/windows/fonts/arial.ttf", "windows-1251", BaseFont.EMBEDDED);
        var font = new Font(baseFont, 16);
        document.Open();

        document.Add(new Paragraph(new Phrase($"Смена №{shiftId}", font)));

        foreach (var item in allOrders)
        {
            document.Add(new Paragraph(new Phrase(
                $"Заказ №{item.Id}; Способ оплаты {item.PaymentStatus.Title}; Тип оплаты {item.PaymentType.Title}",
                font)));
            foreach (var dish in item.DishesInOrders)
                document.Add(new Paragraph(new Phrase($"    - Блюдо: {dish.Dish.Title} Стоимость {dish.Dish.Price}",
                    font)));
        }

        document.Close();

        return true;
    }

    public bool CreateReportPaidOrdersPerShiftXLSX(int shiftId, string savePath)
    {
        var allOrders = GetPaidOrdersPerShift(shiftId);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var wb = new Workbook();
        var sheet = wb.Worksheets[0];

        var cell = sheet.Cells["A1"];
        cell.PutValue($"Смена №{shiftId}");

        var countC = 1;
        foreach (var item in allOrders)
        {
            cell = sheet.Cells[$"C{countC}"];
            cell.PutValue(
                $"Заказ №{item.Id}; Способ оплаты {item.PaymentStatus.Title}; Тип оплаты {item.PaymentType.Title}");
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
}