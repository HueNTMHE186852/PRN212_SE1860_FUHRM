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
        public void ChangeStatus(int leaveRequestId, string newStatus)
        {
            using var _context = new FuhrmContext();
            try
            {
                var leaveRequest = _context.LeaveRequests.FirstOrDefault(lr => lr.LeaveRequestId == leaveRequestId);
                if (leaveRequest == null)
                {
                    throw new Exception("Leave request not found.");
                }
                leaveRequest.Status = newStatus;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating status: " + ex.Message);
            }
        }
        public void AddLeaveRequest(LeaveRequest leaveRequest)
{
    using var context = new FuhrmContext();
    try
    {
        // Validate input
        if (leaveRequest == null)
        {
            throw new ArgumentNullException(nameof(leaveRequest), "Leave request cannot be null.");
        }

        // Check if employee exists
        var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == leaveRequest.EmployeeId);
        if (employee == null)
        {
            throw new Exception($"Employee with ID {leaveRequest.EmployeeId} not found.");
        }

        // Validate date range
        if (leaveRequest.StartDate > leaveRequest.EndDate)
        {
            throw new ArgumentException("Start date must be before or equal to end date.");
        }

        // Set default status if not provided
        if (string.IsNullOrWhiteSpace(leaveRequest.Status))
        {
            leaveRequest.Status = "Pending";
        }

        // Add the leave request
        context.LeaveRequests.Add(leaveRequest);
        context.SaveChanges();
    }
    catch (Exception ex)
    {
        // Log the exception or handle it as needed
        throw new Exception($"Error adding leave request: {ex.Message}", ex);
    }
}

    }
}
