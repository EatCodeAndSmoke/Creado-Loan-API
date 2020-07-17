using CredoLoan.Business;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CredoLoan.Api.Tokens {
    public class JwtTokenManager : IJwtTokenManager {

        private readonly IConfiguration _config;
        private readonly IClientService _clientService;
        private readonly TokenValidationParameters _parameters;

        public JwtTokenManager(IConfiguration config, IClientService clientService, TokenValidationParameters parameters) {
            _config = config;
            _clientService = clientService;
            _parameters = parameters;
            _parameters.ValidateLifetime = false;
        }

        public string GenerateAccessToken(Guid clientId) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecurityKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               issuer: _config["Jwt:Issuer"],
               audience: _config["Jwt:Audience"],
               claims: new Claim[] {
                 new Claim(ClaimTypes.NameIdentifier, clientId.ToString())
               },
               expires: DateTime.Now.Add(TimeSpan.Parse(_config["Jwt:TokenLifeTime"])),
               signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken() {
            return Guid.NewGuid().ToString();
        }

        public async Task<string> ValidateRefreshTokenAsync(string accessToken, string refreshToken) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(
                    token: accessToken,
                    validationParameters: _parameters,
                    out SecurityToken validatedToken
                );

            if (!(validatedToken is JwtSecurityToken validatedJwtToken) || !validatedJwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                throw new SecurityTokenException("invalid token");
            if (!principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                throw new SecurityTokenException("invalid token");

            string clientId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(clientId))
                throw new SecurityTokenException("invalid token");

            Guid parsedClientId = Guid.Parse(clientId);
            string refTokenFromDb = await _clientService.GetRefreshTokenAsync(parsedClientId);
            if (!refreshToken.Equals(refTokenFromDb))
                throw new SecurityTokenException("invalid token");

            return GenerateAccessToken(parsedClientId);
        }
    }
}
