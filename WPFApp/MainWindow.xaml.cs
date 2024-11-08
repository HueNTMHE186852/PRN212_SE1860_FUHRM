
using Repositories;
using System.Windows;
using System.Windows.Controls;
using WPFApp.Models;
using BusinessObjects;
using Microsoft.EntityFrameworkCore;
namespace WPFApp
{
    public partial class MainWindow : Window
    {
        private readonly int employeeID;
        public BusinessObjects.LeaveRequest LoggedInEmployee { get; set; }
        public MainWindow(int employeeId)
        {
            InitializeComponent();
            employeeID=employeeId;
            LoadLeavesRequestByID();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
           
        {
            AttendanceForm attendanceForm = new AttendanceForm(employeeID);
            attendanceForm.Show();
            this.Close();
        }
        public void LoadLeavesRequestByID()
        {
            LeaveRequestRepository leaveRepository = new LeaveRequestRepository();

            // Sử dụng trực tiếp employeeID để lấy danh sách yêu cầu nghỉ phép
            var leaveRequests = leaveRepository.GetLeaveRequestsByEmployeeID(employeeID);

            // Gán dữ liệu cho ViewLeaveRequest
            ViewLeaveRequest.ItemsSource = leaveRequests;

        }

        private void ViewLeaveRequest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ViewLeaveRequest.SelectedItem is BusinessObjects.LeaveRequest selectedLeave)
            {
                LeaveRequestRepository leaveRepository = new LeaveRequestRepository();
                int leaveRequest = selectedLeave.LeaveRequestId;
                BusinessObjects.LeaveRequest leaveRequestDetail = leaveRepository.getLeaveRequest(leaveRequest);
                if(leaveRequestDetail != null)
                {
                    contentTypeEdit.Text =  leaveRequestDetail.LeaveType;
                    startDateEdit.SelectedDate = leaveRequestDetail.StartDate.ToDateTime(TimeOnly.MinValue);
                    endDateEdit.SelectedDate = leaveRequestDetail.StartDate.ToDateTime(TimeOnly.MinValue);

                }
            }
        }
        public bool EditLeaveRequest(int leaveRequestId, string newLeaveType, DateOnly newStartDate, DateOnly newEndDate)
        {
            FuhrmContext _context = new FuhrmContext();
            var leaveRequest = _context.LeaveRequests.FirstOrDefault(lr => lr.LeaveRequestId == leaveRequestId);

            // Check if the leave request exists and the status is "Pending"
            if (leaveRequest != null && leaveRequest.Status == "Pending")
            {
                // Update the properties
                leaveRequest.LeaveType = newLeaveType;
                leaveRequest.StartDate = newStartDate;
                leaveRequest.EndDate = newEndDate;

                // Save changes to the database
                _context.SaveChanges();
                return true; // Return true indicating success
            }

            return false; // Return false if the leave request doesn't exist or isn't pending
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ViewLeaveRequest.SelectedItem is BusinessObjects.LeaveRequest selectedLeave && selectedLeave.Status == "Pending")
            {
                // Validate input
                if (startDateEdit.SelectedDate == null || endDateEdit.SelectedDate == null)
                {
                    MessageBox.Show("Vui lòng chọn ngày hợp lệ.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (startDateEdit.SelectedDate > endDateEdit.SelectedDate)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                selectedLeave.LeaveType = contentTypeEdit.Text;
                selectedLeave.StartDate = DateOnly.FromDateTime(startDateEdit.SelectedDate.Value);
                selectedLeave.EndDate = DateOnly.FromDateTime(endDateEdit.SelectedDate.Value);
                bool result = EditLeaveRequest(selectedLeave.LeaveRequestId, selectedLeave.LeaveType, selectedLeave.StartDate, selectedLeave.EndDate);

                if (result)
                {
                    MessageBox.Show("Đơn nghỉ phép đã được cập nhật.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadLeavesRequestByID();
                }
                else
                {
                    MessageBox.Show("Chỉ có thể chỉnh sửa đơn nghỉ phép với trạng thái đang chờ xử lý.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    LoadLeavesRequestByID();
                }
            }
        }
    }
}