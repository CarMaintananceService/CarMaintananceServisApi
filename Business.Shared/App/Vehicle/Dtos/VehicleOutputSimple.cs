using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Business.Shared.Base.Dtos;
using Cars.Enums;
using Core.Entities;

namespace Cars.Models.Domain
{
    /// <summary>
    /// Araçlar
    /// </summary>

    public class VehicleOutputSimple : BaseDto
    {
        public string? Model { get; set; }
    }


}
