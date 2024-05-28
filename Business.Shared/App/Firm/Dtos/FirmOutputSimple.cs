using Business.Shared.Base.Dtos;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models.Domain
{
    /// <summary>
    /// FİRMALAR
    ///  STOCK CARD TABLOSUNDAKİ NAME OF THE PURCHASİNG COMPANY
    /// </summary>

    public class FirmOutputSimple : BaseDto
    {
        public string? Name { get; set; }
    }
}
