using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Persistence.Repositories
{
	public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
	{
		private readonly LeaveManagementDbContext _context;

        public LeaveTypeRepository(LeaveManagementDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
