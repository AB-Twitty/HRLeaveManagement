using HRLeaveManagement.MVC.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace HRLeaveManagement.MVC.Services.Base
{
	public class BaseHttpService
	{
		private readonly ILocalStorageService _storageService;
		private IClient _client;

        public BaseHttpService(ILocalStorageService storageService, IClient client)
        {
            _client = client;
            _storageService = storageService;
        }

        protected string GetValidationErrors(ICollection<string> errors)
        {
            string result = "";

            foreach (var error in errors)
            {
                result += error + Environment.NewLine;
            }

            return result;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>
                {
                    Message = "Validation errors have occured.",
                    ValidationErrors = ex.Response,
                    Success = false
                };
            }
            else if (ex.StatusCode == 404)
            {
                return new Response<Guid>
                {
                    Message = "The requested item couldn't be found.",
                    Success = false
                };
            }
            else
            {
                return new Response<Guid>
                {
                    Message = "Something went wrong, please try again.",
                    Success = false
                };
            }
        }

        protected void AddBearerTooken()
        {
            if (_storageService.Exists("token"))
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _storageService.GetStorageValue<string>("token"));
        }
    }
}
