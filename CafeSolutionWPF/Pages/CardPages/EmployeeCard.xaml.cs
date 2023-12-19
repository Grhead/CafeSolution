using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.ViewModels;

namespace CafeSolutionWPF.Pages.CardPages;

public partial class EmployeeCard : Page
{
    public EmployeeCard()
    {
        InitializeComponent();
        DataContext = new AdminViewModel();
        var newAdmin = new AdminEndPoints();
        TextBlockFN.Text = Navigation.selectedEmployee.FirstName;
        TextBlockSN.Text = Navigation.selectedEmployee.SecondName;
        TextBlockLN.Text = Navigation.selectedEmployee.LastName;
        TextBlockBirthday.Text = Navigation.selectedEmployee.Birthday.ToString();
        TextBlockRole.Text = Navigation.selectedEmployee.Role;
        TextBlockStatus.Text = Navigation.selectedEmployee.Status;
        TextBlockLogin.Text = Navigation.selectedEmployee.LastName;
        ImagePhotoSign.Text = "Фото " + Navigation.selectedEmployee.SecondName +
                              Navigation.selectedEmployee.FirstName +
                              Navigation.selectedEmployee.LastName;
        ImageScanSign.Text = "Скан " + Navigation.selectedEmployee.SecondName + " " +
                             Navigation.selectedEmployee.FirstName + " " +
                             Navigation.selectedEmployee.LastName;

        var imgUriScan =
            new Uri(newAdmin.GetEmployeeScan(newAdmin.GetEmployeeInfo(Navigation.selectedEmployee.Login).Id));
        var imgUriPhoto =
            new Uri(newAdmin.GetEmployeePhoto(newAdmin.GetEmployeeInfo(Navigation.selectedEmployee.Login).Id));

        ImageSource imgScan = new BitmapImage(imgUriScan);
        ImageSource imgPhoto = new BitmapImage(imgUriPhoto);
        ImagePhoto.Source = imgPhoto;
        ImageScan.Source = imgScan;
    }
}