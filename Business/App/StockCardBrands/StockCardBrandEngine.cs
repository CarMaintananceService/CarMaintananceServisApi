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

namespace Business.App.StockCardBrands
{
    public class StockCardBrandEngine : EngineBase
    {
        public StockCardBrandEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<StockCardBrandOutput> GetByKeyAsync(int id)
        {
            StockCardBrandOutput stockCardBrand = await _dbContext.StockCardBrands.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<StockCardBrandOutput>(s))
                .FirstOrDefaultAsync();

            return stockCardBrand;
        }
        public async Task<StockCardBrandOutputSimple> GetByKeySimpleAsync(int id)
        {
            StockCardBrandOutputSimple stockCardBrand = await _dbContext.StockCardBrands.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<StockCardBrandOutputSimple>(s))
                .FirstOrDefaultAsync();

            return stockCardBrand;
        }
        public async Task<TPagerResponse<StockCardBrandOutputSimple>> Search(DxSearchInput searchInput)
        {
            var query = _dbContext.StockCardBrands.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetSearchResult<StockCardBrand, StockCardBrandOutputSimple>(query, searchInput);
            return new TPagerResponse<StockCardBrandOutputSimple>(result.list, result.count);
        }
        public async Task<TPagerResponse<StockCardBrandOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.StockCardBrands.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<StockCardBrand, StockCardBrandOutput>(query, filterInput);
            return new TPagerResponse<StockCardBrandOutput>(result.list, result.count);
        }
        public async Task<StockCardBrandOutput> InsertOrUpdate(StockCardBrandInput stockCardBrandInput)
        {
            StockCardBrand stockCardBrand = null;

            if (stockCardBrandInput.Id == 0)
                stockCardBrand = new StockCardBrand();
            else
                stockCardBrand = await _dbContext.StockCardBrands.FirstOrDefaultAsync(p => p.Id == stockCardBrandInput.Id);


            if (stockCardBrand == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<StockCardBrandInput, StockCardBrand>(stockCardBrandInput, stockCardBrand);

            if (stockCardBrand.Id == 0)
                _dbContext.Add(stockCardBrand);
            else
                _dbContext.Update(stockCardBrand);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<StockCardBrandOutput>(stockCardBrand);
        }

        public async Task<StockCardBrandOutput> Delete(int id)
        {
            StockCardBrand stockCardBrand = await _dbContext.StockCardBrands.FirstOrDefaultAsync(r => r.Id == id);

            if (stockCardBrand == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(stockCardBrand);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<StockCardBrandOutput>(stockCardBrand);
        }
    }
}