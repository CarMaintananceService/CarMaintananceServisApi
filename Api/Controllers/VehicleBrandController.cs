
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.VehicleBrands;
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
    public class VehicleBrandController : ControllerBase
    {
        VehicleBrandEngine _vehicleBrandEngine;
        public VehicleBrandController(VehicleBrandEngine vehicleBrandEngine)
        {
            _vehicleBrandEngine = vehicleBrandEngine;
        }
        [HttpGet]
        public async Task<TResponse<VehicleBrandOutput>> GetByKey(int id)
        {
            TResponse<VehicleBrandOutput> response = null;

            try
            {
                var vehicleBrandOutput = await _vehicleBrandEngine.GetByKeyAsync(id);
                response = new TResponse<VehicleBrandOutput>(vehicleBrandOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<VehicleBrandOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<VehicleBrandOutputSimple>> GetByKeySimple(int id)
        {
            TResponse<VehicleBrandOutputSimple> response = null;

            try
            {
                var vehicleBrandOutput = await _vehicleBrandEngine.GetByKeySimpleAsync(id);
                response = new TResponse<VehicleBrandOutputSimple>(vehicleBrandOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<VehicleBrandOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<VehicleBrandOutputSimple>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<VehicleBrandOutputSimple> response = null;

            try
            {
                response = await _vehicleBrandEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<VehicleBrandOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<VehicleBrandOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<VehicleBrandOutput> response = null;

            try
            {
                response = await _vehicleBrandEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<VehicleBrandOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<VehicleBrandOutput>> InsertOrUpdate(VehicleBrandInput vehicleBrandInput)
        {
            try
            {
                var result = await _vehicleBrandEngine.InsertOrUpdate(vehicleBrandInput);
                return new TResponse<VehicleBrandOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<VehicleBrandOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<VehicleBrandOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<VehicleBrandOutput>> Delete(int id)
        {
            try
            {
                var result = await _vehicleBrandEngine.Delete(id);
                return new TResponse<VehicleBrandOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<VehicleBrandOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<VehicleBrandOutput>(ex);
            }
        }
    }
}
