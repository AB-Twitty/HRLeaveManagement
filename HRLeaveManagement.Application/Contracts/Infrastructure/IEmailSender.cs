using HRLeaveManagement.Application.Models;
using System.Threading.Tasks;

namespace HRLeaveManagement.Application.Contracts.Infrastructure
{
	public interface IEmailSender
	{
		Task<bool> SendEmailAsync(Email email);
	}
}
