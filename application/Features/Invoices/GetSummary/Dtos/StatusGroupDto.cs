namespace Application.Features.Invoices.GetSummary.Dtos;

/// <summary>Agrupación de facturas por estado con su conteo y monto total.</summary>
/// <param name="Status">Estado de las facturas agrupadas (ej: Pending, Paid, Overdue).</param>
/// <param name="Count">Cantidad de facturas en ese estado.</param>
/// <param name="TotalAmount">Suma total de los montos de las facturas en ese estado.</param>
public record StatusGroupDto(
    string Status,
    int Count,
    decimal TotalAmount
);