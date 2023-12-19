using System.Windows.Controls;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Data;
using CafeSolutionWPF.DTO;

namespace CafeSolutionWPF;

public class Navigation
{
    public static Frame mainFrame;
    public static Frame adminFrame;
    public static Frame waiterFrame;
    public static Frame cookFrame;
    public static readonly DatabaseContext db = new DatabaseContext();
    private static Employee clientSession;
    public static Employee ClientSession
    {
        get => clientSession;
        set => clientSession = value;
    }
    
    public static Order selectedOrder { get; set; }
    public static EmployeeDto selectedEmployee { get; set; }
    public static ShiftDto selectedShift { get; set; }
    public static Employee createEmployeeCache { get; set; }
}