using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
	public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveRequestRepository _leaveRequestRepository;
		private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, 
			ILeaveTypeRepository leaveTypeRepository ,IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
			_leaveTypeRepository = leaveTypeRepository;
			_mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
		{
			var leaveRequest = await _leaveRequestRepository.GetAsync(request.Id);

			if (request.UpdateLeaveRequestDto != null)
			{
				var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository);

				var validationResult = await validator.ValidateAsync(request.UpdateLeaveRequestDto);

				if (!validationResult.IsValid)
					throw new ValidationException(validationResult);

				_mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);

				await _leaveRequestRepository.UpdateAsync(leaveRequest);
			}
			else if (request.changeLeaveRequestApprovalDto != null)
			{
				await _leaveRequestRepository.ChangeLeaveRequestApprovalAsync(leaveRequest, request.changeLeaveRequestApprovalDto.Approved);
			}

			return Unit.Value;
		}
	}
}
