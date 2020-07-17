using CredoLoan.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CredoLoan.DAL {
    public interface ILoanApplicationRepository {
        IQueryable<LoanApplication> Query { get; }
        Task<LoanApplication> GetAsync(Expression<Func<LoanApplication, bool>> expression);
        Task<LoanApplication> AddAsync(LoanApplication loanApplication);
        Task<bool> UpdateAsync(LoanApplication loanApplication, params string[] propsToUpdate);
    }
}
