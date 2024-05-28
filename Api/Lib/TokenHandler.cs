using Business.Shared.Security.Users.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Lib
{
	public class TokenHandler
	{
		private readonly TokenOptions tokenOptions;

		public TokenHandler(IOptions<TokenOptions> tokenOptions)
		{
			this.tokenOptions = tokenOptions.Value;
		}

		public AccessToken CreateAccessToken(UserDto user, string rightsClaim)
		{
			var accessTokenExpiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));

			SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
				audience: tokenOptions.Audience,
				issuer: tokenOptions.Issuer,
				expires: accessTokenExpiration,
				notBefore: DateTime.Now,
				claims: GetClaims(user, rightsClaim),
				signingCredentials: signingCredentials);

			var handler = new JwtSecurityTokenHandler();

			var token = handler.WriteToken(jwtSecurityToken);

			AccessToken accessToken = new AccessToken();

			accessToken.Token = token;
			accessToken.RefreshToken = CreateRefreshToken();
			accessToken.Expiration = accessTokenExpiration;

			return accessToken;
		}

		private string CreateRefreshToken()
		{
			var numberByte = new Byte[32];

			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(numberByte);

				return Convert.ToBase64String(numberByte);
			}
		}

		private IEnumerable<Claim> GetClaims(UserDto user, string rightsClaim)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.EMail),
				new Claim(ClaimTypes.Name, user.Name),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.UserData, rightsClaim),
				//new Claim(ClaimTypes.Role, user.SystemRoleId),

			};

			return claims;
		}


	}
}
