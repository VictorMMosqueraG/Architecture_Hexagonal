namespace Infrastructure;

using System;
using Application.Features.Clients.GetAllClient.Queries;
using Core.Interfaces.Repositories;
using Infrastructure.Persistence.MongoDB.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClientRepository, MongoClientRepository>();
        services.AddScoped<GetAllClientQueryHandler>();
        return services;
    }
}