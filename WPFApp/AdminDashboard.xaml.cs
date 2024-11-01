﻿using System;
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
using WPFApp.Models;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        private readonly FuhrmContext _context;
        public string CurrentWindowName { get; set; } = "Báo cáo thống kê";

        public AdminDashboard()
        {
            InitializeComponent();
            DataContext = this;
            _context = new FuhrmContext();

            DrawBarChart();
            DrawLineChart();
        }

        private void DrawBarChart()
        {
            var data = _context.Departments
                .Select(d => new
                {
                    d.DepartmentName,
                    EmployeeCount = d.Employees.Count()
                })
                .ToList();

            double maxCount = data.Max(d => d.EmployeeCount);
            double barWidth = 50;
            double spacing = 20;

            for (int i = 0; i < data.Count; i++)
            {
                double barHeight = (data[i].EmployeeCount / maxCount) * BarChartCanvas.ActualHeight;

                Rectangle bar = new Rectangle
                {
                    Width = barWidth,
                    Height = barHeight,
                    Fill = Brushes.SteelBlue
                };

                Canvas.SetLeft(bar, i * (barWidth + spacing));
                Canvas.SetBottom(bar, 0);
                BarChartCanvas.Children.Add(bar);

                // Add Label
                TextBlock label = new TextBlock
                {
                    Text = data[i].DepartmentName,
                    RenderTransform = new RotateTransform(-45),
                    FontSize = 10,
                    Foreground = Brushes.Black
                };
                Canvas.SetLeft(label, i * (barWidth + spacing) + barWidth / 4);
                Canvas.SetTop(label, BarChartCanvas.ActualHeight - barHeight - 20);
                BarChartCanvas.Children.Add(label);
            }
        }

        private void DrawLineChart()
        {
            var data = _context.Attendances
                .GroupBy(a => a.Date.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    PresentCount = g.Count(a => a.Status == "Present"),
                    AbsentCount = g.Count(a => a.Status == "Absent")
                })
                .OrderBy(g => g.Month)
                .ToList();

            double maxCount = data.Max(d => Math.Max(d.PresentCount, d.AbsentCount));
            double canvasWidth = LineChartCanvas.ActualWidth;
            double canvasHeight = LineChartCanvas.ActualHeight;
            double pointSpacing = canvasWidth / (data.Count - 1);

            PointCollection presentPoints = new PointCollection();
            PointCollection absentPoints = new PointCollection();

            for (int i = 0; i < data.Count; i++)
            {
                double x = i * pointSpacing;
                double presentY = canvasHeight - (data[i].PresentCount / maxCount) * canvasHeight;
                double absentY = canvasHeight - (data[i].AbsentCount / maxCount) * canvasHeight;

                presentPoints.Add(new Point(x, presentY));
                absentPoints.Add(new Point(x, absentY));
            }

            Polyline presentLine = new Polyline
            {
                Points = presentPoints,
                Stroke = Brushes.Green,
                StrokeThickness = 2
            };
            Polyline absentLine = new Polyline
            {
                Points = absentPoints,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };

            LineChartCanvas.Children.Add(presentLine);
            LineChartCanvas.Children.Add(absentLine);
        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Window currentWindow = Window.GetWindow(this);

                switch (button.Content.ToString())
                {
                    // Uncomment and implement the HomeView navigation if needed
                    case "Trang Chủ":
                        var homeView = new AdminDashboard();
                        homeView.Show();
                        currentWindow.Close();
                        break;
                    case "Tài khoản":
                        var account = new AccountManagement();
                        account.Show();
                        currentWindow.Close();
                        break;
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
                    case "Vị trí":
                        var position = new PositionManagement();
                        position.Show();
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

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the session
            SessionManager.CurrentAccount = null;

            // Redirect to login screen
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();

            // Close the current window
            Window.GetWindow(this).Close();
        }

    }
}
