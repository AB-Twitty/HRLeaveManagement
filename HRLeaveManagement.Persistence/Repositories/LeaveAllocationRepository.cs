using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.Persistence.Repositories
{
	public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
	{
		private readonly LeaveManagementDbContext _context;

        public LeaveAllocationRepository(LeaveManagementDbContext context) : base(context)
        {
			_context = context;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync()
		{
			return await _context.LeaveAllocations
				.Include(p => p.LeaveType)
				.ToListAsync();
		}

		public async Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id)
		{
			return await _context.LeaveAllocations
				.Include(p => p.LeaveType)
				.FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
