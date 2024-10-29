using Repositories;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;

namespace WPFApp
{
    public partial class DepartmentManagement : Window
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentManagement()
        {
            InitializeComponent();
            _departmentRepository = new DepartmentRepository();
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            var departments = _departmentRepository.GetDepartments();
            DepartmentDataGrid.ItemsSource = departments;
        }

        private void DepartmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentDataGrid.SelectedItem is Department selectedDepartment)
            {
                DepartmentIdTextBox.Text = selectedDepartment.DepartmentId.ToString();
                DepartmentNameTextBox.Text = selectedDepartment.DepartmentName;
                CreateDatePicker.SelectedDate = selectedDepartment.CreateDate;
                NumberOfEmployeeTextBox.Text = selectedDepartment.EmployeeCount.ToString();
            }
            else
            {
                Clear();
            }
        }

        private void Clear()
        {
            DepartmentIdTextBox.Text = string.Empty;
            DepartmentNameTextBox.Text = string.Empty;
            CreateDatePicker.SelectedDate = null;
            NumberOfEmployeeTextBox.Text = string.Empty;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var department = new Department();
            department.DepartmentName = DepartmentNameTextBox.Text;
            department.CreateDate = CreateDatePicker.SelectedDate;
            _departmentRepository.AddDepartment(department);
            LoadDepartments();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentDataGrid.SelectedItem is Department selectedDepartment)
            {
                selectedDepartment.DepartmentName = DepartmentNameTextBox.Text;
                selectedDepartment.CreateDate = CreateDatePicker.SelectedDate;
                _departmentRepository.UpdateDepartment(selectedDepartment);
                LoadDepartments();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentDataGrid.SelectedItem is Department selectedDepartment)
            {
                _departmentRepository.RemoveDepartment(selectedDepartment);
                LoadDepartments();
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
