using System;

namespace CredoLoan.Domain.Entities {
    public class ClientRefreshToken {
        public Guid ClientId { get; set; }
        public string RefreshToken { get; set; }
    }
}
