using System.Windows.Controls;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.Data;

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
}