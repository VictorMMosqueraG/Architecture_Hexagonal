namespace Application.Features.Invoices.GetSummary.Dtos;

/// <summary>Resumen consolidado de todas las facturas del sistema.</summary>
/// <param name="Total">Número total de facturas.</param>
/// <param name="TotalAmount">Suma total de todos los montos de facturas.</param>
/// <param name="ByStatus">Desglose de facturas agrupadas por estado.</param>
/// <param name="ByClient">Desglose de facturas agrupadas por cliente.</param>
public record InvoiceSummaryDto(
    int Total,
    decimal TotalAmount,
    IEnumerable<StatusGroupDto> ByStatus,
    IEnumerable<ClientGroupDto> ByClient
);