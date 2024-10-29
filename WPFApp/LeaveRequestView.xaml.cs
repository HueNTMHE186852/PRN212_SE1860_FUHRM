using BusinessObjects;
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
namespace WPFApp
{
    /// <summary>
    /// Interaction logic for LeaveRequestView.xaml
    /// </summary>
    public partial class LeaveRequestView : Window
    {
        private readonly LeaveRequestRepository _leaveRepository;
        public LeaveRequestView()
        {
            InitializeComponent();
            _leaveRepository = new LeaveRequestRepository();
            LoadLeaveRequest();
        }
        public void LoadLeaveRequest()
        {
            var leaveRequest = _leaveRepository.getAllLeaveRequest();
            LeaveRequestDataGrid.ItemsSource = leaveRequest;
        }
        public void LeaveRequestDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LeaveRequestDataGrid.SelectedItem is LeaveRequest selectedLeave)
            {
                int leaveRequestID = selectedLeave.LeaveRequestId;
                LeaveRequest leaveRequestDetail = _leaveRepository.getLeaveRequest(leaveRequestID);
                if (leaveRequestDetail != null) {
                    EmployeeNameText.Text = leaveRequestDetail.Employee.FullName;
                    DepartmentNameText.Text = leaveRequestDetail.Employee.Department.DepartmentName;
                    LeaveTypeText.Text = leaveRequestDetail.LeaveType.ToString();
                    StartDateText.Text = leaveRequestDetail.StartDate.ToString();
                    EndDateText.Text = leaveRequestDetail.EndDate.ToString();
                    StatusText.Text = leaveRequestDetail.Status.ToString();

                }
            }
        }
    }
}
