
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

namespace Business.App.RepairActivitys
{
    public class RepairActivityEngine : EngineBase
    {
        public RepairActivityEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<RepairActivityOutput> GetByKeyAsync(int id)
        {
            RepairActivityOutput repairActivity = await _dbContext.RepairActivitys.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<RepairActivityOutput>(s))
                .FirstOrDefaultAsync();

            return repairActivity;
        }
        public async Task<TPagerResponse<RepairActivityOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.RepairActivitys.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<RepairActivity, RepairActivityOutput>(query, filterInput);
            return new TPagerResponse<RepairActivityOutput>(result.list, result.count);
        }
        public async Task<RepairActivityOutput> InsertOrUpdate(RepairActivityInput repairActivityInput)
        {
            RepairActivity repairActivity = null;

            if (repairActivityInput.Id == 0)
                repairActivity = new RepairActivity();
            else
                repairActivity = await _dbContext.RepairActivitys.FirstOrDefaultAsync(p => p.Id == repairActivityInput.Id);


            if (repairActivity == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<RepairActivityInput, RepairActivity>(repairActivityInput, repairActivity);

            if (repairActivity.Id == 0)
                _dbContext.Add(repairActivity);
            else
                _dbContext.Update(repairActivity);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<RepairActivityOutput>(repairActivity);
        }

        public async Task<RepairActivityOutput> Delete(int id)
        {
            RepairActivity repairActivity = await _dbContext.RepairActivitys.FirstOrDefaultAsync(r => r.Id == id);

            if (repairActivity == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(repairActivity);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<RepairActivityOutput>(repairActivity);
        }
    }
}