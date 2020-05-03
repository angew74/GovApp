﻿using Autofac;
using Gov.Structure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GovApp
{
    public class AutofacGovModule : Autofac.Module
    {
        private string connectionString;

        public AutofacGovModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method. All of this starts
            // with the services.AddAutofac() that happens in Program and registers Autofac
            // as the service provider.
            /*   var assemblies = Assembly.GetExecutingAssembly();          
               builder.RegisterAssemblyTypes(assemblies)
               .PublicOnly();*/
            AppDomain.CurrentDomain.Load("Gov.Core");
            AppDomain.CurrentDomain.Load("Gov.Structure");
            AppDomain.CurrentDomain.Load("GovApp");
          //  builder.RegisterType<ElezioniCookieAuthenticationEvents>().As<CookieAuthenticationEvents>();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies().Where(t => t.FullName.Contains("Gov")).ToArray())
                .AsImplementedInterfaces()
                 .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Controller"));
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<GovContext>().UseMySql(connectionString, x => x.MigrationsAssembly("Gov.Structure"));
            builder.RegisterType(typeof(GovContext)).As(typeof(IContext))
                 .WithParameter("options", dbContextOptionsBuilder.Options)
                .InstancePerLifetimeScope();
          

          /*  builder.RegisterType<GovContext>()
                .WithParameter("options", dbContextOptionsBuilder.Options)
                .InstancePerLifetimeScope();*/
        }
    }
}
