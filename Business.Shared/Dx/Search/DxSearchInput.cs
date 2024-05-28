using Business.Shared.Dx.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Business.Shared.Dx.Search
{
	public class DxSearchInput : IDxFilterInput
	{
		

		[JsonPropertyName("filter")]
		public string? Filter { get; set; }

		[JsonPropertyName("skip")]
		public int Skip { get; set; }
		[JsonPropertyName("take")]
		public int Take { get; set; }

		[JsonPropertyName("searchExpr")]
		public string? SearchExpr { get; set; }

		[JsonPropertyName("searchOperation")]
		public string? SearchOperation { get; set; }

		[JsonPropertyName("searchValue")]
		public string? SearchValue { get; set; }

		[JsonPropertyName("userData")]
		public Dictionary<string, object>? UserData { get; set; } = new Dictionary<string, object>();
	
		//public JsonElement GetUserData(string paramName)
		//{
		//	if (this.UserData.ContainsKey(paramName))
		//		return (this.UserData[paramName] as JsonElement?).Value;
		//	else
		//		return new JsonElement();
		//}

		public JsonElement GetUserData(string paramName, string defaultValue = "0")
		{
			if (this.UserData.ContainsKey(paramName))
				return (this.UserData[paramName] as JsonElement?).Value;
			else
				return JsonDocument.Parse(defaultValue).RootElement;
		}


	}
}
