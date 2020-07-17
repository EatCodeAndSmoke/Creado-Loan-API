using CredoLoan.Domain.Enums;
using System;

namespace CredoLoan.Domain.Entities {
    public class LoanApplication {
        public Guid Id { get; set; }
        public LoanType Type { get; set; }
        public decimal LoanAmount { get; set; }
        public LoanCurrency Currency { get; set; }
        public int Period { get; set; }
        public DateTime Created { get; set; }
        public LoanApplicationStatus Status { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
