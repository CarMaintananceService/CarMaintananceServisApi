
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.Manufacturers;
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
    public class ManufacturerController : ControllerBase
    {
        ManufacturerEngine _manufacturerEngine;
        public ManufacturerController(ManufacturerEngine manufacturerEngine)
        {
            _manufacturerEngine = manufacturerEngine;
        }
        [HttpGet]
        public async Task<TResponse<ManufacturerOutput>> GetByKey(int id)
        {
            TResponse<ManufacturerOutput> response = null;

            try
            {
                var manufacturerOutput = await _manufacturerEngine.GetByKeyAsync(id);
                response = new TResponse<ManufacturerOutput>(manufacturerOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<ManufacturerOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<ManufacturerOutputSimple>> GetByKeySimple(int id)
        {
            TResponse<ManufacturerOutputSimple> response = null;

            try
            {
                var manufacturerOutput = await _manufacturerEngine.GetByKeySimpleAsync(id);
                response = new TResponse<ManufacturerOutputSimple>(manufacturerOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<ManufacturerOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<ManufacturerOutputSimple>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<ManufacturerOutputSimple> response = null;

            try
            {
                response = await _manufacturerEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<ManufacturerOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<ManufacturerOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<ManufacturerOutput> response = null;

            try
            {
                response = await _manufacturerEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<ManufacturerOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<ManufacturerOutput>> InsertOrUpdate(ManufacturerInput manufacturerInput)
        {
            try
            {
                var result = await _manufacturerEngine.InsertOrUpdate(manufacturerInput);
                return new TResponse<ManufacturerOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<ManufacturerOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<ManufacturerOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<ManufacturerOutput>> Delete(int id)
        {
            try
            {
                var result = await _manufacturerEngine.Delete(id);
                return new TResponse<ManufacturerOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<ManufacturerOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<ManufacturerOutput>(ex);
            }
        }
    }
}
