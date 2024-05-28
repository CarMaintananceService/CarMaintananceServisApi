using System.ComponentModel.DataAnnotations;

namespace Api.Lib
{
	public class LoginInput
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }
	}
	
}
