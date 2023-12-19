using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Windows;
using Aspose.Cells;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Interfaces;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf.Structure;
using Font = iTextSharp.text.Font;
using PdfDocument = PdfSharp.Pdf.PdfDocument;

namespace CafeSolutionWPF.FuncEndPoints;

public class AdminEndPoints : IAdminEp
{
    public ObservableCollection<EmployeeDto> GetEmployeesList()
    {
        using DatabaseContext db = new DatabaseContext();
        ObservableCollection<EmployeeDto> employees = new ObservableCollection<EmployeeDto>(db.Employees
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
        using DatabaseContext db = new DatabaseContext();
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>(db.Employees
            .Include(x => x.Status)
            .Include(x => x.Role)
            .ToList());
        return employees;
    }

    public ObservableCollection<Order> GetAllOrders()
    {
        using DatabaseContext db = new DatabaseContext();
        ObservableCollection<Order> allOrders = new ObservableCollection<Order>(db.Orders
            .Include(x => x.Table)
            .Include(x => x.PaymentStatus)
            .Include(x => x.CookingStatus)
            .Include(x => x.PaymentStatus)
            .ToList());
        return allOrders;
    }

    public ObservableCollection<Shift> GetAllShifts()
    {
        using DatabaseContext db = new DatabaseContext();
        ObservableCollection<Shift> allShifts = new ObservableCollection<Shift>(db.Shifts.ToList());
        return allShifts;
    }

    public Order GetOrder(int orderId)
    {
        using DatabaseContext db = new DatabaseContext();
        Order selectedOrder = db.Orders
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
        using DatabaseContext db = new DatabaseContext();
        db.Employees.Add(employee);
        db.SaveChanges();
        EmployeeDto checkEmployee = db.Employees
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
        using DatabaseContext db = new DatabaseContext();
        try
        {
            Employee updateEmployee = db.Employees.FirstOrDefault(x => x.Id == employeeId);
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
        using DatabaseContext db = new DatabaseContext();
        try
        {
            Employee updateEmployee = db.Employees.FirstOrDefault(x => x.Id == employeeId);
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
        using DatabaseContext db = new DatabaseContext();
        string employeePhoto = db.Employees.FirstOrDefault(x => x.Id == employeeId).Photo;
        return employeePhoto;
    }

    public string GetEmployeeScan(int employeeId)
    {
        using DatabaseContext db = new DatabaseContext();
        string employeeScan = db.Employees.FirstOrDefault(x => x.Id == employeeId).ContractScan;
        return employeeScan;
    }

    public ObservableCollection<Order> GetAllOrdersPerShift(int shiftId)
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
            .Where(x => x.PaymentTypeId != null)
            .ToList());
        return AllOrdersPerShift;
    }
    public bool CreateReportOrdersPerShift(int shiftId, string savePath)
    {
        ObservableCollection<Order> allOrders = GetAllOrdersPerShift(shiftId);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
        BaseFont baseFont = BaseFont.CreateFont(@"C:/windows/fonts/arial.ttf", "windows-1251", BaseFont.EMBEDDED);
        Font font = new Font(baseFont, 16);
        document.Open();
        document.Add(new Paragraph(new Phrase($"Смена №{shiftId}", font)));

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
    
    public bool CreateReportOrdersPerShiftXLSX (int shiftId, string savePath)
    {
        ObservableCollection<Order> allOrders = GetAllOrdersPerShift(shiftId);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
       
        Workbook wb = new Workbook();
        Worksheet sheet = wb.Worksheets[0];

        Cell cell = sheet.Cells["A1"];
        cell.PutValue($"Смена №{shiftId}");
            
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

    public Shift CreateShift(DateTime shiftDate, ObservableCollection<Employee> employees)
    {
        if (shiftDate < DateTime.Today.AddDays(5) && employees.Count > 3 && employees.Count < 8)
        {
            using DatabaseContext db = new DatabaseContext();
            Shift newShift = new Shift
            {
                ShiftDate = shiftDate
            };
            db.Shifts.Add(newShift);
            db.SaveChanges();
            foreach (var item in employees)
            {
                EmployeesAtShift addEmployeeAtShift = new EmployeesAtShift
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
        using DatabaseContext db = new DatabaseContext();
        EmployeeDto getEmployee = db.Employees
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
        using DatabaseContext db = new DatabaseContext();
        Employee getEmployee = db.Employees
            .Include(x => x.Status)
            .Include(x => x.Role)
            .Where(x => x.Login == employeeLogin)
            .FirstOrDefault();
        return getEmployee;
    }

    public ShiftDto GetShiftInfo(int shiftId)
    {
        using DatabaseContext db = new DatabaseContext();
        ShiftDto getShift = db.Shifts
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
        {
            totalAmount += (decimal)db.Dishes
                .Include(x => x.DishesInOrders)
                .ThenInclude(x => x.Order)
                .ThenInclude(x => x.Table)
                .ThenInclude(x => x.EmployeesAtTables)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.EmployeesAtShifts)
                .ThenInclude(x => x.Shift)
                .Sum(x => x.Price);
        }

        getShift.AmountByShift = totalAmount;
        return getShift;
    }

    
    // TODO Duplicate with ~AddDish WaiterEndPoints
    public bool ChangeOrderDetails(int orderId, ObservableCollection<Dish> newDishInOrder)
    {
        using DatabaseContext db = new DatabaseContext();
        Order orderLetsChange = db.Orders.FirstOrDefault(x => x.Id == orderId);
        try
        {
            if (orderLetsChange.PaymentStatusId != 2)
            {
                foreach (var item in newDishInOrder)
                {
                    DishesInOrder newDish = new DishesInOrder();
                    newDish.OrderId = orderId;
                    newDish.Dish = item;
                    db.DishesInOrders.Add(newDish);
                    db.SaveChanges();
                }    
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
        ObservableCollection<EmployeeInShift> newList = new ObservableCollection<EmployeeInShift>();
        return newList;
    }

    public bool AddEmployeeToShift(int shiftId, int employeeId)
    {
        try
        {
            using DatabaseContext db = new DatabaseContext();
            EmployeesAtShift newEmpInShift = new EmployeesAtShift();
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
            using DatabaseContext db = new DatabaseContext();
            Employee nextDismiss = db.Employees.FirstOrDefault(x => x.Id == employeeId);
            nextDismiss.StatusId = 2;
            db.SaveChanges();
        }
        catch (Exception e)
        {
            return false;
        }
        
        return true;
    }

    public void ChangeOrderDetails(int orderId, int customersCount, int tableNumber, int statusCook)
    {
        using DatabaseContext db = new DatabaseContext();
        Order order = db.Orders.FirstOrDefault(x => x.Id == orderId);
        order.TableId = db.Tables.FirstOrDefault(x => x.TableNumber == tableNumber).Id;
        order.NumberOfCustomers = customersCount;
        order.CookingStatusId = statusCook;
        db.SaveChanges();
    }

    public void SetEmployeeToTable(int employeeId, int tableId)
    {
        using DatabaseContext db = new DatabaseContext();
        EmployeesAtTable newEmpAtTable = new EmployeesAtTable();
        newEmpAtTable.TableId = tableId;
        newEmpAtTable.EmployeeId = employeeId;
        db.EmployeesAtTables.Add(newEmpAtTable);
        db.SaveChanges();
    }
    
    public ObservableCollection<EmployeesAtTable> GetEmployeesAtTables()
    {
        using DatabaseContext db = new DatabaseContext();
        ObservableCollection<EmployeesAtTable> newList = new ObservableCollection<EmployeesAtTable>(db.EmployeesAtTables
            .Include(x => x.Employee)
            .Include(x => x.Table));
        return newList;
    }

    public int GetEmployee(string login)
    {
        using DatabaseContext db = new DatabaseContext();
        return db.Employees.FirstOrDefault(x => x.Login == login).Id;
    }
    
    public ObservableCollection<Order> GetPaidOrdersPerShift(int shiftId)
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
            .Where(x => x.PaymentStatusId == 2)
            .ToList());
        return AllOrdersPerShift;
    }
    
    public bool CreateReportPaidOrdersPerShift (int shiftId, string savePath)
    {
        ObservableCollection<Order> allOrders = GetPaidOrdersPerShift(shiftId);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
            BaseFont baseFont = BaseFont.CreateFont(@"C:/windows/fonts/arial.ttf", "windows-1251", BaseFont.EMBEDDED);
            Font font = new Font(baseFont, 16);
            document.Open();
            
            document.Add(new Paragraph(new Phrase($"Смена №{shiftId}", font)));

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
    
    public bool CreateReportPaidOrdersPerShiftXLSX (int shiftId, string savePath)
    {
        ObservableCollection<Order> allOrders = GetPaidOrdersPerShift(shiftId);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
       
        Workbook wb = new Workbook();
        Worksheet sheet = wb.Worksheets[0];

        Cell cell = sheet.Cells["A1"];
        cell.PutValue($"Смена №{shiftId}");
            
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
}