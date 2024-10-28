using BusinessObjects;
using Repositories;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    public partial class NotificationManagement : Window
    {
        private readonly NotificationRepository _notificationRepository;

        public NotificationManagement()
        {
            InitializeComponent();
            _notificationRepository = new NotificationRepository();
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            List<Notification> notifications = _notificationRepository.GetNotifications();
            NotificationDataGrid.ItemsSource = notifications;
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
            CreatedDateTextBox.Text = notification.CreatedDate.ToString("MM/dd/yyyy");
        }
    }
}
