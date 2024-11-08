using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using OxyPlot.Axes;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using WPFApp.Models;

namespace WPFApp
{
    public partial class AdminDashboard : Window
    {
        private readonly FuhrmContext _context = new FuhrmContext();

        public AdminDashboard()
        {
            InitializeComponent();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            LoadDepartmentBarChart();
            LoadAttendanceLineChart();
        }
        private void LoadDepartmentBarChart()
        {
            var plotModel = new PlotModel { Title = "Số Nhân Viên Theo Phòng Ban" };

            var barSeries = new BarSeries
            {
                Title = "Nhân Viên",
                LabelPlacement = LabelPlacement.Outside,
                LabelFormatString = "{0}",
                IsStacked = false // Ensures bars are independent, not stacked
            };

            // Querying department data
            var employeesByDepartment = _context.Employees
                .GroupBy(e => e.Department.DepartmentName)
                .Select(g => new { DepartmentName = g.Key, Count = g.Count() })
                .ToList();

            // Adding data points
            foreach (var dept in employeesByDepartment)
            {
                barSeries.Items.Add(new BarItem { Value = dept.Count });
            }

            // Adjusting the CategoryAxis for vertical alignment
            plotModel.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left, // Places department names on the left for a vertical layout
                ItemsSource = employeesByDepartment.Select(d => d.DepartmentName).ToList(),
                Title = "Phòng Ban"
            });

            // Adding a LinearAxis for the number of employees
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom, // Values appear on the bottom, in horizontal orientation
                Title = "Số Nhân Viên",
                MinimumPadding = 0.1,
                MaximumPadding = 0.1
            });

            plotModel.Series.Add(barSeries);
            DepartmentBarChart.Model = plotModel;
        }


        private void LoadAttendanceLineChart()
        {
            var plotModel = new PlotModel { Title = "Thống Kê Điểm Danh Hàng Tháng" };
            var lineSeries = new LineSeries
            {
                Title = "Điểm Danh",
                MarkerType = MarkerType.Circle
            };

            var attendanceData = _context.Attendances
                .GroupBy(a => a.Date.Month)
                .Select(g => new { Month = g.Key, Count = g.Count(a => a.Status == "Present") })
                .OrderBy(a => a.Month)
                .ToList();

            foreach (var data in attendanceData)
            {
                lineSeries.Points.Add(new DataPoint(data.Month, data.Count));
            }

            plotModel.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                Key = "MonthAxis",
                ItemsSource = attendanceData.Select(a => $"Tháng {a.Month}").ToList(),
                Title = "Tháng",
                Angle = 45,
                IsTickCentered = true
            });

            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Số Lần Điểm Danh",
                Minimum = 0
            });

            plotModel.Series.Add(lineSeries);
            AttendanceLineChart.Model = plotModel;
        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                Window currentWindow = Window.GetWindow(this);

                switch (button.Content.ToString())
                {
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
            SessionManager.CurrentAccount = null;
            var loginScreen = new LoginScreen();
            loginScreen.Show();
            Window.GetWindow(this).Close();
        }
    }
}
