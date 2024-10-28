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
using WPFApp.Models;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for DepartmentManagement.xaml
    /// </summary>
    public partial class DepartmentManagement : Window
    {
        FuhrmContext _context = new FuhrmContext();
        DepartmentRepository _departmentRepository;
        public DepartmentManagement()
        {
            InitializeComponent();
            _departmentRepository = new DepartmentRepository();
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            var departments = _departmentRepository.GetDepartments();
            DepartmentDataGrid.ItemsSource = departments;
            DepartmentDataGrid.DisplayMemberPath = "DepartmentName";
            DepartmentDataGrid.SelectedValuePath = "DepartmentId";
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DepartmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

