using System;
using System.Collections.Generic;
using System.Text;

namespace CredoLoan.Business.Models {
    public class ClientAddOrUpdateModel : ClientReadModel {
        public string Password { get; set; }
    }
}
