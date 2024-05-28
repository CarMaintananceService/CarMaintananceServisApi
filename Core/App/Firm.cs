using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    /// <summary>
    /// FİRMALAR
    ///  STOCK CARD TABLOSUNDAKİ NAME OF THE PURCHASİNG COMPANY
    /// </summary>
    public class Firm : AuditedEntity<int>
    {

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Telephone { get; set; }

        [MaxLength(50)]
        public string? TaxNumber { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }
        
        public IEnumerable<StockCard> StockCards { get; set; } // get all the StockCard for a specific Firm (one to many(has many))

    }
}
