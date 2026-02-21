namespace Application.Features.Invoices.GetAllInvoice.Dtos;

/// <summary>Datos de la factura retornados en las consultas.</summary>
/// <param name="Id">Identificador único de la factura.</param>
/// <param name="ClientId">ID del cliente al que pertenece la factura.</param>
/// <param name="InvoiceNumber">Número único de la factura.</param>
/// <param name="Amount">Monto total de la factura.</param>
/// <param name="DueDate">Fecha de vencimiento de la factura.</param>
/// <param name="Status">Estado actual de la factura (ej: Pending, Paid, Overdue).</param>
/// <param name="Description">Descripción opcional de la factura.</param>
/// <param name="CreatedAt">Fecha de creación del registro.</param>
/// <param name="UpdatedAt">Fecha de última actualización del registro.</param>
public record InvoiceResponseDto(
    string Id,
    string ClientId,
    string InvoiceNumber,
    decimal Amount,
    DateTime DueDate,
    string Status,
    string? Description,
    DateTime CreatedAt,
    DateTime UpdatedAt
);