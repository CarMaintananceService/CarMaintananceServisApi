
using Business.Shared.Dx.Filter;
using Business.Shared;
using Business.App.VehicleTypes;
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
    public class VehicleTypeController : ControllerBase
    {
        VehicleTypeEngine _vehicleTypeEngine;
        public VehicleTypeController(VehicleTypeEngine vehicleTypeEngine)
        {
            _vehicleTypeEngine = vehicleTypeEngine;
        }
        [HttpGet]
        public async Task<TResponse<VehicleTypeOutput>> GetByKey(int id)
        {
            TResponse<VehicleTypeOutput> response = null;

            try
            {
                var vehicleTypeOutput = await _vehicleTypeEngine.GetByKeyAsync(id);
                response = new TResponse<VehicleTypeOutput>(vehicleTypeOutput);
            }
            catch (Exception ex)
            {
                response = new TResponse<VehicleTypeOutput>(ex);
            }

            return response;
        }
        [HttpGet]
        public async Task<TResponse<VehicleTypeOutputSimple>> GetByKeySimple(int id)
        {
            TResponse<VehicleTypeOutputSimple> response = null;

            try
            {
                var vehicleTypeOutput = await _vehicleTypeEngine.GetByKeySimpleAsync(id);
                response = new TResponse<VehicleTypeOutputSimple>(vehicleTypeOutput);

            }
            catch (Exception ex)
            {
                response = new TResponse<VehicleTypeOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<VehicleTypeOutputSimple>> Search(DxSearchInput dxSearchInput)
        {
            TPagerResponse<VehicleTypeOutputSimple> response = null;

            try
            {
                response = await _vehicleTypeEngine.Search(dxSearchInput);
            }
            catch (Exception ex)
            {
                response = new TPagerResponse<VehicleTypeOutputSimple>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TPagerResponse<VehicleTypeOutput>> Filter(DxFilterInput filterInput)
        {
            TPagerResponse<VehicleTypeOutput> response = null;

            try
            {
                response = await _vehicleTypeEngine.FilterAsync(filterInput);

            }
            catch (Exception ex)
            {
                response = new TPagerResponse<VehicleTypeOutput>(ex);
            }

            return response;
        }
        [HttpPost]
        public async Task<TResponse<VehicleTypeOutput>> InsertOrUpdate(VehicleTypeInput vehicleTypeInput)
        {
            try
            {
                var result = await _vehicleTypeEngine.InsertOrUpdate(vehicleTypeInput);
                return new TResponse<VehicleTypeOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<VehicleTypeOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<VehicleTypeOutput>(ex);
            }
        }


        [HttpDelete]
        public async Task<TResponse<VehicleTypeOutput>> Delete(int id)
        {
            try
            {
                var result = await _vehicleTypeEngine.Delete(id);
                return new TResponse<VehicleTypeOutput>(result);
            }
            catch (WarningException ex)
            {
                return new TResponse<VehicleTypeOutput>().SetWarning(ex.Message);
            }
            catch (Exception ex)
            {
                return new TResponse<VehicleTypeOutput>(ex);
            }
        }
    }
}
