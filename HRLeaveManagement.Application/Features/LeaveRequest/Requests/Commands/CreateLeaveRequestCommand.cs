using HRLeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Requests.Commands
{
	public class CreateLeaveRequestCommand : IRequest<int>
	{
        public CreateLeaveRequestDto LeaveRequestDto { get; set; }
    }
}
