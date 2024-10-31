using DataAccessObjects;
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
using Repositories;
using Microsoft.Identity.Client;
namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AttendanceForm.xaml
    /// </summary>
    public partial class AttendanceForm : Window
    {
        private int _accountId; // Biến để lưu AccountId
        public AttendanceForm(int accountId)
        {
            InitializeComponent();
            _accountId = accountId;
        }

        private void btnSubmitLeaveRequest_Click(object sender, RoutedEventArgs e)
        {
            // Lấy EmployeeId dựa trên AccountId
            var leaveRequestRepo = new LeaveRequestRepository();
            var employee = leaveRequestRepo.GetEmployeeByAccountId(MainWindow.CurrentAccountId);

            if (employee != null)
            {
                // Lấy thông tin từ các trường trong form
                var leaveRequest = new LeaveRequest
                {
                    EmployeeId = employee.EmployeeId, // Sử dụng EmployeeID
                    LeaveType = LeaveType.Text,
                    StartDate = DateOnly.FromDateTime(StartDate.SelectedDate.Value),
                    EndDate = DateOnly.FromDateTime(EndDate.SelectedDate.Value),
                    Status = "Pending"
                };

                // Gọi phương thức thêm yêu cầu nghỉ phép từ DAO
                leaveRequestRepo.AddLeaveRequest(leaveRequest);

                MessageBox.Show("Leave request submitted successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Employee not found for the current account.");
            }
        }
    }

}
