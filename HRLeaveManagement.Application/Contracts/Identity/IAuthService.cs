using HRLeaveManagement.Application.Models.Identity;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Contracts.Identity
{
	public interface IAuthService
	{
		Task<AuthResponse> Login(AuthRequest request);

		Task<RegisterResponse> Register(RegisterRequest request);
	}
}
