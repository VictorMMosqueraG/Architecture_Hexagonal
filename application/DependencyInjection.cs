namespace Application
{
    using Behaviours;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    /// <summary>
    /// Registro de dependencias de la capa Application.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registra los servicios de la capa Application: AutoMapper, FluentValidation,
        /// MediatR y el pipeline de validación.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación.</param>
        /// <returns>La misma instancia de <see cref="IServiceCollection"/> para encadenamiento.</returns>
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}