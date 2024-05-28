using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    /// <summary>
    /// FİRMALAR
    ///  STOCK CARD TABLOSUNDAKİ NAME OF THE PURCHASİNG COMPANY
    /// </summary>

    public class FirmOutput : BaseDto
    {

        public string? Name { get; set; }

        public string? Telephone { get; set; }

        public string? TaxNumber { get; set; }

        public string? Address { get; set; } // get all the StockCard for a specific Firm (one to many(has many))

    }
}
