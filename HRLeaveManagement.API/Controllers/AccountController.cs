﻿using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRLeaveManagement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
		public async Task<ActionResult<AuthResponse>> Login(AuthRequest request) =>
			Ok(await _authService.Login(request));

		[HttpPost("register")]
		public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request) =>
			Ok(await _authService.Register(request));
	}
}
