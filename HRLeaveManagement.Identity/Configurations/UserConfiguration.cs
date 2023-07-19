using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Identity.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			var hasher = new PasswordHasher<ApplicationUser>();

			builder.HasData(
				new ApplicationUser
				{
					Id = "7a397aa0-3197-447b-80ab-a5c2f7c283da",
					FirstName = "System",
					LastName = "Admin",
					UserName = "Admin",
					NormalizedUserName = "ADMIN",
					Email = "admin@localhost.com",
					NormalizedEmail = "ADMIN@LOCALHOST.COM",
					EmailConfirmed = true,
					PasswordHash = hasher.HashPassword(null, "P@ssword")
				},
				new ApplicationUser
				{
					Id = "7a397aa1-3197-447b-80ab-a5c2f7c283da",
					FirstName = "System",
					LastName= "User",
					UserName= "User",
					NormalizedUserName = "USER",
					Email = "user@localhost.com",
					NormalizedEmail = "USER@LOCALHOST.COM",
					EmailConfirmed = true,
					PasswordHash = hasher.HashPassword(null, "P@ssword")
				});
		}
	}
}
