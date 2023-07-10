using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.DTOs.LeaveType.Validators
{
	public class UpdateLeaveTypeDtoValidator : AbstractValidator<LeaveTypeDto>
	{
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public UpdateLeaveTypeDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            Include(new ILeaveTypeDtoValidator());

            RuleFor(p => p.Id)
                .NotNull().WithMessage("{PropertyName} must be present.")
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    return !(await _leaveTypeRepository.Exists(id));
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
