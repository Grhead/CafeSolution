using System.Collections.Generic;
using CafeSolution.Models;

namespace CafeSolution.Interfaces;

public interface IAdminEp
{
    List<Employee> GetEmployeesList();
    List<Order> GetAllOrders();
    List<Shift> GtAllShifts();
    Order GetOrder(int orderId);
    Employee CreateEmployee(Employee employee);
    bool AddEmployeePhoto(string photo, int employeeId);
    string GetEmployeePhoto(int employeeId);
    List<Order> GetAllOrdersPerShift(int shiftId);
    
    //TODO change void to report type. Int also change
    void CreateReportOrdersPerShift(int shiftId, int type);
    List<Shift> CreateShift(List<Shift> shifts, List<Employee> employees);

    //TODO employee data (need DTO)

    //TODO info about shift (need DTO)
}