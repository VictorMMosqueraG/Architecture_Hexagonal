namespace Application.Features.Clients.GetByIdClient.Dtos;

public record ClientByIdResponseDto(
    string Id,
    string Name,
    string Email,
    string DocumentNumber,
    string? Phone,
    string Status,
    DateTime CreatedAt
);