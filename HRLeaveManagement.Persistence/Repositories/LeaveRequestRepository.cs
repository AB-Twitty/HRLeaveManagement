using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.Persistence.Repositories
{
	public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
	{
		private readonly LeaveManagementDbContext _context;

        public LeaveRequestRepository(LeaveManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task ChangeLeaveRequestApprovalAsync(LeaveRequest leaveRequest, bool? ApprovalStatus)
		{
			leaveRequest.Approved = ApprovalStatus;
			_context.Entry(leaveRequest).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync()
		{
			return _context.LeaveRequests
				.Include(p => p.LeaveType)
				.ToListAsync();
		}

		public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id)
		{
			return await _context.LeaveRequests
				.Include(p => p.LeaveType)
				.FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
