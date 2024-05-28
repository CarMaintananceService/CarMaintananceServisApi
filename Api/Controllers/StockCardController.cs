
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.StockCards;
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
    public class StockCardController : ControllerBase
    {
        StockCardEngine _stockCardEngine;
        public StockCardController(StockCardEngine stockCardEngine)
        {
            _stockCardEngine = stockCardEngine;
        }
        [HttpGet]
        public async Task<TResponse<StockCardOutput>> GetByKey(int id)
        {
            TResponse<StockCardOutput> response = null;

            try
            {
                var stockCardOutput = await _stockCardEngine.GetByKeyAsync(id);
                response = new TResponse<StockCardOutput>(stockCardOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<StockCardOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<StockCardOutputSimple>> GetByKeySimple(int id)
        {
            TResponse<StockCardOutputSimple> response = null;

            try
            {
                var stockCardOutput = await _stockCardEngine.GetByKeySimpleAsync(id);
                response = new TResponse<StockCardOutputSimple>(stockCardOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<StockCardOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<StockCardOutputSimple>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<StockCardOutputSimple> response = null;

            try
            {
                response = await _stockCardEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<StockCardOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<StockCardOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<StockCardOutput> response = null;

            try
            {
                response = await _stockCardEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<StockCardOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<StockCardOutput>> InsertOrUpdate(StockCardInput stockCardInput)
        {
            try
            {
                var result = await _stockCardEngine.InsertOrUpdate(stockCardInput);
                return new TResponse<StockCardOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<StockCardOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<StockCardOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<StockCardOutput>> Delete(int id)
        {
            try
            {
                var result = await _stockCardEngine.Delete(id);
                return new TResponse<StockCardOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<StockCardOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<StockCardOutput>(ex);
            }
        }
    }
}
