using Microsoft.Extensions.DependencyInjection;
using WrenchFlow.BLL.Services.Implementations;
using WrenchFlow.BLL.Services.Interfaces;

namespace WrenchFlow.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<ISpecializationService, SpecializationService>();
        return services;
    }
}