using HRLeaveManagement.Application.DTOs.LeaveType;
using HRLeaveManagement.Application.Models;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Requests.Commands
{
	public class CreateLeaveTypeCommand : IRequest<BaseCommandResponse>
	{
        public CreateLeaveTypeDto LeaveTypeDto { get; set; }
    }
}
