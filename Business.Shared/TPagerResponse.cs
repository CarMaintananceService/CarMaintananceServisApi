namespace Business.Shared
{
    [Serializable]
    public class TPagerResponse<T> : TBaseResponse
    {
        //[JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        //[JsonPropertyName("items")]
        public IEnumerable<T> Items { get; set; } = new List<T>();

		public TPagerResponse(Exception ex) : base(ex)
		{

		}

		public TPagerResponse(string errorMessage) : base(errorMessage)
        {

        }

        public TPagerResponse(IEnumerable<T> items, int totalCount)
        {

            Items = items;
            TotalCount = totalCount;
        }

        public TPagerResponse<T> SetError(string error)
        {
            Error = error;
            return this;
        }

        public TPagerResponse<T> SetWarning(string warning)
        {
            Warning = warning;
            return this;
        }

        public TPagerResponse<T> SetInformation(string information)
        {
            Information = information;
            return this;
        }

    }

}
