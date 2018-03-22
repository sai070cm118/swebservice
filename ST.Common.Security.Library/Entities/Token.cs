using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Entities
{
    public class Token
    {
        public int Id { get; set; }

        public Profile Profile { get; set; }

        public string RefreshToken { get; set; }

        public string SessionToken { get; set; }

        public string UserAgent { get; set; }

        public string DeviceId { get; set; }

        public bool? IsMobile { get; set; }

        public string Details { get; set; }
    }
}
