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

namespace Business.App.StockCardUnits
{
    public class StockCardUnitEngine : EngineBase
    {
        public StockCardUnitEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<StockCardUnitOutput> GetByKeyAsync(int id)
        {
            StockCardUnitOutput stockCardUnit = await _dbContext.StockCardUnits.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<StockCardUnitOutput>(s))
                .FirstOrDefaultAsync();

            return stockCardUnit;
        }
        public async Task<StockCardUnitOutputSimple> GetByKeySimpleAsync(int id)
        {
            StockCardUnitOutputSimple stockCardUnit = await _dbContext.StockCardUnits.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<StockCardUnitOutputSimple>(s))
                .FirstOrDefaultAsync();

            return stockCardUnit;
        }
        public async Task<TPagerResponse<StockCardUnitOutputSimple>> Search(DxSearchInput searchInput)
        {
            var query = _dbContext.StockCardUnits.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetSearchResult<StockCardUnit, StockCardUnitOutputSimple>(query, searchInput);
            return new TPagerResponse<StockCardUnitOutputSimple>(result.list, result.count);
        }
        public async Task<TPagerResponse<StockCardUnitOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.StockCardUnits.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<StockCardUnit, StockCardUnitOutput>(query, filterInput);
            return new TPagerResponse<StockCardUnitOutput>(result.list, result.count);
        }
        public async Task<StockCardUnitOutput> InsertOrUpdate(StockCardUnitInput stockCardUnitInput)
        {
            StockCardUnit stockCardUnit = null;

            if (stockCardUnitInput.Id == 0)
                stockCardUnit = new StockCardUnit();
            else
                stockCardUnit = await _dbContext.StockCardUnits.FirstOrDefaultAsync(p => p.Id == stockCardUnitInput.Id);


            if (stockCardUnit == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<StockCardUnitInput, StockCardUnit>(stockCardUnitInput, stockCardUnit);

            if (stockCardUnit.Id == 0)
                _dbContext.Add(stockCardUnit);
            else
                _dbContext.Update(stockCardUnit);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<StockCardUnitOutput>(stockCardUnit);
        }

        public async Task<StockCardUnitOutput> Delete(int id)
        {
            StockCardUnit stockCardUnit = await _dbContext.StockCardUnits.FirstOrDefaultAsync(r => r.Id == id);

            if (stockCardUnit == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(stockCardUnit);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<StockCardUnitOutput>(stockCardUnit);
        }
    }
}