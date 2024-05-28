using Business.Shared.Base.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Shared.Security.Users.Dtos
{
	public class UserOutputSimple: BaseDto
	{
		public string? Code { get; set; }
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
