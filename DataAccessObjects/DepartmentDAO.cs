using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessObjects
{
    public class DepartmentDAO
    {
        public DepartmentDAO() { }
        public static List<Department> GetDepartments()
        {
            List<Department> list = new List<Department>();
            try
            {
                using var db = new FuhrmContext();
                list = db.Departments.ToList();
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public static void AddDepartment(Department department)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Add(department);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public static List<Department> SearchDepartment(string Name)
        {
            List<Department> list = new List<Department>();
            try
            {
                using var db = new FuhrmContext();
                list = db.Departments
                    .Where(d => d.DepartmentName.Equals(Name))
                    .ToList();
            }
            catch (Exception ex)
            {

            }
            return list;
        }

        public static void RemoveDepartment(Department department)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Remove(department);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public static void UpdateDepartment(Department department)
        {
            try
            {
                using var db = new FuhrmContext();
                db.Update(department);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
