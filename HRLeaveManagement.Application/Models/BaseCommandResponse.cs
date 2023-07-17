using System.Collections.Generic;

namespace HRLeaveManagement.Application.Models
{
	public class BaseCommandResponse
	{
        public int Id { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public bool Success { get; set; }
    }
}
