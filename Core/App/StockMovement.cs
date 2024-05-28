using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cars.Enums;
using Core.Entities;
using Core.Shared;

namespace Cars.Models.Domain
{
    public class StockMovement : AuditedEntity<int>
    {
        public int? ManufacturerId { get; set; }          //From Manufacturer Table
        public int? StockCardId { get; set; }          //From Manufacturer Table



        [ForeignKey("StockCardId")]
        public StockCard StockCard { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }




        [MaxLength(50)]
        public string? Name { get; set; }
        public DateTime? MovementDate { get; set; }
        public string? InvoiceNo { get; set; }
        public double? Price { get; set; }
        public StockMovementType StockMovementType { get; set; }

        

    }
}
