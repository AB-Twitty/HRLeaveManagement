using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Http.Connections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Services
{
	public class LeaveTypeService : BaseHttpService, ILeaveTypeService
	{
		private readonly IClient _client;
		private readonly ILocalStorageService _storageService;
		private readonly IMapper _mapper;

		public LeaveTypeService(ILocalStorageService storageService, IClient client, IMapper mapper) : base(storageService, client)
		{
			_client = client;
			_storageService = storageService;
			_mapper = mapper;
		}

		public async Task<LeaveTypeVM> GetLeaveType(int id) =>
			_mapper.Map<LeaveTypeVM>(await _client.LeaveTypesGETAsync(id));

		public async Task<List<LeaveTypeVM>> GetLeaveTypes() =>
			_mapper.Map<List<LeaveTypeVM>>(await _client.LeaveTypesAllAsync());

		public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType)
		{
			try
			{
				var apiResponse = await _client.LeaveTypesPOSTAsync(_mapper.Map<CreateLeaveTypeDto>(leaveType));
				if (apiResponse.Success)
					return new Response<int>
					{
						Success = true,
						Data = apiResponse.Id
					};

				else
					return new Response<int>
					{
						Success = false,
						ValidationErrors = GetValidationErrors(apiResponse.Errors)
					};
			}
			catch (ApiException ex)
			{
				return ConvertApiExceptions<int>(ex);
			}
		}

		public async Task<Response<int>> DeleteLeaveType(LeaveTypeVM leaveType)
		{
			try
			{
				await _client.LeaveTypesDELETEAsync(leaveType.Id);
				return new Response<int> { Success = true };
			}
			catch (ApiException ex)
			{
				return ConvertApiExceptions<int>(ex);
			}
		}

		public async Task<Response<int>> UpdateLeaveType(LeaveTypeVM leaveType)
		{
			try
			{
				await _client.LeaveTypesPUTAsync(leaveType.Id.ToString(), _mapper.Map<LeaveTypeDto>(leaveType));
				return new Response<int> { Success = true };
			}
			catch (ApiException ex)
			{
				return ConvertApiExceptions<int>(ex);
			}
		}
	}
}
