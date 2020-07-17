using System;

namespace CredoLoan.Business.Models {
    public class ClientReadModel {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
