using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    
     public class LeaveRequestDAO
    {
        public List<LeaveRequest> getAllLeaveRequest()
        {
            using var _context = new FuhrmContext();
            var leaveList = new List<LeaveRequest>();
            try
            {
                leaveList = _context.LeaveRequests.ToList();
            }
            catch (Exception ex) { 
            throw new Exception(ex.Message);
            }
                return leaveList;
        }
        public LeaveRequest getLeaveRequest(int id) { 
        using var _context = new FuhrmContext();
        var leaveRequestDetail = _context.LeaveRequests
                .Include(e => e.Employee)
                .ThenInclude(d => d.Department)
                .FirstOrDefault(l => l.LeaveRequestId == id);
            return leaveRequestDetail;
        }
    }
}
