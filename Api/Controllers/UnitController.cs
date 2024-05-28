
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.Units;
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
    public class UnitController : ControllerBase
    {
        UnitEngine _unitEngine;
        public UnitController(UnitEngine unitEngine)
        {
            _unitEngine = unitEngine;
        }
        [HttpGet]
        public async Task<TResponse<UnitOutput>> GetByKey(int id)
        {
            TResponse<UnitOutput> response = null;

            try
            {
                var unitOutput = await _unitEngine.GetByKeyAsync(id);
                response = new TResponse<UnitOutput>(unitOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<UnitOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<UnitOutputSimple>> GetByKeySimple(int id)
        {
            TResponse<UnitOutputSimple> response = null;

            try
            {
                var unitOutput = await _unitEngine.GetByKeySimpleAsync(id);
                response = new TResponse<UnitOutputSimple>(unitOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<UnitOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<UnitOutputSimple>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<UnitOutputSimple> response = null;

            try
            {
                response = await _unitEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<UnitOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<UnitOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<UnitOutput> response = null;

            try
            {
                response = await _unitEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<UnitOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<UnitOutput>> InsertOrUpdate(UnitInput unitInput)
        {
            try
            {
                var result = await _unitEngine.InsertOrUpdate(unitInput);
                return new TResponse<UnitOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<UnitOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<UnitOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<UnitOutput>> Delete(int id)
        {
            try
            {
                var result = await _unitEngine.Delete(id);
                return new TResponse<UnitOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<UnitOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<UnitOutput>(ex);
            }
        }
    }
}
