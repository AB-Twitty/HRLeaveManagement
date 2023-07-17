namespace HRLeaveManagement.Application.Models.Identity
{
	public class AuthResponse
	{
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
