using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars.Models.Domain
{
    public class OutSourceLaborOutputSimple : BaseDto
    {
        public string? LaborName { get; set; }
    }
}
