using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Constant;
using Core.Entities;

namespace Core.Security
{
    public class User : AuditedEntity, IHasActive
	{

		[Required]
        public bool IsActive { get; set; }

        [MaxLength(50, ErrorMessage = ConstMessages.Char50)]
        [Required]
        public string? Code { get; set; }

        [MaxLength(50, ErrorMessage = ConstMessages.Char50)]
        [Required]
        public string? UserName { get; set; }

		[MaxLength(500, ErrorMessage = ConstMessages.Char500)]
		[Required]
		public string? Password { get; set; }


		[Required]
        public int CookieTimeout { get; set; }


        [MaxLength(50, ErrorMessage = ConstMessages.Char50)]
        [Required]
        public string? EMail { get; set; }

        [MaxLength(150, ErrorMessage = ConstMessages.Char150)]
        [Required]
        public string? Name { get; set; }

        [MaxLength(150, ErrorMessage = ConstMessages.Char150)]
        [Required]
        public string? Surname { get; set; }

        [MaxLength(150, ErrorMessage = ConstMessages.Char150)]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public string FullName => $"{Name} {Surname}";


        [MaxLength(50, ErrorMessage = ConstMessages.Char50)]
        public string? GsmNo { get; set; }
      
		[MaxLength(50, ErrorMessage = ConstMessages.Char50)]
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpireIn { get; set; }
        public string? UserRightsView { get; set; }


		[MaxLength(1024 * 1000, ErrorMessage = "Max size is 1MB")]
		public byte[]? Picture { get; set; }


	}
}
