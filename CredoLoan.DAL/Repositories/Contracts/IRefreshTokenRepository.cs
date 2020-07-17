using CredoLoan.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace CredoLoan.DAL {
    public interface IRefreshTokenRepository {
        Task<ClientRefreshToken> AddAsync(ClientRefreshToken clientRefreshToken);
        Task<ClientRefreshToken> GetByIdAsync(Guid id);
    }
}
