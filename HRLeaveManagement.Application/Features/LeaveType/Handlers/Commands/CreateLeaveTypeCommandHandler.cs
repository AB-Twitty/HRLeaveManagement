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
	public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
			_mapper = mapper;
        }

        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateLeaveTypeDtoValidator();

			var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

			if (!validationResult.IsValid)
				throw new ValidationException(validationResult);

			var leaveType = _mapper.Map<Domain.LeaveType>(request.LeaveTypeDto);

			leaveType = await _leaveTypeRepository.AddAsync(leaveType);

			return leaveType.Id;
		}
	}
}
