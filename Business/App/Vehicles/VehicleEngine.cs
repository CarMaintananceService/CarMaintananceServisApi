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

namespace Business.App.Vehicles
{
    public class VehicleEngine : EngineBase
    {
        public VehicleEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<VehicleOutput> GetByKeyAsync(int id)
        {
            VehicleOutput vehicle = await _dbContext.Vehicles.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<VehicleOutput>(s))
                .FirstOrDefaultAsync();

            return vehicle;
        }
        public async Task<VehicleOutputSimple> GetByKeySimpleAsync(int id)
        {
            VehicleOutputSimple vehicle = await _dbContext.Vehicles.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<VehicleOutputSimple>(s))
                .FirstOrDefaultAsync();

            return vehicle;
        }
        public async Task<TPagerResponse<VehicleOutputSimple>> Search(DxSearchInput searchInput)
        {
            var query = _dbContext.Vehicles.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetSearchResult<Vehicle, VehicleOutputSimple>(query, searchInput);
            return new TPagerResponse<VehicleOutputSimple>(result.list, result.count);
        }
        public async Task<TPagerResponse<VehicleOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.Vehicles.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<Vehicle, VehicleOutput>(query, filterInput);
            return new TPagerResponse<VehicleOutput>(result.list, result.count);
        }
        public async Task<VehicleOutput> InsertOrUpdate(VehicleInput vehicleInput)
        {
            Vehicle vehicle = null;

            if (vehicleInput.Id == 0)
                vehicle = new Vehicle();
            else
                vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(p => p.Id == vehicleInput.Id);


            if (vehicle == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<VehicleInput, Vehicle>(vehicleInput, vehicle);

            if (vehicle.Id == 0)
                _dbContext.Add(vehicle);
            else
                _dbContext.Update(vehicle);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<VehicleOutput>(vehicle);
        }

        public async Task<VehicleOutput> Delete(int id)
        {
            Vehicle vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(r => r.Id == id);

            if (vehicle == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(vehicle);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<VehicleOutput>(vehicle);
        }
    }
}