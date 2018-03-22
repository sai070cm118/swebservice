using Autofac;
using Autofac.Integration.WebApi;
using ST.Common.Logger;
using ST.Common.Security.Library.Repositories;
using ST.Common.Security.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ST.UserManagement.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.EnableCors(new EnableCorsAttribute("*", "*", "*") { SupportsCredentials = true });
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new RepositoryModule());


            LoggerModule.RegisterConfigFile("ErrorLogger.config");
            builder.RegisterModule(new LoggerModule());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(
                container);

        }
    }
}
