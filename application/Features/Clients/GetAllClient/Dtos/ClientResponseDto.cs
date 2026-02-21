namespace Application.Features.Clients.GetAllClient.Dtos;

/// <summary>Datos del cliente retornados en las consultas.</summary>
/// <param name="Id">Identificador único del cliente.</param>
/// <param name="Name">Nombre completo del cliente.</param>
/// <param name="Email">Correo electrónico del cliente.</param>
/// <param name="DocumentNumber">Número de documento de identidad.</param>
/// <param name="Phone">Teléfono de contacto (opcional).</param>
/// <param name="Status">Estado actual del cliente (ej: Active, Inactive).</param>
/// <param name="CreatedAt">Fecha de creación del registro.</param>
public record ClientResponseDto(
    string Id,
    string Name,
    string Email,
    string DocumentNumber,
    string? Phone,
    string Status,
    DateTime CreatedAt
);