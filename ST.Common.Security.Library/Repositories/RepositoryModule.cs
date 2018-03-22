using Autofac;
using ST.Common.Security.Library.Repositories.Implementation;
using ST.Common.Security.Library.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.Common.Security.Library.Repositories
{ 
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(UserRepository)).As(typeof(IUserRepository)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(ProfileRepository)).As(typeof(IProfileRepository)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(TokenRepository)).As(typeof(ITokenRepository)).InstancePerLifetimeScope();
        }
    }
}
