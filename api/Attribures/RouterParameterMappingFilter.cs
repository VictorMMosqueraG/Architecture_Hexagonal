namespace Api.Attributes;

 using Microsoft.AspNetCore.Mvc.Filters;

    public class RouteParameterMappingFilter: IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var routeMappingAttributes = context.ActionDescriptor.EndpointMetadata
                .OfType<RouteParameterMappingAttribute>()
                .ToList();

            if (routeMappingAttributes.Any() is false) return;

            foreach (var parameter in context.ActionArguments.ToList())
            {
                var parameterValue = parameter.Value;
                if (parameterValue == null) continue;

                var parameterType = parameterValue.GetType();
                var hasBeenModified = false;

                foreach (var mapping in routeMappingAttributes)
                {
                    if (context.RouteData.Values.TryGetValue(mapping.RouteParameterName, out var routeValue))
                    {
                        var property = parameterType.GetProperty(mapping.DtoPropertyName);
                        if (property != null && property.CanWrite)
                        {
                            var convertedValue = ConvertValue(routeValue, property.PropertyType);
                            property.SetValue(parameterValue, convertedValue);
                            hasBeenModified = true;
                        }
                    }
                }

                if (hasBeenModified) context.ActionArguments[parameter.Key] = parameterValue;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        private static object? ConvertValue(object? value, Type targetType)
        {
            if (value == null) return null;

            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (underlyingType == typeof(Guid)) return Guid.Parse(value.ToString()!);
            else if (underlyingType == typeof(int)) return int.Parse(value.ToString()!);
            else if (underlyingType == typeof(long)) return long.Parse(value.ToString()!);

            return Convert.ChangeType(value, underlyingType);
        }
    }