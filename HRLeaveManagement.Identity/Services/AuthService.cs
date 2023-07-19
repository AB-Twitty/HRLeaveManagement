using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Models.Identity;
using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Identity.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
			_userManager = userManager;
			_signInManager = signInManager;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);

			if (user == null)
				throw new Exception("Invalid Email or Password.");

			var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);

			if (!result.Succeeded)
				throw new Exception("Invalid Email or Password.");

			var token = await GenerateToken(user);

			var response = new AuthResponse
			{
				Id = user.Id,
				Email = user.Email,
				UserName = user.UserName,
				Token = new JwtSecurityTokenHandler().WriteToken(token)
			};

			return response;
		}

		public async Task<RegisterResponse> Register(RegisterRequest request)
		{
			var existingUserName = await _userManager.FindByNameAsync(request.UserName);

			if (existingUserName != null)
				throw new Exception($"Username {request.UserName} already exists.");

			var existingEmail = await _userManager.FindByEmailAsync(request.Email);

			if (existingEmail != null)
				throw new Exception($"Email {request.Email} already exists");

			var user = new ApplicationUser
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				UserName = request.UserName,
				EmailConfirmed = true
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "Employee");
				return new RegisterResponse { Id = user.Id };
			}

			throw new Exception($"{result.Errors}");
		}

		private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);

			var roles = await _userManager.GetRolesAsync(user);

			var roleClaims = new List<Claim>();

			foreach(var role in roles)
				roleClaims.Add(new Claim(ClaimTypes.Role, role));

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim("uid", user.Id)
			}.Union(userClaims).Union(roleClaims);

			var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var signingCredentials = new SigningCredentials(SymmetricSecurityKey , SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
				signingCredentials: signingCredentials
			);

			return token;
		}
	}
}
