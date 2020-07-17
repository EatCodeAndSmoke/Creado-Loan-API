using System;
using System.Collections.Generic;
using System.Text;

namespace CredoLoan.Business.Models {
    public class RefreshTokenModel {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
