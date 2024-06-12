using AutoMapper;
using Business.Shared;
using Core.Security;
using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Lib
{
	public class JwtEngineUser
	{
		private readonly TokenOptions _tokenOptions;
		private ApplicationDbContext _dbContext;
		private IMapper _objectMapper;

		public JwtEngineUser(IOptions<TokenOptions> tokenOptions, ApplicationDbContext dbContext, IMapper objectMapper)
		{
			_tokenOptions = tokenOptions.Value;
			_dbContext = dbContext;
			_objectMapper = objectMapper;
		}

		public AccessTokenResponse CreateAccessToken(User user)
		{
			AccessTokenResponse accessTokenResponse = new AccessTokenResponse();
			accessTokenResponse.UserRights = _getUserRights(user);
			user.UserRightsView = JsonConvert.SerializeObject(accessTokenResponse.UserRights);

			var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
			//var accessTokenExpiration = DateTime.Now.AddMinutes(1);
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

			SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
				audience: _tokenOptions.Audience,
				issuer: _tokenOptions.Issuer,
				expires: accessTokenExpiration,
				notBefore: DateTime.Now,
				claims: _getClaims(user),
				signingCredentials: signingCredentials);

			var handler = new JwtSecurityTokenHandler();
			var token = handler.WriteToken(jwtSecurityToken);


			accessTokenResponse.AccessToken = new AccessToken()
			{
				Token = token,
				RefreshToken = _createRefreshToken(),
				Expiration = accessTokenExpiration,
				
			};

			accessTokenResponse.UserInfo = new TokenUserInfo()
			{
				Id = user.Id,
				EMail = user.EMail,
				FullName = $"{user.Name} {user.Surname}",
				Picture = Convert.ToBase64String(user.Picture ?? new byte[] { }),
				IsActive = user.IsActive,
			};
			accessTokenResponse.IsActive = user.IsActive;
			return accessTokenResponse;
		}


		public AccessTokenResponse RefreshToken(User user)
		{
			AccessTokenResponse accessTokenResponse = new AccessTokenResponse();
			accessTokenResponse.UserRights = _getUserRights(user);
			user.UserRightsView = JsonConvert.SerializeObject(accessTokenResponse.UserRights);

			var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
			//var accessTokenExpiration = DateTime.Now.AddMinutes(1);

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

			SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
				audience: _tokenOptions.Audience,
				issuer: _tokenOptions.Issuer,
				expires: accessTokenExpiration,
				notBefore: DateTime.Now,
				claims: _getClaims(user),
				signingCredentials: signingCredentials);

			var handler = new JwtSecurityTokenHandler();
			var token = handler.WriteToken(jwtSecurityToken);



			accessTokenResponse.AccessToken = new AccessToken()
			{
				Token = token,
				RefreshToken = _createRefreshToken(),
				Expiration = accessTokenExpiration,
			};

			return accessTokenResponse;
		}

		public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey)),
				ValidateLifetime = false
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
			if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("Invalid token");

			return principal;

		}



		private string _createRefreshToken()
		{
			var numberByte = new Byte[32];

			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(numberByte);

				return Convert.ToBase64String(numberByte);
			}
		}

		private IEnumerable<Claim> _getClaims(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimType.UserId.ToString(), user.Id.ToString()),
				new Claim(ClaimType.UserName.ToString(), Uri.EscapeDataString($"{user.Name} {user.Surname}")),
				new Claim(ClaimType.RightsData.ToString(), user.UserRightsView),
			};

			return claims;
		}

		private Dictionary<int, Dictionary<int, int>> _getUserRights(User user)
		{
			//var rightDomains = _dbContext.RightDomains.AsNoTracking()
			//	.Include(d => d.UserRights.Where(r => r.UserId == user.Id))
			//	.Include(d => d.UserGroupRights.Where(r => r.UserGroupId == (user.UserGroupId ?? 0)))
			//	.Include(d => d.SystemRoleRights.Where(r => r.SystemRoleId == (user.SystemRoleId ?? 0)))
			//	.Where(d => d.IsVisible && !d.IsAlwaysFalse && !d.IsSystemOnly)
			//	.ToList();

			Dictionary<int, Dictionary<int, int>> userRights = new Dictionary<int, Dictionary<int, int>>();

			//fill rights

			return userRights;

		}




	}
}
