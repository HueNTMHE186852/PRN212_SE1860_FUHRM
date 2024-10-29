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
                // Lấy dữ liệu cho các ComboBox
                DepartmentComboBox.ItemsSource = context.Departments.ToList();
                PositionComboBox.ItemsSource = context.Positions.ToList();
                RoleComboBox.ItemsSource = context.Roles.ToList();

                // Thiết lập SelectedValuePath để liên kết ID
                DepartmentComboBox.SelectedValuePath = "DepartmentId"; // hoặc tên thuộc tính ID của Department
                PositionComboBox.SelectedValuePath = "PositionId"; // hoặc tên thuộc tính ID của Position
                RoleComboBox.SelectedValuePath = "RoleId"; // hoặc tên thuộc tính ID của Role
            }
        }

        private void DisplayEmployeeDetails(Employee employee)
        {
            EmployeeIdTextBox.Text = employee.EmployeeId.ToString();
            FullNameTextBox.Text = employee.FullName;
            DateOfBirthDatePicker.SelectedDate = employee.DateOfBirth;

            // Set Gender
            GenderComboBox.SelectedItem = GenderComboBox.Items.OfType<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString() == employee.Gender);

            AddressTextBox.Text = employee.Address;
            PhoneNumberTextBox.Text = employee.PhoneNumber;


            // Set SelectedValue cho Department, Position, và Role
            DepartmentComboBox.SelectedValue = employee.DepartmentId;
            PositionComboBox.SelectedValue = employee.PositionId;
            RoleComboBox.SelectedValue = employee.Account.RoleId;
            CreateDatePicker.SelectedDate = employee.StartDate;
            SalaryTextBox.Text = employee.Salary.ToString();

        }


        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                DisplayEmployeeDetails(selectedEmployee);
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có nhân viên nào được chọn không
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                // Cập nhật thông tin của đối tượng Employee từ các điều khiển
                selectedEmployee.FullName = FullNameTextBox.Text;
                selectedEmployee.DateOfBirth = DateOfBirthDatePicker.SelectedDate.GetValueOrDefault();
                selectedEmployee.Gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                selectedEmployee.Address = AddressTextBox.Text;
                selectedEmployee.PhoneNumber = PhoneNumberTextBox.Text;
                selectedEmployee.DepartmentId = (int)DepartmentComboBox.SelectedValue; // Chuyển đổi sang int nếu cần
                selectedEmployee.PositionId = (int)PositionComboBox.SelectedValue; // Chuyển đổi sang int nếu cần
                selectedEmployee.Account.RoleId = (int)RoleComboBox.SelectedValue; // Chuyển đổi sang int nếu cần
                selectedEmployee.Salary = Double.Parse(SalaryTextBox.Text); // Cần xử lý ngoại lệ nếu cần
                selectedEmployee.StartDate = CreateDatePicker.SelectedDate.GetValueOrDefault();
                try
                {
                    if (selectedEmployee.Salary <= 0)
                    {
                        MessageBox.Show("Nhập lương lớn hơn 0");
                        return;
                    }
                    // Lưu thay đổi vào cơ sở dữ liệu
                    _employeeRepository.UpdateEmployee(selectedEmployee);

                    // Cập nhật lại danh sách nhân viên trong DataGrid
                    LoadEmployees();

                    // Thông báo thành công
                    MessageBox.Show("Thông tin nhân viên đã được lưu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    MessageBox.Show($"Có lỗi xảy ra khi lưu thông tin: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để lưu thông tin!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }
}
