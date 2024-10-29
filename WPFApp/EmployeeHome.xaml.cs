using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Repositories;
using BusinessObjects;

namespace WPFApp
{
    public partial class EmployeeHome : Window
    {
        private readonly IAttendanceRepository _attendanceRepository;
        public ObservableCollection<Attendance> AttendanceRecords { get; set; }
        public Attendance SelectedAttendance { get; set; }

        public EmployeeHome(IAttendanceRepository attendanceRepository)
        {
            InitializeComponent();
            _attendanceRepository = attendanceRepository;
            AttendanceRecords = new ObservableCollection<Attendance>(_attendanceRepository.GetAttendances());
            AttendanceDataGrid.ItemsSource = AttendanceRecords;
        }

        private void AttendanceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AttendanceDataGrid.SelectedItem is Attendance selectedAttendance)
            {
                SelectedAttendance = selectedAttendance;
                AttendanceIdTextBox.Text = selectedAttendance.AttendanceId.ToString();
                EmployeeNameTextBox.Text = selectedAttendance.Employee.FullName;
                EmployeeIdTextBox.Text = selectedAttendance.EmployeeId.ToString();
                DateTextBox.Text = selectedAttendance.Date.ToString();
                StatusTextBox.Text = selectedAttendance.Status;
            }
        }

        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            var newAttendance = new Attendance
            {
                EmployeeId = 1, // Replace with actual employee ID
                Date = DateOnly.FromDateTime(DateTime.Now),
                Status = "Present",
                OvertimeHours = 0
            };
            _attendanceRepository.AddAttendance(newAttendance);
            AttendanceRecords.Add(newAttendance);
        }

        private void ClearAttendanceFields()
        {
            AttendanceIdTextBox.Clear();
            EmployeeNameTextBox.Clear();
            EmployeeIdTextBox.Clear();
            DateTextBox.Clear();
            StatusTextBox.Clear();
        }
    }
}
