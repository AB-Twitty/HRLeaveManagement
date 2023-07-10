using AutoMapper;
using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using HRLeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Handlers.Queries
{
	public class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest, LeaveAllocationDto>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public GetLeaveAllocationDetailRequestHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
			_mapper = mapper;
        }

        public async Task<LeaveAllocationDto> Handle(GetLeaveAllocationDetailRequest request, CancellationToken cancellationToken)
		{
			var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetailsAsync(request.Id);

			if(leaveAllocation == null) 
				throw new NotFoundException(nameof(leaveAllocation), request.Id);

			return _mapper.Map<LeaveAllocationDto>(leaveAllocation);
		}
	}
}
