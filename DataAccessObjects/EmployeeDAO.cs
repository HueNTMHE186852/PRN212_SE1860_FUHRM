using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    
    public class EmployeeDAO
    {
        private readonly FuhrmContext _context;
        public EmployeeDAO(FuhrmContext context)
        {
            _context = context;
        }
        public List<Employee> GetAllEmployees()
        {
            try
            {
                using (var context = new FuhrmContext())
                {
                    var employees = context.Employees
                        .Include(e => e.Department)
                        .Include(e => e.Position)
                        .Include(e => e.Account)
                        .ThenInclude(a => a.Role) 
                        .ToList();

                    return employees;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching employees: {ex.Message}");
                return new List<Employee>();
            }
        }


        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employees.Find(employeeId);
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.Find(employee.EmployeeId);
            if (existingEmployee != null)
            {
                // Cập nhật các thuộc tính
                existingEmployee.FullName = employee.FullName;
                existingEmployee.DateOfBirth = employee.DateOfBirth;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Address = employee.Address;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.DepartmentId = employee.DepartmentId;
                existingEmployee.PositionId = employee.PositionId;
                existingEmployee.Salary = employee.Salary;

                // Lưu thay đổi
                _context.SaveChanges();
            }
        }


        public void DeleteEmployee(int employeeId)
        {
            var employee = _context.Employees.Find(employeeId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
    }
}
