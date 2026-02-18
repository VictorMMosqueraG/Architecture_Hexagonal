namespace Application.Features.Invoices.GetSummary.Dtos;

public record ClientGroupDto(
    string ClientId,
    int InvoiceCount,
    decimal TotalAmount
);