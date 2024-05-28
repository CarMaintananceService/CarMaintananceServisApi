using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public class TokenEntity
	{
		public string userId { get; set; }
		public string appId { get; set; }
	}

	public class TokenManager
	{
		protected string secretKey = "lewpch+5(*'3^6&/%!(6546([1].:,~0,.";
		protected string issuer = "http://esource.com.tr";
		protected string audience = "http://esource-customer.com";
		public string GenerateJwtToken(TokenEntity tokenEntity)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secretKey);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] {
					new Claim("userId", tokenEntity.userId),
					new Claim("appId", tokenEntity.appId)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public TokenEntity ValidateJwtToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secretKey);
			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					// set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;

				TokenEntity tokenEntity = new TokenEntity()
				{
					appId = jwtToken.Claims.First(x => x.Type == "appId").Value,
					userId = jwtToken.Claims.First(x => x.Type == "userId").Value,
				};
				return tokenEntity;
			}
			catch
			{
				// return null if validation fails
				return null;
			}
		}
	}
}
