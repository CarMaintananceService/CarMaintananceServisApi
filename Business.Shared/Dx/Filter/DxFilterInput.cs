using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Business.Shared.Dx.Filter
{

    public interface IDxFilterInput
    {
		string? Filter { get; set; }
	}


	public class DxFilterInput : IDxFilterInput
	{
        [JsonPropertyName("skip")]
        public int Skip { get; set; }
        [JsonPropertyName("take")]
        public int Take { get; set; }

        [JsonPropertyName("sort")]
        public List<DxSort> Sort { get; set; } = new List<DxSort>();

        [JsonPropertyName("filter")]
        public string? Filter { get; set; }

        [JsonPropertyName("isLoadingAll")]
        public bool IsLoadingAll { get; set; } = false;

        [JsonPropertyName("dataField")]
        public string? DataField { get; set; }

        [JsonPropertyName("requireTotalCount")]
        public bool RequireTotalCount { get; set; }

        [JsonPropertyName("userData")]
        public Dictionary<string, object>? UserData { get; set; } = new Dictionary<string, object>();


		public JsonElement GetUserData(string paramName, string defaultValue = "0")
		{
            if (this.UserData.ContainsKey(paramName))
                return (this.UserData[paramName] as JsonElement?).Value;
            else
                return JsonDocument.Parse(defaultValue).RootElement;
		}

		public bool HasUserData(string paramName)
		{
            return this.UserData.ContainsKey(paramName);
		}


	}
}
