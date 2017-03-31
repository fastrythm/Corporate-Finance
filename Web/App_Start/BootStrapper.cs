using Autofac;
using Autofac.Integration.Mvc;
using CorporateAndFinance.Core.Model;
using CorporateAndFinance.Data.Infrastructure;
using CorporateAndFinance.Data.Repositoreis;
using CorporateAndFinance.Data.Repositories;
using CorporateAndFinance.Service.Implementation;
using CorporateAndFinance.Web.Mappings;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web;

namespace CorporateAndFinance.Web.App_Start
{
    public static class Bootstrapper
    {
        public static void Run(IAppBuilder app)
        {
            SetAutofacContainer(app);
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            // Services.
            builder.RegisterAssemblyTypes(typeof(PettyCashManagement).Assembly)
               .Where(t => t.Name.EndsWith("Management"))
               .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(BankPositionManagement).Assembly)
              .Where(t => t.Name.EndsWith("Management"))
              .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ComplianceManagement).Assembly)
            .Where(t => t.Name.EndsWith("Management"))
            .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(CompanyManagement).Assembly)
           .Where(t => t.Name.EndsWith("Management"))
           .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(UserCardExpenseManagement).Assembly)
          .Where(t => t.Name.EndsWith("Management"))
          .AsImplementedInterfaces().InstancePerRequest();
           builder.RegisterAssemblyTypes(typeof(UserCardManagement).Assembly)
        .Where(t => t.Name.EndsWith("Management"))
        .AsImplementedInterfaces().InstancePerRequest();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(PettyCashRepository).Assembly)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(BankPositionRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ComplianceRepository).Assembly)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(CompanyRepository).Assembly)
             .Where(t => t.Name.EndsWith("Repository"))
             .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(UserCardExpenseRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(UserCardRepository).Assembly)
          .Where(t => t.Name.EndsWith("Repository"))
          .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}