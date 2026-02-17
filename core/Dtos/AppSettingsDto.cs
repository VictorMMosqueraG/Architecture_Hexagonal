namespace Core.Dtos;

public class SecretAppSettingDto
{
    public string? DataBase { get; set; }
    public string? MinIO { get; set; }
    public string? Keycloak { get; set; }
    public string? RabbitMQ { get; set; }
    public string? SQS { get; set; }
}

public class KeycloakSecretDto { }
