using Business.Shared.Base.Dtos;
using Core.Constant;
using Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Shared.Security.Users.Dtos
{
	public class UserInput : BaseDto
	{ 
		[Required]
		public bool IsActive { get; set; }

		[MaxLength(50, ErrorMessage = ConstMessages.Char50)]
		[Required]
		public string? Code { get; set; }

		[MaxLength(50, ErrorMessage = ConstMessages.Char50)]
		[Required]
		public string? UserName { get; set; }

		[MaxLength(50, ErrorMessage = ConstMessages.Char50)]
		[Required]
		public string? Password { get; set; }

		[Required]
		public int CookieTimeout { get; set; }

		[Required]
		public string? EMail { get; set; }

		[MaxLength(150, ErrorMessage = ConstMessages.Char150)]
		[Required]
		public string? Name { get; set; }


		[MaxLength(50, ErrorMessage = ConstMessages.Char50)]
		[Required]
		public string? Surname { get; set; }


	}
}
