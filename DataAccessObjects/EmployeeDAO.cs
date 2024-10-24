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
            var employees = _context.Employees
                .Include(e => e.Department) // Eager load Department
                .Include(e => e.Position)   // Eager load Position
                .Include(e => e.Account)    // Eager load Account
                .ThenInclude(a => a.Role)   // Eager load Role through Account
                .Select(e => new Employee
                {
                    EmployeeId = e.EmployeeId,
                    FullName = e.FullName,
                    DateOfBirth = e.DateOfBirth, // Convert DateOnly to DateTime
                    Gender = e.Gender,
                    Address = e.Address,
                    PhoneNumber = e.PhoneNumber,
                    DepartmentName = e.Department.DepartmentName,
                    PositionName = e.Position.PositionName,
                    RoleName = e.Account.Role.RoleName, // Access RoleName via Account
                    Salary = e.Salary,
                    StartDate = e.StartDate,
                    ProfilePicture = e.ProfilePicture
                }).ToList();

            return employees;
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
            _context.Employees.Update(employee);
            _context.SaveChanges();
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
