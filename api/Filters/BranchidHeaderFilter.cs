namespace Api.Filters
{
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class BranchIdHeaderFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Branch-ID",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "string" },
                Description = "Identificador de la sucursal (opcional)"
            });
        }
    }
}