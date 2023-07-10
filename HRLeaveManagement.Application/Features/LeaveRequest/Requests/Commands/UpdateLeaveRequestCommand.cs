using HRLeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Requests.Commands
{
	public class UpdateLeaveRequestCommand : IRequest<Unit>
	{
        public int Id { get; set; }

        public UpdateLeaveRequestDto UpdateLeaveRequestDto { get; set; }

        public ChangeLeaveRequestApprovalDto changeLeaveRequestApprovalDto { get; set; }
    }
}
