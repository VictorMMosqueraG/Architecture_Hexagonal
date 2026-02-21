namespace Application.Features.Clients.GetAllClient.Queries;

using Application.Features.Clients.GetAllClient.Dtos;
using Core.Dtos.PaginationsDto;
using Core.Dtos.ResponsesDto;
using MediatR;

/// <summary>
/// Query para obtener todos los clientes con soporte de paginación, ordenamiento y dirección.
/// Hereda los parámetros de <see cref="PaginationDto"/>.
/// </summary>
public class GetAllClientQuery : PaginationDto, IRequest<PaginatedResultDto<IEnumerable<ClientResponseDto>>>;