using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Commands
{
	public class CreateLeaveAllocationCommand : IRequest<int>
	{
        public CreateLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
