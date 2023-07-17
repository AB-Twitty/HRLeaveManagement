using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;
using HRLeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRLeaveManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LeaveAllocationsController : ControllerBase
	{
		private readonly IMediator _mediator;

        public LeaveAllocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

		// GET: api/<LeaveAllocationsController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<LeaveAllocationDto>>> Get() =>
			Ok(await _mediator.Send(new GetLeaveAllocationListRequest()));

		// GET api/<LeaveAllocationsController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<LeaveAllocationDto>> Get(int id) =>
			Ok(await _mediator.Send(new GetLeaveAllocationDetailRequest { Id = id }));

		// POST api/<LeaveAllocationsController>
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationDto createLeaveAllocationDto) =>
			Ok(await _mediator.Send(new CreateLeaveAllocationCommand { LeaveAllocationDto = createLeaveAllocationDto }));
		

		// PUT api/<LeaveAllocationsController>
		[HttpPut("{id}")]
		public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto updateLeaveAllocationDto)
		{
			await _mediator.Send(new UpdateLeaveAllocationCommand { LeaveAllocationDto =  updateLeaveAllocationDto });
			return NoContent();
		}

		// DELETE api/<LeaveAllocationsController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			await _mediator.Send(new DeleteLeaveAllocationCommand { Id = id });
			return NoContent();
		}
	}
}
