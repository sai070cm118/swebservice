//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ST.Common.Security.Library.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        public string C_id { get; set; }
        public string Email { get; set; }
        public string RegistrationIP { get; set; }
        public System.DateTime RegistrationTime { get; set; }
        public string EmailVerification { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Mobile { get; set; }
        public string TempMobile { get; set; }
        public string MobileVerificationOTP { get; set; }
        public string GooglePlus { get; set; }
        public string FacebookID { get; set; }
        public string KeepMe { get; set; }
        public Nullable<bool> RecoverType { get; set; }
        public string RecoverHash { get; set; }
        public Nullable<System.DateTime> RecoverTimeStamp { get; set; }
    
        public virtual profile profile { get; set; }
    }
}
