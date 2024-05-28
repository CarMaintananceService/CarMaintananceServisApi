
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

namespace Business.App.StockCards
{
    public class StockCardEngine : EngineBase
    {
        public StockCardEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<StockCardOutput> GetByKeyAsync(int id)
        {
            StockCardOutput stockCard = await _dbContext.StockCards.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<StockCardOutput>(s))
                .FirstOrDefaultAsync();

            return stockCard;
        }
        public async Task<StockCardOutputSimple> GetByKeySimpleAsync(int id)
        {
            StockCardOutputSimple stockCard = await _dbContext.StockCards.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<StockCardOutputSimple>(s))
                .FirstOrDefaultAsync();

            return stockCard;
        }
        public async Task<TPagerResponse<StockCardOutputSimple>> Search(DxSearchInput searchInput)
        {
            var query = _dbContext.StockCards.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetSearchResult<StockCard, StockCardOutputSimple>(query, searchInput);
            return new TPagerResponse<StockCardOutputSimple>(result.list, result.count);
        }
        public async Task<TPagerResponse<StockCardOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.StockCards.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<StockCard, StockCardOutput>(query, filterInput);
            return new TPagerResponse<StockCardOutput>(result.list, result.count);
        }
        public async Task<StockCardOutput> InsertOrUpdate(StockCardInput stockCardInput)
        {
            StockCard stockCard = null;

            if (stockCardInput.Id == 0)
                stockCard = new StockCard();
            else
                stockCard = await _dbContext.StockCards.FirstOrDefaultAsync(p => p.Id == stockCardInput.Id);


            if (stockCard == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<StockCardInput, StockCard>(stockCardInput, stockCard);

            if (stockCard.Id == 0)
                _dbContext.Add(stockCard);
            else
                _dbContext.Update(stockCard);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<StockCardOutput>(stockCard);
        }

        public async Task<StockCardOutput> Delete(int id)
        {
            StockCard stockCard = await _dbContext.StockCards.FirstOrDefaultAsync(r => r.Id == id);

            if (stockCard == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(stockCard);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<StockCardOutput>(stockCard);
        }
    }
}