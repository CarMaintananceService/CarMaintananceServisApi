
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

namespace Business.App.Settings
{
    public class SettingEngine : EngineBase
    {
        public SettingEngine(
           IMemoryCache cache,
           IMapper objectMapper,
           UserDataProvider userDataProvider,
           CurrentServiceProvider serviceProvider
            ) : base(0, 0, cache, objectMapper, userDataProvider, serviceProvider)
        {

        }

        public async Task<SettingOutput> GetByKeyAsync(int id)
        {
            SettingOutput setting = await _dbContext.Settings.Where(u => u.Id == id)
                .Select(s => _objectMapper.Map<SettingOutput>(s))
                .FirstOrDefaultAsync();

            return setting;
        }

        public async Task<TPagerResponse<SettingOutput>> FilterAsync(DxFilterInput filterInput)
        {
            var query = _dbContext.Settings.AsNoTracking();
            var result = await new QueryHelper(_objectMapper).GetFilterResult<Setting, SettingOutput>(query, filterInput);
            return new TPagerResponse<SettingOutput>(result.list, result.count);
        }
        public async Task<SettingOutput> InsertOrUpdate(SettingInput settingInput)
        {
            Setting setting = null;

            if (settingInput.Id == 0)
                setting = new Setting();
            else
                setting = await _dbContext.Settings.FirstOrDefaultAsync(p => p.Id == settingInput.Id);


            if (setting == null)
                throw new BusinessException("Kayıt bulunamadı!");

            //mainProject objesinin içini dolduruyor
            _objectMapper.Map<SettingInput, Setting>(settingInput, setting);

            if (setting.Id == 0)
                _dbContext.Add(setting);
            else
                _dbContext.Update(setting);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<SettingOutput>(setting);
        }

        public async Task<SettingOutput> Delete(int id)
        {
            Setting setting = await _dbContext.Settings.FirstOrDefaultAsync(r => r.Id == id);

            if (setting == null)
                throw new BusinessException("Kayıt bulunamadı!");
            else
                _dbContext.Remove(setting);

            await _dbContext.SaveChangesAsync();

            return _objectMapper.Map<SettingOutput>(setting);
        }
    }
}