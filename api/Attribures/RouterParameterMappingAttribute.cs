namespace Api.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class RouteParameterMappingAttribute(string routeParameterName, string dtoPropertyName) : Attribute
{
    public string RouteParameterName { get; } = routeParameterName;
    public string DtoPropertyName { get; } = dtoPropertyName;
}