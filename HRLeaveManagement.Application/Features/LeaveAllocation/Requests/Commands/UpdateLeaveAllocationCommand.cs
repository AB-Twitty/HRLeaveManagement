﻿using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Commands
{
	public class UpdateLeaveAllocationCommand : IRequest<Unit>
	{
        public UpdateLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
