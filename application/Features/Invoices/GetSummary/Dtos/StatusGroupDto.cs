namespace Application.Features.Invoices.GetSummary.Dtos;

public record StatusGroupDto(
    string Status,
    int Count,
    decimal TotalAmount
);