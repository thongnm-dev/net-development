using Microsoft.IdentityModel.Tokens;
using Net.Core.Configuration;
using AppReviewService.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace AppReviewService.Authentication.Impl
{
    internal class TokenProviderService : Net.Core.Authentication.ITokenProviderService
    {
        #region Variable
        private readonly IUserservice _Userservice;
        private readonly AppSettings _appSettings;
        #endregion

        #region CTor
        public TokenProviderService(
            AppSettings iAppSettings,
            IUserservice iUserservice)
        {
            _appSettings = iAppSettings;
            _Userservice = iUserservice;
        }
        #endregion

        public async Task<bool> IsValidTokenAsync(string token)
        {
            // get security config from app settings
            var _securityConfig = _appSettings.Get<SecurityConfig>();

            var tokenHandle = new JwtSecurityTokenHandler();

            // valid token with security key and Algogithm
            var tokenValidationResult = await tokenHandle.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.FromDays(_securityConfig.ExpireDate),
                IssuerSigningKey = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityConfig.SecurityKey)), _securityConfig.Algorithm).Key
            });

            // Token has been accepted after check
            if (tokenValidationResult.IsValid)
            {
                var ClaimsIdentity = tokenValidationResult.ClaimsIdentity;
                // If user identity existed in system
                var userId = ClaimsIdentity.Claims.Where(claim => claim.Type.Equals(AuthenticationDefaults.ClaimUserId)).FirstOrDefault()?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    return await _Userservice.IsExistUserByIdAsync(Int32.Parse(userId));
                }
            }
            return await Task.FromResult(false);
        }

        public async Task<string> GenerateTokenAsync(string userName, string password)
        {
            var user = await _Userservice.GetUserByUserNameOrEmailAsync(userName);

            if (user == null)
            {
                return String.Empty;
            }

            // get security config from app settings
            var _securityConfig = _appSettings.Get<SecurityConfig>();
            var sysDate = DateTimeOffset.UtcNow;

            // expire time of token
            var expirationTime = sysDate.AddMinutes(_appSettings.Get<ApiConfig>().TokenExpireTimeMiniutes);

            var claims = new List<Claim>
            {
                new Claim(AuthenticationDefaults.ClaimUserId, user.Id.ToString()),
                new Claim(AuthenticationDefaults.ClaimUserGuid, user.UserGuid.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, sysDate.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, expirationTime.ToUnixTimeSeconds().ToString()),
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityConfig.SecurityKey)), _securityConfig.Algorithm);

            var token = new JwtSecurityToken(new JwtHeader(signingCredentials), new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
