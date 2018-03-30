using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Entities
{
    public class Profile
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePic { get; set; }
        public string Location { get; set; }
        public bool? Live { get; set; }
        public bool? IsActive { get; set; }
        public ProfileStatus? Status { get; set; }
        public int? AccountType { get; set; }

        public User User { get; set; }
        public Token Token { get; set; }
    }
}
