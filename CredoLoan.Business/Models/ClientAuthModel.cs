using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CredoLoan.Business.Models {
    public class ClientAuthModel {
        public string PersonalId { get; set; }
        public string Password { get; set; }
    }
}
