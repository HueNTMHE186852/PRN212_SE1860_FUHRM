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
            Clear();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDepartment = DepartmentDataGrid.SelectedItem as Department;
            
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Clear()
        {
            DepartmentIdTextBox.Text = string.Empty;
            DepartmentNameTextBox.Text = string.Empty;
            CreateDatePicker.Text = string.Empty;
            NumberOfEmployeeTextBox.Text = string.Empty;
        }

        private void DepartmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("SelectionChanged event triggered."); // Debug message

            if (DepartmentDataGrid.SelectedItem is Department selectedDepartment)
            {
                DepartmentIdTextBox.Text = selectedDepartment.DepartmentId.ToString();
                DepartmentNameTextBox.Text = selectedDepartment.DepartmentName;
                CreateDatePicker.SelectedDate = selectedDepartment.CreateDate;
                NumberOfEmployeeTextBox.Text = selectedDepartment.NumberOfEmployee.ToString();
            }
            else
            {
                Clear();
            }
        }



    }
}

