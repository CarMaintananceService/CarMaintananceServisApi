using Api.Lib;
using Business;
using Business.Security.Users;
using Business.Shared;
using Core.Security;
using EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		JwtEngineUser _jwtEngine;
		UserLoginEngine _userLoginEngine;
		protected readonly TokenOptions _tokenOptions;
		protected readonly CurrentServiceProvider _serviceProvider;
		public ApplicationDbContext _dbContext { get; protected set; }

		public LoginController(
			JwtEngineUser jwtEngine,
			UserLoginEngine userLoginEngine,
			IOptions<TokenOptions> tokenOptions,
			CurrentServiceProvider serviceProvider
		)
		{
			_tokenOptions = tokenOptions.Value;
			_userLoginEngine = userLoginEngine;
			_jwtEngine = jwtEngine;
			_serviceProvider = serviceProvider;
			_dbContext = serviceProvider.GetDbContext() as ApplicationDbContext;
		}


		[HttpPost]
		public async Task<TResponse<AccessTokenResponse>> GenerateAccesstoken(LoginInput loginInput)
		{
			User user = await _userLoginEngine.GetByUserName(loginInput.UserName);

			if (user == null)
				return new TResponse<AccessTokenResponse>("Invalid user info");
			else if (!user.IsActive)
				return new TResponse<AccessTokenResponse>(new AccessTokenResponse() { IsActive = false });

#if (!DEBUG)
			if (user.Password != new Business.Lib.TCryptor().mGetEncryptedValue(loginInput.Password))
			{
				//log wrong input
				return new TResponse<AccessTokenResponse>("Invalid user info");
			}
#endif
			AccessTokenResponse accessTokenResponse = _jwtEngine.CreateAccessToken(user);
			
			await _userLoginEngine.UpdateTokenInfo(user.Id, accessTokenResponse.AccessToken.RefreshToken, DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration), user.UserRightsView);

			return new TResponse<AccessTokenResponse>(accessTokenResponse);
		}


		[HttpPost]
		public async Task<TResponse<AccessTokenResponse>> RefreshToken(AccessToken accessToken)
		{
			var principal = _jwtEngine.GetPrincipalFromExpiredToken(accessToken.Token);
			if (principal == null)
				return new TResponse<AccessTokenResponse>("Invalid access token or refresh token");

			string userId = principal.Claims.Where(c => c.Type == ClaimType.UserId.ToString()).FirstOrDefault()?.Value;

			User user = await _userLoginEngine.GetById(Convert.ToInt32(userId));

			if (user == null)
				return new TResponse<AccessTokenResponse>("Invalid access token or refresh token");

            if (user.RefreshToken != accessToken.RefreshToken)
                return new TResponse<AccessTokenResponse>("Invalid refresh token");

            AccessTokenResponse accessTokenResponse = null;

			if (!user.IsActive)
			{
				accessTokenResponse = new AccessTokenResponse();
				accessTokenResponse.IsActive = false;
				return new TResponse<AccessTokenResponse>(accessTokenResponse);
			}
			else if (user.RefreshTokenExpireIn < DateTime.Now)
			{
				accessTokenResponse = new AccessTokenResponse();
				accessTokenResponse.IsExpired = true;
				return new TResponse<AccessTokenResponse>(accessTokenResponse);
			}

			accessTokenResponse = _jwtEngine.RefreshToken(user);
			await _userLoginEngine.UpdateTokenInfo(user.Id, accessTokenResponse.AccessToken.RefreshToken, DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration), user.UserRightsView);

			return new TResponse<AccessTokenResponse>(accessTokenResponse);
		}


		[HttpPost]
		public async Task RevokeRefreshToken(AccessToken accessToken)
		{
			var principal = _jwtEngine.GetPrincipalFromExpiredToken(accessToken.Token);
			if (principal == null)
				throw new Exception("Invalid access token or refresh token");

			string userId = principal.Claims.Where(c => c.Type == ClaimType.UserId.ToString()).FirstOrDefault()?.Value;

			await _userLoginEngine.RevokeRefreshToken(Convert.ToInt32(userId));
		}

	}
}
