using HRLeaveManagement.Application.DTOs.LeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Requests.Commands;
using HRLeaveManagement.Application.Features.LeaveType.Requests.Queries;
using HRLeaveManagement.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRLeaveManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Administrator")]
	public class LeaveTypesController : ControllerBase
	{
		private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
			_mediator = mediator;
        }

        // GET: api/<LeaveTypeController>
        [HttpGet]
		public async Task<ActionResult<IEnumerable<LeaveTypeDto>>> Get() =>
			Ok(await _mediator.Send(new GetLeaveTypeListRequest()));


		// GET api/<LeaveTypeController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<LeaveTypeDto>> Get(int id) =>
			Ok(await _mediator.Send(new GetLeaveTypeDetailRequest { Id = id }));


		// POST api/<LeaveTypeController>
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveTypeDto createLeaveTypeDto) =>
			Ok(await _mediator.Send(new CreateLeaveTypeCommand { LeaveTypeDto = createLeaveTypeDto }));
		

		// PUT api/<LeaveTypeController>
		[HttpPut("{id}")]
		public async Task<ActionResult> Put([FromBody] LeaveTypeDto leaveTypeDto)
		{
			await _mediator.Send(new UpdateLeaveTypeCommand { LeaveTypeDto = leaveTypeDto });
			return NoContent();
		}

		// DELETE api/<LeaveTypeController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			await _mediator.Send(new DeleteLeaveTypeCommand { Id = id });
			return NoContent();
		}
	}
}
