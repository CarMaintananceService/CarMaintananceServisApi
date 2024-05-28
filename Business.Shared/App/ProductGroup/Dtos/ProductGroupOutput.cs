using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    /// <summary>
    /// ÜRÜN GRUBU
    /// </summary>

    public class ProductGroupOutput : BaseDto
    {
        public string? Mechanical { get; set; }

        public string? Electricity { get; set; }

        //ENUM OR COOL // get all the StockCard for a specific Firm (one to many(has many))


    }
}
