using AuthorizationServer.Services.Clients;
using AuthorizationServer.Services.Emails;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Autofac
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<ClientService>().As<IClientService>().InstancePerLifetimeScope();
        }
    }
}
