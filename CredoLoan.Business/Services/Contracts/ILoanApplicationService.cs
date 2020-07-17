using CredoLoan.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CredoLoan.Business {
    public interface ILoanApplicationService {
        Task<LoanApplicationModel> GetByIdAsync(Guid id);
        Task<int> UpdateApplicationAsync(LoanApplicationModel loanApplication);
        Task<LoanApplicationModel> RegisterApplicationAsync(LoanApplicationModel loanApplication, Guid clientId);
        Task<IEnumerable<LoanApplicationModel>> GetByClientIdAsync(Guid clientId);
    }
}
