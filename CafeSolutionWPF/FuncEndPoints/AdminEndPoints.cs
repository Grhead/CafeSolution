using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Windows;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Interfaces;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Structure;

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
        Order selectedOrder = db.Orders.FirstOrDefault(x => x.Id == orderId);
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
            .ToList());
        return AllOrdersPerShift;
    }
    // TODO check format output
    public bool CreateReportOrdersPerShift(int shiftId, int type)
    {
        ShiftDto shiftInfo = GetShiftInfo(shiftId);
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
            gfx.DrawString($"Дата смены №{shiftId} {shiftInfo.ShiftDate}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
            height = height + 20;
            width = 0;
            gfx.DrawString($"Выручка за смену{shiftInfo.AmountByShift}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
            height = height + 15;
            width = 0;
            foreach (var item in shiftInfo.EmployeesAtShift)
            {
                gfx.DrawString($"Сотрудник {item.SecondName} {item.FirstName} {item.LastName}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
                height = height + 23;
                width = 0;
            }
            document.Save(filename);
        }
        catch (Exception e)
        {
            return false;
        }

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
        // TODO add amount count
        return getShift;
    }

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
}