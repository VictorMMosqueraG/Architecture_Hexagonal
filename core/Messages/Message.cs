namespace Core.Messages;

public static class Message
{
    public const string GetAllData = "Se obtuvieron todo los datos con exito";
    public const string InternalServerError = "Ocurrio un error durante la ejecucion.";
    public const string ErrorMappingEnviroment = "Error de mapeo de infraestructura. Resultado";
    public const string ErrorInizialiteMongoDB = "MongoDB:ConnectionString no configurado";
}