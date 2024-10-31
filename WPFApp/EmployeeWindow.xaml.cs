using DataAccessObjects;
using Repositories;
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
using System.Windows.Shapes;
using BusinessObjects;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media.Imaging;
using Microsoft.Win32;  // Add this for OpenFileDialog
namespace WPFApp

{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeWindow()
        {
            InitializeComponent();
            var context = new FuhrmContext();
            var employeeDAO = new EmployeeDAO(context);
            _employeeRepository = new EmployeeRepository(employeeDAO);
            LoadComboBoxData();
            LoadEmployees();
        }
        public void LoadEmployees()
        {
            EmployeeDataGrid.ItemsSource = _employeeRepository.GetAllEmployees();
        }
        private void LoadComboBoxData()
        {
            using (var context = new FuhrmContext())
            {
         
                DepartmentComboBox.ItemsSource = context.Departments.ToList();
                PositionComboBox.ItemsSource = context.Positions.ToList();
                RoleComboBox.ItemsSource = context.Roles.ToList();

                
                DepartmentComboBox.SelectedValuePath = "DepartmentId"; 
                PositionComboBox.SelectedValuePath = "PositionId"; 
                RoleComboBox.SelectedValuePath = "RoleId"; 
            }
        }

        private void DisplayEmployeeDetails(Employee employee)
        {
            EmployeeIdTextBox.Text = employee.EmployeeId.ToString();
            FullNameTextBox.Text = employee.FullName;
            DateOfBirthDatePicker.SelectedDate = employee.DateOfBirth;

            GenderComboBox.SelectedItem = GenderComboBox.Items.OfType<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString() == employee.Gender);

            AddressTextBox.Text = employee.Address;
            PhoneNumberTextBox.Text = employee.PhoneNumber;

          
            DepartmentComboBox.SelectedValue = employee.DepartmentId;
            PositionComboBox.SelectedValue = employee.PositionId;
            RoleComboBox.SelectedValue = employee.Account.RoleId;
            CreateDatePicker.SelectedDate = employee.StartDate;
            SalaryTextBox.Text = employee.Salary.ToString();

           
            if (!string.IsNullOrEmpty(employee.ProfilePicture))
            {
                try
                {
                    EmployeeImage.Source = new BitmapImage(new Uri(employee.ProfilePicture, UriKind.RelativeOrAbsolute));
                    ProfilePictureUrlTextBlock.Text = employee.ProfilePicture; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    EmployeeImage.Source = null; 
                    ProfilePictureUrlTextBlock.Text = string.Empty; 
                }
            }
            else
            {
                EmployeeImage.Source = null; 
                ProfilePictureUrlTextBlock.Text = string.Empty; 
            }
        }

        private void ChooseImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
               
                EmployeeImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));

              
                ProfilePictureUrlTextBlock.Text = openFileDialog.FileName;
            }
        }
       



        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                DisplayEmployeeDetails(selectedEmployee);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeIdTextBox.Text = null;
            FullNameTextBox.Text = null;
            DateOfBirthDatePicker.SelectedDate = null;

            
            GenderComboBox.SelectedItem = null;

            AddressTextBox.Text = null;
            PhoneNumberTextBox.Text = null;


            DepartmentComboBox.SelectedValue = null;
            PositionComboBox.SelectedValue = null;
            RoleComboBox.SelectedValue = null;
            CreateDatePicker.SelectedDate = null;
            SalaryTextBox.Text = null;
        }
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
      
                selectedEmployee.FullName = FullNameTextBox.Text;
                selectedEmployee.DateOfBirth = DateOfBirthDatePicker.SelectedDate.GetValueOrDefault();
                selectedEmployee.Gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                selectedEmployee.Address = AddressTextBox.Text;
                selectedEmployee.PhoneNumber = PhoneNumberTextBox.Text;
                selectedEmployee.DepartmentId = (int)DepartmentComboBox.SelectedValue; 
                selectedEmployee.PositionId = (int)PositionComboBox.SelectedValue; 
                selectedEmployee.Salary = Double.Parse(SalaryTextBox.Text); 
                selectedEmployee.StartDate = CreateDatePicker.SelectedDate.GetValueOrDefault();
                try
                {
                    if (selectedEmployee.Salary <= 0)
                    {
                        MessageBox.Show("Nhập lương lớn hơn 0");
                        return;
                    }
                    else if (selectedEmployee.DateOfBirth >= DateTime.Today)
                    {
                        MessageBox.Show("Lỗi ngày sinh lớn hơn ngày hiện tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else if (DateTime.Today.Year - selectedEmployee.DateOfBirth.Year < 18)
                    {
                        MessageBox.Show("Nhân viên phải trên 18 tủi", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else if (selectedEmployee.StartDate >= DateTime.Today)
                    {
                        MessageBox.Show("Ngày tham gia không được lớn hơn ngày hiện tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    _employeeRepository.UpdateEmployee(selectedEmployee);

                   
                    LoadEmployees();

                    MessageBox.Show("Thông tin nhân viên đã được lưu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
            
                    MessageBox.Show($"Có lỗi xảy ra khi lưu thông tin: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để lưu thông tin!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = (Employee)EmployeeDataGrid.SelectedItem;

            if (selectedEmployee == null)
            {
                MessageBox.Show("Please select an employee to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Directly attempt to delete for testing
            bool isDeleted = _employeeRepository.DeleteEmployee(selectedEmployee.EmployeeId);

            if (isDeleted)
            {
                MessageBox.Show("Employee deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadEmployees(); // Refresh the data grid
            }
            else
            {
                MessageBox.Show("Failed to delete the employee. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Content.ToString())
                {
                    //case "Trang Chủ":
                    //    // Navigate to Home screen
                    //    var homeView = new HomeView();
                    //    homeView.Show();
                    //    this.Close();
                    //    break;
                    case "Nhân viên":
                        // Navigate to Employee screen
                        var employeeView = new EmployeeWindow();
                        employeeView.Show();
                        this.Close();
                        break;
                    case "Bộ phận":
                        // Navigate to Department screen
                        var departmentView = new DepartmentManagement();
                        departmentView.Show();
                        this.Close();
                        break;
                    case "Chấm công":
                        // Navigate to Attendance screen
                        var attendanceView = new AttendanceView();
                        attendanceView.Show();
                        this.Close();
                        break;
                    case "Bảng lương":
                        // Navigate to Salary screen
                        var salaryView = new SalaryView();
                        salaryView.Show();
                        this.Close();
                        break;
                    case "Nghỉ phép":
                        // Navigate to Leave screen
                        var leaveView = new LeaveRequestView();
                        leaveView.Show();
                        this.Close();
                        break;

                    default:
                        break;
                }
            }
        }
        private void LeaveRequestButton_Click(object sender, RoutedEventArgs e)
        {
            AttendanceForm attendanceForm = new AttendanceForm();
            attendanceForm.Show();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var filteredEmployees= _employeeRepository.GetAllEmployees().Where(e => e.FullName.ToLower().Contains(searchText)).ToList();
            EmployeeDataGrid.ItemsSource = filteredEmployees;

        }
    }
}
