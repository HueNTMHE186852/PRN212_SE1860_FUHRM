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
namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AttendanceForm.xaml
    /// </summary>
    public partial class AttendanceForm : Window
    {

        public AttendanceForm()
        {
            InitializeComponent();
            // Lưu EmployeeId để tạo yêu cầu nghỉ phép
        }

        private void btnSubmitLeaveRequest_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin từ các trường trong form (ví dụ: loại nghỉ phép, ngày bắt đầu, ngày kết thúc)
            var leaveRequest = new LeaveRequest
            {
                LeaveType = LeaveType.Text,
                StartDate = DateOnly.FromDateTime(StartDate.SelectedDate.Value),
            EndDate = DateOnly.FromDateTime(EndDate.SelectedDate.Value),
                Status = "Pending"
            };

            // Gọi phương thức thêm yêu cầu nghỉ phép từ DAO
            var leaveRequestRepo = new LeaveRequestRepository();
            leaveRequestRepo.AddLeaveRequest(leaveRequest);

            MessageBox.Show("Leave request submitted successfully!");
            this.Close();
        }
    }

}
