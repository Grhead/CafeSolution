using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using CafeSolution.Data;
using CafeSolution.DTO;
using CafeSolution.Interfaces;
using CafeSolution.Models;
using Microsoft.EntityFrameworkCore;

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
        Console.WriteLine(employees);
        return employees;
    }

    public List<Order> GetAllOrders()
    {
        using DatabaseContext db = new DatabaseContext();
        throw new System.NotImplementedException();
    }

    public List<Shift> GtAllShifts()
    {
        throw new System.NotImplementedException();
    }

    public Order GetOrder(int orderId)
    {
        throw new System.NotImplementedException();
    }

    public Employee CreateEmployee(Employee employee)
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }

    public List<Shift> CreateShift(List<Shift> shifts, List<Employee> employees)
    {
        throw new System.NotImplementedException();
    }

    public EmployeeDto GetImEmployeeInfo(int employeeId)
    {
        throw new System.NotImplementedException();
    }

    public ShiftDto GetShiftInfo(int shiftId)
    {
        throw new System.NotImplementedException();
    }
}