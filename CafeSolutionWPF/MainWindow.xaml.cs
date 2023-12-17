using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CafeSolutionWPF.Pages;
using CafeSolutionWPF.ViewModels;
using Frame = ModernWpf.Controls.Frame;

namespace CafeSolutionWPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Navigation.mainFrame = MainFrame;
        Navigation.mainFrame.Navigate(new AuthPage());
    }
}