using System.Collections.Generic;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.Interfaces;

public interface IAdminEp
{
    List<EmployeeDto> GetEmployeesList();
    List<Order> GetAllOrders();
    List<Shift> GtAllShifts();
    Order GetOrder(int orderId);
    EmployeeDto CreateEmployee(Employee employee);
    bool AddEmployeePhoto(string photo, int employeeId);
    bool AddEmployeeScan(string photo, int employeeId);
    string GetEmployeePhoto(int employeeId);
    string GetEmployeeScan(int employeeId);
    List<Order> GetAllOrdersPerShift(int shiftId);
    
    //nothing to return. Bool is process result
    bool CreateReportOrdersPerShift(int shiftId, int type);
    Shift CreateShift(DateTime shiftDate, List<Employee> employees);
    EmployeeDto GetEmployeeInfo(int employeeId);
    ShiftDto GetShiftInfo(int shiftId);
    bool Dismiss(int employeeId);
}