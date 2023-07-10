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
	public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;
		private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, 
			ILeaveTypeRepository leaveTypeRepository ,IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
			_leaveTypeRepository = leaveTypeRepository;
			_mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateLeaveAllocationDtoValidator(_leaveTypeRepository, _leaveAllocationRepository);

			var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

			if (!validationResult.IsValid)
				throw new ValidationException(validationResult);

			var leaveAllocation = await _leaveAllocationRepository.GetAsync(request.LeaveAllocationDto.Id);
				
			_mapper.Map(request.LeaveAllocationDto, leaveAllocation);

			await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

			return Unit.Value;
		}
	}
}
