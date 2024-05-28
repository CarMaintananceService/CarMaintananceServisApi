
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.StockCardBrands;
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
    public class StockCardBrandController : ControllerBase
    {
        StockCardBrandEngine _stockCardBrandEngine;
        public StockCardBrandController(StockCardBrandEngine stockCardBrandEngine)
        {
            _stockCardBrandEngine = stockCardBrandEngine;
        }
        [HttpGet]
        public async Task<TResponse<StockCardBrandOutput>> GetByKey(int id)
        {
            TResponse<StockCardBrandOutput> response = null;

            try
            {
                var stockCardBrandOutput = await _stockCardBrandEngine.GetByKeyAsync(id);
                response = new TResponse<StockCardBrandOutput>(stockCardBrandOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<StockCardBrandOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<StockCardBrandOutputSimple>> GetByKeySimple(int id)
        {
            TResponse<StockCardBrandOutputSimple> response = null;

            try
            {
                var stockCardBrandOutput = await _stockCardBrandEngine.GetByKeySimpleAsync(id);
                response = new TResponse<StockCardBrandOutputSimple>(stockCardBrandOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<StockCardBrandOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<StockCardBrandOutputSimple>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<StockCardBrandOutputSimple> response = null;

            try
            {
                response = await _stockCardBrandEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<StockCardBrandOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<StockCardBrandOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<StockCardBrandOutput> response = null;

            try
            {
                response = await _stockCardBrandEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<StockCardBrandOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<StockCardBrandOutput>> InsertOrUpdate(StockCardBrandInput stockCardBrandInput)
        {
            try
            {
                var result = await _stockCardBrandEngine.InsertOrUpdate(stockCardBrandInput);
                return new TResponse<StockCardBrandOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<StockCardBrandOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<StockCardBrandOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<StockCardBrandOutput>> Delete(int id)
        {
            try
            {
                var result = await _stockCardBrandEngine.Delete(id);
                return new TResponse<StockCardBrandOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<StockCardBrandOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<StockCardBrandOutput>(ex);
            }
        }
    }
}
