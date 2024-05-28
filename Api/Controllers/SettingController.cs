
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.Settings;
using Business.Shared.Dx.Search;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Cars.Models.Domain;

namespace Api.Controllers.App
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        SettingEngine _settingEngine;
        public SettingController(SettingEngine settingEngine)
        {
            _settingEngine = settingEngine;
        }
        [HttpGet]
        public async Task<TResponse<SettingOutput>> GetByKey(int id)
        {
            TResponse<SettingOutput> response = null;

            try
            {
                var settingOutput = await _settingEngine.GetByKeyAsync(id);
                response = new TResponse<SettingOutput>(settingOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<SettingOutput>(ex);
            }

            return response;
        }

        [HttpPost]
        public async Task<TPagerResponse<SettingOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<SettingOutput> response = null;

            try
            {
                response = await _settingEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<SettingOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<SettingOutput>> InsertOrUpdate(SettingInput settingInput)
        {
            try
            {
                var result = await _settingEngine.InsertOrUpdate(settingInput);
                return new TResponse<SettingOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<SettingOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<SettingOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<SettingOutput>> Delete(int id)
        {
            try
            {
                var result = await _settingEngine.Delete(id);
                return new TResponse<SettingOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<SettingOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<SettingOutput>(ex);
            }
        }
    }
}
