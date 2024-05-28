using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models.Domain
{

    /// <summary>
    /// 
    /// </summary>
    public class OutSourceLabor : AuditedEntity<int>
    {
        public int? RepairActivityId { get; set; } //From RepairActivity Table

        [MaxLength(50)]
        public string? LaborName { get; set; }

        [MaxLength(50)]
        public string? PersonName { get; set; }

        [MaxLength(50)]
        public string? PersonSurname { get; set; }

        public string? Quantity { get; set; }

        [ForeignKey("RepairActivityId")]  
        public RepairActivity RepairActivity { get; set; }

        



    }
}