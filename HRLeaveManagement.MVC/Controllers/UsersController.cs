using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Controllers
{
	public class UsersController : Controller
	{
		private readonly IAuthenticationService _authService;

        public UsersController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public IActionResult Login(string returnUrl) =>
			View(new LoginVM { ReturnUrl = returnUrl });

		[HttpPost]
		public async Task<IActionResult> Login(LoginVM login)
		{
			if (ModelState.IsValid)
			{
				login.ReturnUrl ??= Url.Content("~/");

				var isLoggedIn = await _authService.Login(login.Email, login.Password);
				if (isLoggedIn)
					return LocalRedirect(login.ReturnUrl);
			}

			ModelState.AddModelError("", "Login attempt failed, please try again.");
			return View(login);
		}

		[HttpGet]
		public IActionResult Register(string returnUrl) =>
			View(new RegisterVM { ReturnUrl = returnUrl });

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM register)
		{
			if (ModelState.IsValid)
			{
				register.ReturnUrl ??= Url.Content("~/");

				var isRegistered = await _authService.Register
				(
					firstName: register.FirstName,
					lastName: register.LastName,
					email: register.Email,
					userName: register.UserName,
					password: register.Password
				);

				if (isRegistered)
					return LocalRedirect(register.ReturnUrl);
			}

			return View(register);
		}

		[HttpPost]
		public async Task<IActionResult> Logout(string returnUrl)
		{
			await _authService.Logout();
			return LocalRedirect(returnUrl);
		}
			
	}
}
