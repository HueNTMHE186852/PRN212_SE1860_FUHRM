using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DepartmentRepository :IDepartmentRepository    
    {
        public List<Department> GetDepartments()
            => DepartmentDAO.GetDepartments();
        public void AddDepartment(Department department)
            => DepartmentDAO.AddDepartment(department);
        public List<Department>? SearchDepartment(string Name)
            => DepartmentDAO.SearchDepartment(Name);
        public void RemoveDepartment(Department department)
            => DepartmentDAO.RemoveDepartment(department);
        public void UpdateDepartment(Department department)
            => DepartmentDAO.UpdateDepartment(department);
        public int CountEmployees(int departmentId)
            => DepartmentDAO.CountEmployees(departmentId);
    }
}
