namespace Application.Features.Invoices.UpdateStatusInvoice.Dtos;

/// <summary>Parámetro de identificador de factura para actualización de estado.</summary>
public class UpdateStatusParamDto
{
    /// <summary>ID de la factura a actualizar.</summary>
    /// <example>64f1a2b3c4d5e6f7a8b9c0d1</example>
    public string IdInvoice { get; set; } = string.Empty;
}