
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.ProductGroups;
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
    public class ProductGroupController : ControllerBase
    {
        ProductGroupEngine _productGroupEngine;
        public ProductGroupController(ProductGroupEngine productGroupEngine)
        {
            _productGroupEngine = productGroupEngine;
        }
        [HttpGet]
        public async Task<TResponse<ProductGroupOutput>> GetByKey(int id)
        {
            TResponse<ProductGroupOutput> response = null;

            try
            {
                var productGroupOutput = await _productGroupEngine.GetByKeyAsync(id);
                response = new TResponse<ProductGroupOutput>(productGroupOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<ProductGroupOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<ProductGroupOutputSimple>> GetByKeySimple(int id)
        {
            TResponse<ProductGroupOutputSimple> response = null;

            try
            {
                var productGroupOutput = await _productGroupEngine.GetByKeySimpleAsync(id);
                response = new TResponse<ProductGroupOutputSimple>(productGroupOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<ProductGroupOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<ProductGroupOutputSimple>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<ProductGroupOutputSimple> response = null;

            try
            {
                response = await _productGroupEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<ProductGroupOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<ProductGroupOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<ProductGroupOutput> response = null;

            try
            {
                response = await _productGroupEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<ProductGroupOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<ProductGroupOutput>> InsertOrUpdate(ProductGroupInput productGroupInput)
        {
            try
            {
                var result = await _productGroupEngine.InsertOrUpdate(productGroupInput);
                return new TResponse<ProductGroupOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<ProductGroupOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<ProductGroupOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<ProductGroupOutput>> Delete(int id)
        {
            try
            {
                var result = await _productGroupEngine.Delete(id);
                return new TResponse<ProductGroupOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<ProductGroupOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<ProductGroupOutput>(ex);
            }
        }
    }
}
