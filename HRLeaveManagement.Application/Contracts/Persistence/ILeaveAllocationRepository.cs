using HRLeaveManagement.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Contracts.Persistence
{
	public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
	{
		Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id);
		Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync();
	}
}
