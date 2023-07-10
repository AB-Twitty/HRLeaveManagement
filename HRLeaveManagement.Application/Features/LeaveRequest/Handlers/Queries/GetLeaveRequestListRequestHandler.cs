using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Requests.Queries;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Handlers.Queries
{
	public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveRequestRepository _leaveRequestRepository;

        public GetLeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
			_mapper = mapper;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
		{
			var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsAsync();
			return _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
		}
	}
}
