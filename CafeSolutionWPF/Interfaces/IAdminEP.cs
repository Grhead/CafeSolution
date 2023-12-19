using System.Collections.Generic;
using System.Collections.ObjectModel;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.Interfaces;

public interface IAdminEp
{
    ObservableCollection<EmployeeDto> GetEmployeesList();
    ObservableCollection<Employee> GetWorkEmployeesList();
    ObservableCollection<Order> GetAllOrders();
    ObservableCollection<Shift> GetAllShifts();
    Order GetOrder(int orderId);
    EmployeeDto CreateEmployee(Employee employee);
    bool AddEmployeePhoto(string photo, int employeeId);
    bool AddEmployeeScan(string photo, int employeeId);
    string GetEmployeePhoto(int employeeId);
    string GetEmployeeScan(int employeeId);
    ObservableCollection<Order> GetAllOrdersPerShift(int shiftId);
    
    //nothing to return. Bool is process result
    bool CreateReportOrdersPerShift(int shiftId, string filePath);
    Shift CreateShift(DateTime shiftDate, ObservableCollection<Employee> employees);
    EmployeeDto GetEmployeeInfoDto(int employeeId);
    Employee GetEmployeeInfo(string employeeLogin);
    ShiftDto GetShiftInfo(int shiftId);
    bool Dismiss(int employeeId);
    bool ChangeOrderDetails (int orderId, ObservableCollection<Dish> newDishInOrder);
    ObservableCollection<EmployeeInShift> GetEmployeesByShift(int shiftId);
    bool AddEmployeeToShift(int shiftId, int employeeId);
}