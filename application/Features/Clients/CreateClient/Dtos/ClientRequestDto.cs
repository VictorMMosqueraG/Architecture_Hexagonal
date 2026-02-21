
namespace Application.Features.Clients.CreateClient.Dtos;

/// <summary>Datos para crear un cliente.</summary>
public class CreateClientRequestDto
{
    /// <summary>Nombre completo del cliente.</summary>
    /// <example>Juan Pérez</example>
    public required string Name { get; set; }

    /// <summary>Correo electrónico único.</summary>
    /// <example>juan.perez@email.com</example>
    public required string Email { get; set; }

    /// <summary>Número de documento de identidad.</summary>
    /// <example>1234567890</example>
    public required string DocumentNumber { get; set; }

    /// <summary>Teléfono de contacto (opcional).</summary>
    /// <example>3001234567</example>
    public string? Phone { get; set; }
}