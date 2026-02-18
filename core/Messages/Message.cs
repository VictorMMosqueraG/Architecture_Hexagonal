namespace Core.Messages;

public static class Message
{
    public const string GetAllData = "Se obtuvieron todo los datos con exito";
    public const string InternalServerError = "Ocurrio un error durante la ejecucion.";
    public const string ErrorMappingEnviroment = "Error de mapeo de infraestructura. Resultado";
    public const string ErrorInizialiteMongoDB = "MongoDB:ConnectionString no configurado";
    public static string EntityCreateSuccess(string entity) => $"Se creo un registro exitoso para la entidad {entity}";
    public static string AlreadyExist(string entity, string value) => $"Ya existe un {entity} con el {value} ";
    public static string NotFoundEntity(string entity, string value) => $"{entity} {value} no encontrado";
    public static string EmailSentValid = "Se finalizo el proceso de enviar correos";
    public static string InvalidSupportData(string value) => $"Tipo de recordatorio no soportado {value}";
}