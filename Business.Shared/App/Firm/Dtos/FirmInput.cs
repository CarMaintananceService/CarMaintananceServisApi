using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    /// <summary>
    /// FİRMALAR
    ///  STOCK CARD TABLOSUNDAKİ NAME OF THE PURCHASİNG COMPANY
    /// </summary>

    public class FirmInput : BaseDto
    {

        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Telephone { get; set; }

        [MaxLength(50)]
        public string? TaxNumber { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; } // get all the StockCard for a specific Firm (one to many(has many))

    }
}
