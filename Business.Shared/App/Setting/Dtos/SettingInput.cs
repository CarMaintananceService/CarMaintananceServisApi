using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    public class SettingInput : BaseDto
    {
        [StringLength(50)]
        public string? Code { get; set; }

        [StringLength(50)]
        public string? Value { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

    }
}
