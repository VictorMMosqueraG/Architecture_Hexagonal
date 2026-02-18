namespace Application.Features.Invoices.GetByClientInvoice.Dtos;

public record InvoicByClientResponseDto(
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