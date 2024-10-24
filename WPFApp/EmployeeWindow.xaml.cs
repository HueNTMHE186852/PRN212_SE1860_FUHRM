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
            LoadEmployees();
        }
        public void LoadEmployees()
        {
            EmployeeDataGrid.ItemsSource = _employeeRepository.GetAllEmployees();
        }
        private void DisplayEmployeeDetails(Employee employee)
        {
            EmployeeIdTextBox.Text = employee.EmployeeId.ToString();
            FullNameTextBox.Text = employee.FullName;
            DateOfBirthDatePicker.SelectedDate = employee.DateOfBirth;
            GenderComboBox.SelectedItem = GenderComboBox.Items.OfType<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == employee.Gender);
            AddressTextBox.Text = employee.Address;
            PhoneNumberTextBox.Text = employee.PhoneNumber;
            DepartmentTextBox.Text = employee.DepartmentName;
            PositionTextBox.Text = employee.PositionName;
            SalaryTextBox.Text = employee.Salary.ToString();
        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
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
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                // Cập nhật thông tin nhân viên
                selectedEmployee.FullName = FullNameTextBox.Text;
                selectedEmployee.DateOfBirth = (DateTime)DateOfBirthDatePicker.SelectedDate;
                selectedEmployee.Gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                selectedEmployee.Address = AddressTextBox.Text;
                selectedEmployee.PhoneNumber = PhoneNumberTextBox.Text;
                selectedEmployee.DepartmentName = DepartmentTextBox.Text;
                selectedEmployee.PositionName = PositionTextBox.Text;
                selectedEmployee.Salary = Double.Parse(SalaryTextBox.Text);

                // Gọi phương thức lưu thay đổi từ repository
                _employeeRepository.UpdateEmployee(selectedEmployee);

                // Cập nhật lại DataGrid
                LoadEmployees();
            }
        }
    }
}
