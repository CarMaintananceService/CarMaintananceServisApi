using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    /// <summary>
    /// KASA TİPİ
    /// </summary>

    public class CaseTypeOutput : BaseDto
    {

        public string? Name { get; set; }
    }
}
