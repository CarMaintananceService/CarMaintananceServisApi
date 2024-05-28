using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{

    public class VehicleTypeOutput : BaseDto
    {

        public string? Name { get; set; } // get all the vehicles for a specific brand (one to many(has many))
    }
}
