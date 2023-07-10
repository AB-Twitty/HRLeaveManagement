using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Requests.Commands
{
	public class DeleteLeaveTypeCommand : IRequest<Unit>
	{
        public int Id { get; set; }
    }
}
