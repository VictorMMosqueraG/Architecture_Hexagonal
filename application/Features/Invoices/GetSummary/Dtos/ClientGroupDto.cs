namespace Application.Features.Invoices.GetSummary.Dtos;

/// <summary>Agrupación de facturas por cliente con su conteo y monto total.</summary>
/// <param name="ClientId">ID del cliente agrupado.</param>
/// <param name="InvoiceCount">Cantidad de facturas del cliente.</param>
/// <param name="TotalAmount">Suma total de los montos de las facturas del cliente.</param>
public record ClientGroupDto(
    string ClientId,
    int InvoiceCount,
    decimal TotalAmount
);