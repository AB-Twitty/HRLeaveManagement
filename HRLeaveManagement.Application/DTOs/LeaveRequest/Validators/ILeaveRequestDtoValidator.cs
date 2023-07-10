using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;

namespace HRLeaveManagement.Application.DTOs.LeaveRequest.Validators
{
	public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
	{
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.StartDate)
                .LessThan(p => p.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}.");

            RuleFor(p => p.EndDate)
                .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}.");

            RuleFor(p => p.LeaveTypeId)
                .NotNull().WithMessage("{PrpertyName} must be present.")
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    return !(await _leaveTypeRepository.Exists(id));
                }).WithMessage("{PropertyName} does not exist.");
        }
    }
}
