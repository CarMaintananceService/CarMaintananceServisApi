using Business.Shared.Base.Dtos;
using Core.Entities;
using Core.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cars.Models.Domain
{
    /// <summary>
    /// TAMİR HAREKETİ
    /// </summary>

    public class RepairActivityOutput : BaseDto
    {

        public DateTime? AcceptanceDateOfTheVehicle { get; set; }
        public int? ServiceEntryKm { get; set; }

        public string? PersonWhoBroughtTheVehicle { get; set; }

        public string? VehicleMalfunctionDescription { get; set; }

        public string? PersonnelReceivingTheVehicle { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public int? StatusId { get; set; }             //From Status Table

        public string? ChangingPart { get; set; }

        public string? ChangedPartPhoto { get; set; }
        public double? AmountOfChangedParts { get; set; }
        public double? AmountOfOutsourcedLabor { get; set; }

        public string? Description { get; set; }
        public Status Status { get; set; } // get all the OutSourceLabor for a specific RepairActivity (one to many(has many))


    }
}
