using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.ViewModels;
using Microsoft.Win32;

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
        AdminEndPoints newAdmin = new AdminEndPoints();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == true)
        {
            var folderName = saveFileDialog.FileName;
            newAdmin.CreateReportOrdersPerShift(GeneralEndPoints.GetCurrentShift().Id, folderName);
        }
    }

    private void XlsxBtnGet_OnClick(object sender, RoutedEventArgs e)
    {
        AdminEndPoints newAdmin = new AdminEndPoints();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == true)
        {
            var folderName = saveFileDialog.FileName;
            newAdmin.CreateReportOrdersPerShiftXLSX(GeneralEndPoints.GetCurrentShift().Id, folderName);
        }
    }

    private void PdfBtnPaid_OnClick(object sender, RoutedEventArgs e)
    {
        AdminEndPoints newAdmin = new AdminEndPoints();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == true)
        {
            var folderName = saveFileDialog.FileName;
            newAdmin.CreateReportPaidOrdersPerShift(GeneralEndPoints.GetCurrentShift().Id, folderName);
        }
    }

    private void XlsxBtnPaid_OnClick(object sender, RoutedEventArgs e)
    {
        AdminEndPoints newAdmin = new AdminEndPoints();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == true)
        {
            var folderName = saveFileDialog.FileName;
            newAdmin.CreateReportPaidOrdersPerShiftXLSX(GeneralEndPoints.GetCurrentShift().Id, folderName);
        }
    }
}