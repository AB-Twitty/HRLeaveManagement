using HRLeaveManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Contracts.Persistence
{
	public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
	{
		Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);
		Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync();
		Task ChangeLeaveRequestApprovalAsync(LeaveRequest leaveRequest, bool? ApprovalStatus);

	}
}
