using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models.Domain
{
    public class StockCard : AuditedEntity<int>
    {
        public int? ProductGroupId { get; set; }           //From ProductGroup Table

        [MaxLength(50)]
        public string? StockCode { get; set; }

        [MaxLength(50)]
        public string? ProductCode { get; set; }

        [MaxLength(50)]
        public string? Unit { get; set; }
        public int? StockCardUnitId { get; set; }      //From StockCardUnit Table
        public int? StockCardBrandId { get; set; }     //From StockCardBrand Table
        public int? SpecialCode { get; set; }
        public int? ManufacturerCode { get; set; }

        [MaxLength(50)]
        public string? ShelfNumber { get; set; }

        [MaxLength(50)]
        public string? BoxNumber { get; set; }
        public double? PurchasePrice { get; set; }

        [MaxLength(50)]
        public string? InvoiceNo { get; set; }
        public int? NameOfThePurchasingCompanyId { get; set; }         //From the Firms Table
        public double? Quantity { get; set; }

        [MaxLength(100)]
        public string? PacanianPhoto { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [ForeignKey("ProductGroupId")]
        public ProductGroup ProductGroup { get; set; }

        [ForeignKey("NameOfThePurchasingCompanyId")]
        public Firm Firm { get; set; }

        [ForeignKey("StockCardBrandId")]
        public StockCardBrand StockCardBrand { get; set; }

        [ForeignKey("StockCardUnitId")]
        public StockCardUnit StockCardUnit { get; set; }

        public IEnumerable<StockMovement> StockMovements { get; set; } // get all the StockMovement for a specific Firm (one to many(has many))


    }
}
