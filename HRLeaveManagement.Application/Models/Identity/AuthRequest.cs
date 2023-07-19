using System.ComponentModel.DataAnnotations;

namespace HRLeaveManagement.Application.Models.Identity
{
	public class AuthRequest
	{
		[Required]
        public string Email { get; set; }

		[Required] 
		public string Password { get; set; }
    }
}
