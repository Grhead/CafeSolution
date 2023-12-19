using System.Windows;
using System.Windows.Controls;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Models;
using CafeSolutionWPF.ViewModels;
using Microsoft.Win32;

namespace CafeSolutionWPF.Pages.FunctionPages;

/// <summary>
///     Interaction logic for GetEmployeePage.xaml
/// </summary>
public partial class CreateEmployeePage : Page
{
    public CreateEmployeePage()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
        ComboBoxRole.Items.Insert(0, "1");
        ComboBoxRole.Items.Insert(1, "2");
        ComboBoxRole.Items.Insert(2, "3");
    }

    private void ImagePanelPhoto_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var newAdmin = new AdminEndPoints();
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (Navigation.createEmployeeCache.Login != null)
            {
                newAdmin.AddEmployeePhoto(files[0], newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id);
                var i = new Uri(
                    newAdmin.GetEmployeePhoto(newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id));
            }
        }
    }

    private void ImagePanelScan_Drop(object sender, DragEventArgs e)
    {
        var newAdmin = new AdminEndPoints();
        var files = (string[])e.Data.GetData(DataFormats.FileDrop);
        if (Navigation.createEmployeeCache.Login != null)
        {
            newAdmin.AddEmployeeScan(files[0], newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id);
            var i = new Uri(
                newAdmin.GetEmployeePhoto(newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id));
        }
    }

    private void SelectScanButton_OnClick(object sender, RoutedEventArgs e)
    {
        var newAdmin = new AdminEndPoints();
        var dlg = new OpenFileDialog();
        dlg.DefaultExt = ".png";
        dlg.Filter =
            "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
        var result = dlg.ShowDialog();
        newAdmin.AddEmployeeScan(dlg.FileName, newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id);
        var i = new Uri(newAdmin.GetEmployeeScan(newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id));
    }

    private void SelectPhotoButton_OnClick(object sender, RoutedEventArgs e)
    {
        var newAdmin = new AdminEndPoints();
        var dlg = new OpenFileDialog();
        dlg.DefaultExt = ".png";
        dlg.Filter =
            "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
        var result = dlg.ShowDialog();
        newAdmin.AddEmployeePhoto(dlg.FileName, newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id);
        var i = new Uri(newAdmin.GetEmployeePhoto(newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id));
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var newAdmin = new AdminViewModel();
        newAdmin.CreateEmployeeFN = new Employee();
        newAdmin.CreateEmployeeFN.Login = TextBoxLogin.Text;
        if (CalendarOfBirthday.SelectedDate != null)
            newAdmin.CreateEmployeeFN.Birthday = DateOnly.FromDateTime((DateTime)CalendarOfBirthday.SelectedDate);
        newAdmin.CreateEmployeeFN.FirstName = TextBoxFN.Text;
        newAdmin.CreateEmployeeFN.SecondName = TextBoxSN.Text;
        newAdmin.CreateEmployeeFN.LastName = TextBoxLN.Text;
        newAdmin.CreateEmployeeFN.StatusId = 1;
        newAdmin.CreateEmployeeFN.RoleId = Convert.ToInt32(ComboBoxRole.SelectionBoxItem);
        newAdmin.CreateEmployeeFN.PassHash = GeneralEndPoints.CreateHash(TextBoxPass.Text);
        if (newAdmin.CreateEmployeeFN.Birthday != null)
        {
            newAdmin.CreateEmployee(newAdmin.CreateEmployeeFN);
            Navigation.createEmployeeCache = newAdmin.CreateEmployeeFN;
        }
    }
}