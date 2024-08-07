using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    public class VehicleBrand : AuditedEntity<int>
    {

        [MaxLength(50)]
        public string? Name { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; set; } // get all the vehicles for a specific brand (one to many(has many))
    }
}
