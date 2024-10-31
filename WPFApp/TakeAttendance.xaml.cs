using Repositories;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;
using DataAccessObjects;

namespace WPFApp
{
    public partial class TakeAttendance : Window
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly int _employeeId;

        public TakeAttendance(int employeeId)
        {
            InitializeComponent();
            var context = new FuhrmContext(); // Assuming FuhrmContext is your DbContext
            _employeeRepository = new EmployeeRepository(new EmployeeDAO(context));
            _attendanceRepository = new AttendanceRepository();
            _employeeId = employeeId;
            LoadEmployeeDetails();
        }

        private void LoadEmployeeDetails()
        {
            var employee = _employeeRepository.GetEmployeeById(_employeeId);
            if (employee != null)
            {
                EmployeeIdTextBox.Text = employee.EmployeeId.ToString();
                EmployeeNameTextBox.Text = employee.FullName;
                AttendanceDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                StatusTextBox.Text = "Present";
            }
            else
            {
                MessageBox.Show("Employee not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void SaveAttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var attendance = new Attendance
                {
                    EmployeeId = _employeeId,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Status = "Present"
                };
                _attendanceRepository.AddAttendance(attendance);
                MessageBox.Show("Attendance recorded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error recording attendance: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
