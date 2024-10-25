using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SideMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SideMenu.SelectedItem is ListBoxItem selectedItem)
            {
                //switch (selectedItem.Content.ToString())
                //{
                //    case "Nhân viên":
                //        MainContentFrame.Navigate(new EmployeeManagement());
                //        break;
                //    case "Bộ phận":
                //        MainContentFrame.Navigate(new DepartmentManagement());
                //        break;
                //    case "Chấm công":
                //        MainContentFrame.Navigate(new AttendanceManagement());
                //        break;
                //    case "Bảng lương":
                //        MainContentFrame.Navigate(new SalaryManagement());
                //        break;
                //    case "Dự án":
                //        MainContentFrame.Navigate(new ProjectManagement());
                //        break;
                //    case "Nghỉ phép":
                //        MainContentFrame.Navigate(new LeaveManagement());
                //        break;
                //    case "Báo cáo thống kê":
                //        MainContentFrame.Navigate(new Report());
                //        break;
                //    case "Tra cứu thông tin":
                //        MainContentFrame.Navigate(new Search());
                //        break;
                //    case "Hệ thống":
                //        MainContentFrame.Navigate(new SystemSettings());
                //        break;
                //    case "Chat":
                //        MainContentFrame.Navigate(new Chat());
                //        break;
                //    default:
                //        MainContentFrame.Navigate(null);
                //        break;
                //}
            }
        }
    }
}