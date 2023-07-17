using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Contracts
{
	public interface ILeaveTypeService
	{
		Task<List<LeaveTypeVM>> GetLeaveTypes();
		
		Task<LeaveTypeVM> GetLeaveType(int id);

		Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM createLeave);

		Task<Response<int>> UpdateLeaveType(LeaveTypeVM leaveType);

		Task<Response<int>> DeleteLeaveType(LeaveTypeVM leaveType);
	}
}
