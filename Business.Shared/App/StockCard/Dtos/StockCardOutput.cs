using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models.Domain
{

    public class StockCardOutput : BaseDto
    {
        public int? ProductGroupId { get; set; }           //From ProductGroup Table

        public string? StockCode { get; set; }

        public string? ProductCode { get; set; }

        public string? Unit { get; set; }
        public int? StockCardUnitId { get; set; }      //From StockCardUnit Table
        public int? StockCardBrandId { get; set; }     //From StockCardBrand Table
        public int? SpecialCode { get; set; }
        public int? ManufacturerCode { get; set; }

        public string? ShelfNumber { get; set; }

        public string? BoxNumber { get; set; }
        public double? PurchasePrice { get; set; }

        public string? InvoiceNo { get; set; }
        public int? NameOfThePurchasingCompanyId { get; set; }         //From the Firms Table
        public double? Quantity { get; set; }

        public string? PacanianPhoto { get; set; }

        public string? Description { get; set; }

        public ProductGroupOutputSimple ProductGroup { get; set; }

        public FirmOutputSimple Firm { get; set; }

        public StockCardBrandOutputSimple StockCardBrand { get; set; }

        public StockCardUnitOutputSimple StockCardUnit { get; set; } // get all the StockMovement for a specific Firm (one to many(has many))

    }
}
