using System;

namespace CredoLoan.Business.Models {
    public class LoanApplicationModel {
        public Guid Id { get; set; }
        public byte LoanType { get; set; }
        public decimal LoanAmount { get; set; }
        public byte Currency { get; set; }
        public int Period { get; set; }
        public byte Status { get; set; }
    }
}
