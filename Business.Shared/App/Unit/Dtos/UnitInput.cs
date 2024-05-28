using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    /// <summary>
    /// BİRİMLER
    /// </summary>
    
public class UnitInput : BaseDto
{

        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
