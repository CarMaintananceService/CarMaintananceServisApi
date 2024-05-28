using Business.Shared.Base.Dtos;
using Core.Constant;

namespace Business.Shared.Security.Users.Dtos
{
	public class UserOutput 
	{
		public bool IsActive { get; set; }
		public string? Code { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
		public int CookieTimeout { get; set; }
		public int Source { get; set; }
		public string? EMail { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string FullName
		{
			get
			{
				return $"{this.Name} {this.Surname}";
			}
		}

	}
}
