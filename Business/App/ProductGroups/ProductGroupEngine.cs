
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

namespace Business.App.ProductGroups
{
    public class ProductGroupEngine : EngineBase
    {
        public ProductGroupEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<ProductGroupOutput> GetByKeyAsync(int id)
        {
            ProductGroupOutput productGroup = await _dbContext.ProductGroups.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<ProductGroupOutput>(s))
                .FirstOrDefaultAsync();

            return productGroup;
        }
        public async Task<ProductGroupOutputSimple> GetByKeySimpleAsync(int id)
        {
            ProductGroupOutputSimple productGroup = await _dbContext.ProductGroups.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<ProductGroupOutputSimple>(s))
                .FirstOrDefaultAsync();

            return productGroup;
        }
        public async Task<TPagerResponse<ProductGroupOutputSimple>> Search(DxSearchInput searchInput)
        {
            var query = _dbContext.ProductGroups.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetSearchResult<ProductGroup, ProductGroupOutputSimple>(query, searchInput);
            return new TPagerResponse<ProductGroupOutputSimple>(result.list, result.count);
        }
        public async Task<TPagerResponse<ProductGroupOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.ProductGroups.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<ProductGroup, ProductGroupOutput>(query, filterInput);
            return new TPagerResponse<ProductGroupOutput>(result.list, result.count);
        }
        public async Task<ProductGroupOutput> InsertOrUpdate(ProductGroupInput productGroupInput)
        {
            ProductGroup productGroup = null;

            if (productGroupInput.Id == 0)
                productGroup = new ProductGroup();
            else
                productGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(p => p.Id == productGroupInput.Id);


            if (productGroup == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<ProductGroupInput, ProductGroup>(productGroupInput, productGroup);

            if (productGroup.Id == 0)
                _dbContext.Add(productGroup);
            else
                _dbContext.Update(productGroup);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<ProductGroupOutput>(productGroup);
        }

        public async Task<ProductGroupOutput> Delete(int id)
        {
            ProductGroup productGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(r => r.Id == id);

            if (productGroup == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(productGroup);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<ProductGroupOutput>(productGroup);
        }
    }
}