namespace Application.Features.Invoices.CreateInvoice.Dtos;

/// <summary>Datos requeridos para crear una nueva factura.</summary>
public class CreateInvoiceRequestDto
{
    /// <summary>ID del cliente al que pertenece la factura.</summary>
    /// <example>64f1a2b3c4d5e6f7a8b9c0d1</example>
    public required string ClientId { get; set; }

    /// <summary>Número único de la factura.</summary>
    /// <example>INV-2026-0001</example>
    public required string InvoiceNumber { get; set; }

    /// <summary>Monto total de la factura.</summary>
    /// <example>150000.00</example>
    public decimal Amount { get; set; }

    /// <summary>Fecha de vencimiento de la factura.</summary>
    /// <example>2026-06-30</example>
    public DateTime DueDate { get; set; }

    /// <summary>Descripción opcional de la factura.</summary>
    /// <example>Servicios de consultoría mes de marzo</example>
    public string? Description { get; set; }
}