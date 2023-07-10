using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveRequest.Requests.Queries;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Handlers.Queries
{
	public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveRequestRepository _leaveRequestRepository;

        public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
			_mapper = mapper;
        }

        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
		{
			var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetailsAsync(request.Id);

			if (leaveRequest == null)
				throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);

			return _mapper.Map<LeaveRequestDto>(leaveRequest);
		}
	}
}
