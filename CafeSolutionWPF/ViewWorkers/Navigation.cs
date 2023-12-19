using System.Windows.Controls;
using CafeSolutionWPF.Data;
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF;

public class Navigation
{
    public static Frame mainFrame;
    public static Frame adminFrame;
    public static Frame waiterFrame;
    public static Frame cookFrame;
    public static readonly DatabaseContext db = new();

    public static Employee ClientSession { get; set; }

    public static Order selectedOrder { get; set; }
    public static EmployeeDto selectedEmployee { get; set; }
    public static ShiftDto selectedShift { get; set; }
    public static Employee createEmployeeCache { get; set; }
}