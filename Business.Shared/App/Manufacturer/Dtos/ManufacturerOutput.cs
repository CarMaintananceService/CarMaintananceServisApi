using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    /// <summary>
    /// ÜRETİCİ FİRMA STOCK HAREKETİ TABLOSUNDA
    /// </summary>

    public class ManufacturerOutput : BaseDto
    {

        public string? Name { get; set; } // get all the StockMovement for a specific Firm (one to many(has many))

    }
}
