
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.Vehicles;
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
    public class VehicleController : ControllerBase
    {
        VehicleEngine _vehicleEngine;
        public VehicleController(VehicleEngine vehicleEngine)
        {
            _vehicleEngine = vehicleEngine;
        }
        [HttpGet]
        public async Task<TResponse<VehicleOutput>> GetByKey(int id)
        {
            TResponse<VehicleOutput> response = null;

            try
            {
                var vehicleOutput = await _vehicleEngine.GetByKeyAsync(id);
                response = new TResponse<VehicleOutput>(vehicleOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<VehicleOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<VehicleOutputSimple>> GetByKeySimple(int id)
        {
            TResponse<VehicleOutputSimple> response = null;

            try
            {
                var vehicleOutput = await _vehicleEngine.GetByKeySimpleAsync(id);
                response = new TResponse<VehicleOutputSimple>(vehicleOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<VehicleOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<VehicleOutputSimple>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<VehicleOutputSimple> response = null;

            try
            {
                response = await _vehicleEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<VehicleOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<VehicleOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<VehicleOutput> response = null;

            try
            {
                response = await _vehicleEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<VehicleOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<VehicleOutput>> InsertOrUpdate(VehicleInput vehicleInput)
        {
            try
            {
                var result = await _vehicleEngine.InsertOrUpdate(vehicleInput);
                return new TResponse<VehicleOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<VehicleOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<VehicleOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<VehicleOutput>> Delete(int id)
        {
            try
            {
                var result = await _vehicleEngine.Delete(id);
                return new TResponse<VehicleOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<VehicleOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<VehicleOutput>(ex);
            }
        }
    }
}
