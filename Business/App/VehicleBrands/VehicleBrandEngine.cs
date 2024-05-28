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

namespace Business.App.VehicleBrands
{
    public class VehicleBrandEngine : EngineBase
    {
        public VehicleBrandEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<VehicleBrandOutput> GetByKeyAsync(int id)
        {
            VehicleBrandOutput vehicleBrand = await _dbContext.VehicleBrands.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<VehicleBrandOutput>(s))
                .FirstOrDefaultAsync();

            return vehicleBrand;
        }
        public async Task<VehicleBrandOutputSimple> GetByKeySimpleAsync(int id)
        {
            VehicleBrandOutputSimple vehicleBrand = await _dbContext.VehicleBrands.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<VehicleBrandOutputSimple>(s))
                .FirstOrDefaultAsync();

            return vehicleBrand;
        }
        public async Task<TPagerResponse<VehicleBrandOutputSimple>> Search(DxSearchInput searchInput)
        {
            var query = _dbContext.VehicleBrands.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetSearchResult<VehicleBrand, VehicleBrandOutputSimple>(query, searchInput);
            return new TPagerResponse<VehicleBrandOutputSimple>(result.list, result.count);
        }
        public async Task<TPagerResponse<VehicleBrandOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.VehicleBrands.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<VehicleBrand, VehicleBrandOutput>(query, filterInput);
            return new TPagerResponse<VehicleBrandOutput>(result.list, result.count);
        }
        public async Task<VehicleBrandOutput> InsertOrUpdate(VehicleBrandInput vehicleBrandInput)
        {
            VehicleBrand vehicleBrand = null;

            if (vehicleBrandInput.Id == 0)
                vehicleBrand = new VehicleBrand();
            else
                vehicleBrand = await _dbContext.VehicleBrands.FirstOrDefaultAsync(p => p.Id == vehicleBrandInput.Id);


            if (vehicleBrand == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<VehicleBrandInput, VehicleBrand>(vehicleBrandInput, vehicleBrand);

            if (vehicleBrand.Id == 0)
                _dbContext.Add(vehicleBrand);
            else
                _dbContext.Update(vehicleBrand);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<VehicleBrandOutput>(vehicleBrand);
        }

        public async Task<VehicleBrandOutput> Delete(int id)
        {
            VehicleBrand vehicleBrand = await _dbContext.VehicleBrands.FirstOrDefaultAsync(r => r.Id == id);

            if (vehicleBrand == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(vehicleBrand);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<VehicleBrandOutput>(vehicleBrand);
        }
    }
}