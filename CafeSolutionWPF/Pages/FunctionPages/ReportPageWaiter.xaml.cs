using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using Microsoft.Win32;

namespace CafeSolutionWPF.Pages.FunctionPages;

public partial class ReportPageWaiter : Page
{
    public ReportPageWaiter()
    {
        InitializeComponent();
    }

    private void PdfBtn_OnClick(object sender, RoutedEventArgs e)
    {
        WaiterEndPoints newWaiter = new WaiterEndPoints();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == true)
        {
            var folderName = saveFileDialog.FileName;
            newWaiter.CreateReportOrdersPerShift(GeneralEndPoints.GetCurrentShift().Id,Navigation.ClientSession.Id, folderName);
        }
    }

    private void XlsxBtn_OnClick(object sender, RoutedEventArgs e)
    {
        WaiterEndPoints newWaiter = new WaiterEndPoints();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == true)
        {
            var folderName = saveFileDialog.FileName;
            newWaiter.CreateReportOrdersPerShiftXLSX(GeneralEndPoints.GetCurrentShift().Id,Navigation.ClientSession.Id, folderName);
        }
    }
}