
using Repositories;
using System.Windows;
using System.Windows.Controls;
using WPFApp.Models;
using BusinessObjects;
namespace WPFApp
{
    public partial class MainWindow : Window
    {
        private readonly BusinessObjects.Employee _currentEmployee;
        public MainWindow(BusinessObjects.Employee employee)
        {
            InitializeComponent();
            _currentEmployee = employee;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
           
        {
            AttendanceForm attendanceForm = new AttendanceForm(_currentEmployee);
            attendanceForm.Show();
            this.Close();
        }
    }
}