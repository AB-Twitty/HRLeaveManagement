using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
	public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
	{
        public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            Include(new ILeaveAllocationDtoValidator(leaveTypeRepository));
        }
    }
}
