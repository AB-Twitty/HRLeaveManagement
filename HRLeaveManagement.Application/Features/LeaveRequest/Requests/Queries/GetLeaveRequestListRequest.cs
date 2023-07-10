using HRLeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;
using System.Collections.Generic;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Requests.Queries
{
	public class GetLeaveRequestListRequest : IRequest<List<LeaveRequestListDto>>
	{
	}
}
