using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{

    public class SettingOutput : BaseDto
    {
        public string? Code { get; set; }

        public string? Value { get; set; }

        public string? Description { get; set; }

    }
}
