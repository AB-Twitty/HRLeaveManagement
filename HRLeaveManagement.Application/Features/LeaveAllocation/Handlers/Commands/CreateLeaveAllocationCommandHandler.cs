using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Handlers.Commands
{
	public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;
		private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, 
			ILeaveTypeRepository leaveTypeRepository ,IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
			_leaveTypeRepository = leaveTypeRepository;
			_mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepository);

			var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

			if (!validationResult.IsValid)
				throw new ValidationException(validationResult);

			var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request.LeaveAllocationDto);

			leaveAllocation = await _leaveAllocationRepository.AddAsync(leaveAllocation);

			return leaveAllocation.Id;
		}
	}
}
