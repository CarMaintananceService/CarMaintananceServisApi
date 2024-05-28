
				using Core.App;
				using Core.Shared.Entities;
				using Business.Shared;
				using Business.Shared.Dx.Filter;
				using Business.Shared.Dx.Search;
				using Business.Shared.Languages;
				using Business.Shared.App.Unit.Dtos;
				
				using Dx.Common;
				using AutoMapper;
				using EntityFrameworkCore.Data;
				using AutoMapper.Internal.Mappers;
				using Microsoft.EntityFrameworkCore;
				using Microsoft.Extensions.Caching.Memory;
				namespace Business.App.Units
				{
				public class UnitEngine : EngineBase
					{
						public UnitEngine(
						   IMemoryCache cache,
						   ILanguageManager languageManager,
						   IMapper objectMapper,
						   UserDataProvider userDataProvider,
						   CurrentServiceProvider serviceProvider
							) : base(0, 0, cache, languageManager, objectMapper, userDataProvider, serviceProvider)
						{

						}
				
				public async Task<UnitOutput> GetByKeyAsync(int id)
				{
					UnitOutput unit = await _dbContext.Units.Where(u => u.Id == id)
						.Select(s => _objectMapper.Map<UnitOutput>(s))
						.FirstOrDefaultAsync();

					return unit;
				}
				public async Task<UnitOutputSimple> GetByKeySimpleAsync(int id)
				{
					UnitOutputSimple unit = await _dbContext.Units.Where(u => u.Id == id)
						.Select(s => _objectMapper.Map<UnitOutputSimple>(s))
						.FirstOrDefaultAsync();

					return unit;
				}
				public async Task<TPagerResponse<UnitOutputSimple>> Search(DxSearchInput searchInput)
				{
					var query = _dbContext.Units.AsNoTracking();
					var result = await new QueryHelper(_objectMapper).GetSearchResult<Unit, UnitOutputSimple>(query, searchInput);
					return new TPagerResponse<UnitOutputSimple>(result.list, result.count);
				}
				public async Task<TPagerResponse<UnitOutput>> FilterAsync(DxFilterInput filterInput)
				{
					var query = _dbContext.Units.AsNoTracking();
					var result = await new QueryHelper(_objectMapper).GetFilterResult<Unit, UnitOutput>(query, filterInput);
					return new TPagerResponse<UnitOutput>(result.list, result.count);
				}
				public async Task<UnitOutput> InsertOrUpdate(UnitInput unitInput)
				{
					Unit unit = null;

					if (unitInput.Id == 0)
						unit = new Unit();
					else
						unit = await _dbContext.Units.FirstOrDefaultAsync(p => p.Id == unitInput.Id);


					if (unit == null)
						throw new BusinessException("Kayıt bulunamadı!");

					//mainProject objesinin içini dolduruyor
					_objectMapper.Map<UnitInput, Unit>(unitInput, unit);

					if (unit.Id == 0)
						_dbContext.Add(unit);
					else
						_dbContext.Update(unit);

					await _dbContext.SaveChangesAsync();

					return _objectMapper.Map<UnitOutput>(unit);
				}

				public async Task<UnitOutput> Delete(int id)
				{
					Unit unit = await _dbContext.Units.FirstOrDefaultAsync(r => r.Id == id);

					if (unit == null)
						throw new BusinessException("Kayıt bulunamadı!");
					else
						_dbContext.Remove(unit);

					await _dbContext.SaveChangesAsync();

					return _objectMapper.Map<UnitOutput>(unit);
				}
			}
		}