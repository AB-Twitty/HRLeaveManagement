using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Commands
{
	public class DeleteLeaveAllocationCommand : IRequest<Unit>
	{
        public int Id { get; set; }
    }
}
