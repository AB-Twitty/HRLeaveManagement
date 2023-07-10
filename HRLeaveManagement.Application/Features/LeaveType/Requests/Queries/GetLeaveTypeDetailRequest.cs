using HRLeaveManagement.Application.DTOs.LeaveType;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Requests.Queries
{
	public class GetLeaveTypeDetailRequest : IRequest<LeaveTypeDto>
	{
        public int Id { get; set; }
    }
}
