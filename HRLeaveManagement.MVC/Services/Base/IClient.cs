using System.Net.Http;

namespace HRLeaveManagement.MVC.Services.Base
{
	public partial interface IClient
	{
		public HttpClient HttpClient { get; }
	}
}
