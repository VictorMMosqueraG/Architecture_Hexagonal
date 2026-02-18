namespace Infrastructure;

using Core.Interfaces.Repositories;
using Infrastructure.Persistence.MongoDB.Clients;
using Infrastructure.Persistence.MongoDB.Invoices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClientRepository, MongoClientRepository>();
        services.AddScoped<IInvoiceRepository, MongoInvoiceRepository>();

        return services;
    }
}