﻿using HRLeaveManagement.Application.Contracts.Identity;
using HRLeaveManagement.Application.Models.Identity;
using HRLeaveManagement.Identity.Models;
using HRLeaveManagement.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace HRLeaveManagement.Identity
{
	public static class IdentityServicesRegistration
	{
		public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

			services.AddDbContext<LeaveManagementIdentityDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("LeaveManagementIdentityConnectionString"))
			);

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<LeaveManagementIdentityDbContext>()
				.AddDefaultTokenProviders();

			services.AddTransient<IAuthService, AuthService>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = configuration["JwtSettings:Issuer"],
					ValidateIssuer = true,
					ValidateIssuerSigningKey = true,
					ValidAudience = configuration["JwtSettings:Audience"],
					ValidateAudience = true,
					ClockSkew = TimeSpan.Zero,
					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
				}
			);

			return services;
		}
	}
}
