using Business.Shared.Dx.Filter;
using Business.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Shared.Security.Users.Dtos;
using Business.Security.Users;
using System.ComponentModel;
using Business.Shared.Dx.Search;

namespace Api.Controllers.Security
{
	[Authorize]
	[Route("[controller]/[action]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		UserEngine _userEngine;
		public UserController(UserEngine userEngine)
		{
			_userEngine = userEngine;
		}


		
		[HttpGet]
		public async Task<TResponse<UserOutputSimple>> GetByKeySimple(int id)
		{
			TResponse<UserOutputSimple> response = null;

			try
			{
				var userOutput = await _userEngine.GetByKeySimpleAsync(id);
				response = new TResponse<UserOutputSimple>(userOutput);

			}
			catch (Exception ex)
			{
				response = new TResponse<UserOutputSimple>(ex.Message);
			}

			return response;
		}


		[HttpGet]
        public async Task<TResponse<UserOutput>> GetByKey(int id)
		{
			TResponse<UserOutput> response = null;

			try
			{
				var userOutput = await _userEngine.GetByKeyAsync(id);
				response = new TResponse<UserOutput>(userOutput);

			}
			catch (Exception ex)
			{
				response = new TResponse<UserOutput>(ex.Message);
			}

			return response;
        }


		[HttpPost]
		public async Task<TPagerResponse<UserOutputSimple>> Search(DxSearchInput dxSearchInput)
		{
			TPagerResponse<UserOutputSimple> response = null;

			try
			{
				response = await _userEngine.Search(dxSearchInput);

			}
			catch (Exception ex)
			{
				response = new TPagerResponse<UserOutputSimple>(ex.Message);
			}

			return response;
		}

		[HttpPost]
		public async Task<TPagerResponse<UserOutput>> Filter(DxFilterInput filterInput)
		{
			TPagerResponse<UserOutput> response = null;

			try
			{
				response = await _userEngine.FilterAsync(filterInput);

			}
			catch (Exception ex)
			{
				response = new TPagerResponse<UserOutput>(ex.Message);
			}

			return response;
		}


		[HttpPost]
		public async Task<TResponse<UserOutput>> InsertOrUpdate(UserInput userInput)
		{
			try
			{
				UserOutput result = await _userEngine.InsertOrUpdate(userInput);
				return new TResponse<UserOutput>(result);
			}
			catch (WarningException ex)
			{
				return new TResponse<UserOutput>().SetWarning(ex.Message);
			}
			catch (Exception ex)
			{
				return new TResponse<UserOutput>(ex.Message);
			}
		}


		[HttpDelete]
		public async Task<TResponse<UserOutput>> Delete(int id)
		{
			try
			{
				UserOutput result = await _userEngine.Delete(id);
				return new TResponse<UserOutput>(result);
			}
			catch (WarningException ex)
			{
				return new TResponse<UserOutput>().SetWarning(ex.Message);
			}
			catch (Exception ex)
			{
				return new TResponse<UserOutput>(ex.Message);
			}
		}


	}
}
