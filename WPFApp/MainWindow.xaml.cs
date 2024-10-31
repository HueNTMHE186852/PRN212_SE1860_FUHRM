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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow(this);
            var leavesRequestForm = new AttendanceForm();
            leavesRequestForm.Show();
            currentWindow.Close();
        }
    }
}