using System.Windows;
using CafeSolutionWPF.Pages;

namespace CafeSolutionWPF;

/// <summary>
///     Interaction logic for MainWindow.xaml
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