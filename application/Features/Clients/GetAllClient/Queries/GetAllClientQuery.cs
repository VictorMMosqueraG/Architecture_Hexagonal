namespace Application.Features.Clients.GetAllClient.Queries;

using Application.Features.Clients.GetAllClient.Dtos;
using Core.Dtos.PaginationsDto;
using Core.Dtos.ResponsesDto;
using MediatR;
public class GetAllClientQuery : PaginationDto, IRequest<PaginatedResultDto<IEnumerable<ClientResponseDto>>>;