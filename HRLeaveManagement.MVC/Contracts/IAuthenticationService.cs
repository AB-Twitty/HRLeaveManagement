using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Contracts
{
	public interface IAuthenticationService
	{
		Task<bool> Login(string email, string password);

		Task<bool> Register(string firstName, string lastName, string userName, string email, string password);

		Task Logout();
	}
}
