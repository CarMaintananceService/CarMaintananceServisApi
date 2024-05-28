
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.CaseTypes;
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
    public class CaseTypeController : ControllerBase
    {
        CaseTypeEngine _caseTypeEngine;
        public CaseTypeController(CaseTypeEngine caseTypeEngine)
        {
            _caseTypeEngine = caseTypeEngine;
        }
        [HttpGet]
        public async Task<TResponse<CaseTypeOutput>> GetByKey(int id)
        {
            TResponse<CaseTypeOutput> response = null;

            try
            {
                var caseTypeOutput = await _caseTypeEngine.GetByKeyAsync(id);
                response = new TResponse<CaseTypeOutput>(caseTypeOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<CaseTypeOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<CaseTypeOutput>> GetByKeySimple(int id)
        {
            TResponse<CaseTypeOutput> response = null;

            try
            {
                var caseTypeOutput = await _caseTypeEngine.GetByKeySimpleAsync(id);
                response = new TResponse<CaseTypeOutput>(caseTypeOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<CaseTypeOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<CaseTypeOutput>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<CaseTypeOutput> response = null;

            try
            {
                response = await _caseTypeEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<CaseTypeOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<CaseTypeOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<CaseTypeOutput> response = null;

            try
            {
                response = await _caseTypeEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<CaseTypeOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<CaseTypeOutput>> InsertOrUpdate(CaseTypeInput caseTypeInput)
        {
            try
            {
                var result = await _caseTypeEngine.InsertOrUpdate(caseTypeInput);
                return new TResponse<CaseTypeOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<CaseTypeOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<CaseTypeOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<CaseTypeOutput>> Delete(int id)
        {
            try
            {
                var result = await _caseTypeEngine.Delete(id);
                return new TResponse<CaseTypeOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<CaseTypeOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<CaseTypeOutput>(ex);
            }
        }
    }
}
