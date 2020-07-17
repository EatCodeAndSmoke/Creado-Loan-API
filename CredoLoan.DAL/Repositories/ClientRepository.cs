using CredoLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CredoLoan.DAL {
    internal class ClientRepository : BaseRepository, IClientRepository {

        private readonly CredoLoanDbContext _context;

        public ClientRepository(CredoLoanDbContext context) : base(context) {
            _context = context;
        }

        public async Task<Client> GetAsync(Expression<Func<Client, bool>> expression) {
            return await _context.Clients.FirstOrDefaultAsync(expression);
        }

        public async Task<Client> AddAsync(Client client) {
            return await AddEntityAsync(client);
        }

        public async Task<bool> UpdateAsync(Client client, params string[] propsToUpdate) {
            return await UpdateEntityAsync(client, propsToUpdate);
        }

        public async Task<bool> AnyAsync(Expression<Func<Client, bool>> expression) {
            return await _context.Clients.AnyAsync(expression);
        }

        public Task BeginTransactionAsync() {
            //await _context.Database.BeginTransactionAsync();
            return Task.CompletedTask;
        }

        public void RollbackTransaction() {
            //_context.Database.RollbackTransaction();
        }

        public void CommitTransaction() {
            //_context.Database.CommitTransaction();
        }
    }
}
