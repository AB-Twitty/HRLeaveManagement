using HRLeaveManagement.Application.Contracts.Infrastructure;
using HRLeaveManagement.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;

namespace HRLeaveManagement.Infrastructure.Mail
{
	public class EmailSender : IEmailSender
	{
		private EmailSettings _emailSettings { get; }

        public EmailSender(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task<bool> SendEmailAsync(Email email)
		{
			var client = new SendGridClient(_emailSettings.ApiKey);

			var To = new EmailAddress(email.To);

			var From = new EmailAddress
			{
				Email = _emailSettings.FromAddress,
				Name = _emailSettings.FromName
			};

			var message = MailHelper.CreateSingleEmail(From, To, email.Subject, email.Body, email.Body);

			var response = await client.SendEmailAsync(message);

			return response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted;
		}
	}
}
