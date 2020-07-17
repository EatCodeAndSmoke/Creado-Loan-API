using CredoLoan.Business.Models;
using System;
using System.Threading.Tasks;

namespace CredoLoan.Business {
    public interface IClientService {
        Task<ClientReadModel> AuthenticateAsync(ClientAuthModel authModel);
        Task<ClientReadModel> RegisterAsync(ClientAddOrUpdateModel client, string refreshToken);
        Task<bool> UpdateClientAsync(ClientAddOrUpdateModel client);
        Task<bool> ClientExists(Guid clientId);
        Task<string> GetRefreshTokenAsync(Guid clientId);
    }
}
