
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

namespace Business.App.Manufacturers
{
    public class ManufacturerEngine : EngineBase
    {
        public ManufacturerEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<ManufacturerOutput> GetByKeyAsync(int id)
        {
            ManufacturerOutput manufacturer = await _dbContext.Manufacturers.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<ManufacturerOutput>(s))
                .FirstOrDefaultAsync();

            return manufacturer;
        }
        public async Task<ManufacturerOutputSimple> GetByKeySimpleAsync(int id)
        {
            ManufacturerOutputSimple manufacturer = await _dbContext.Manufacturers.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<ManufacturerOutputSimple>(s))
                .FirstOrDefaultAsync();

            return manufacturer;
        }
        public async Task<TPagerResponse<ManufacturerOutputSimple>> Search(DxSearchInput searchInput)
        {
            var query = _dbContext.Manufacturers.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetSearchResult<Manufacturer, ManufacturerOutputSimple>(query, searchInput);
            return new TPagerResponse<ManufacturerOutputSimple>(result.list, result.count);
        }
        public async Task<TPagerResponse<ManufacturerOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.Manufacturers.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<Manufacturer, ManufacturerOutput>(query, filterInput);
            return new TPagerResponse<ManufacturerOutput>(result.list, result.count);
        }
        public async Task<ManufacturerOutput> InsertOrUpdate(ManufacturerInput manufacturerInput)
        {
            Manufacturer manufacturer = null;

            if (manufacturerInput.Id == 0)
                manufacturer = new Manufacturer();
            else
                manufacturer = await _dbContext.Manufacturers.FirstOrDefaultAsync(p => p.Id == manufacturerInput.Id);


            if (manufacturer == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<ManufacturerInput, Manufacturer>(manufacturerInput, manufacturer);

            if (manufacturer.Id == 0)
                _dbContext.Add(manufacturer);
            else
                _dbContext.Update(manufacturer);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<ManufacturerOutput>(manufacturer);
        }

        public async Task<ManufacturerOutput> Delete(int id)
        {
            Manufacturer manufacturer = await _dbContext.Manufacturers.FirstOrDefaultAsync(r => r.Id == id);

            if (manufacturer == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(manufacturer);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<ManufacturerOutput>(manufacturer);
        }
    }
}