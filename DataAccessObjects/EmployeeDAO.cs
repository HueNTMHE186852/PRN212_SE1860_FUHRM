using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var employees = _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.Position)
                    .Include(e => e.Account)
                    .ThenInclude(a => a.Role)
                    .ToList();

                return employees;
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

        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.Find(employee.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.FullName = employee.FullName;
                existingEmployee.DateOfBirth = employee.DateOfBirth;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Address = employee.Address;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.DepartmentId = employee.DepartmentId;
                existingEmployee.PositionId = employee.PositionId;
                existingEmployee.Salary = employee.Salary;
                existingEmployee.StartDate = employee.StartDate;

                _context.SaveChanges();
            }
        }

        public bool DeleteEmployee(int employeeId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var employee = _context.Employees
                    .Include(e => e.Account)
                    .Include(e => e.Attendances)
                    .Include(e => e.LeaveRequests)
                    .Include(e => e.Salaries)
                    .FirstOrDefault(e => e.EmployeeId == employeeId);

                if (employee == null)
                {
                    return false;
                }

                if (employee.Attendances.Any())
                {
                    _context.Attendances.RemoveRange(employee.Attendances);
                }
                if (employee.LeaveRequests.Any())
                {
                    _context.LeaveRequests.RemoveRange(employee.LeaveRequests);
                }
                if (employee.Salaries.Any())
                {
                    _context.Salaries.RemoveRange(employee.Salaries);
                }

                if (employee.Account != null)
                {
                    _context.Accounts.Remove(employee.Account);
                }

                _context.Employees.Remove(employee);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error deleting employee: {ex.Message}");
                return false;
            }
        }

    }
}
