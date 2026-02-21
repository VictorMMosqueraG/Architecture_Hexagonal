namespace Application.Features.Invoices.GetByClientInvoice.Dtos;

/// <summary>Parámetro de identificador de cliente para filtrar facturas.</summary>
public class ParamDto
{
    /// <summary>ID del cliente cuyas facturas se desean consultar.</summary>
    /// <example>64f1a2b3c4d5e6f7a8b9c0d1</example>
    public required string ClientId { get; set; }
}