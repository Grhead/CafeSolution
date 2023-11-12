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
    Employee CreateEmployee(Employee employee);
    bool AddEmployeePhoto(string photo, int employeeId);
    string GetEmployeePhoto(int employeeId);
    List<Order> GetAllOrdersPerShift(int shiftId);
    
    //nothing to return. Bool is process result
    bool CreateReportOrdersPerShift(int shiftId, int type);
    List<Shift> CreateShift(List<Shift> shifts, List<Employee> employees);
    EmployeeDto GetImEmployeeInfo(int employeeId);
    ShiftDto GetShiftInfo(int shiftId);
}