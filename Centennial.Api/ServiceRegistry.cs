using System;
using Centennial.Api.Interfaces;
using Centennial.Api.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Centennial.Api
{
    public static class ServiceRegistry
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
            services.AddScoped<ICustomerRepository, CustomerService>();
            services.AddScoped<IProductRepository, ProductService>();
            services.AddScoped<IMaterialRepository, MaterialService>();
            services.AddScoped<IEmployeeRepository, EmployeeService>();
            services.AddScoped<IRawMaterialRepository, RawMaterialService>();

            return services;
        }
    }
}
