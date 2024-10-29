using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for SideMenu.xaml
    /// </summary>
    public partial class SideMenu : UserControl
    {
        public SideMenu()
        {
            InitializeComponent();
        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Window currentWindow = Window.GetWindow(this);

                switch (button.Content.ToString())
                {
                    // Uncomment and implement the HomeView navigation if needed
                    // case "Trang Chủ":
                    //     var homeView = new HomeView();
                    //     homeView.Show();
                    //     currentWindow.Close();
                    //     break;
                    case "Nhân viên":
                        var employeeView = new EmployeeWindow();
                        employeeView.Show();
                        currentWindow.Close();
                        break;
                    case "Bộ phận":
                        var departmentView = new DepartmentManagement();
                        departmentView.Show();
                        currentWindow.Close();
                        break;
                    case "Chấm công":
                        var attendanceView = new AttendanceView();
                        attendanceView.Show();
                        currentWindow.Close();
                        break;
                    case "Bảng lương":
                        var salaryView = new SalaryView();
                        salaryView.Show();
                        currentWindow.Close();
                        break;
                    case "Nghỉ phép":
                        var leaveView = new LeaveRequestView();
                        leaveView.Show();
                        currentWindow.Close();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
