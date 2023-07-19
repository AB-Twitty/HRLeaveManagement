using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Identity.Configurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
	{
		public void Configure(EntityTypeBuilder<IdentityRole> builder)
		{
			builder.HasData(
				new IdentityRole
				{
					Id = "49122c90-e808-4207-8652-94cb7c3f021d",
					Name = "Administrator",
					NormalizedName = "ADMINISTRATOR"
				},
				new IdentityRole
				{
					Id = "49122c91-e808-4207-8652-94cb7c3f021d",
					Name = "Employee",
					NormalizedName = "EMPLOYEE"
				});
		}
	}
}
