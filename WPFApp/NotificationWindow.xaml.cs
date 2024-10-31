using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BusinessObjects;
using Repositories;

namespace WPFApp
{
    public partial class NotificationWindow : Window
    {
        private readonly INotificationRepository _notificationRepository;
        public Employee LoggedInEmployee { get; set; }
        public ObservableCollection<Notification> Notifications { get; set; }

        public NotificationWindow(Employee employee, INotificationRepository notificationRepository)
        {
            InitializeComponent();
            LoggedInEmployee = employee;
            _notificationRepository = notificationRepository;
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            var notifications = _notificationRepository.GetNotificationsByDepartmentId(LoggedInEmployee.DepartmentId);
            Notifications = new ObservableCollection<Notification>(notifications);
            NotificationListView.ItemsSource = Notifications;
        }
    }
}
