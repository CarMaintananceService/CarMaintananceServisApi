using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
	public interface IBaseRight
	{
		public int? Value { get; set; }
		public int? DefaultValue { get; set; }
	}
}
