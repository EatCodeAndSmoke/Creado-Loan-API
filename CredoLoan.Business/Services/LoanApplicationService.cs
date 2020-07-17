using AutoMapper;
using CredoLoan.Business.Models;
using CredoLoan.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CredoLoan.Domain.Entities;
using System.Linq;
using CredoLoan.Domain.Enums;

namespace CredoLoan.Business {
    internal class LoanApplicationService : ILoanApplicationService {

        private readonly ILoanApplicationRepository _repository;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public LoanApplicationService(ILoanApplicationRepository repository, IClientService clientService, IMapper mapper) {
            _repository = repository;
            _clientService = clientService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LoanApplicationModel>> GetByClientIdAsync(Guid clientId) {
            return await Task.Factory.StartNew(() => {
                var query = from app in _repository.Query
                            where app.ClientId == clientId
                            orderby app.Created descending
                            select app;

                List<LoanApplicationModel> result = new List<LoanApplicationModel>();
                foreach (var item in query) 
                    result.Add(_mapper.Map<LoanApplicationModel>(item));
                return result;
            });
        }

        public async Task<LoanApplicationModel> GetByIdAsync(Guid id) {
            var application = await _repository.GetAsync(l => l.Id == id);
            return application == null ? null : _mapper.Map<LoanApplicationModel>(application);
        }

        public async Task<LoanApplicationModel> RegisterApplicationAsync(LoanApplicationModel loanApplication, Guid clientId) {
            if (!await _clientService.ClientExists(clientId))
                return null;

            var applicationToSave = _mapper.Map<LoanApplication>(loanApplication);
            applicationToSave.Id = Guid.NewGuid();
            applicationToSave.Status = LoanApplicationStatus.Sent;
            applicationToSave.ClientId = clientId;
            await _repository.AddAsync(applicationToSave);
            return _mapper.Map<LoanApplicationModel>(applicationToSave);
        }

        public async Task<int> UpdateApplicationAsync(LoanApplicationModel loanApplication) {
            LoanApplicationStatus appStatus = (LoanApplicationStatus)loanApplication.Status;
            if (appStatus == LoanApplicationStatus.Approved || appStatus == LoanApplicationStatus.Denied)
                return -1;

            var applicationToUpdate = _mapper.Map<LoanApplication>(loanApplication);
            var updatableProps = new[] { nameof(LoanApplication.Currency),
                                         nameof(LoanApplication.LoanAmount),
                                         nameof(LoanApplication.Period),
                                         nameof(LoanApplication.Type),
                                      };
            bool updated = await _repository.UpdateAsync(applicationToUpdate, updatableProps);
            return updated ? 1 : 0;
        }
    }
}
