using System.Text.Json.Serialization;

namespace Api.Lib
{

	public class AccessToken
	{
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("expiration")]
        public DateTime Expiration { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

    }



	public class AccessTokenResponse
	{
		[JsonPropertyName("isActive")]
		public bool IsActive { get; set; }

		[JsonPropertyName("isExpired")]
		public bool IsExpired { get; set; }

		public AccessToken AccessToken { get; set; }
		public Dictionary<string, bool> Modules { get; set; }
		
		[JsonPropertyName("userInfo")]
		public TokenUserInfo UserInfo { get; set; }

		[JsonPropertyName("userRights")]
		public Dictionary<int, Dictionary<int, int>> UserRights { get; set; }

	
	

	}

	public class TokenUserInfo
	{
		public int Id { get; set; }
		public bool IsActive { get; set; }
		public string FullName { get; set; }
		public string EMail { get; set; }
		public string Picture { get; set; }
	}


}
