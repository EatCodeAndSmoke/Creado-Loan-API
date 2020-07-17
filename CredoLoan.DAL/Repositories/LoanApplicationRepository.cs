using CredoLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CredoLoan.DAL {
    internal class LoanApplicationRepository : BaseRepository, ILoanApplicationRepository {

        private readonly CredoLoanDbContext _context;

        public LoanApplicationRepository(CredoLoanDbContext context) : base(context) {
            _context = context;
        }

        public IQueryable<LoanApplication> Query => _context.LoanApplications;

        public async Task<LoanApplication> GetAsync(Expression<Func<LoanApplication, bool>> expression) {
            return await _context.LoanApplications.FirstOrDefaultAsync(expression);
        }

        public async Task<LoanApplication> AddAsync(LoanApplication loanApplication) {
            return await AddEntityAsync(loanApplication);
        }

        public async Task<bool> UpdateAsync(LoanApplication loanApplication, params string[] propsToUpdate) {
            return await UpdateEntityAsync(loanApplication, propsToUpdate);
        }
    }
}
