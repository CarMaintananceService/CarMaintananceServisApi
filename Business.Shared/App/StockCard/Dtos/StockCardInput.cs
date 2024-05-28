using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models.Domain
{
    
public class StockCardInput : BaseDto
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

        public ProductGroupOutputSimple ProductGroup{ get; set; }

        public FirmOutputSimple Firm{ get; set; }

        public StockCardBrandOutputSimple StockCardBrand{ get; set; }

        public StockCardUnitOutputSimple StockCardUnit{ get; set; } // get all the StockMovement for a specific Firm (one to many(has many))


    }
}
