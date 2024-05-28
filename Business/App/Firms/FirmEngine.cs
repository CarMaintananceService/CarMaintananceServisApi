
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

namespace Business.App.Firms
{
    public class FirmEngine : EngineBase
    {
        public FirmEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<FirmOutput> GetByKeyAsync(int id)
        {
            FirmOutput firm = await _dbContext.Firms.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<FirmOutput>(s))
                .FirstOrDefaultAsync();

            return firm;
        }
        public async Task<FirmOutputSimple> GetByKeySimpleAsync(int id)
        {
            FirmOutputSimple firm = await _dbContext.Firms.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<FirmOutputSimple>(s))
                .FirstOrDefaultAsync();

            return firm;
        }
        public async Task<TPagerResponse<FirmOutputSimple>> Search(DxSearchInput searchInput)
        {
            var query = _dbContext.Firms.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetSearchResult<Firm, FirmOutputSimple>(query, searchInput);
            return new TPagerResponse<FirmOutputSimple>(result.list, result.count);
        }
        public async Task<TPagerResponse<FirmOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.Firms.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<Firm, FirmOutput>(query, filterInput);
            return new TPagerResponse<FirmOutput>(result.list, result.count);
        }
        public async Task<FirmOutput> InsertOrUpdate(FirmInput firmInput)
        {
            Firm firm = null;

            if (firmInput.Id == 0)
                firm = new Firm();
            else
                firm = await _dbContext.Firms.FirstOrDefaultAsync(p => p.Id == firmInput.Id);


            if (firm == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<FirmInput, Firm>(firmInput, firm);

            if (firm.Id == 0)
                _dbContext.Add(firm);
            else
                _dbContext.Update(firm);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<FirmOutput>(firm);
        }

        public async Task<FirmOutput> Delete(int id)
        {
            Firm firm = await _dbContext.Firms.FirstOrDefaultAsync(r => r.Id == id);

            if (firm == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(firm);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<FirmOutput>(firm);
        }
    }
}