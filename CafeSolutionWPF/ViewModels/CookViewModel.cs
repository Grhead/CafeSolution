using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.ViewModels;

public class CookViewModel: UpdateProperty
{
    public object selectedOrder { get; }
    public Order OrderView { get; }
}