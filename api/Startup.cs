namespace Api
{
    using Api.Attributes;
    using Api.Binders;
    using Api.Configuration;
    using Api.Configurations;
    using Api.Filters;
    using Api.Middlewares;
    using Core.Dtos.AppSettingDto;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using System.Text.Json;
    using System.Text.RegularExpressions;

    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDependencyInjection(Configuration);

            ValidatorOptions.Global.PropertyNameResolver = (type, memberInfo, expression) => Regex.Replace(memberInfo!.Name, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
            services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new SnakeModelBinderProvider());
                options.Filters.Add<RouteParameterMappingFilter>();
                options.Filters.Add<ApiExceptionFilterAttribute>();
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower;
            });
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddDataProtection();
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddSwaggerConfiguration();
            services.AddApiVersioningConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<SwaggerSettingDto> swaggerSettings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                // app.UseHttpsRedirection(); only prod
            }

            

            app.UseCors("AllowAll");
            app.UseMiddleware<HeaderValidationMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.AddSwaggerConfiguration(swaggerSettings);
        }
    }


}

