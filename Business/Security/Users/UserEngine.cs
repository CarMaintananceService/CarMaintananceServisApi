using AutoMapper;
using Business.Shared;
using Business.Shared.Dx.Filter;
using Business.Shared.Dx.Search;
using Business.Shared.Security.Users.Dtos;
using Core.Security;
using Dx.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Business.Security.Users
{
	public class UserEngine : EngineBase
	{

		private string fakePassword = "*****";
		public UserEngine(
		   IMemoryCache cache,
		   IMapper objectMapper,
		   UserDataProvider userDataProvider,
		   CurrentServiceProvider serviceProvider
			) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
		{

		}

		public async Task<UserOutputSimple> GetByKeySimpleAsync(int id)
		{
			UserOutputSimple user = await _dbContext.Users.AsNoTracking().Where(u => u.Id == id)
				.Select(s => _objectMapper.Map<UserOutputSimple>(s))
				.FirstOrDefaultAsync();

			return user;
		}

		public async Task<UserOutput> GetByKeyAsync(int id)
		{
			UserOutput user = await _dbContext.Users.AsNoTracking().Where(u => u.Id == id)
				.Select(s => _objectMapper.Map<UserOutput>(s))
				.FirstOrDefaultAsync();

			user.Password = fakePassword;
			return user;
		}

		public async Task<TPagerResponse<UserOutputSimple>> Search(DxSearchInput searchInput)
		{
			Expression<Func<User, bool>> searchExpression = null;

			if (!string.IsNullOrEmpty(searchInput.SearchValue))
				searchExpression = e => (e.Name + " " + e.Surname).Contains(searchInput.SearchValue);

			var query = _dbContext.Users
				.AsNoTracking();
			
			var result = await new QueryHelper(_objectMapper).GetSearchResult<User, UserOutputSimple>(query, new string[] { "Name", "Surname" }, searchInput.Skip, searchInput.Take, searchExpression);
			return new TPagerResponse<UserOutputSimple>(result.list, result.count);
		}

		public async Task<TPagerResponse<UserOutput>> FilterAsync(DxFilterInput filterInput)
		{
			var query = _dbContext.Users.AsNoTracking();

			var result = await new QueryHelper(_objectMapper).GetFilterResult<User, UserOutput>(query, filterInput);
			result.list.ForEach(r =>
			{
				r.Password = fakePassword;
			});
			return new TPagerResponse<UserOutput>(result.list, result.count);
		}


		public async Task<UserOutput> InsertOrUpdate(UserInput userInput)
		{
			User user = null;

			if (userInput.Id == 0)
			{
				if (userInput.Password == fakePassword)
					throw new BusinessException("Geçersiz parola!");

				user = new User();
			}
			else
			{
				user = await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == userInput.Id);

				if (user == null)
					throw new BusinessException("Kayıt bulunamadı!");

				if (userInput.Password == fakePassword)
					userInput.Password = user.Password;
			}

			_objectMapper.Map<UserInput, User>(userInput, user);

			if (user.Id == 0)
				_dbContext.Add(user);
			else
				_dbContext.Update(user);

			await _dbContext.SaveChangesAsync();

			return _objectMapper.Map<UserOutput>(user);
		}

		public async Task<UserOutput> Delete(int id)
		{
			User user = await _dbContext.Users.FirstOrDefaultAsync(r => r.Id == id);

			if (user == null)
				throw new BusinessException("Kayıt bulunamadı!");
			else
				_dbContext.Remove(user);

			await _dbContext.SaveChangesAsync();

			return _objectMapper.Map<UserOutput>(user);
		}


	}
}
