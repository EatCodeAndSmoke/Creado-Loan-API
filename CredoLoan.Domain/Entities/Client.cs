using System;
using System.Collections.Generic;

namespace CredoLoan.Domain.Entities {
    public class Client {

        public Client() {
            Applications = new HashSet<LoanApplication>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public string PasswordHash { get; set; }
        public string RefreshToken { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }

        public ICollection<LoanApplication> Applications { get; set; }
    }
}
