namespace Api.Filters
{
    using System.Reflection;
    using Api.Attributes;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class RouteParameterDocumentationFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var routeMappingAttributes = context.MethodInfo
                .GetCustomAttributes<RouteParameterMappingAttribute>()
                .ToList();

            if (routeMappingAttributes.Any() is false) return;

            foreach (var mapping in routeMappingAttributes)
            {
                var existingParameter = operation.Parameters?
                    .FirstOrDefault(p => p.Name == mapping.RouteParameterName && p.In == ParameterLocation.Path);

                if (existingParameter == null)
                {
                    operation.Parameters ??= [];
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = mapping.RouteParameterName,
                        In = ParameterLocation.Path,
                        Required = true,
                        Schema = new OpenApiSchema { Type = "string" },
                        Description = $"ID del recurso padre"
                    });
                }
            }

            if (operation.RequestBody?.Content is not null)
            {
                foreach (var contentType in operation.RequestBody.Content.Keys)
                {
                    var mediaType = operation.RequestBody.Content[contentType];
                    RemovePropertiesFromSchema(mediaType.Schema, routeMappingAttributes, context);
                }
            }
        }

        private static void RemovePropertiesFromSchema(OpenApiSchema schema, List<RouteParameterMappingAttribute> mappings, OperationFilterContext context)
        {
            if (schema == null) return;

            if (schema.Reference is not null)
            {
                var referencedSchemaId = schema.Reference.Id;
                if (context.SchemaRepository.Schemas.TryGetValue(referencedSchemaId, out var referencedSchema)) RemovePropertiesFromSchema(referencedSchema, mappings, context);
                return;
            }

            if (schema.Properties is not null)
            {
                foreach (var mapping in mappings)
                {
                    var snakeCaseProperty = ToSnakeCase(mapping.DtoPropertyName);

                    if (schema.Properties.ContainsKey(snakeCaseProperty))
                    {
                        schema.Properties.Remove(snakeCaseProperty);
                        schema.Required?.Remove(snakeCaseProperty);

                    }
                }
            }
        }

        private static string ToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + char.ToLower(x) : char.ToLower(x).ToString()));
        }
    }
}