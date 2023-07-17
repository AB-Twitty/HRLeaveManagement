using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HRLeaveManagement.Application.Features.LeaveRequest.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRLeaveManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LeaveRequestsController : ControllerBase
	{
		private readonly IMediator _mediator;

        public LeaveRequestsController(IMediator mediator)
        {
			_mediator = mediator;
        }

		// GET: api/<LeaveRequestsController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<LeaveRequestListDto>>> Get() =>
			Ok(await _mediator.Send(new GetLeaveRequestListRequest()));

		// GET api/<LeaveRequestsController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<LeaveRequestDto>> Get(int id) =>
			Ok(await _mediator.Send(new GetLeaveRequestDetailRequest { Id = id }));

		// POST api/<LeaveRequestsController>
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CreateLeaveRequestDto createLeaveRequestDto) =>
			Ok(await _mediator.Send(new CreateLeaveRequestCommand { LeaveRequestDto = createLeaveRequestDto }));

		// PUT api/<LeaveRequestsController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto updateLeaveRequestDto)
		{
			await _mediator.Send(new UpdateLeaveRequestCommand { Id = id, UpdateLeaveRequestDto = updateLeaveRequestDto });
			return NoContent();
		}

		// PUT api/<LeaveRequestsController>/changeApproval/5
		[HttpPut("changeApproval/{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] ChangeLeaveRequestApprovalDto changeLeaveRequestApprovalDto)
		{
			await _mediator.Send(new UpdateLeaveRequestCommand { Id = id, changeLeaveRequestApprovalDto = changeLeaveRequestApprovalDto });
			return NoContent();
		}

		// DELETE api/<LeaveRequestsController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			await _mediator.Send(new DeleteLeaveRequestCommand { Id = id });
			return NoContent();
		}
	}
}
