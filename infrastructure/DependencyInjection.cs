namespace Infrastructure;

using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Persistence.MongoDB.Clients;
using Infrastructure.Persistence.MongoDB.Invoices;
using Infrastructure.Persistence.MongoDB.Reminder;
using Infrastructure.Service.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClientRepository, MongoClientRepository>();
        services.AddScoped<IInvoiceRepository, MongoInvoiceRepository>();
        services.AddScoped<IClientRepository,      MongoClientRepository>();
        services.AddScoped<IInvoiceRepository,     MongoInvoiceRepository>();
        services.AddScoped<IReminderLogRepository, MongoReminderLogRepository>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}