using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    public class StockCardBrandInput : BaseDto
    {
        [MaxLength(50)]
        public string Name { get; set; } // get all the StockCard for a specific Firm (one to many(has many))

    }
}