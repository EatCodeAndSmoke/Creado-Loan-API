using CredoLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CredoLoan.DAL {
    class RefreshTokenRepository : BaseRepository, IRefreshTokenRepository {

        private readonly CredoLoanDbContext _context;

        public RefreshTokenRepository(CredoLoanDbContext context) : base(context) {
            _context = context;
        }

        public async Task<ClientRefreshToken> AddAsync(ClientRefreshToken clientRefreshToken) {
            return await AddEntityAsync(clientRefreshToken);
        }

        public async Task<ClientRefreshToken> GetByIdAsync(Guid id) {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.ClientId == id);
        }
    }
}
