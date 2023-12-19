using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages;

public partial class ReportPageAdmin : Page
{
    public ReportPageAdmin()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
    }

    private void PdfBtnGet_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void XlsxBtnGet_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void PdfBtnPaid_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void XlsxBtnPaid_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
}