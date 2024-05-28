
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.OutSourceLabors;
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
    public class OutSourceLaborController : ControllerBase
    {
        OutSourceLaborEngine _outSourceLaborEngine;
        public OutSourceLaborController(OutSourceLaborEngine outSourceLaborEngine)
        {
            _outSourceLaborEngine = outSourceLaborEngine;
        }
        [HttpGet]
        public async Task<TResponse<OutSourceLaborOutput>> GetByKey(int id)
        {
            TResponse<OutSourceLaborOutput> response = null;

            try
            {
                var outSourceLaborOutput = await _outSourceLaborEngine.GetByKeyAsync(id);
                response = new TResponse<OutSourceLaborOutput>(outSourceLaborOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<OutSourceLaborOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<OutSourceLaborOutputSimple>> GetByKeySimple(int id)
        {
            TResponse<OutSourceLaborOutputSimple> response = null;

            try
            {
                var outSourceLaborOutput = await _outSourceLaborEngine.GetByKeySimpleAsync(id);
                response = new TResponse<OutSourceLaborOutputSimple>(outSourceLaborOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<OutSourceLaborOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<OutSourceLaborOutputSimple>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<OutSourceLaborOutputSimple> response = null;

            try
            {
                response = await _outSourceLaborEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<OutSourceLaborOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<OutSourceLaborOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<OutSourceLaborOutput> response = null;

            try
            {
                response = await _outSourceLaborEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<OutSourceLaborOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<OutSourceLaborOutput>> InsertOrUpdate(OutSourceLaborInput outSourceLaborInput)
        {
            try
            {
                var result = await _outSourceLaborEngine.InsertOrUpdate(outSourceLaborInput);
                return new TResponse<OutSourceLaborOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<OutSourceLaborOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<OutSourceLaborOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<OutSourceLaborOutput>> Delete(int id)
        {
            try
            {
                var result = await _outSourceLaborEngine.Delete(id);
                return new TResponse<OutSourceLaborOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<OutSourceLaborOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<OutSourceLaborOutput>(ex);
            }
        }
    }
}
