using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Requests.Commands
{
	public class DeleteLeaveRequestCommand : IRequest<Unit>
	{
        public int Id { get; set; }
    }
}
