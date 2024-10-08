
namespace BusinessObjects
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }

}
