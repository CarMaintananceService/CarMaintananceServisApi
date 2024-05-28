
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.Firms;
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
    public class FirmController : ControllerBase
    {
        FirmEngine _firmEngine;
        public FirmController(FirmEngine firmEngine)
        {
            _firmEngine = firmEngine;
        }
        [HttpGet]
        public async Task<TResponse<FirmOutput>> GetByKey(int id)
        {
            TResponse<FirmOutput> response = null;

            try
            {
                var firmOutput = await _firmEngine.GetByKeyAsync(id);
                response = new TResponse<FirmOutput>(firmOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<FirmOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<FirmOutputSimple>> GetByKeySimple(int id)
        {
            TResponse<FirmOutputSimple> response = null;

            try
            {
                var firmOutput = await _firmEngine.GetByKeySimpleAsync(id);
                response = new TResponse<FirmOutputSimple>(firmOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<FirmOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<FirmOutputSimple>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<FirmOutputSimple> response = null;

            try
            {
                response = await _firmEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<FirmOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<FirmOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<FirmOutput> response = null;

            try
            {
                response = await _firmEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<FirmOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<FirmOutput>> InsertOrUpdate(FirmInput firmInput)
        {
            try
            {
                var result = await _firmEngine.InsertOrUpdate(firmInput);
                return new TResponse<FirmOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<FirmOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<FirmOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<FirmOutput>> Delete(int id)
        {
            try
            {
                var result = await _firmEngine.Delete(id);
                return new TResponse<FirmOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<FirmOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<FirmOutput>(ex);
            }
        }
    }
}
