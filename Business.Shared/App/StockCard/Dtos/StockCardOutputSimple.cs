using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models.Domain
{

    public class StockCardOutputSimple : BaseDto
    {
        public string? StockCode { get; set; }
        public string? Description { get; set; }
    }
}
