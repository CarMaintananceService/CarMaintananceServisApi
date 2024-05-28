
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

namespace Business.App.OutSourceLabors
{
    public class OutSourceLaborEngine : EngineBase
    {
        public OutSourceLaborEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<OutSourceLaborOutput> GetByKeyAsync(int id)
        {
            OutSourceLaborOutput outSourceLabor = await _dbContext.OutSourceLabors.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<OutSourceLaborOutput>(s))
                .FirstOrDefaultAsync();

            return outSourceLabor;
        }
        public async Task<OutSourceLaborOutputSimple> GetByKeySimpleAsync(int id)
        {
            OutSourceLaborOutputSimple outSourceLabor = await _dbContext.OutSourceLabors.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<OutSourceLaborOutputSimple>(s))
                .FirstOrDefaultAsync();

            return outSourceLabor;
        }
        public async Task<TPagerResponse<OutSourceLaborOutputSimple>> Search(DxSearchInput searchInput)
        {
            var query = _dbContext.OutSourceLabors.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetSearchResult<OutSourceLabor, OutSourceLaborOutputSimple>(query, searchInput);
            return new TPagerResponse<OutSourceLaborOutputSimple>(result.list, result.count);
        }
        public async Task<TPagerResponse<OutSourceLaborOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.OutSourceLabors.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<OutSourceLabor, OutSourceLaborOutput>(query, filterInput);
            return new TPagerResponse<OutSourceLaborOutput>(result.list, result.count);
        }
        public async Task<OutSourceLaborOutput> InsertOrUpdate(OutSourceLaborInput outSourceLaborInput)
        {
            OutSourceLabor outSourceLabor = null;

            if (outSourceLaborInput.Id == 0)
                outSourceLabor = new OutSourceLabor();
            else
                outSourceLabor = await _dbContext.OutSourceLabors.FirstOrDefaultAsync(p => p.Id == outSourceLaborInput.Id);


            if (outSourceLabor == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<OutSourceLaborInput, OutSourceLabor>(outSourceLaborInput, outSourceLabor);

            if (outSourceLabor.Id == 0)
                _dbContext.Add(outSourceLabor);
            else
                _dbContext.Update(outSourceLabor);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<OutSourceLaborOutput>(outSourceLabor);
        }

        public async Task<OutSourceLaborOutput> Delete(int id)
        {
            OutSourceLabor outSourceLabor = await _dbContext.OutSourceLabors.FirstOrDefaultAsync(r => r.Id == id);

            if (outSourceLabor == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(outSourceLabor);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<OutSourceLaborOutput>(outSourceLabor);
        }
    }
}