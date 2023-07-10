using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
	public class UpdateLeaveAllocationDtoValidator : AbstractValidator<UpdateLeaveAllocationDto>
	{
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        public UpdateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;

            Include(new ILeaveAllocationDtoValidator(leaveTypeRepository));

            RuleFor(p => p.Id)
                .NotNull().WithMessage("{PropertyName} must be present.")
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    return !(await _leaveAllocationRepository.Exists(id));
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
