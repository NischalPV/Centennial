using System;
using Autofac;
using Centennial.Api.Infrastructure.Idempotency;
using Centennial.Api.Interfaces;
using Centennial.Api.Repositories;

namespace Centennial.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {

        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterType<CustomerService>()
                .As<ICustomerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmployeeService>()
                .As<IEmployeeRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RawMaterialService>()
                .As<IRawMaterialRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockRecordService>()
                .As<IStockRecordRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MaterialService>()
               .As<IMaterialRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>()
               .As<IProductRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
               .As<IRequestManager>()
               .InstancePerLifetimeScope();

        }
    }
}
