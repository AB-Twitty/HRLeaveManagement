using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;
using System.Collections.Generic;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Queries
{
	public class GetLeaveAllocationListRequest : IRequest<List<LeaveAllocationDto>>
	{
	}
}
