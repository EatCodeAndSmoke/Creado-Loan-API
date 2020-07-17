using System;
using System.Collections.Generic;
using System.Text;

namespace CredoLoan.Business {
    internal interface IPasswordService {
        string ComputeHash(string password);
        bool Match(string password, string hash);
    }
}
