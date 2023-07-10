using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.DTOs.LeaveRequest.Validators
{
	public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
	{
        public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            Include(new ILeaveRequestDtoValidator(leaveTypeRepository));

            RuleFor(p => p.Cancelled)
                .NotNull().WithMessage("{PropertyName} must be present.");
        }
    }
}
