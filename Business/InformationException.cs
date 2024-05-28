using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class InformationException<T> : Exception
    {

        public T Item { get; set; }

		public InformationException(string message, T item) : base(message)
		{
            Item = item;
		}
		public InformationException(string message) : base(message)
        {

        }

        public InformationException(Exception ex)
        {

        }

    }
   
}
