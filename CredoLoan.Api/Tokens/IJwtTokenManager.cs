using System;
using System.Threading.Tasks;

namespace CredoLoan.Api.Tokens {
    public interface IJwtTokenManager {
        string GenerateAccessToken(Guid clientId);
        string GenerateRefreshToken();
        Task<string> ValidateRefreshTokenAsync(string accessToken, string refreshToken);
    }
}
