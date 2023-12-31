﻿using HRLeaveManagement.Application.DTOs.LeaveType;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Requests.Commands
{
	public class UpdateLeaveTypeCommand : IRequest<Unit>
	{
        public LeaveTypeDto LeaveTypeDto { get; set; }
    }
}
