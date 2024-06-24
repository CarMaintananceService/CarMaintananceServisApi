using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    /// <summary>
    /// ÜRETİCİ FİRMA, STOCK HAREKETİ TABLOSUNDA
    /// </summary>
    public class Manufacturer : AuditedEntity<int>
    {

        [MaxLength(50)]
        public string? Name { get; set; }

        public IEnumerable<StockMovement> StockMovements { get; set; } // get all the StockMovement for a specific Firm (one to many(has many))

    }
}
