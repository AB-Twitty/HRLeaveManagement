using HRLeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Requests.Queries
{
	public class GetLeaveRequestDetailRequest : IRequest<LeaveRequestDto>
	{
        public int Id { get; set; }
    }
}
