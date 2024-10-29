using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
      private LeaveRequestDAO leaveRequestDAO = new LeaveRequestDAO();

        public List<LeaveRequest> getAllLeaveRequest()
       => leaveRequestDAO.getAllLeaveRequest();

        public LeaveRequest getLeaveRequest(int id)
        => leaveRequestDAO.getLeaveRequest(id);
        public void ChangeStatus(int leaveRequestId, string newStatus)
            =>leaveRequestDAO.ChangeStatus(leaveRequestId, newStatus);
    }
}
