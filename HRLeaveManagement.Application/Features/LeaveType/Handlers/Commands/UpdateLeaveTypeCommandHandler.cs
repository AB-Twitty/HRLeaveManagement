using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveType.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Requests.Commands;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveType.Handlers.Commands
{
	public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
			_mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateLeaveTypeDtoValidator(_leaveTypeRepository);

			var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

			if (!validationResult.IsValid)
				throw new ValidationException(validationResult);

			var leaveType = await _leaveTypeRepository.GetAsync(request.LeaveTypeDto.Id);
				
			_mapper.Map(request.LeaveTypeDto, leaveType);

			await _leaveTypeRepository.UpdateAsync(leaveType);

			return Unit.Value;
		}
	}
}
