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
    
    public partial class profile
    {
        public string C_id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePic { get; set; }
        public string Location { get; set; }
        public bool Live { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public int AccountType { get; set; }
    
        public virtual user user { get; set; }
    }
}
