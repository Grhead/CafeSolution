using CafeSolutionWPF.ViewModels;
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
using CafeSolutionWPF.DTO;
using CafeSolutionWPF.FuncEndPoints;
using CafeSolutionWPF.Models;

namespace CafeSolutionWPF.Pages.FunctionPages
{
    /// <summary>
    /// Interaction logic for GetEmployeePage.xaml
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
                AdminEndPoints newAdmin = new AdminEndPoints();
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (Navigation.createEmployeeCache.Login != null)
                {
                    newAdmin.AddEmployeePhoto(files[0], newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id);
                    Uri i = new Uri(
                        newAdmin.GetEmployeePhoto(newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id));
                }
            }
        }

        private void ImagePanelScan_Drop(object sender, DragEventArgs e)
        {
            AdminEndPoints newAdmin = new AdminEndPoints();
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (Navigation.createEmployeeCache.Login != null)
            {
                newAdmin.AddEmployeeScan(files[0], newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id);
                Uri i = new Uri(
                    newAdmin.GetEmployeePhoto(newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id));
            }
        }

        private void SelectScanButton_OnClick(object sender, RoutedEventArgs e)
        {
            AdminEndPoints newAdmin = new AdminEndPoints();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter =
                "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dlg.ShowDialog();
            newAdmin.AddEmployeeScan(dlg.FileName, newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id);
            Uri i = new Uri(newAdmin.GetEmployeeScan(newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id));
        }

        private void SelectPhotoButton_OnClick(object sender, RoutedEventArgs e)
        {
            AdminEndPoints newAdmin = new AdminEndPoints();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter =
                "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dlg.ShowDialog();
            newAdmin.AddEmployeePhoto(dlg.FileName, newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id);
            Uri i = new Uri(newAdmin.GetEmployeePhoto(newAdmin.GetEmployeeInfo(Navigation.createEmployeeCache.Login).Id));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            AdminViewModel newAdmin = new AdminViewModel();
            newAdmin.CreateEmployeeFN = new Employee();
            newAdmin.CreateEmployeeFN.Login = TextBoxLogin.Text;
            if (CalendarOfBirthday.SelectedDate != null)
            {
                newAdmin.CreateEmployeeFN.Birthday =  DateOnly.FromDateTime((DateTime)CalendarOfBirthday.SelectedDate);
            }
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
}