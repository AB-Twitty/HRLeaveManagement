using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using HRLeaveManagement.Application.Contracts.Infrastructure;
using HRLeaveManagement.Application.Models;
using System;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
	public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveRequestRepository _leaveRequestRepository;
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly IEmailSender _emailSender;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, 
			ILeaveTypeRepository leaveTypeRepository ,IMapper mapper, IEmailSender emailSender)
        {
            _leaveRequestRepository = leaveRequestRepository;
			_leaveTypeRepository = leaveTypeRepository;
			_mapper = mapper;
			_emailSender = emailSender;
        }

        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);

			var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);

			if (!validationResult.IsValid)
				throw new ValidationException(validationResult);

			var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request.LeaveRequestDto);

			leaveRequest = await _leaveRequestRepository.AddAsync(leaveRequest);

			var email = new Email
			{
				To = "employee@org.com",
				Body = $"Your leave request for {leaveRequest.StartDate:D} To {leaveRequest.EndDate:D} has been submitted successfully",
				Subject = "Leave Request Submitted"
			};

			try
			{
				await _emailSender.SendEmailAsync(email);
			} 
			catch (Exception ex)
			{

			}

			return leaveRequest.Id;
		}
	}
}
