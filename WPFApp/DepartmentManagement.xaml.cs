using DataAccessObjects;
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
        }

        private void LoadDepartments()
        {
            try
            {
                using var db = new FuhrmContext();
            }
        }
}
