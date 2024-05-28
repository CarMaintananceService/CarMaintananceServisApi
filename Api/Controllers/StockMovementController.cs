
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.StockMovements;
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
    public class StockMovementController : ControllerBase
    {
        StockMovementEngine _stockMovementEngine;
        public StockMovementController(StockMovementEngine stockMovementEngine)
        {
            _stockMovementEngine = stockMovementEngine;
        }
        [HttpGet]
        public async Task<TResponse<StockMovementOutput>> GetByKey(int id)
        {
            TResponse<StockMovementOutput> response = null;

            try
            {
                var stockMovementOutput = await _stockMovementEngine.GetByKeyAsync(id);
                response = new TResponse<StockMovementOutput>(stockMovementOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<StockMovementOutput>(ex);
            }

            return response;
        }

        [HttpPost]
        public async Task<TPagerResponse<StockMovementOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<StockMovementOutput> response = null;

            try
            {
                response = await _stockMovementEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<StockMovementOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<StockMovementOutput>> InsertOrUpdate(StockMovementInput stockMovementInput)
        {
            try
            {
                var result = await _stockMovementEngine.InsertOrUpdate(stockMovementInput);
                return new TResponse<StockMovementOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<StockMovementOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<StockMovementOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<StockMovementOutput>> Delete(int id)
        {
            try
            {
                var result = await _stockMovementEngine.Delete(id);
                return new TResponse<StockMovementOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<StockMovementOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<StockMovementOutput>(ex);
            }
        }
    }
}
