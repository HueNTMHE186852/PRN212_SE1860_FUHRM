using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int NotificationType { get; set; }
        public int NotificationTypeId { get; set;
        public string Title { get; set; }
        public string Content { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Department? Department { get; set; }

    }
}
