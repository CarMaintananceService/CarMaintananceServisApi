using System.Text.Json.Serialization;

namespace Business.Shared
{
	[Serializable]
	public class TBaseResponse
	{

		public bool Success { get; set; }
		public string Error { get; set; }
        public string Warning { get; set; }
        public string Information { get; set; }
        public bool IsBusinessError { get; set; }
		public string FileName { get; set; }

		public Dictionary<string, object> ExtraData { get; set; } = new Dictionary<string, object>();

		protected TBaseResponse(Exception ex)
		{
			this.Success = false;
			this.Error = ex.Message;

			if (ex.InnerException != null)
				this.Error += "!Error detail:" + ex.InnerException;
		}

		protected TBaseResponse(string errorMessage)
		{
			this.Success = false;
			this.Error = errorMessage;
		}

		protected TBaseResponse()
		{
			this.Success = true;
		}

        

	}


    [Serializable]
    public class TResponse<T> : TBaseResponse
    {
        public T Result { get; set; }

		public TResponse(Exception ex) : base(ex)
		{
			this.Success = false;
		}

		public TResponse(string errorMessage) : base(errorMessage)
        {
            this.Success = false;
            this.Error = errorMessage;
        }

        public TResponse(object result)
        {
            this.Success = true;
            this.Result = (T)result;
        }

        public TResponse()
        {
            this.Success = true;
        }

        public TResponse<T> SetError(string error)
        {
            this.Success = false;
            Error = error;
            return this;
        }

        public TResponse<T> SetWarning(string warning)
        {
			this.Success = true;
			Warning = warning;
            return this;
        }

        public TResponse<T> SetInformation(string information)
        {
			this.Success = true;
			Information = information;
            return this;
        }

		public TResponse<T> SetAsBusinessError()
		{
			this.Success = false;
			IsBusinessError = true;
			return this;
		}

		public TResponse<T> SetFileName(string fileName)
		{
			FileName = fileName;
			return this;
		}
		

	}
}