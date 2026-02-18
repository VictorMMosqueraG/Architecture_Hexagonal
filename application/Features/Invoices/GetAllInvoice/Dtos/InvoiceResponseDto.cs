namespace Application.Features.Invoices.GetAllInvoice.Dtos;

public record InvoiceResponseDto(
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