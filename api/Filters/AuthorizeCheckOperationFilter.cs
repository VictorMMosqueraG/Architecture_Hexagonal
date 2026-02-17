namespace Api.Filters
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class AuthorizeCheckOperationFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var methodAttributes = context.MethodInfo.GetCustomAttributes(true);
            var controllerAttributes = context.MethodInfo.DeclaringType?.GetCustomAttributes(true) ?? Array.Empty<object>();

            var hasAuthorizeAttribute = methodAttributes.OfType<AuthorizeAttribute>().Any() || controllerAttributes.OfType<AuthorizeAttribute>().Any();

            if (hasAuthorizeAttribute)
            {
                operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            };
            }
            else
            {
                operation.Security = null;
            }
        }
    }
}