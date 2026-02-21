namespace Application.Features.Invoices.UpdateStatusInvoice.Dtos;

/// <summary>Datos requeridos para actualizar el estado de una factura.</summary>
public class UpdateStatusRequestDto : UpdateStatusParamDto
{
    /// <summary>Nuevo estado a asignar a la factura.</summary>
    /// <example>pagado</example>
    public required string NewStatus { get; set; }
}