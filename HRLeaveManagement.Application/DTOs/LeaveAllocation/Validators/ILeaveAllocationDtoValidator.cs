using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistence;
using System;

namespace HRLeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
	public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
	{
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.LeaveTypeId)
                .NotNull().WithMessage("{PropertyName} must be present.")
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    return !(await _leaveTypeRepository.Exists(id));
                }).WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.NumberOfDays)
                .NotNull().WithMessage("{PropertyName} must be present.")
                .GreaterThan(0).WithMessage("{PropertyName} must be at least 1.")
                .LessThan(100).WithMessage("{PropertyName} must not exceed {ComparisonValue}.");

            RuleFor(p => p.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}.");
        }
    }
}
