using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class LeaveTypesController : Controller
	{
		private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypesController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

		// GET: LeaveTypesController
		public async Task<ActionResult> Index()
		{
			var leaveTypes = await _leaveTypeService.GetLeaveTypes();
			return View(leaveTypes);
		}

		// GET: LeaveTypesController/Details/5
		public async Task<ActionResult> Details(int id) =>
			View(await _leaveTypeService.GetLeaveType(id));

		// GET: LeaveTypesController/Create
		public async Task<ActionResult> Create() =>
			View();

		// POST: LeaveTypesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(CreateLeaveTypeVM leaveType)
		{
			try
			{
				var response = await _leaveTypeService.CreateLeaveType(leaveType);
				if (response.Success)
					return RedirectToAction("Index");

				ModelState.AddModelError("", response.ValidationErrors);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
			}

			return View();
		}

		// GET: LeaveTypesController/Edit/5
		public async Task<ActionResult> Edit(int id) =>
			View(await _leaveTypeService.GetLeaveType(id));

		// POST: LeaveTypesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(LeaveTypeVM leaveType)
		{
			try
			{
				var response = await _leaveTypeService.UpdateLeaveType(leaveType);
				if (response.Success)
					return RedirectToAction("Index");

				ModelState.AddModelError("", response.ValidationErrors);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
			}

			return View();
		}

		// POST: LeaveTypesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				var response = await _leaveTypeService.DeleteLeaveType(await _leaveTypeService.GetLeaveType(id));
				if (response.Success)
					return RedirectToAction("Index");

			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
			}

			return BadRequest();
		}
	}
}
