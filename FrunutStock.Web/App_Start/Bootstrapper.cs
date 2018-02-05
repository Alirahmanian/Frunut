using Autofac;
using Autofac.Integration.Mvc;
using FrunutStock.Data.Infrastructure;
using FrunutStock.Data.Repositories;
using FrunutStock.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace FrunutStock.Web.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            //Configure AutoMapper
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

           //  Repositories
            builder.RegisterAssemblyTypes(typeof(OrderRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(OrderDetailRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(CompanyRepository).Assembly)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(EmployeeRepository).Assembly)
             .Where(t => t.Name.EndsWith("Repository"))
             .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ItemGroupRepository).Assembly)
             .Where(t => t.Name.EndsWith("Repository"))
             .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ItemRepository).Assembly)
             .Where(t => t.Name.EndsWith("Repository"))
             .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(WarehouseRepository).Assembly)
             .Where(t => t.Name.EndsWith("Repository"))
             .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(OrderService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(OrderDetailService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(CompanyService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(EmployeeService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ItemGroupService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ItemService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(WarehouseService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();


            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}