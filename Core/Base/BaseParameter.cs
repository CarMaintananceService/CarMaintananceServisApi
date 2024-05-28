using Core;
using Core.Constant;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
  
    public class BaseParameter: AuditedEntity<int>
    {
        [MaxLength(50, ErrorMessage = ConstMessages.Char50)]
        [Required]
        public string? Code { get; set; }

        [MaxLength(150, ErrorMessage = ConstMessages.Char150)]
        [Required]
        public string? Definition { get; set; }
        
        [MaxLength(150, ErrorMessage = ConstMessages.Char150)]
        public string? Definition_Eng { get; set; }

        [MaxLength(150, ErrorMessage = ConstMessages.Char150)]
        [Required]
        public string? ParamName { get; set; }
    }
}
