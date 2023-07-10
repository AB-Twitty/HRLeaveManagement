using HRLeaveManagement.Application.DTOs.Common;
using HRLeaveManagement.Application.DTOs.LeaveType;
using System;

namespace HRLeaveManagement.Application.DTOs.LeaveRequest
{
	public class LeaveRequestListDto : BaseDto
	{
		public LeaveTypeDto LeaveType { get; set; }
		public DateTime DateRequested { get; set; }
		public bool? Approved { get; set; }

	}
}
