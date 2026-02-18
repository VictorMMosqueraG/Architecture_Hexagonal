namespace Application.Features.Invoices.GetSummary.Dtos;

public record InvoiceSummaryDto(
    int Total,
    decimal TotalAmount,
    IEnumerable<StatusGroupDto> ByStatus,
    IEnumerable<ClientGroupDto> ByClient
);