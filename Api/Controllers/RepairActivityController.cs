
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.RepairActivitys;
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
    public class RepairActivityController : ControllerBase
    {
        RepairActivityEngine _repairActivityEngine;
        public RepairActivityController(RepairActivityEngine repairActivityEngine)
        {
            _repairActivityEngine = repairActivityEngine;
        }
        [HttpGet]
        public async Task<TResponse<RepairActivityOutput>> GetByKey(int id)
        {
            TResponse<RepairActivityOutput> response = null;

            try
            {
                var repairActivityOutput = await _repairActivityEngine.GetByKeyAsync(id);
                response = new TResponse<RepairActivityOutput>(repairActivityOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<RepairActivityOutput>(ex);
            }

            return response;
        }

        [HttpPost]
        public async Task<TPagerResponse<RepairActivityOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<RepairActivityOutput> response = null;

            try
            {
                response = await _repairActivityEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<RepairActivityOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<RepairActivityOutput>> InsertOrUpdate(RepairActivityInput repairActivityInput)
        {
            try
            {
                var result = await _repairActivityEngine.InsertOrUpdate(repairActivityInput);
                return new TResponse<RepairActivityOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<RepairActivityOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<RepairActivityOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<RepairActivityOutput>> Delete(int id)
        {
            try
            {
                var result = await _repairActivityEngine.Delete(id);
                return new TResponse<RepairActivityOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<RepairActivityOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<RepairActivityOutput>(ex);
            }
        }
    }
}
