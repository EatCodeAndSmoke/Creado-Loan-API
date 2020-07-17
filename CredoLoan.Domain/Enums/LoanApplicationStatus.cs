using System;
using System.Collections.Generic;
using System.Text;

namespace CredoLoan.Domain.Enums {
    public enum LoanApplicationStatus : byte {
        Sent,
        InProcessing,
        Approved,
        Denied
    }
}
