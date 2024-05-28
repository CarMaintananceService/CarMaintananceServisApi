using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cars.Enums;
using Core.Entities;

namespace Cars.Models.Domain
{
    /// <summary>
    /// Araçlar
    /// </summary>
    public class Vehicle : AuditedEntity<int>
    {
        public int? VehicleBrandId { get; set; }            
        public int? VehicleTypeId { get; set; }         
        public int? CaseTypeId { get; set; }           





        [ForeignKey("VehicleBrandId")]  
        public VehicleBrand VehicleBrand { get; set; }

        [ForeignKey("VehicleTypeId")]  
        public VehicleType VehicleType { get; set; }

        [ForeignKey("CaseTypeId")] 
        public CaseType CaseType { get; set; }





        [MaxLength(50)]
        public string? LicensePlateNo { get; set; }
   

        [MaxLength(50)]
        public string? Model { get; set; }
        public DateTime? ModelYear { get; set; }

        [MaxLength(50)]
        public string? Engine { get; set; }

        [MaxLength(50)]
        public string? Chassis { get; set; }

        [MaxLength(50)]
        public string? License { get; set; }


        [MaxLength(50)]
        public string? Chance { get; set; }

        [MaxLength(50)]
        public string? ColorKeyCode { get; set; }

        [MaxLength(50)]
        public string? RadioCode { get; set; }
        public DateTime? DateOfRegistration { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? EngineDisplacementPowerKW { get; set; }
        public int? FuelTypeId { get; set; }
        public int? NetWeight { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public DateTime? DateOfLastMedicalInspection { get; set; }
        public DateTime? FenniMuayeneEndDate { get; set; }
        public DateTime? LastExhaustInspectionDate { get; set; }
        public DateTime? TrafficInsuranceStartDate { get; set; }
        public DateTime? TrafficInsuranceEndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? InsuranceStartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? InsuranceEndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string? UnitUsingTheTool { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(50)]
        public string? PhotoOfTheVehicle { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        public FuelType FuelType { get; set; }

    }
}
