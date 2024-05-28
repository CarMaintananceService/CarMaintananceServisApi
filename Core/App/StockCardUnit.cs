using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    public class StockCardUnit : AuditedEntity<int>
    {
        
        [MaxLength(50)]
        public string? Name { get; set; }

        public IEnumerable<StockCard> StockCards { get; set; } // get all the StockCard for a specific Firm (one to many(has many))
    }
}
