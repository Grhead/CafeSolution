using System.Collections.Generic;
using CafeSolution.DTO;
using CafeSolution.Models;

namespace CafeSolution.Interfaces;

public interface IAdminEp
{
    List<EmployeeDto> GetEmployeesList();
    List<Order> GetAllOrders();
    List<Shift> GtAllShifts();
    Order GetOrder(int orderId);
    EmployeeDto CreateEmployee(Employee employee);
    bool AddEmployeePhoto(string photo, int employeeId);
    string GetEmployeePhoto(int employeeId);
    List<Order> GetAllOrdersPerShift(int shiftId);
    
    //nothing to return. Bool is process result
    bool CreateReportOrdersPerShift(int shiftId, int type);
    Shift CreateShift(DateTime shiftDate, List<Employee> employees);
    EmployeeDto GetEmployeeInfo(int employeeId);
    ShiftDto GetShiftInfo(int shiftId);
    bool Dismiss(int employeeId);
}