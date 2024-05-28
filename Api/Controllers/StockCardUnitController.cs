
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.StockCardUnits;
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
    public class StockCardUnitController : ControllerBase
    {
        StockCardUnitEngine _stockCardUnitEngine;
        public StockCardUnitController(StockCardUnitEngine stockCardUnitEngine)
        {
            _stockCardUnitEngine = stockCardUnitEngine;
        }
        [HttpGet]
        public async Task<TResponse<StockCardUnitOutput>> GetByKey(int id)
        {
            TResponse<StockCardUnitOutput> response = null;

            try
            {
                var stockCardUnitOutput = await _stockCardUnitEngine.GetByKeyAsync(id);
                response = new TResponse<StockCardUnitOutput>(stockCardUnitOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<StockCardUnitOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<StockCardUnitOutputSimple>> GetByKeySimple(int id)
        {
            TResponse<StockCardUnitOutputSimple> response = null;

            try
            {
                var stockCardUnitOutput = await _stockCardUnitEngine.GetByKeySimpleAsync(id);
                response = new TResponse<StockCardUnitOutputSimple>(stockCardUnitOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<StockCardUnitOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<StockCardUnitOutputSimple>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<StockCardUnitOutputSimple> response = null;

            try
            {
                response = await _stockCardUnitEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<StockCardUnitOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<StockCardUnitOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<StockCardUnitOutput> response = null;

            try
            {
                response = await _stockCardUnitEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<StockCardUnitOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<StockCardUnitOutput>> InsertOrUpdate(StockCardUnitInput stockCardUnitInput)
        {
            try
            {
                var result = await _stockCardUnitEngine.InsertOrUpdate(stockCardUnitInput);
                return new TResponse<StockCardUnitOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<StockCardUnitOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<StockCardUnitOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<StockCardUnitOutput>> Delete(int id)
        {
            try
            {
                var result = await _stockCardUnitEngine.Delete(id);
                return new TResponse<StockCardUnitOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<StockCardUnitOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<StockCardUnitOutput>(ex);
            }
        }
    }
}
