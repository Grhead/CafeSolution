using System.Collections.Generic;
using System.IO.Packaging;
using CafeSolution.DTO;
using CafeSolution.Interfaces;
using CafeSolution.Models;

namespace CafeSolution.FuncEndPoints;

public class AdminEndPoints : IAdminEp
{
    public List<Employee> GetEmployeesList()
    {
        throw new System.NotImplementedException();
    }

    public List<Order> GetAllOrders()
    {
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