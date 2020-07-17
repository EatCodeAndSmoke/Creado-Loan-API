using CredoLoan.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CredoLoan.DAL {
    public interface IClientRepository {
        Task<bool> AnyAsync(Expression<Func<Client, bool>> expression);
        Task<Client> GetAsync(Expression<Func<Client, bool>> expression);
        Task<Client> AddAsync(Client client);
        Task<bool> UpdateAsync(Client client, params string[] propsToUpdate);
        Task BeginTransactionAsync();
        void RollbackTransaction();
        void CommitTransaction();
    }
}
