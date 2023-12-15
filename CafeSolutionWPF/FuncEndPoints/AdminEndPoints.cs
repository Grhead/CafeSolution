using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Windows;
using CafeSolution.DTO;
using CafeSolution.Interfaces;
using CafeSolution.Models;
using CafeSolutionWPF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace CafeSolution.FuncEndPoints;

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
                Role = x.RoleNavigation.Title,
                Status = x.StatusNavigation.Title
                
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
                Role = x.RoleNavigation.Title,
                Status = x.StatusNavigation.Title

            }).Where(x => x.FirstName == employee.FirstName
                          && x.SecondName == employee.SecondName
                          && x.Birthday == employee.Birthday).FirstOrDefault();
        return checkEmployee;
    }

    public bool AddEmployeePhoto(string photo, int employeeId)
    {
        throw new System.NotImplementedException();
    }

    public string GetEmployeePhoto(int employeeId)
    {
        throw new System.NotImplementedException();
    }

    public List<Order> GetAllOrdersPerShift(int shiftId)
    {
        throw new System.NotImplementedException();
    }

    public bool CreateReportOrdersPerShift(int shiftId, int type)
    {
        // TODO add paragraphs
        try
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            document.Info.Title = "Created with PDFsharp";
            var filename = ($"{shiftId}_{DateTime.Now.ToUniversalTime()}.pdf");
            document.Save(filename);
        }
        catch (Exception e)
        {
            return false;
        }

        return true;
    }

    public List<Shift> CreateShift(List<Shift> shifts, List<Employee> employees)
    {
        throw new System.NotImplementedException();
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
                Role = x.RoleNavigation.Title,
                Status = x.StatusNavigation.Title

            }).FirstOrDefault();
        return getEmployee;
    }

    public ShiftDto GetShiftInfo(int shiftId)
    {
        throw new System.NotImplementedException();
    }

    public bool Dismiss(int employeeId)
    {
        throw new NotImplementedException();
    }
}