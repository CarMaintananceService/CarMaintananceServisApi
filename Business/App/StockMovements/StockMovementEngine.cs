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

namespace Business.App.StockMovements
{
    public class StockMovementEngine : EngineBase
    {
        public StockMovementEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<StockMovementOutput> GetByKeyAsync(int id)
        {
            StockMovementOutput stockMovement = await _dbContext.StockMovements.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<StockMovementOutput>(s))
                .FirstOrDefaultAsync();

            return stockMovement;
        }
        
        public async Task<TPagerResponse<StockMovementOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.StockMovements.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<StockMovement, StockMovementOutput>(query, filterInput);
            return new TPagerResponse<StockMovementOutput>(result.list, result.count);
        }
        public async Task<StockMovementOutput> InsertOrUpdate(StockMovementInput stockMovementInput)
        {
            StockMovement stockMovement = null;

            if (stockMovementInput.Id == 0)
                stockMovement = new StockMovement();
            else
                stockMovement = await _dbContext.StockMovements.FirstOrDefaultAsync(p => p.Id == stockMovementInput.Id);


            if (stockMovement == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<StockMovementInput, StockMovement>(stockMovementInput, stockMovement);

            if (stockMovement.Id == 0)
                _dbContext.Add(stockMovement);
            else
                _dbContext.Update(stockMovement);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<StockMovementOutput>(stockMovement);
        }

        public async Task<StockMovementOutput> Delete(int id)
        {
            StockMovement stockMovement = await _dbContext.StockMovements.FirstOrDefaultAsync(r => r.Id == id);

            if (stockMovement == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(stockMovement);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<StockMovementOutput>(stockMovement);
        }
    }
}