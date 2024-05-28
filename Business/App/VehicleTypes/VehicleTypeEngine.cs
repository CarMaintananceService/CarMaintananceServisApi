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

namespace Business.App.VehicleTypes
{
    public class VehicleTypeEngine : EngineBase
    {
        public VehicleTypeEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<VehicleTypeOutput> GetByKeyAsync(int id)
        {
            VehicleTypeOutput vehicleType = await _dbContext.VehicleTypes.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<VehicleTypeOutput>(s))
                .FirstOrDefaultAsync();

            return vehicleType;
        }
        public async Task<VehicleTypeOutputSimple> GetByKeySimpleAsync(int id)
        {
            VehicleTypeOutputSimple vehicleType = await _dbContext.VehicleTypes.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<VehicleTypeOutputSimple>(s))
                .FirstOrDefaultAsync();

            return vehicleType;
        }
        public async Task<TPagerResponse<VehicleTypeOutputSimple>> Search(DxSearchInput searchInput)
        {
            var query = _dbContext.VehicleTypes.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetSearchResult<VehicleType, VehicleTypeOutputSimple>(query, searchInput);
            return new TPagerResponse<VehicleTypeOutputSimple>(result.list, result.count);
        }
        public async Task<TPagerResponse<VehicleTypeOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.VehicleTypes.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<VehicleType, VehicleTypeOutput>(query, filterInput);
            return new TPagerResponse<VehicleTypeOutput>(result.list, result.count);
        }
        public async Task<VehicleTypeOutput> InsertOrUpdate(VehicleTypeInput vehicleTypeInput)
        {
            VehicleType vehicleType = null;

            if (vehicleTypeInput.Id == 0)
                vehicleType = new VehicleType();
            else
                vehicleType = await _dbContext.VehicleTypes.FirstOrDefaultAsync(p => p.Id == vehicleTypeInput.Id);


            if (vehicleType == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<VehicleTypeInput, VehicleType>(vehicleTypeInput, vehicleType);

            if (vehicleType.Id == 0)
                _dbContext.Add(vehicleType);
            else
                _dbContext.Update(vehicleType);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<VehicleTypeOutput>(vehicleType);
        }

        public async Task<VehicleTypeOutput> Delete(int id)
        {
            VehicleType vehicleType = await _dbContext.VehicleTypes.FirstOrDefaultAsync(r => r.Id == id);

            if (vehicleType == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(vehicleType);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<VehicleTypeOutput>(vehicleType);
        }
    }
}