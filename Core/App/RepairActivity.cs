using Core.Entities;
using Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cars.Models.Domain
{
    /// <summary>
    /// TAMİR HAREKETİ
    /// </summary>
    public class RepairActivity : AuditedEntity<int>
    {

        public DateTime? AcceptanceDateOfTheVehicle { get; set; }
        public int? ServiceEntryKm { get; set; }

        [MaxLength(50)]
        public string? PersonWhoBroughtTheVehicle { get; set; }

        [MaxLength(500)]
        public string? VehicleMalfunctionDescription { get; set; }

        [MaxLength(50)]
        public string? PersonnelReceivingTheVehicle { get; set; }
        public DateTime PlannedEndDate { get; set; }
        

        [MaxLength(500)]
        public string? ChangingPart { get; set; }

        [MaxLength(100)]
        public string? ChangedPartPhoto { get; set; }
        public double? AmountOfChangedParts { get; set; }
        public double? AmountOfOutsourcedLabor { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public Status Status { get; set; }

        public IEnumerable<OutSourceLabor> OutSourceLabor { get; set; } // get all the OutSourceLabor for a specific RepairActivity (one to many(has many))


    }
}
