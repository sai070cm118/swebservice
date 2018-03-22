using Autofac;
using ST.Common.Security.Library.Services.Implementation;
using ST.Common.Security.Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Services
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(UserService)).As(typeof(IUserService)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(ProfileService)).As(typeof(IProfileService)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(TokenService)).As(typeof(ITokenService)).InstancePerLifetimeScope();
        }
    }
}
