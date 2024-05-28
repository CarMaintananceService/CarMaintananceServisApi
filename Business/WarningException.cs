using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{

	public class WarningException : Exception
	{
		public WarningException(string message) : base(message)
		{


		}

		public WarningException(Exception ex)
		{

		}

	}

	public class WarningException<T> : Exception
    {
		public T Item { get; set; }

		public WarningException(string message, T item) : base(message)
		{
			Item = item;
		}
		public WarningException(string message) : base(message) { 
     
            
        }
        
        public WarningException(Exception ex)
        {
            
        }

    }
}
