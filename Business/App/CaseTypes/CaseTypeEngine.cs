
using Core.Shared.Entities;
using Business.Shared;
using Business.Shared.Dx.Filter;
using Business.Shared.Dx.Search;

using Dx.Common;
using AutoMapper;
using EntityFrameworkCore.Data;
using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Cars.Models.Domain;

namespace Business.App.CaseTypes
{
    public class CaseTypeEngine : EngineBase
    {
        public CaseTypeEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<CaseTypeOutput> GetByKeyAsync(int id)
        {
            CaseTypeOutput caseType = await _dbContext.CaseTypes.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<CaseTypeOutput>(s))
                .FirstOrDefaultAsync();

            return caseType;
        }
        public async Task<CaseTypeOutput> GetByKeySimpleAsync(int id)
        {
            CaseTypeOutput caseType = await _dbContext.CaseTypes.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<CaseTypeOutput>(s))
                .FirstOrDefaultAsync();

            return caseType;
        }
        public async Task<TPagerResponse<CaseTypeOutput>> Search(DxSearchInput searchInput)
        {
            var query = _dbContext.CaseTypes.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetSearchResult<CaseType, CaseTypeOutput>(query, searchInput);
            return new TPagerResponse<CaseTypeOutput>(result.list, result.count);
        }
        public async Task<TPagerResponse<CaseTypeOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.CaseTypes.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<CaseType, CaseTypeOutput>(query, filterInput);
            return new TPagerResponse<CaseTypeOutput>(result.list, result.count);
        }
        public async Task<CaseTypeOutput> InsertOrUpdate(CaseTypeInput caseTypeInput)
        {
            CaseType caseType = null;

            if (caseTypeInput.Id == 0)
                caseType = new CaseType();
            else
                caseType = await _dbContext.CaseTypes.FirstOrDefaultAsync(p => p.Id == caseTypeInput.Id);


            if (caseType == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<CaseTypeInput, CaseType>(caseTypeInput, caseType);

            if (caseType.Id == 0)
                _dbContext.Add(caseType);
            else
                _dbContext.Update(caseType);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<CaseTypeOutput>(caseType);
        }

        public async Task<CaseTypeOutput> Delete(int id)
        {
            CaseType caseType = await _dbContext.CaseTypes.FirstOrDefaultAsync(r => r.Id == id);

            if (caseType == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(caseType);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<CaseTypeOutput>(caseType);
        }
    }
}