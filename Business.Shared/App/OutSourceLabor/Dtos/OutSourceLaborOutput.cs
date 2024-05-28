using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models.Domain
{

    /// <summary>
    /// 
    /// </summary>

    public class OutSourceLaborOutput : BaseDto
    {
        public int? RepairActivityId { get; set; } //From RepairActivity Table

        public string? LaborName { get; set; }

        public string? PersonName { get; set; }

        public string? PersonSurname { get; set; }

        public string? Quantity { get; set; }

        public RepairActivityOutput RepairActivity{ get; set; }





}
}
