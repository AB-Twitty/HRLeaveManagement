using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Identity.Configurations
{
	public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
		{
			builder.HasData(
				new IdentityUserRole<string>
				{
					UserId = "7a397aa0-3197-447b-80ab-a5c2f7c283da",
					RoleId = "49122c90-e808-4207-8652-94cb7c3f021d"
				},
				new IdentityUserRole<string>
				{
					UserId = "7a397aa1-3197-447b-80ab-a5c2f7c283da",
					RoleId = "49122c91-e808-4207-8652-94cb7c3f021d"
				});
		}
	}
}
