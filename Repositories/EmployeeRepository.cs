using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObjects;
namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDAO _employeeDAO;

        public EmployeeRepository(EmployeeDAO employeeDAO)
        {
            _employeeDAO = employeeDAO;
        }

        public EmployeeRepository()
        {
            _employeeDAO = new EmployeeDAO();
        }

        public List<Employee> GetAllEmployees()
        {
            return _employeeDAO.GetAllEmployees();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeDAO.GetEmployeeById(employeeId);
        }

        public void AddEmployee(Employee employee)
        {
            _employeeDAO.AddEmployee(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeDAO.UpdateEmployee(employee);
        }

        public bool DeleteEmployee(int employeeId)
        {
            return _employeeDAO.DeleteEmployee(employeeId); 
        }


    }
}
