using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    /// <summary>
    /// BİRİMLER
    /// </summary>

    public class UnitOutputSimple : BaseDto
    {

        public string? Name { get; set; }
    }
}
