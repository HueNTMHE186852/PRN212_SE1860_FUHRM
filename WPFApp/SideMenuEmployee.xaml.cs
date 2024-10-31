using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    public partial class SideMenuEmployee : UserControl, INotifyPropertyChanged
    {
        private string _currentWindowName;

        public string CurrentWindowName
        {
            get => _currentWindowName;
            set
            {
                _currentWindowName = value;
                OnPropertyChanged();
            }
        }

        public SideMenuEmployee()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += SideMenuEmployee_Loaded;
        }

        private void SideMenuEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            SetInitialWindowName();
        }

        private void SetInitialWindowName()
        {
            Window currentWindow = Window.GetWindow(this);
            if (currentWindow != null)
            {
                CurrentWindowName = currentWindow.Title; // Set the window title as the initial window name
            }
            else
            {
                CurrentWindowName = "Unknown Window"; // Fallback value if the window is not found
            }
        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Window currentWindow = Window.GetWindow(this);

                switch (button.Content.ToString())
                {
                    case "Home":
                        var homeView = new HomeEmployee();
                        homeView.Show();
                        currentWindow.Close();
                        break;
                    case "Take Attendance":
                        var takeAttendanceView = new TakeAttendance(SessionManager.CurrentAccount.AccountId);
                        takeAttendanceView.Show();
                        currentWindow.Close();
                        break;
                    default:
                        break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
