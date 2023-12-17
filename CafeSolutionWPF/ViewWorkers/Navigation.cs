using System.Windows.Controls;
using CafeSolution.Models;

namespace CafeSolutionWPF;

public class Navigation
{
    public static Frame mainFrame;
    public static Frame adminFrame;
    public static Frame waiterFrame;
    public static Frame cookFrame;
    private static Employee clientSession = new Employee();
    public static Employee ClientSession
    {
        get => clientSession;
        set => clientSession = value;

    }
}