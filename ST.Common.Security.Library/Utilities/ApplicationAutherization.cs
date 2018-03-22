using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace ST.Common.Security.Library.Utilities
{
    public class ApplicationAutherizationAttribute : System.Web.Http.AuthorizeAttribute
    {

        public override void OnAuthorization(
           System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var autherizationHeader = actionContext.Request.Headers.Authorization?.Scheme;

            return true;
        }
    }
}
