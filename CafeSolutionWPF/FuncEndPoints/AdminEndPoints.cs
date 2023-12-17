using System;
using System.Collections.Generic;
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
    public List<EmployeeDto> GetEmployeesList()
    {
        using DatabaseContext db = new DatabaseContext();
        List<EmployeeDto> employees = db.Employees
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
                
            }).ToList();
        return employees;
    }

    public List<Order> GetAllOrders()
    {
        using DatabaseContext db = new DatabaseContext();
        List<Order> allOrders = db.Orders
            .Include(x => x.Table)
            .Include(x => x.PaymentStatus)
            .Include(x => x.CookingStatus)
            .Include(x => x.PaymentStatus)
            .ToList();
        return allOrders;
    }

    public List<Shift> GtAllShifts()
    {
        using DatabaseContext db = new DatabaseContext();
        List<Shift> allShifts = db.Shifts.ToList();
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

    public List<Order> GetAllOrdersPerShift(int shiftId)
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
            .ToList();
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
                gfx.DrawString($"Сотрудник №{item.Id}: {item.SecondName} {item.FirstName} {item.LastName}", font, XBrushes.Black, new XRect(width, height, 0, 0), XStringFormats.BaseLineLeft);
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

    public Shift CreateShift(DateTime shiftDate, List<Employee> employees)
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

    public EmployeeDto GetEmployeeInfo(int employeeId)
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

    public ShiftDto GetShiftInfo(int shiftId)
    {
        using DatabaseContext db = new DatabaseContext();
        ShiftDto getShift = db.Shifts
            .Include(x => x.ShiftDate)
            .Include(x => x.EmployeesAtShifts)
            .ThenInclude(x => x.Employee)
            .Select(x => new ShiftDto
            {
                ShiftDate = x.ShiftDate
            })
            .FirstOrDefault();
        
        getShift.EmployeesAtShift = db.EmployeesAtShifts
            .Where(x => x.ShiftId == shiftId)
            .Select(x => x.Employee)
            .ToList();
        // TODO add amount count
        return getShift;
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
}