using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveType.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Requests.Commands;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HRLeaveManagement.Application.Models;
using System.Linq;

namespace HRLeaveManagement.Application.Features.LeaveType.Handlers.Commands
{
	public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
			_mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateLeaveTypeDtoValidator();

			var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);

			if (!validationResult.IsValid)
			{
				return new BaseCommandResponse
				{
					Success = false,
					Message = "Creation Failed",
					Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
				};
			}

			var leaveType = _mapper.Map<Domain.LeaveType>(request.LeaveTypeDto);

			leaveType = await _leaveTypeRepository.AddAsync(leaveType);

			return new BaseCommandResponse
			{
				Id = leaveType.Id,
				Message = "Creation Succeeded",
				Success = true
			};
		}
	}
}
