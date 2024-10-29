using System.Windows;
using System.Windows.Controls;
using Repositories;
using BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace WPFApp
{
    public partial class NotificationManagement : Window
    {
        private readonly NotificationRepository _notificationRepository;
        private readonly DepartmentRepository _departmentRepository;

        public NotificationManagement()
        {
            InitializeComponent();
            _notificationRepository = new NotificationRepository();
            _departmentRepository = new DepartmentRepository();
            LoadDepartments();
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            List<Notification> notifications = _notificationRepository.GetNotifications();
            NotificationDataGrid.ItemsSource = notifications;
        }

        private void LoadDepartments()
        {
            DepartmentComboBox.ItemsSource = _departmentRepository.GetDepartments();
        }

        private void NotificationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NotificationDataGrid.SelectedItem is Notification selectedNotification)
            {
                DisplayNotificationDetails(selectedNotification);
            }
        }

        private void DisplayNotificationDetails(Notification notification)
        {
            TitleTextBox.Text = notification.Title;
            ContentTextBox.Text = notification.Content;
            DepartmentComboBox.SelectedValue = notification.DepartmentId;
            CreatedDatePicker.SelectedDate = notification.CreatedDate;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var notification = new Notification()
            {
                Title = TitleTextBox.Text,
                Content = ContentTextBox.Text,
                DepartmentId = (DepartmentComboBox.SelectedItem as Department).DepartmentId,
                CreatedDate = CreatedDatePicker.SelectedDate ?? DateTime.Now
            };
            _notificationRepository.AddNotification(notification);
            LoadNotifications();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationDataGrid.SelectedItem is Notification selectedNotification)
            {
                selectedNotification.Title = TitleTextBox.Text;
                selectedNotification.Content = ContentTextBox.Text;
                selectedNotification.DepartmentId = (DepartmentComboBox.SelectedItem as Department).DepartmentId;
                selectedNotification.CreatedDate = CreatedDatePicker.SelectedDate ?? DateTime.Now;
                _notificationRepository.UpdateNotification(selectedNotification);
                LoadNotifications();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationDataGrid.SelectedItem is Notification selectedNotification)
            {
                _notificationRepository.RemoveNotification(selectedNotification);
                LoadNotifications();
            }
        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Content.ToString())
                {
                    //case "Trang Chủ":
                    //    // Navigate to Home screen
                    //    var homeView = new HomeView();
                    //    homeView.Show();
                    //    this.Close();
                    //    break;
                    case "Nhân viên":
                        // Navigate to Employee screen
                        var employeeView = new EmployeeWindow();
                        employeeView.Show();
                        this.Close();
                        break;
                    case "Bộ phận":
                        // Navigate to Department screen
                        var departmentView = new DepartmentManagement();
                        departmentView.Show();
                        this.Close();
                        break;
                    case "Chấm công":
                        // Navigate to Attendance screen
                        var attendanceView = new AttendanceView();
                        attendanceView.Show();
                        this.Close();
                        break;
                    case "Bảng lương":
                        // Navigate to Salary screen
                        var salaryView = new SalaryView();
                        salaryView.Show();
                        this.Close();
                        break;
                    case "Nghỉ phép":
                        // Navigate to Leave screen
                        var leaveView = new LeaveRequestView();
                        leaveView.Show();
                        this.Close();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
