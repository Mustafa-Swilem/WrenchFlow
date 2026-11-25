using Microsoft.Extensions.DependencyInjection;
using WrenchFlow.DAL.Repositories.Implementations;
using WrenchFlow.DAL.Repositories.Interfaces;

namespace WrenchFlow.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        return services;
    }
}