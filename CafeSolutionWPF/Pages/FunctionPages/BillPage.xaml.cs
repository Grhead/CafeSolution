using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages.FunctionPages
{
    /// <summary>
    /// Interaction logic for BillPage.xaml
    /// </summary>
    public partial class BillPage : Page
    {
        public BillPage()
        {
            InitializeComponent();
            DataContext = new WaiterViewModel();
        }
    }
}
