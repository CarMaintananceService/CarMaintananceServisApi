using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cars.Enums;
using Core.Entities;
using Core.Shared;

namespace Cars.Models.Domain
{
    public class StockMovement : AuditedEntity<int>
    {
        public int? StockMovementTypeId { get; set; }

        [MaxLength(50)]
        public string? Name { get; set; }
        public DateTime? MovementDate { get; set; }
        public int? InvoiceNo { get; set; }
        public int? Price { get; set; }
        public int? ManufacturerId { get; set; }          //From Manufacturer Table

        public StockMovementType StockMovementType { get; set; }

        [ForeignKey("StockCardId")]
        public StockCard StockCard { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }

    }
}
