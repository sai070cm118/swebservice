using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string RegistrationIP { get; set; }
        public DateTime RegistrationTime { get; set; }
        public string EmailVerification { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Mobile { get; set; }
        public string TempMobile { get; set; }
        public string MobileVerificationOTP { get; set; }
        public string GooglePlus { get; set; }
        public string FacebookID { get; set; }
        public string KeepMe { get; set; }
        public bool? RecoverType { get; set; }
        public string RecoverHash { get; set; }
        public DateTime? RecoverTimeStamp { get; set; }

    }   
}
