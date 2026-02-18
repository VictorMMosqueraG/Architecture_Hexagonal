namespace Application.Features.Clients.GetAllClient.Dtos;

public record ClientResponseDto(
    string Id,
    string Name,
    string Email,
    string DocumentNumber,
    string? Phone,
    string Status,
    DateTime CreatedAt
);