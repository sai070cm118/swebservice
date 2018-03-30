using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Entities
{
    public class Response
    {
        public object data { get; set; }

        public bool IsError { get; set; }

        public List<string> errors { get; set; }

        public Response(Exception ex)
        {
            errors = new List<string>();

            data = null;
            IsError = true;
            errors.Add(ex.Message);
        }

        public Response(object resData)
        {
            data = resData;
            IsError = false;
        }
    }
}
