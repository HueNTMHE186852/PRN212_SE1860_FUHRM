using BusinessObjects;
using System.Collections.Generic;

namespace Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        void UpdateEmployee(Employee employee);
        bool DeleteEmployee(int employeeId);
        
    }
}
