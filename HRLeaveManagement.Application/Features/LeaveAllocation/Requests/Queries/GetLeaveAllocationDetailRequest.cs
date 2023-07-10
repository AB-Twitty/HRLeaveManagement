using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Queries
{
	public class GetLeaveAllocationDetailRequest : IRequest<LeaveAllocationDto>
	{
        public int Id { get; set; }
    }
}
