namespace Application.Features.Clients.GetByIdClient.Dtos;

/// <summary>Parámetro de identificador para consultas por ID.</summary>
public class ParamIdDto
{
    /// <summary>Identificador único del recurso.</summary>
    /// <example>64f1a2b3c4d5e6f7a8b9c0d1</example>
    public required string Id { get; set; }
}