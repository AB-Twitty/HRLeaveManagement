using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Services
{
	public class AuthenticationService : BaseHttpService, Contracts.IAuthenticationService
    {
		private readonly ILocalStorageService _storageService;
		private readonly IClient _client;
		private readonly JwtSecurityTokenHandler _jwtHandler;
		private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(ILocalStorageService storageService, IClient client, IHttpContextAccessor httpContextAccessor) : base(storageService, client)
        {
			_client = client;
			_storageService = storageService;
			_httpContextAccessor = httpContextAccessor;
            _jwtHandler = new JwtSecurityTokenHandler();
        }

        public async Task<bool> Login(string email, string password)
		{
			try
			{
				var request = new AuthRequest { Email = email, Password = password };

				var response = await _client.LoginAsync(request);

				if (response.Token == string.Empty)
					return false;

				var tokenContent = _jwtHandler.ReadJwtToken(response.Token);

				var claims = ParseClaims(tokenContent);

				var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

				var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

				_storageService.SetStorageValue<string>("token", response.Token);

				return true;
			} 
			catch
			{
				return false;
			}
		}

		public async Task<bool> Register(string firstName, string lastName, string userName, string email, string password)
		{
			try
			{
				var request = new RegisterRequest
				{
					FirstName = firstName,
					LastName = lastName,
					UserName = userName,
					Email = email,
					Password = password
				};

				var response = await _client.RegisterAsync(request);

				if (string.IsNullOrEmpty(response.Id))
					return false;

				await Login(request.Email, request.Password);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task Logout()
		{
			_storageService.ClearStorage(new List<string> { "token" });
			await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}

		private List<Claim> ParseClaims(JwtSecurityToken token)
		{
			var claims = token.Claims.ToList();
			claims.Add(new Claim(ClaimTypes.Name, token.Subject));
			return claims;
		}
	}
}
